using DSS_Project.Data.Models;
using DSS_Project.Data.Repositories.Interfaces;
using DSS_Project.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using NuGet.Protocol.Core.Types;

namespace DSS_Project.Web.Controllers;

public class PostsController : BaseController
{
    public PostsController(IPostRepository repository)
    {
        _repository = repository;
    }

    protected IPostRepository _repository;

    // GET: PostsController
    public async Task<ActionResult> Index()
    {
        var posts = await _repository.GetListAsync(1, 10);
        return View(new PostIndexVM() { Posts = posts });
    }

    // GET: PostsController/Details/5
    public async Task<ActionResult> Details(Guid id)
    {
        var post = await _repository.GetByIdAsync(id);

        return View(new PostDetailsVM() { Title = post.Title, Body = post.Body });
    }

    // GET: PostsController/Create
    public ActionResult Create()
    {
        if (GetUserId() is null)
        {
            return RedirectToAction("Login", "Auth");
        }

        return View(new PostCreateVM() { Title = "", Body = "" });
    }

    // POST: PostsController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(IFormCollection collection)
    {
        try
        {
            var userId = GetUserId();

            if (userId is null)
            {
                return Unauthorized();
            }

            var post = new Post()
            {
                Title = collection["Title"]!,
                Body = collection["Body"]!,
                UserId = userId.Value
            };

            await _repository.CreateAsync(post);

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View(new PostCreateVM() { Title = "", Body = "" });
        }
    }

    // GET: PostsController/Edit/5
    public async Task<ActionResult> Edit(Guid id)
    {
        if (GetUserId() is null)
        {
            return RedirectToAction("Login", "Auth");
        }

        var post = await _repository.GetByIdAsync(id);

        return View("Update", new PostUpdateVM() { Title = post.Title, Body = post.Body });
    }

    // POST: PostsController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(Guid id, IFormCollection collection)
    {
        try
        {
            var userId = GetUserId();

            if (userId is null)
            {
                return Unauthorized();
            }

            var post = await _repository.GetByIdAsync(id);

            if(!UserOwnsPost(post, userId.Value))
            {
                return Unauthorized();
            }

            post!.Title = collection["Title"]!;
            post.Body = collection["Body"]!;

            await _repository.UpdateAsync(post);

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return RedirectToAction("Edit", id);
        }
    }

    // GET: PostsController/Delete/5
    public async Task<ActionResult> Delete(Guid id)
    {
        if (GetUserId() is null)
        {
            return RedirectToAction("Login", "Auth");
        }

        var post = await _repository.GetByIdAsync(id);
       
        return View(new PostDeleteVM() { Title = post.Title, Body = post.Body });
    }

    // POST: PostsController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(Guid id, IFormCollection collection)
    {
        try
        {
            var userId = GetUserId();

            if (userId is null)
            {
                return Unauthorized();
            }

            var post = await _repository.GetByIdAsync(id);

            if (!UserOwnsPost(post, userId.Value))
            {
                return Unauthorized();
            }

            await _repository.HardDeleteAsync(post!);

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return RedirectToAction("Delete", id);
        }
    }

    private static bool UserOwnsPost(Post? post, Guid userId)
    {
        if (post is null)
        {
            return false;
        }

        if (post.UserId != userId)
        {
            return false;
        }

        return true;
    }
}

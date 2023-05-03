using DSS_Project.Data.Models;
using DSS_Project.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DSS_Project.Web.Controllers;

public class AuthController : BaseController
{
    public AuthController(IUserRepository repository)
    {
        _userRepository = repository;
    }

    protected IUserRepository _userRepository;

    [HttpGet]
    public IActionResult Register()
    {
        if(GetUserId() is not null)
        {
            return RedirectToAction("Index", "Posts");
        }
        
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (GetUserId() is not null)
        {
            return RedirectToAction("Index", "Posts");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> RegisterPost(IFormCollection collection)
    {
        var existingUser = await _userRepository.GetUserByEmailAsync(collection["Email"]!);

        if (existingUser is not null)
        {
            return View();
        }

        var user = new User()
        {
            Email = collection["Email"]!,
            Password = collection["Password"]!,
            Name = collection["Name"]!,
        };

        await _userRepository.CreateAsync(user);

        HttpContext.Session.SetString("UserId", user.Id.ToString());
        return RedirectToAction("Index", "Posts");
    }

    [HttpPost]
    public async Task<IActionResult> LoginPost(IFormCollection collection)
    {
        var user =  await _userRepository.GetUserByEmailAsync(collection["Email"]!);
        var p = collection["Password"].First();
        if (user is not null && user.Password == p)
        {
            HttpContext.Session.SetString("UserId", user.Id.ToString());

            return RedirectToAction("Index", "Posts");
        }

        return View("Login");
    }

    [HttpPost]
    public void Logout()
    {
        HttpContext.Session.SetString("UserId", "");
    }
}

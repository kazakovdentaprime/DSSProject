using DSS_Project.Data.Models;
using DSS_Project.Data.Repositories.Interfaces;
using DSS_Project.Web.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace DSS_Project.Web.Controllers.Api;

[ApiController]
[Route("/Comments")]
public class CommentsController: BaseApiController
{
    public CommentsController(ICommentsRepository repository)
    {
        _repository = repository;
    }

    protected ICommentsRepository _repository;

    [HttpGet]
    [Route("/{postId}")]
    public async Task<IActionResult> GetComments([FromRoute] Guid postId)
    {
        var comments = await _repository.GetCommentsForPost(postId);

        var commentDtos = comments.Select(c => new CommentDto()
        {
            Content = c.Content,
            UserId = c.UserId
        }).ToList();

        return new JsonResult(commentDtos);
    }

    [HttpPost]
    public async Task<IActionResult> AddComments([FromBody] CommentDto commentDto)
    {
        var comment = new Comment()
        {
            Content = commentDto.Content,
            UserId = commentDto.UserId
        };

        await _repository.CreateAsync(comment);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteComments([FromBody] DeleteCommentDto commentDto)
    {
        var comment = await _repository.GetByIdAsync(commentDto.CommentId);

        var userId = GetUserId();

        if (userId is null || comment.UserId != userId)
        {
            return Unauthorized();
        }

        await _repository.HardDeleteAsync(comment);

        return Ok();
    }
}

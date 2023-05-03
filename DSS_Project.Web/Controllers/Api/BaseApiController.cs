using Microsoft.AspNetCore.Mvc;

namespace DSS_Project.Web.Controllers.Api;

public class BaseApiController: ControllerBase
{
    protected Guid? GetUserId()
    {
        var userIdString = HttpContext.Session.GetString("UserId");

        if (userIdString == null)
        {
            return null;
        }

        return new Guid(userIdString);
    }
}

using Microsoft.AspNetCore.Mvc;

namespace DSS_Project.Web.Controllers;

public class BaseController: Controller
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

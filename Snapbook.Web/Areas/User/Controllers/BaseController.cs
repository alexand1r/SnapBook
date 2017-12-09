namespace Snapbook.Web.Areas.User.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area(WebConstants.UserArea)]
    [Authorize(Roles = WebConstants.UserRole)]
    public abstract class BaseController : Controller
    {
    }
}

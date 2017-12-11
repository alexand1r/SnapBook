namespace Snapbook.Web.Areas.Advertiser.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area(WebConstants.AdvertiserArea)]
    [Authorize(Roles = WebConstants.AdvertiserRole)]
    public class BaseController : Controller
    {
    }
}

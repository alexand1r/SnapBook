namespace Snapbook.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using System.Threading.Tasks;

    public class AlbumsController : Controller
    {
        private readonly IAlbumService albums;

        public AlbumsController(IAlbumService albums)
        {
            this.albums = albums;
        }

        public async Task<IActionResult> Details(int id)
        {
            var album = await this.albums.Details(id);

            if (album == null)
            {
                return this.NotFound();
            }

            return this.View(album);
        }
    }
}

namespace Snapbook.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models.Album;
    using System.Linq;
    using System.Threading.Tasks;

    public class AlbumService : IAlbumService
    {
        private readonly SnapbookDbContext db;

        public AlbumService(SnapbookDbContext db)
        {
            this.db = db;
        }

        public async Task<AlbumDetailsServiceModel> Details(int id)
            => await this.db
                .Albums
                .Where(a => a.Id == id)
                .ProjectTo<AlbumDetailsServiceModel>()
                .FirstOrDefaultAsync();
    }
}

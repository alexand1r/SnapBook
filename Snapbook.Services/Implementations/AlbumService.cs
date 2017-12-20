namespace Snapbook.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models.Album;
    using System.Collections.Generic;
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

        public async Task<IEnumerable<AlbumListingServiceModel>> Find(string searchText)
        {
            if (searchText == null)
            {
                return new List<AlbumListingServiceModel>();
            }

            var albums = await this.db
                .Albums
                .OrderByDescending(a => a.Id)
                .Where(a => a.Description.ToLower().Contains(searchText.ToLower()) 
                    || a.Category.Name.ToLower().Contains(searchText.ToLower())
                    || a.Title.ToLower().Contains(searchText.ToLower()))
                .ProjectTo<AlbumListingServiceModel>()
                .ToListAsync();

            return albums;
        }
    }
}

namespace Snapbook.Services.Admin.Implementations
{
    using Admin.Models.Album;
    using AutoMapper.QueryableExtensions;
    using Data;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    public class AdminAlbumService : IAdminAlbumService
    {
        private readonly SnapbookDbContext db;

        public AdminAlbumService(SnapbookDbContext db)
        {
            this.db = db;
        }

        public void Delete(int id)
        {
            var album = this.db.Albums.FirstOrDefault(a => a.Id == id);

            if (album == null)
            {
                return;
            }

            this.db.Albums.Remove(album);
            this.db.SaveChanges();
        }

        public async Task<IEnumerable<AlbumListingServiceModel>> All()
            => await this.db
                .Albums
                .OrderByDescending(a => a.Id)
                .ProjectTo<AlbumListingServiceModel>()
                .ToListAsync();

        public async Task<bool> Exists(int id)
            => await this.db
                .Albums
                .AnyAsync(a => a.Id == id);

        public async Task<EditAlbumServiceModel> Find(int id)
            => await this.db
                .Albums
                .Where(a => a.Id == id)
                .ProjectTo<EditAlbumServiceModel>()
                .FirstOrDefaultAsync();

        public void Edit(string title, string description, int categoryId, int albumId)
        {
            var album = this.db.Albums.FirstOrDefault(a => a.Id == albumId);

            if (album == null)
            {
                return;
            }

            album.Title = title;
            album.Description = description;
            album.CategoryId = categoryId;

            this.db.SaveChanges();
        }
    }
}

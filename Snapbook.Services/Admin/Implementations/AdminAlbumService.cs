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

        public async Task<bool> Delete(int id)
        {
            var album =  await this.db
                .Albums
                .FirstOrDefaultAsync(a => a.Id == id);

            if (album == null)
            {
                return false;
            }

            this.db.Albums.Remove(album);
            this.db.SaveChanges();

            return true;
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

        public async Task<bool> Edit(string title, string description, int categoryId, int albumId)
        {
            var album = await this.db
                .Albums
                .FirstOrDefaultAsync(a => a.Id == albumId);

            if (album == null)
            {
                return false;
            }

            album.Title = title;
            album.Description = description;
            album.CategoryId = categoryId;

            this.db.SaveChanges();

            return true;
        }
    }
}

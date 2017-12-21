namespace Snapbook.Services.Admin.Implementations
{
    using Admin.Models.Album;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
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

            var photos = this.db.Photos.Where(p => p.AlbumId == album.Id).ToList();
            foreach (var photo in photos)
            {
                var likedPhotos = this.db.UsersLikedImages.Where(uli => uli.PhotoId == photo.Id).ToList();
                foreach (var likedPhoto in likedPhotos)
                {
                    this.db.UsersLikedImages.Remove(likedPhoto);
                }
                var savedPhotos = this.db.UsersSavedImages.Where(usi => usi.PhotoId == photo.Id).ToList();
                foreach (var savedPhoto in savedPhotos)
                {
                    this.db.UsersSavedImages.Remove(savedPhoto);
                }
                this.db.Photos.Remove(photo);
            }

            this.db.Remove(album);
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

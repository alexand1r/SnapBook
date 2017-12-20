namespace Snapbook.Services.Admin.Implementations
{
    using Admin.Models.Photo;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AdminPhotoService : IAdminPhotoService
    {
        private readonly SnapbookDbContext db;

        public AdminPhotoService(SnapbookDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> Delete(int id)
        {
            var photo = await this.db
                .Photos
                .FirstOrDefaultAsync(a => a.Id == id);

            if (photo == null)
            {
                return false;
            }

            var likers = this.db.UsersLikedImages.Where(uli => uli.PhotoId == photo.Id).ToList();
            var savers = this.db.UsersSavedImages.Where(usi => usi.PhotoId == photo.Id).ToList();

            foreach (var liker in likers)
            {
                this.db.UsersLikedImages.Remove(liker);
            }

            foreach (var saver in savers)
            {
                this.db.UsersSavedImages.Remove(saver);
            }

            this.db.Photos.Remove(photo);
            this.db.SaveChanges();

            return true;
        }

        public async Task<IEnumerable<PhotoListingServiceModel>> All()
            => await this.db
                .Photos
                .OrderByDescending(a => a.Id)
                .ProjectTo<PhotoListingServiceModel>()
                .ToListAsync();

        public async Task<bool> Exists(int id)
            => await this.db
                .Photos
                .AnyAsync(a => a.Id == id);
    }
}

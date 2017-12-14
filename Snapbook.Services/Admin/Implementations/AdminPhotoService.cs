namespace Snapbook.Services.Admin.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices.ComTypes;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Snapbook.Data;
    using Snapbook.Services.Admin.Models.Photo;

    public class AdminPhotoService : IAdminPhotoService
    {
        private readonly SnapbookDbContext db;

        public AdminPhotoService(SnapbookDbContext db)
        {
            this.db = db;
        }

        public void Delete(int id)
        {
            var photo = this.db.Photos.FirstOrDefault(a => a.Id == id);

            if (photo == null)
            {
                return;
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

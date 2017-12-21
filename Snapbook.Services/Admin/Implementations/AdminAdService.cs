namespace Snapbook.Services.Admin.Implementations
{
    using Admin.Models.Ad;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AdminAdService : IAdminAdService
    {
        private readonly SnapbookDbContext db;

        public AdminAdService(SnapbookDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> Delete(int id)
        {
            var ad = await this.db
                .Ads
                .FirstOrDefaultAsync(a => a.Id == id);

            if (ad == null)
            {
                return false;
            }

            var photos = this.db.Photos.Where(p => p.AdId == ad.Id).ToList();
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

            this.db.Ads.Remove(ad);
            this.db.SaveChanges();

            return true;
        }

        public async Task<IEnumerable<AdListingServiceModel>> All()
            => await this.db
                .Ads
                .OrderByDescending(a => a.Id)
                .ProjectTo<AdListingServiceModel>()
                .ToListAsync();

        public async Task<bool> Exists(int id)
            => await this.db
                .Ads
                .AnyAsync(a => a.Id == id);

        public async Task<bool> Edit(
            string name,
            string description,
            string imageUrl,
            string website,
            int id)
        {
            var ad = await this.db
                .Ads
                .FirstOrDefaultAsync(a => a.Id == id);

            if (ad == null)
            {
                return false;
            }

            ad.Name = name;
            ad.Description = description;
            ad.AdProfilePicUrl = imageUrl;
            ad.Website = website;

            this.db.SaveChanges();

            return true;
        }

        public async Task<AdEditServiceModel> FindForEdit(int id)
            => await this.db
                .Ads
                .Where(a => a.Id == id)
                .ProjectTo<AdEditServiceModel>()
                .FirstOrDefaultAsync();
    }
}

namespace Snapbook.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Comment;
    using Models.Photo;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PhotoService : IPhotoService
    {
        private readonly SnapbookDbContext db;

        public PhotoService(SnapbookDbContext db)
        {
            this.db = db;
        }

        public async Task<EditPhotoServiceModel> FindForEdit(int id)
            => await this.db
                .Photos
                .Where(p => p.Id == id)
                .ProjectTo<EditPhotoServiceModel>()
                .FirstOrDefaultAsync();

        public void Edit(
            int id,
            string description,
            string location,
            string latitude,
            string longitude,
            string tags)
        {
            var photo = this.db
                .Photos
                .FirstOrDefault(p => p.Id == id);

            if (photo == null)
            {
                return;
            }

            photo.Description = description;
            photo.Location = location;
            photo.Latitude = latitude;
            photo.Longitude = longitude;

            var tagsToDelete = this.db.Tags.Where(t => t.PhotoId == id).ToList();

            foreach (var tag in tagsToDelete)
            {
                this.db.Tags.Remove(tag);
            }

            this.db.SaveChanges();

            var tagsList = tags.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var t in tagsList)
            {
                var tag = new Tag
                {
                    Content = t,
                    PhotoId = photo.Id
                };
                this.db.Add(tag);
            }

            this.db.SaveChanges();
        }

        public async Task<IEnumerable<PhotoHomeServiceModel>> All()
            => await this.db
                .Photos
                //.Where(p => p.Album != null)
                .OrderByDescending(p => p.Id)
                .ProjectTo<PhotoHomeServiceModel>()
                .ToListAsync();

        public async Task<PhotoDetailsServiceModel> Details(int id)
            => await this.db
                .Photos
                .Where(p => p.Id == id)
                .ProjectTo<PhotoDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<PhotoListingServiceModel>> Find(string searchText)
        {
            if (searchText == null)
            {
                return new List<PhotoListingServiceModel>();
            }

            var photos = await this.db
                .Photos
                .OrderByDescending(p => p.Id)
                .Where(p => p.Description.ToLower().Contains(searchText.ToLower())
                            || p.Tags.Any(t => t.Content.ToLower().Contains(searchText.ToLower()))
                            || p.Location.ToLower().Contains(searchText.ToLower()))
                .ProjectTo<PhotoListingServiceModel>()
                .ToListAsync();

            return photos;
        }

        public async Task<IEnumerable<CommentServiceModel>> Comment(int photoId, string content, string authorId)
        {
            var comment = new Comment
            {
                Content = content,
                PublishDate = DateTime.UtcNow,
                PhotoId = photoId,
                AuthorId = authorId
            };

            this.db.Add(comment);
            await this.db.SaveChangesAsync();

            return await this.db
                .Comments
                .Where(c => c.PhotoId == photoId)
                .ProjectTo<CommentServiceModel>()
                .ToListAsync();
        }

        public async Task<bool> CanLike(string userId, int photoId)
            => await this.db
                .UsersLikedImages
                .AnyAsync(uli => uli.UserId == userId 
                    && uli.PhotoId == photoId);

        public async Task<int> Like(string userId, int photoId)
        {
            var photoInfo = await this.db
                .Photos
                .Where(p => p.Id == photoId)
                .Select(p => new
                {
                    UserIdHasLiked = p.Likers.Any(u => u.UserId == userId)
                })
                .FirstOrDefaultAsync();

            if (photoInfo == null || photoInfo.UserIdHasLiked)
            {
                return this.db.UsersLikedImages.Count();
            }

            var userLike = new UsersLikedImages
            {
                UserId = userId,
                PhotoId = photoId
            };

            this.db.Add(userLike);
            await this.db.SaveChangesAsync();

            return this.db.UsersLikedImages.Count(uli => uli.PhotoId == photoId);
        }

        public async Task<int> Unlike(string userId, int photoId)
        {
            var photoInfo = await this.db
                .Photos
                .Where(p => p.Id == photoId)
                .Select(p => new
                {
                    UserIdHasLiked = p.Likers.Any(u => u.UserId == userId)
                })
                .FirstOrDefaultAsync();

            if (photoInfo == null || !photoInfo.UserIdHasLiked)
            {
                return this.db.UsersLikedImages.Count();
            }

            var userUnlike = await this.db
                .UsersLikedImages
                .Where(uli => uli.UserId == userId && uli.PhotoId == photoId)
                .FirstOrDefaultAsync();

            this.db.UsersLikedImages.Remove(userUnlike);
            await this.db.SaveChangesAsync();

            return this.db.UsersLikedImages.Count(uli => uli.PhotoId == photoId);
        }

        public async Task<bool> CanSave(string userId, int photoId)
            => await this.db
                .UsersSavedImages
                .AnyAsync(usi => usi.UserId == userId
                                 && usi.PhotoId == photoId);

        public async Task<bool> Save(string userId, int photoId)
        {
            var photoInfo = await this.db
                .Photos
                .Where(p => p.Id == photoId)
                .Select(p => new
                {
                    UserIdHasSaved = p.Savers.Any(u => u.UserId == userId)
                })
                .FirstOrDefaultAsync();

            if (photoInfo == null || photoInfo.UserIdHasSaved)
            {
                return false;
            }

            var userSave = new UsersSavedImages
            {
                UserId = userId,
                PhotoId = photoId
            };

            this.db.Add(userSave);
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Unsave(string userId, int photoId)
        {
            var photoInfo = await this.db
                .Photos
                .Where(p => p.Id == photoId)
                .Select(p => new
                {
                    UserIdHasSaved = p.Savers.Any(u => u.UserId == userId)
                })
                .FirstOrDefaultAsync();

            if (photoInfo == null || !photoInfo.UserIdHasSaved)
            {
                return false;
            }

            var userUnsave = await this.db
                .UsersSavedImages
                .Where(uli => uli.UserId == userId && uli.PhotoId == photoId)
                .FirstOrDefaultAsync();

            this.db.UsersSavedImages.Remove(userUnsave);
            await this.db.SaveChangesAsync();

            return true;
        }
    }
}

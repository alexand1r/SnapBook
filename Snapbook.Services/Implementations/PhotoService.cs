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

        public async Task<IEnumerable<PhotoHomeServiceModel>> All()
            => await this.db
                .Photos
                .Where(p => p.Album != null)
                .OrderByDescending(p => p.Id)
                .ProjectTo<PhotoHomeServiceModel>()
                .ToListAsync();

        public async Task<PhotoDetailsServiceModel> Details(int id)
            => await this.db
                .Photos
                .Where(p => p.Id == id)
                .ProjectTo<PhotoDetailsServiceModel>()
                .FirstOrDefaultAsync();

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
                .OrderByDescending(c => c.Id)
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

            return this.db.UsersLikedImages.Count();
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

            return this.db.UsersLikedImages.Count();
        }
    }
}

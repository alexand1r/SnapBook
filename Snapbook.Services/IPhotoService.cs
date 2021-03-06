﻿namespace Snapbook.Services
{
    using Models.Comment;
    using Models.Photo;
    using Models.User;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPhotoService
    {
        Task<EditPhotoServiceModel> FindForEdit(int id);

        Task<bool> Edit(
            int id,
            string description,
            string location,
            string latitude,
            string longitude,
            string tags);

        Task<IEnumerable<PhotoHomeServiceModel>> All();

        Task<PhotoDetailsServiceModel> Details(int id);

        Task<IEnumerable<PhotoRelatedServiceModel>> RelatedPhotos(int id);

        Task<IEnumerable<PhotoListingServiceModel>> Find(string searchText);

        Task<IEnumerable<CommentServiceModel>> Comment(
            int photoId,
            string content,
            string authorId);

        Task<bool> CanLike(string userId, int photoId);

        Task<IEnumerable<PhotoLikerServiceModel>> Like(string userId, int photoId);

        Task<IEnumerable<PhotoLikerServiceModel>> Unlike(string userId, int photoId);

        Task<bool> CanSave(string userId, int photoId);

        Task<bool> Save(string userId, int photoId);

        Task<bool> Unsave(string userId, int photoId);

    }
}

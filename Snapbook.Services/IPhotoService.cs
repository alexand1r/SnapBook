namespace Snapbook.Services
{
    using Models.Comment;
    using Models.Photo;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPhotoService
    {
        Task<IEnumerable<PhotoHomeServiceModel>> All();

        Task<PhotoDetailsServiceModel> Details(int id);

        Task<IEnumerable<CommentServiceModel>> Comment(
            int photoId,
            string content,
            string authorId);

        Task<bool> CanLike(string userId, int photoId);

        Task<int> Like(string userId, int photoId);

        Task<int> Unlike(string userId, int photoId);
    }
}

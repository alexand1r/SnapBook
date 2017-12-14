namespace Snapbook.Services
{
    using Models.User;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<UserProfileServiceModel> ProfileAsync(string id);

        void EditProfilePic(string username, string imageUrl);

        Task<IEnumerable<UserListingServiceModel>> Find(string searchText);
    }
}

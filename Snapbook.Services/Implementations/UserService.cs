namespace Snapbook.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models.User;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly SnapbookDbContext db;

        public UserService(SnapbookDbContext db)
        {
            this.db = db;
        }

        public async Task<UserProfileServiceModel> ProfileAsync(string id)
        {
            var userProfile = await this.db
                .Users
                .Where(u => u.Id == id)
                .ProjectTo<UserProfileServiceModel>()
                .FirstOrDefaultAsync();

            return userProfile;
        }

        public void EditProfilePic(string username, string imageUrl)
        {
            var user = this.db
                .Users
                .FirstOrDefault(u => u.UserName == username);

            if (user == null)
            {
                return;
            }

            user.ProfilePicUrl = imageUrl;

            this.db.SaveChanges();
        }

        public async Task<IEnumerable<UserListingServiceModel>> Find(string searchText)
        {
            if (searchText == null)
            {
                return new List<UserListingServiceModel>();
            }

            var users = await this.db
                .Users
                .OrderByDescending(u => u.UserName)
                .Where(u => u.UserName.ToLower().Contains(searchText.ToLower())
                    || u.Name.ToLower().Contains(searchText.ToLower()))
                .ProjectTo<UserListingServiceModel>()
                .ToListAsync();

            return users;
        }
    }
}

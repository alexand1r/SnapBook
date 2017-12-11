namespace Snapbook.Services.User.Implementations
{
    using Data;
    using System.Linq;

    public class UserUserService : IUserUserService
    {
        private readonly SnapbookDbContext db;

        public UserUserService(SnapbookDbContext db)
        {
            this.db = db;
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
    }
}

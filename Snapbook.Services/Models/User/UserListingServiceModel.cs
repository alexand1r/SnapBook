namespace Snapbook.Services.Models.User
{
    using Common.Mapping;
    using Data.Models;

    public class UserListingServiceModel : IMapFrom<User>
    {
        public string UserName { get; set; }

        public string ProfilePicUrl { get; set; }
    }
}

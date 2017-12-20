namespace Snapbook.Services.Models.User
{
    using Common.Mapping;
    using Data.Models;

    public class PhotoLikerServiceModel : IMapFrom<User>
    {
        public string Username { get; set; }
    }
}

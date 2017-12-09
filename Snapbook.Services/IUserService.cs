namespace Snapbook.Services
{
    using Models.User;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<UserProfileServiceModel> ProfileAsync(string id);
    }
}

using ChocolateFactoryApi.Models;

namespace ChocolateFactoryApi.Services
{
    public interface IUserService
    {
        bool RegisterUser(User user);
    }
}

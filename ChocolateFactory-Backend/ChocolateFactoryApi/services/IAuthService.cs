using ChocolateFactoryApi.Models;

namespace ChocolateFactoryApi.services
{
    public interface IAuthService
    {
       public  Task<User> RegisterAsync( string Name,string email, string username, string password, string role);
        public Task<User> LoginAsync(string username, string password, string role);
        public Task<string> GenerateJwtToken(User user);
    }
}

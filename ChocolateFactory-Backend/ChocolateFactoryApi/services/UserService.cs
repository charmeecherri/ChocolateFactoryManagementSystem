
using ChocolateFactoryApi.Models;
using ChocolateFactoryApi.Data;

namespace ChocolateFactoryApi.Services
{
    public class UserService:IUserService
    {
        //private readonly IUserRepository _userRepository;
        private readonly AppDbContext context;

        public UserService(AppDbContext context)
        {
           this. context = context;
        }

        public bool RegisterUser(User user)
        {
            // Check if email already exists
            if (context.Users.Any(u=>u.Email == user.Email))
                return false;
            user.Role = string.IsNullOrEmpty(user.Role) ? "User" : user.Role;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

            // Save the user
            context.Users.Add(user);
            context.SaveChanges();
            return true;
        }

       

    }
}

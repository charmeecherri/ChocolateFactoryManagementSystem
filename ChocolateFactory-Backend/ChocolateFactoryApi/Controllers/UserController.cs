using ChocolateFactoryApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using ChocolateFactoryApi.Models;
using ChocolateFactoryApi.Services;
using ChocolateFactoryApi.repositories.interfaces;

namespace ChocolateFactoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IUserService _userService;
        private readonly INotificationRepository _notificaionRepository;

        public UserController(AppDbContext context,IUserService userService,INotificationRepository notificationRepository)
        {
            this.context = context;
            _userService = userService;
            _notificaionRepository = notificationRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await context.Users.ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult<User>> CreatedUser(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsers),new {id= user.UserId},user);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.UserId) return BadRequest();
            context.Entry(user).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet]
        [Route("notification")]
        public async Task<IActionResult> getNotification(int userId)
        {
            return Ok(await _notificaionRepository.getUserNotifications(userId));
        }

    }
}

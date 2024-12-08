using ChocolateFactoryApi.DTO.request;
using ChocolateFactoryApi.Models;
using ChocolateFactoryApi.repositories.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChocolateFactoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationController(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        [HttpPost]
        public async Task<IActionResult> sendNotification(NotificationRequestDto notificationRequestDto)
        {
            Notification notificaiton = new Notification()
            {
                Type = notificationRequestDto.Type,
                Message = notificationRequestDto.Message,
                TimeStamp = DateTime.Now,
                UserId = notificationRequestDto.UserId,
            };

            await _notificationRepository.sendNotification(notificaiton);
            return Ok("Notification sent successfully");

            
        }
    }
}

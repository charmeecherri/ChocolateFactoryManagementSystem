using ChocolateFactoryApi.Data;
using ChocolateFactoryApi.Models;
using ChocolateFactoryApi.repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChocolateFactoryApi.repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext context;

        public NotificationRepository(AppDbContext appDbContext)
        {
            context = appDbContext;
        }

        public async Task<List<Notification>> getUserNotifications(int userId)
        {
            return await context.Notification.Where(n => n.UserId == userId).ToListAsync();
        }

        public async Task sendNotification(Notification notification)
        {
            await context.Notification.AddAsync(notification);
            await context.SaveChangesAsync();
        }
    }
}

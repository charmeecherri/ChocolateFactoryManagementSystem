using ChocolateFactoryApi.Models;

namespace ChocolateFactoryApi.repositories.interfaces
{
    public interface INotificationRepository
    {
        Task sendNotification(Notification notification);

        Task<List<Notification>> getUserNotifications(int userId);
    }
}

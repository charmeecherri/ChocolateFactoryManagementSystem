using System.ComponentModel.DataAnnotations;

namespace ChocolateFactoryApi.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

        public string Type { get; set; }

        public string Message { get; set; }

        public DateTime TimeStamp { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

    }
}

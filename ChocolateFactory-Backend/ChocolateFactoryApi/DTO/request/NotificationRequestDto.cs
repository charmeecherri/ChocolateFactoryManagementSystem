using System.ComponentModel.DataAnnotations;

namespace ChocolateFactoryApi.DTO.request
{
    public class NotificationRequestDto
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public string Message { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "User Id can't be null or zero")]
        public int UserId { get; set; }

    }
}

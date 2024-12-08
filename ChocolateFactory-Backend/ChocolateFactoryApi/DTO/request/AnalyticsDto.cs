using System.ComponentModel.DataAnnotations;

namespace ChocolateFactoryApi.DTO.request
{
    public class AnalyticsDto
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public string Data { get; set; }

        [Required]
        public string CreatedBy { get; set; }
    }
}

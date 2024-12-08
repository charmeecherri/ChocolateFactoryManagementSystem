using ChocolateFactoryApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ChocolateFactoryApi.DTO.request
{
    public class ProductionScheduleRequestDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Production Id can't be null or zero")]
        public int ProductId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Shift { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "SuperVisor Id can't be null or zero")]
        public int SupervisorId { get; set; }

        [Required]
        public string Status { get; set; }
    }
}

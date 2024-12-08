using System.ComponentModel.DataAnnotations;

namespace ChocolateFactoryApi.DTO.request
{
    public class MaintanenceDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Equipment Id can't be null or zero")]
        public int EquipmentId { get; set; }

        [Required]
        public DateTime MaintanenceDate { get; set; }

        [Required]
        public string Technician { get; set; }

        [Required]
        public string Details { get; set; }

        [Required]
        public DateTime NextSchedulingDate { get; set; }
    }
}

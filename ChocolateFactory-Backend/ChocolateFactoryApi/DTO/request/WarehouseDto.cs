using ChocolateFactoryApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ChocolateFactoryApi.DTO.request
{
    public class WarehouseDto
    {
        [Required]
        public string Location { get; set; }

        [Required]
        public string Capacity { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Warehouse Id can't be null or zero")]
        public int ManagerId { get; set; }

    }
}

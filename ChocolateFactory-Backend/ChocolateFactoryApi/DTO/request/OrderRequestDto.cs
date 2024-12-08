using ChocolateFactoryApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ChocolateFactoryApi.DTO.request
{
    public class OrderRequestDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Customer Id can't be null or zero")]
        public int CustomerId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Product Id can't be null or zero")]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity can't be null or zero")]
        public int Quantity { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }

        [Required]  
        public string status { get; set; }
    }
}

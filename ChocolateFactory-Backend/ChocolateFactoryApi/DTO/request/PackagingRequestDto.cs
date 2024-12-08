using ChocolateFactoryApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ChocolateFactoryApi.DTO.request
{
    public class PackagingRequestDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Product Id can't be null or zero")]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Batch Id can't be null or zero")]
        public int BatchId { get; set; }

        public int Quantity { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public DateTime PackagingDate { get; set; }

        [Required]
        public string WarehouseLocation { get; set; }
    }
}

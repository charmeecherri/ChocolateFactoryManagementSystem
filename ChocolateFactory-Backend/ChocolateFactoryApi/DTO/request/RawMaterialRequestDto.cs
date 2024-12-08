using System.ComponentModel.DataAnnotations;

namespace ChocolateFactoryApi.DTO.request
{
    public class RawMaterialRequestDto
    {
        [Required]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Stock Quantity can't be null or zero")]
        public int StockQuantity { get; set; }

        [Required]
        public string Unit { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Supplier Id can't be null or zero")]
        public int SupplierId { get; set; }

        [Required]
        public decimal CostPerUnit { get; set; }
    }
}

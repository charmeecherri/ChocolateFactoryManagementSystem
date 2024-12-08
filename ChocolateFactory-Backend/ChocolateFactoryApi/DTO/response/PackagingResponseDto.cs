using ChocolateFactoryApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ChocolateFactoryApi.DTO.response
{
    public class PackagingResponseDto
    {
        public int PackagingId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int BatchId { get; set; }

        public int Quantity { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime PackagingDate { get; set; }

        public string WarehouseLocation { get; set; }
    }
}

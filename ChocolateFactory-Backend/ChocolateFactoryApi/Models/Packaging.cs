using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ChocolateFactoryApi.Models
{
    public class Packaging
    {
        [Key]
        public int PackagingId { get; set; }
        
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int BatchId { get; set; }

        public int Quantity {  get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime PackagingDate { get; set; }

        public string WarehouseLocation { get; set; }


    }
}

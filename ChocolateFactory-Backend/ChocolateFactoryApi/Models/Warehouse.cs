using System.ComponentModel.DataAnnotations;

namespace ChocolateFactoryApi.Models
{
    public class Warehouse
    {
        [Key]
        public int WarehouseId { get; set; }

        public string Location { get; set; }

        public string Capacity { get; set; }

        public int ManagerId { get; set; }

        public User user { get; set; }

        public string CurrentStockCapacity { get; set; }

    }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ChocolateFactoryApi.Models
{
    [Index(nameof(ProductName), IsUnique = true)]
    public class Product
    {
        [Key]
        public int ProductId {  get; set; }

        public string ProductName { get; set; }

        [JsonIgnore]
        public ICollection<ProductionSchedule> ProductionSchedules { get; set; }

        [JsonIgnore]
        public List<Recipe> recipes { get; set; }

        [JsonIgnore]
        public List<Packaging> packagings { get; set; }

        [JsonIgnore]
        public List<Order> orderings { get; set; }

    }
}

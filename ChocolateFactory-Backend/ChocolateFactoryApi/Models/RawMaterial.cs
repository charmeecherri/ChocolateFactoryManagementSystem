using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ChocolateFactoryApi.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class RawMaterial
    {
        [Key]
        public int MaterialId {  get; set; }

        public string Name {  get; set; }

        public int StockQuantity {  get; set; }

        public string Unit {  get; set; }

        public DateTime ExpiryDate { get; set; }

        public int SupplierId {  get; set; }

        public decimal CostPerUnit {  get; set; }

        [JsonIgnore]
        public List<Ingredients> Ingredients { get; set; }

    }
}

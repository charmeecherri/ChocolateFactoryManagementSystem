using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ChocolateFactoryApi.Models
{
    [Index(nameof(ProductId), IsUnique = true)]
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }

        public int ProductId { get; set; }

        public Product product { get; set; }

        public List<Ingredients> Ingredients { get; set; } 
        public string QuantityPerBatch { get; set; }    

        public string Instructions { get; set; }
    }
}

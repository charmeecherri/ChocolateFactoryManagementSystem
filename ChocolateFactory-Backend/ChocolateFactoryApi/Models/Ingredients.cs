using System.ComponentModel.DataAnnotations;

namespace ChocolateFactoryApi.Models
{
    public class Ingredients
    {
        [Key]
        public int Id { get; set; }

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        public int MaterialId { get; set; }

        public RawMaterial RawMaterial { get; set; }

        public int Quantity { get; set;  }
    }
}

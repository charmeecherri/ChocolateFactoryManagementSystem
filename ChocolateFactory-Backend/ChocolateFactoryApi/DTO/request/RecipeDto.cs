using ChocolateFactoryApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ChocolateFactoryApi.DTO.request
{
    public class RecipeDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Product Id can't be null or zero")]
        public int ProductId { get; set; }

        public List<IngredientsDto> Ingredients { get; set; }

        [Required]
        public string QuantityPerBatch { get; set; }

        [Required]
        public string Instructions { get; set; }
    }
}

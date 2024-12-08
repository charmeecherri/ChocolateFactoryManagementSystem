using ChocolateFactoryApi.Models;

namespace ChocolateFactoryApi.DTO.response
{
    public class RecipeResponseDto
    {
        public int RecipeId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public List<RawMaterial> Ingredients { get; set; }
        public string QuantityPerBatch { get; set; }

        public string Instructions { get; set; }
    }
}

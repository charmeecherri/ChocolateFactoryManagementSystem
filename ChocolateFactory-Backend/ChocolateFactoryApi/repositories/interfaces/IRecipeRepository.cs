using ChocolateFactoryApi.Data;
using ChocolateFactoryApi.DTO.response;
using ChocolateFactoryApi.Models;

namespace ChocolateFactoryApi.repositories.interfaces
{
    public interface IRecipeRepository
    {
        Task<List<RecipeResponseDto>> getRecipesAsync();
        Task<Recipe> getRecipeById(int id);

        Task createRecipeAsync(Recipe recipe);

        Task createIngredientsAsync(List<Ingredients> ingredients);

        Task updateRecipeAsync(Recipe recipe);

        Task deleteRecipe(int id);

        AppDbContext getContext();

        Task<RecipeResponseDto> getRecipeByProjectIdAsycn(int projectId);
    }
}

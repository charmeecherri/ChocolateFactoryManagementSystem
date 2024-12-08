using ChocolateFactoryApi.Data;
using ChocolateFactoryApi.DTO.request;
using ChocolateFactoryApi.Models;
using ChocolateFactoryApi.repositories.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ChocolateFactoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IRawMaterialRepository _rawMaterialRepository;

        public RecipeController(IRecipeRepository recipeRepository,IRawMaterialRepository rawMaterialRepository)
        {
            _recipeRepository = recipeRepository;
            _rawMaterialRepository = rawMaterialRepository;
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<IActionResult> GetRecipes()
        {
            return Ok(await _recipeRepository.getRecipesAsync());
        }

        // POST: api/Recipes
        [HttpPost]
        public async Task<IActionResult> AddRecipe(RecipeDto recipeDto)
        {
            using(var transaction = await _recipeRepository.getContext().Database.BeginTransactionAsync())
            {
                try
                {
                    Recipe recipe = new Recipe()
                    {
                        ProductId = recipeDto.ProductId,
                        QuantityPerBatch = recipeDto.QuantityPerBatch,
                        Instructions = recipeDto.Instructions,
                    };
                    await _recipeRepository.createRecipeAsync(recipe);

                    List<Ingredients> ingredients = new List<Ingredients>();
                    for (int i = 0; i < recipeDto.Ingredients.Count; i++)
                    {
                        IngredientsDto ingredientsDto = recipeDto.Ingredients[i];
                        RawMaterial material = await _rawMaterialRepository.getRawMaterialByNameAsync(ingredientsDto.name);
                        Ingredients ingredientsObj = new Ingredients()
                        {
                            RecipeId = recipe.RecipeId,
                            MaterialId = material.MaterialId,
                            Quantity = ingredientsDto.quantity
                        };
                        ingredients.Add(ingredientsObj);
                    }

                    await _recipeRepository.createIngredientsAsync(ingredients);
                    await transaction.CommitAsync();
                }
                catch(DbUpdateException e)
                {
                    await transaction.RollbackAsync();
                    if (e.InnerException is not null)
                    {
                        return BadRequest(e.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return BadRequest(ex.Message);
                }
                return StatusCode(StatusCodes.Status201Created, "Recipe created successfully");

            }

        }

        [HttpDelete]
        public async Task<IActionResult> deleteRecipe(int id)
        {
            await _recipeRepository.deleteRecipe(id);
            return Ok("recipe is deleted");
        }
    }
}


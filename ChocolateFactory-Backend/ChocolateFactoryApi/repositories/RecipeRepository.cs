using ChocolateFactoryApi.Data;
using ChocolateFactoryApi.DTO.response;
using ChocolateFactoryApi.Models;
using ChocolateFactoryApi.repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChocolateFactoryApi.repositories
{
    public class RecipeRepository : IRecipeRepository
    {

        private readonly AppDbContext context;

        public RecipeRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task createIngredientsAsync(List<Ingredients> ingredients)
        {
            await context.Ingredients.AddRangeAsync(ingredients);
            await context.SaveChangesAsync();
        }

        public async Task createRecipeAsync(Recipe recipe)
        {
            await context.Recipes.AddAsync(recipe);
            await context.SaveChangesAsync();
        }


        public async Task<Recipe> getRecipeById(int id)
        {
            return await context.Recipes.Where( r => r.RecipeId == id).FirstAsync();
        }

        public async Task<List<RecipeResponseDto>> getRecipesAsync()
        {
            return await context.Recipes.Select(r => new RecipeResponseDto()
            {
                RecipeId = r.RecipeId,
                ProductId = r.ProductId,
                ProductName = r.product.ProductName,
                Ingredients = r.Ingredients.Select(i => new RawMaterial()
                {
                    MaterialId = i.RawMaterial.MaterialId,
                    Name = i.RawMaterial.Name,
                    Unit = i.RawMaterial.Unit,
                    StockQuantity = i.Quantity,
                    ExpiryDate = i.RawMaterial.ExpiryDate,
                    SupplierId = i.RawMaterial.SupplierId,
                    CostPerUnit = i.RawMaterial.CostPerUnit

                }).ToList(),
                QuantityPerBatch = r.QuantityPerBatch,
                Instructions = r.Instructions,
            }
           ).ToListAsync();
        }

        public AppDbContext getContext()
        {
            return context;
        }

        public async Task deleteRecipe(int id)
        {
            context.Recipes.Remove(await getRecipeById(id));
            context.SaveChanges();
        }

        public async Task<RecipeResponseDto> getRecipeByProjectIdAsycn(int projectId)
        {
            return await context.Recipes.Where(r => projectId == r.ProductId).Select(r => new RecipeResponseDto()
            {
                RecipeId = r.RecipeId,
                ProductId = r.ProductId,
                ProductName = r.product.ProductName,
                Ingredients = r.Ingredients.Select(i => new RawMaterial()
                {
                    MaterialId = i.RawMaterial.MaterialId,
                    Name = i.RawMaterial.Name,
                    Unit = i.RawMaterial.Unit,
                    StockQuantity = i.Quantity,
                    ExpiryDate = i.RawMaterial.ExpiryDate,
                    SupplierId = i.RawMaterial.SupplierId,
                    CostPerUnit = i.RawMaterial.CostPerUnit

                }).ToList(),
                QuantityPerBatch = r.QuantityPerBatch,
                Instructions = r.Instructions,
            }).FirstAsync();
        }

        public async Task updateRecipeAsync(Recipe recipe)
        {
            context.Recipes.Update(recipe);
            await context.SaveChangesAsync();
        }
    }
}

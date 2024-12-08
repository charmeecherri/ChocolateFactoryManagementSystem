using ChocolateFactoryApi.Data;
using ChocolateFactoryApi.DTO.request;
using ChocolateFactoryApi.DTO.response;
using ChocolateFactoryApi.Models;
using ChocolateFactoryApi.repositories;
using ChocolateFactoryApi.repositories.interfaces;
using ChocolateFactoryApi.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;

namespace ChocolateFactoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionScheduleController : ControllerBase
    {
        private readonly IProductionScheduleRepository _productionScheduleRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly CommonService _commonService;
        private readonly IRawMaterialRepository _rawMaterialRepository;

        public ProductionScheduleController(IProductionScheduleRepository productionScheduleRepository,IRecipeRepository recipeRepository,
            CommonService commonService,IRawMaterialRepository rawMaterialRepository)
        {
            _productionScheduleRepository = productionScheduleRepository;
            _recipeRepository = recipeRepository;
            _commonService = commonService;
            _rawMaterialRepository = rawMaterialRepository;
        }

        [HttpGet]
        public async Task<IActionResult> getProductionSchedules()
        {
            return Ok(await _productionScheduleRepository.getProductSchedulesAsync());

        }

        [HttpGet]
        [Route("completed")]
        public async Task<IActionResult> getCompletedProducts()
        {
            return Ok(await _productionScheduleRepository.getCompletedProductionSchedulesAsync());
        }


        [HttpPost]
        public async Task<IActionResult> createProductionSchedule(ProductionScheduleRequestDto productionScheduleRequestDto)
        {
            using (var transaction = await _productionScheduleRepository.getAppDbContext().Database.BeginTransactionAsync())
            {

                try
                {
                    if(productionScheduleRequestDto.StartDate <= DateTime.Now.ToUniversalTime())
                    {
                        return BadRequest("StartDate cannot be in the past");
                    }
                    if (productionScheduleRequestDto.EndDate <= productionScheduleRequestDto.StartDate)
                    {
                        return BadRequest("Start Date cannot be greater than the End date");
                    }

                    ProductionSchedule productionSchedule = new ProductionSchedule()
                    {
                        ProductId = productionScheduleRequestDto.ProductId,
                        StartDate = productionScheduleRequestDto.StartDate,
                        EndDate = productionScheduleRequestDto.EndDate,
                        Shift = productionScheduleRequestDto.Shift,
                        SupervisorId = productionScheduleRequestDto.SupervisorId,
                        Status = productionScheduleRequestDto.Status,


                    };

                    RecipeResponseDto recipe = await _recipeRepository.getRecipeByProjectIdAsycn(productionScheduleRequestDto.ProductId);
                    if (await _commonService.checkRawMaterialExists(recipe.Ingredients))
                    {
                        await _productionScheduleRepository.createProductionScheduleAsync(productionSchedule);
                        for (int i = 0; i < recipe.Ingredients.Count; i++)
                        {
                            //Updation of raw material stock
                            RawMaterial rawMaterial = await _rawMaterialRepository.getRawMaterialByNameAsync(recipe.Ingredients[i].Name);
                            rawMaterial.StockQuantity = rawMaterial.StockQuantity - recipe.Ingredients[i].StockQuantity;
                            await _rawMaterialRepository.updateRawMaterialAsync(rawMaterial);                          
                        }
                        await transaction.CommitAsync();

                        return StatusCode(StatusCodes.Status201Created, "Production is created");
                    }
                    else
                    {
                        return BadRequest("Raw materials required to make the product are out of stock");
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return BadRequest(ex.Message);

                }
                
            }
            
        }


        [HttpPut]
        public async Task<IActionResult> updateProductionStatus(int id,string status)
        {
            ProductionSchedule productionSchedule = await _productionScheduleRepository.getProductScheduleByIdAsync(id);
            productionSchedule.Status = status;
            await _productionScheduleRepository.updateProductionScheduleAsync(productionSchedule);
            return Ok("updated the production schedule status");
        }

        [HttpDelete]
        public async Task<IActionResult> deleteProductionStatus(int id)
        {

            using (var transaction = await _productionScheduleRepository.getAppDbContext().Database.BeginTransactionAsync())
            {

                try
                {
                    ProductionSchedule productionSchedule = await _productionScheduleRepository.getProductScheduleByIdAsync(id);
                    if (productionSchedule.Status != "completed")
                    {
                        RecipeResponseDto recipe = await _recipeRepository.getRecipeByProjectIdAsycn(productionSchedule.ProductId);
                        for (int i = 0; i < recipe.Ingredients.Count; i++)
                        {
                            //Updation of raw material stock
                            RawMaterial rawMaterial = await _rawMaterialRepository.getRawMaterialByNameAsync(recipe.Ingredients[i].Name);
                            rawMaterial.StockQuantity = rawMaterial.StockQuantity + recipe.Ingredients[i].StockQuantity;
                            await _rawMaterialRepository.updateRawMaterialAsync(rawMaterial);
                        }

                    }
                    await _productionScheduleRepository.deleteProductionScheduleAsync(productionSchedule);
                    await transaction.CommitAsync();
                    return Ok("deleted the production schedule");
                }
                catch(Exception e)
                {
                    await transaction.RollbackAsync();
                    if (e.InnerException != null)
                        return BadRequest(e.InnerException.Message);
                    else
                        return BadRequest(e.Message);
                }

            }
        }


        
    }
}

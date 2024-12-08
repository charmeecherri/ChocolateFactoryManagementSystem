using ChocolateFactoryApi.Models;
using ChocolateFactoryApi.repositories.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChocolateFactoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> getProducts()
        {
            return Ok(await _productRepository.getProductsAsync());

        }


        [HttpPost]
        public async Task<IActionResult> createProduct(string name)
        {
            Product product = new Product()
            {
                ProductName = name
            };
            await _productRepository.createProductAsync(product);
            return StatusCode(StatusCodes.Status201Created,"Product is created");
        }

        [HttpPut]
        public async Task<IActionResult> updatePrduct(int id,string name)
        {
            Product product = await _productRepository.getProductByIdAsync(id);
            product.ProductName = name;
            await _productRepository.updateProductAsync(product);
            return Ok("product is updated");
        }

        [HttpDelete]
        public async Task<IActionResult> deleteProduct(int id)
        {
            Product product = await _productRepository.getProductByIdAsync(id);
            await _productRepository.deleteProductAsync(product);
            return Ok("Product is deleted");
        }
    }
}

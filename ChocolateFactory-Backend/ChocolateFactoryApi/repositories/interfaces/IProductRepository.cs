using ChocolateFactoryApi.Models;

namespace ChocolateFactoryApi.repositories.interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> getProductsAsync();
        Task<Product> getProductByIdAsync(int id);

        Task createProductAsync(Product product);
        Task updateProductAsync(Product product);
        Task deleteProductAsync(Product product);

    }
}

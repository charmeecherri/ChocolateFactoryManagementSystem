using ChocolateFactoryApi.Models;

namespace ChocolateFactoryApi.repositories.interfaces
{
    public interface IWarehouseRepository
    {
        Task<List<Warehouse>> getAllWarehouses();

        Task<Warehouse> getWarehouseByIdAsync(int id);

        Task createWarehouseAsync(Warehouse warehouse);

        Task updateWarehouseAsync(Warehouse warehouse);

        Task deleteWarehouseAsync(Warehouse warehouse);

    }
}

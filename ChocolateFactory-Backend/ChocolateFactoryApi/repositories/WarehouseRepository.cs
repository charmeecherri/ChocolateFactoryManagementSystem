using ChocolateFactoryApi.Data;
using ChocolateFactoryApi.Models;
using ChocolateFactoryApi.repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChocolateFactoryApi.repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly AppDbContext context;

        public WarehouseRepository(AppDbContext appDbContext)
        {
            context = appDbContext;
        }
        public async Task createWarehouseAsync(Warehouse warehouse)
        {
            await context.Warehouse.AddAsync(warehouse);
            await context.SaveChangesAsync();
        }

        public async Task deleteWarehouseAsync(Warehouse warehouse)
        {
            context.Warehouse.Remove(warehouse);
            await context.SaveChangesAsync();
        }

        public async Task<List<Warehouse>> getAllWarehouses()
        {
            return await context.Warehouse.ToListAsync();
        }

        public async Task<Warehouse> getWarehouseByIdAsync(int id)
        {
            return await context.Warehouse.Where(w => w.WarehouseId == id).FirstAsync();    
        }

        public async Task updateWarehouseAsync(Warehouse warehouse)
        {
            context.Warehouse.Update(warehouse);
            await context.SaveChangesAsync();
        }
    }
}

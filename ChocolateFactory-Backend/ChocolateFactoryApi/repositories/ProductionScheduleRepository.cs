using ChocolateFactoryApi.Data;
using ChocolateFactoryApi.Models;
using ChocolateFactoryApi.repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChocolateFactoryApi.repositories
{
    public class ProductionScheduleRepository : IProductionScheduleRepository
    {
        private readonly AppDbContext context;

        public ProductionScheduleRepository(AppDbContext context)
        {
            this.context = context;
        } 

        public async Task createProductionScheduleAsync(ProductionSchedule productionSchedule)
        {
            await context.ProductionSchedules.AddAsync(productionSchedule);
            await context.SaveChangesAsync();
        }

        public async Task deleteProductionScheduleAsync(ProductionSchedule productionSchedule)
        {
            context.ProductionSchedules.Remove(productionSchedule);
            await context.SaveChangesAsync();
        }

        public async Task<List<ProductionSchedule>> getProductSchedulesAsync()
        {
            return await context.ProductionSchedules.ToListAsync();
        }

        public async Task<ProductionSchedule> getProductScheduleByIdAsync(int id)
        {
            return await context.ProductionSchedules.Where(ps => id == ps.ScheduleId).FirstAsync();
        }

        public async Task updateProductionScheduleAsync(ProductionSchedule productionSchedule)
        {
            context.ProductionSchedules.Update(productionSchedule);
            await context.SaveChangesAsync();
        }

        public async Task<List<ProductionSchedule>> getCompletedProductionSchedulesAsync()
        {
            return await context.ProductionSchedules.Where(ps => ps.Status == "completed").ToListAsync();
        }

        public AppDbContext getAppDbContext() {
            return context;
        }
    }
}

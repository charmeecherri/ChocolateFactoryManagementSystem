using ChocolateFactoryApi.Data;
using ChocolateFactoryApi.Models;

namespace ChocolateFactoryApi.repositories.interfaces
{
    public interface IProductionScheduleRepository
    {
        Task<List<ProductionSchedule>> getProductSchedulesAsync();

        Task<List<ProductionSchedule>> getCompletedProductionSchedulesAsync();
        Task<ProductionSchedule> getProductScheduleByIdAsync(int id);

        Task createProductionScheduleAsync(ProductionSchedule productionSchedule);
        Task updateProductionScheduleAsync(ProductionSchedule productionSchedule);
        Task deleteProductionScheduleAsync(ProductionSchedule productionSchedule);

        AppDbContext getAppDbContext();
    }
}

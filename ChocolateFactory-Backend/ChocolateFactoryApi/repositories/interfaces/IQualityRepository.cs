using ChocolateFactoryApi.Data;
using ChocolateFactoryApi.Models;

namespace ChocolateFactoryApi.repositories.interfaces
{
    public interface IQualityRepository
    {

        Task createQualityAsync(Quality quality);

        Task<List<Quality>> GetQualitiesAsync();
        Task deleteQualityAsync(Quality quality);
        Task updateQualityAsync(Quality quality);

        Task<Quality> getQualityByIdAsync(int qualityId);

        Task<Quality> getQualityByBatchId(int batchId);

        AppDbContext getAppDbContext();

    }
}

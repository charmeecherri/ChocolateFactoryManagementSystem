using ChocolateFactoryApi.Data;
using ChocolateFactoryApi.Models;
using ChocolateFactoryApi.repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChocolateFactoryApi.repositories
{
    public class QualityRepository : IQualityRepository
    {
        private readonly AppDbContext context;

        public QualityRepository(AppDbContext appDbContext)
        {
            context = appDbContext;
        }
        public async Task createQualityAsync(Quality quality)
        {
            await context.Quality.AddAsync(quality);
            await context.SaveChangesAsync();
        }

        public async Task deleteQualityAsync(Quality quality)
        {
            context.Quality.Update(quality);
            await context.SaveChangesAsync();
        }
        

        public async Task<List<Quality>> GetQualitiesAsync()
        {
            return await context.Quality
                .Include(q => q.Batch)
                .Include(q => q.Batch.Product)
                .ToListAsync();
        }

        public async Task<Quality> getQualityByIdAsync(int qualityId)
        {
            return await context.Quality.Where(q => q.CheckId == qualityId).FirstAsync();
        }

        public async Task updateQualityAsync(Quality quality)
        {
            context.Quality.Update(quality);
            await context.SaveChangesAsync();
        }

        public AppDbContext getAppDbContext()
        {
            return context;
        }

        public async Task<Quality> getQualityByBatchId(int batchId)
        {
            return await context.Quality.Where(q => q.BatchId == batchId).FirstAsync();
        }
    }
}

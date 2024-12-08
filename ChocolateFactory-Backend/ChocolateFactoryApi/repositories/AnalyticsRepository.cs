using ChocolateFactoryApi.Data;
using ChocolateFactoryApi.Models;
using ChocolateFactoryApi.repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChocolateFactoryApi.repositories
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private readonly AppDbContext context;

        public AnalyticsRepository(AppDbContext appDbContext)
        {
            context = appDbContext;
        }
        public async Task createAnalystics(Analytics analytics)
        {
            await context.Analytics.AddAsync(analytics);
            await context.SaveChangesAsync();
        }

        public async Task<List<Analytics>> GetAllAnalyticsAsync()
        {
            return await context.Analytics.ToListAsync();
        }
    }
}

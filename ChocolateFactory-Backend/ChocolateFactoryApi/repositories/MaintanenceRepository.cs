using ChocolateFactoryApi.Data;
using ChocolateFactoryApi.Models;
using ChocolateFactoryApi.repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChocolateFactoryApi.repositories
{
    public class MaintanenceRepository : IMaintanenceRepostiory
    {
        private readonly AppDbContext context;

        public MaintanenceRepository(AppDbContext appDbContext)
        {
            context = appDbContext;
        }
        public async Task createMaintanence(Maintanence maintainence)
        {
            await context.Maintanence.AddAsync(maintainence);
            await context.SaveChangesAsync();
        }

        public async Task<List<Maintanence>> getMaintanences()
        {
            return await context.Maintanence.ToListAsync();
        }
    }
}

using ChocolateFactoryApi.Data;
using ChocolateFactoryApi.Models;
using ChocolateFactoryApi.repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChocolateFactoryApi.repositories
{
    public class RawMaterialRepository : IRawMaterialRepository
    {
        private readonly AppDbContext context;

        public RawMaterialRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task createRawMaterialAsync(RawMaterial rawMaterial)
        {
            await context.RawMaterials.AddAsync(rawMaterial);
            await context.SaveChangesAsync();
        }

        public async Task deleteRawMaterialAsync(RawMaterial rawMaterial)
        {
            context.RawMaterials.Remove(rawMaterial);
            await context.SaveChangesAsync();
        }

        public async Task<RawMaterial> getRawMaterialByIdAsync(int id)
        {
            return await context.RawMaterials.Where(r => r.MaterialId == id).FirstAsync();
        }

        public async Task<RawMaterial> getRawMaterialByNameAsync(string name)
        {
            return await context.RawMaterials.Where(r => name == r.Name).FirstAsync();
        }

        public async Task<List<RawMaterial>> getRawMaterialsAsync()
        {
            return await context.RawMaterials.ToListAsync();
        }

        public async Task updateRawMaterialAsync(RawMaterial rawMaterial)
        {
            context.RawMaterials.Update(rawMaterial);
            await context.SaveChangesAsync();
        }
    }
}

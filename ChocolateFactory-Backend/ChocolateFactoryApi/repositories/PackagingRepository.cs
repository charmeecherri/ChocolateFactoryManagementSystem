using ChocolateFactoryApi.Data;
using ChocolateFactoryApi.DTO.response;
using ChocolateFactoryApi.Models;
using ChocolateFactoryApi.repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChocolateFactoryApi.repositories
{
    public class PackagingRepository : IPackagingRepository
    {
        private readonly AppDbContext context;

        public PackagingRepository(AppDbContext appDbContext)
        {
            context = appDbContext;
        }
        public async Task createPackageAsync(Packaging packaging)
        {
            await context.Packagings.AddAsync(packaging);
            await context.SaveChangesAsync();
        }

        public async Task deletePackageAsync(Packaging packaging)
        {
            context.Packagings.Remove(packaging);
            await context.SaveChangesAsync();

        }

        public async Task<List<PackagingResponseDto>> getPackagesAsync()
        {
            return await context.Packagings.Select(p => new PackagingResponseDto()
            {
                PackagingId = p.PackagingId,
                ProductId = p.ProductId,
                ProductName = p.Product.ProductName,
                BatchId = p.BatchId,
                Quantity = p.Quantity,
                ExpiryDate = p.ExpiryDate,
                PackagingDate = p.PackagingDate,
                WarehouseLocation = p.WarehouseLocation
            }).ToListAsync();
        }

        public async Task<Packaging> getpackagingByIdAsync(int id)
        {
            return await context.Packagings.Where(p => p.PackagingId == id).FirstAsync();
        }

        public async Task updatePackagingAsync(Packaging packaging)
        {
            context.Packagings.Update(packaging);
            await context.SaveChangesAsync();
        }
    }
}

using ChocolateFactoryApi.DTO.response;
using ChocolateFactoryApi.Models;

namespace ChocolateFactoryApi.repositories.interfaces
{
    public interface IPackagingRepository
    {
        Task createPackageAsync(Packaging packaging);

        Task<List<PackagingResponseDto>> getPackagesAsync();
        Task<Packaging> getpackagingByIdAsync(int id);

        Task updatePackagingAsync(Packaging packaging);

        Task deletePackageAsync(Packaging packaging); 
    }
}

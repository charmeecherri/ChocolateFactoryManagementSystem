using ChocolateFactoryApi.Models;

namespace ChocolateFactoryApi.repositories.interfaces
{
    public interface IRawMaterialRepository
    {
        Task<List<RawMaterial>> getRawMaterialsAsync();

        Task<RawMaterial> getRawMaterialByNameAsync(string name);
        Task<RawMaterial> getRawMaterialByIdAsync(int id);

        Task createRawMaterialAsync(RawMaterial rawMaterial);
        Task updateRawMaterialAsync(RawMaterial rawMaterial);
        Task deleteRawMaterialAsync(RawMaterial rawMaterial);

    }
}

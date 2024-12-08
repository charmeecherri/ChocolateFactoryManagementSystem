using ChocolateFactoryApi.Models;
using ChocolateFactoryApi.repositories.interfaces;

namespace ChocolateFactoryApi.services
{
    public class CommonService
    {
        private readonly IRawMaterialRepository _rawMaterialRepository;

        public CommonService(IRawMaterialRepository rawMaterialRepository)
        {
            _rawMaterialRepository = rawMaterialRepository;

        }

        public async Task<bool> checkRawMaterialExists(List<RawMaterial> ingredients)
        {
            for (int i = 0; i < ingredients.Count; i++)
            {
                RawMaterial rawMaterial = await _rawMaterialRepository.getRawMaterialByNameAsync(ingredients[i].Name);
                if(rawMaterial.StockQuantity - ingredients[i].StockQuantity < 0)
                    return false;
            }
            return true;
        }

        public async Task<int> getMaterialStockQuantity()
        {
            List<RawMaterial> rawMaterials = await _rawMaterialRepository.getRawMaterialsAsync();
            int stockCount = 0;
            for (int i = 0; i < rawMaterials.Count; i++)
                stockCount = stockCount + rawMaterials[i].StockQuantity;

            return stockCount;
       
        }
    }
}

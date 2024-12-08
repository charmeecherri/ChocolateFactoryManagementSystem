using ChocolateFactoryApi.Models;

namespace ChocolateFactoryApi.repositories.interfaces
{
    public interface IAnalyticsRepository
    {
        Task<List<Analytics>> GetAllAnalyticsAsync();

        Task createAnalystics(Analytics analytics);
    }
}

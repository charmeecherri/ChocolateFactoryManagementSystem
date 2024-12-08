using ChocolateFactoryApi.Models;

namespace ChocolateFactoryApi.repositories.interfaces
{
    public interface IMaintanenceRepostiory
    {
        Task<List<Maintanence>> getMaintanences();

        Task createMaintanence(Maintanence maintainence);

    }
}

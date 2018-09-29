using NethereumChain.Core.Models;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace NethereumChain.Core.Contracts
{
    public interface ISupplyContractRepository
    {
        Task<int> GetChainCount();
        Task<Location> GetLocation(string locationName);
        Task<ImmutableList<Location>> GetAllLocations();
        Task<string> AddNewLocation(string userAddress, string privateKey, int gasLimit, int amount, Location location);
    }
}

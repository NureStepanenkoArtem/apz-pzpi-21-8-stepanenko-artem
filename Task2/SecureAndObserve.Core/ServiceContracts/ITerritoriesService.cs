using SecureAndObserve.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureAndObserve.Core.ServiceContracts
{
    public interface ITerritoriesService
    {
        Task<TerritoryResponse> AddTerritory(TerritoryAddRequest? territoryAddRequest);
        Task<List<TerritoryResponse>> GetAllTerritories();
        Task<TerritoryResponse?> GetTerritoryByTerritoryId(Guid? territoryId);
    }
}

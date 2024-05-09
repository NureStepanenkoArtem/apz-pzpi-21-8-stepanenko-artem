using SecureAndObserve.Core.Domain.Entities;
using SecureAndObserve.Core.Domain.RepositoryContracts;
using SecureAndObserve.Core.DTO;
using SecureAndObserve.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureAndObserve.Core.Services
{
    public class TerritoriesService : ITerritoriesService
    {
        private readonly ITerritoriesRepository _territoriesRepository;

        public TerritoriesService(ITerritoriesRepository territoriesRepository)
        {
            _territoriesRepository = territoriesRepository;
        }
        public async Task<TerritoryResponse> AddTerritory(TerritoryAddRequest? territoryAddRequest)
        {
            if (territoryAddRequest == null)
                throw new ArgumentNullException(nameof(territoryAddRequest));
            if (territoryAddRequest.Name == null)
                throw new ArgumentException(nameof(territoryAddRequest.Name));
            Territory territory = territoryAddRequest.ToTerritory();
            territory.Id = Guid.NewGuid();
            await _territoriesRepository.AddTerritory(territory);
            return territory.ToTerritoryResponse();
        }

        public async Task<List<TerritoryResponse>> GetAllTerritories()
        {
            List<Territory> territories = await _territoriesRepository.GetAllTerritories();
            return territories.Select(territory => territory.ToTerritoryResponse()).ToList();
        }
        public async Task<TerritoryResponse?> GetTerritoryByTerritoryId(Guid? territoryId)
        {
            if (territoryId == null)
                return null;
            Territory? territory = await _territoriesRepository.GetTerritoryByTerritoryId(territoryId.Value);
            if (territory == null)
                return null;
            return territory.ToTerritoryResponse();
        }
    }
}

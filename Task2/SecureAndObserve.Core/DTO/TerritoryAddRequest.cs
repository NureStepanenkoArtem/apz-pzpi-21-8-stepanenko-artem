using SecureAndObserve.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureAndObserve.Core.DTO
{
    public class TerritoryAddRequest
    {
        public Guid OwnerId { get; set; }
        public string? Name { get; set; }
        public string? Square { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public Territory ToTerritory()
        {
            return new Territory()
            {
                OwnerId = OwnerId,
                Name = Name,
                Square = Square,
                Description = Description,
                Type = Type
            };
        }
    }
}

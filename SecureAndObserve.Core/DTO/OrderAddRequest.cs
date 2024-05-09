using SecureAndObserve.Core.Domain.Entities;

namespace SecureAndObserve.Core.DTO
{
    public class OrderAddRequest
    {
        public Guid OwnerId { get; set; }
        public string? TypeOfService { get; set; }
        public string? SecurityLevel { get; set; }
        public Order ToOrder()
        {
            return new Order()
            {
                OwnerId = OwnerId,
                TypeOfService = TypeOfService,
                SecurityLevel = SecurityLevel
            };
        }
    }
}

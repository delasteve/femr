using System;
using System.Threading.Tasks;

namespace FEMR.Core
{
    public interface IAggregateRepository
    {
        Task<bool> Exists(Guid aggregateId);
        Task<T> Demand<T>(Guid aggregateId) where T : IAggregate;
        Task<IAggregate> Demand(Guid aggregateId);
        Task Save<T>(T aggregate) where T : IAggregate;
        Task Delete(Guid aggregateId);
    }
}

using System.Collections.Generic;

namespace FEMR.Core
{
    public interface IAggregateFactory
    {
        T Create<T>(IEnumerable<IEvent> events) where T : IAggregate;
        IAggregate Create(string aggregateType, IEnumerable<IEvent> events);
    }
}

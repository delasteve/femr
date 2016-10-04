using System;
using System.Collections.Generic;

namespace FEMR.Core
{
    public interface IAggregate
    {
        Guid Id { get; }
        void ApplyEvent(object @event);
        IEnumerable<IEvent> GetUncommittedEvents();
        void ClearUncommittedEvents();
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FEMR.Core
{
    public abstract class Aggregate : IAggregate
    {
        private readonly IEventRouter _router = new RegistrationEventRouter();
        private readonly LinkedList<IEvent> _uncommittedEvents = new LinkedList<IEvent>();

        public abstract Guid Id { get; }

        public void ApplyEvent(object @event)
        {
            _router.Dispatch(@event);
        }

        public IEnumerable<IEvent> GetUncommittedEvents()
        {
            return new ReadOnlyCollection<IEvent>(_uncommittedEvents.ToList());
        }

        public void ClearUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }

        protected void Register<T>(Action<T> route)
        {
            _router.Register(route);
        }

        public void RaiseEvent(IEvent @event)
        {
            ApplyEvent(@event);
            _uncommittedEvents.AddLast(@event);
        }
    }
}

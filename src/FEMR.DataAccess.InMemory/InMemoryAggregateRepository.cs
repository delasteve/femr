using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEMR.Core;

namespace FEMR.DataAccess.InMemory
{
    public class InMemoryAggregateRepository : IAggregateRepository
    {
        private List<EventData> _events;
        private readonly IAggregateFactory _aggregateFactory;

        public InMemoryAggregateRepository(IAggregateFactory aggregateFactory)
        {
            _events = new List<EventData>();
            _aggregateFactory = aggregateFactory;
        }

        public async Task<bool> Exists(Guid aggregateId)
        {
            var eventData = _events
                .Where(@event => @event.AggregateId == aggregateId)
                .ToArray();

            return await Task.FromResult(eventData.Any());
        }

        public async Task<T> Demand<T>(Guid aggregateId) where T : IAggregate
        {
            var eventData = _events
                .Where(@event => @event.AggregateId == aggregateId)
                .ToArray();

            var events = eventData
                .OrderBy(e => e.Created)
                .ThenBy(e => e.CommitSequence)
                .Select(e => e.Event as IEvent)
                .ToArray();

            return await Task.FromResult(_aggregateFactory.Create<T>(events));
        }

        public async Task Save<T>(T aggregate) where T : IAggregate
        {
            var created = DateTime.Now.ToUniversalTime();
            var uncommittedEvents = aggregate.GetUncommittedEvents()
                .Select((e, i) => new EventData
                {
                    EventId = Guid.NewGuid(),
                    AggregateId = aggregate.Id,
                    AggregateType = typeof(T).AssemblyQualifiedName,
                    Event = e,
                    Created = created,
                    CommitSequence = i
                });

            _events.AddRange(uncommittedEvents);

            aggregate.ClearUncommittedEvents();

            await Task.FromResult(Task.CompletedTask);
        }
    }
}

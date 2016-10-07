using System;

namespace FEMR.DataAccess.InMemory
{
    public class EventData
    {
        public Guid EventId { get; set; }
        public Guid AggregateId { get; set; }
        public string AggregateType { get; set; }
        public object Event { get; set; }
        public DateTime Created { get; set; }
        public int CommitSequence { get; set; }
    }
}

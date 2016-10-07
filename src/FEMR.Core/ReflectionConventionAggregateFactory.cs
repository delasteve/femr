using System;
using System.Reflection;
using System.Collections.Generic;

namespace FEMR.Core
{
    public class ReflectionConventionAggregateFactory : IAggregateFactory
    {
        public T Create<T>(IEnumerable<IEvent> events) where T : IAggregate
        {
            var type = typeof(T);
            var ctor = type.GetTypeInfo().GetConstructor(new[] {typeof(IEnumerable<IEvent>)});

            return (T) ctor.Invoke(new object[] {events});
        }

        public IAggregate Create(string aggregateType, IEnumerable<IEvent> events)
        {
            var type = Type.GetType(aggregateType);
            var ctor = type.GetTypeInfo().GetConstructor(new[] {typeof(IEnumerable<IEvent>)});

            return (IAggregate) ctor.Invoke(new object[] {events});
        }
    }
}

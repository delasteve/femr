using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace FEMR.Core.Tests
{
    public class AggregateTests
    {
        [Fact]
        public void ApplyEvent_ExecutesHandlerOnAggregate()
        {
            var aggregate = new AggregateTestSpecificSubclass();

            aggregate.RaiseFirstEvent();

            aggregate.FirstEventHandlerWasCalled.Should().BeTrue();
        }

        [Fact]
        public void ClearUncommittedEvents_RemovesAllUncommittedEvents()
        {
            var aggregate = new AggregateTestSpecificSubclass();

            aggregate.RaiseSecondEvent();
            aggregate.RaiseFirstEvent();

            aggregate.ClearUncommittedEvents();

            aggregate.GetUncommittedEvents().Should().HaveCount(0);
        }

        [Fact]
        public void GetUncommittedEvents_ContainsAllEventsInOrder()
        {
            var aggregate = new AggregateTestSpecificSubclass();

            aggregate.RaiseSecondEvent();
            aggregate.RaiseFirstEvent();

            aggregate.GetUncommittedEvents().Should().HaveCount(2);
            aggregate.GetUncommittedEvents().First().Should().BeOfType<SecondEventFake>();
            aggregate.GetUncommittedEvents().Last().Should().BeOfType<FirstEventFake>();
        }

        #region TestSpecificSubclass

        private class AggregateTestSpecificSubclass : Aggregate
        {
            public AggregateTestSpecificSubclass()
            {
                Register<FirstEventFake>(Apply);
                Register<SecondEventFake>(Apply);
            }

            public bool FirstEventHandlerWasCalled { get; private set; }
            public bool SecondEventHandlerWasCalled { get; private set; }

            public override Guid Id { get; }

            public void RaiseFirstEvent()
            {
                RaiseEvent(new FirstEventFake());
            }

            public void RaiseSecondEvent()
            {
                RaiseEvent(new SecondEventFake());
            }

            private void Apply(FirstEventFake @event)
            {
                FirstEventHandlerWasCalled = true;
            }

            private void Apply(SecondEventFake @event)
            {
                SecondEventHandlerWasCalled = true;
            }
        }

        #endregion

        #region FakeEvents

        private class FirstEventFake : IEvent
        {
        }

        private class SecondEventFake : IEvent
        {
        }

        #endregion

    }
}

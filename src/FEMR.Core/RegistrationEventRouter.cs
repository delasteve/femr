using System;
using System.Collections.Generic;

namespace FEMR.Core
{
    public class RegistrationEventRouter : IEventRouter
    {
        private readonly IDictionary<Type, Action<object>> _handlers;

        public RegistrationEventRouter()
        {
            _handlers = new Dictionary<Type, Action<object>>();
        }

        public void Register<T>(Action<T> handler)
        {
            _handlers[typeof(T)] = @event => handler((T) @event);
        }

        public void Dispatch(object eventMessage)
        {
            Action<object> handler;

            if (!_handlers.TryGetValue(eventMessage.GetType(), out handler))
            {
                throw new Exception();
            }

            handler(eventMessage);
        }
    }
}

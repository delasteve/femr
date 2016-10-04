using System;

namespace FEMR.Core
{
    public interface IEventRouter
    {
        void Register<T>(Action<T> handler);
        void Dispatch(object eventMessage);
    }
}

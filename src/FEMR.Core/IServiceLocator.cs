using System;

namespace FEMR.Core
{
    public interface IServiceLocator
    {
        object GetInstance(Type type);
    }
}

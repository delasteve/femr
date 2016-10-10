using System;
using FEMR.Core;

namespace FEMR.WebAPI.DependencyResolvers
{
    public class AutoFacServiceLocator : IServiceLocator
    {
        private readonly IServiceProvider _serviceProvider;

        public AutoFacServiceLocator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object GetInstance(Type type)
        {
            return _serviceProvider.GetService(type);;
        }
    }
}

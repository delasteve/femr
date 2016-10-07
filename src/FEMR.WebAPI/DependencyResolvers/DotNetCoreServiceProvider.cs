using System;
using FEMR.Core;

namespace FEMR.WebAPI.DependencyResolvers
{
    public class DotNetServiceLocator : IServiceLocator
    {
        private readonly IServiceProvider _serviceProvider;

        public DotNetServiceLocator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object GetInstance(Type type)
        {
            return _serviceProvider.GetService(type);;
        }
    }
}

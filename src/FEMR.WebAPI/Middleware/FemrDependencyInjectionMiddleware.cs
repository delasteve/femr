using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FEMR.Commands;
using FEMR.Core;
using FEMR.DataAccess.InMemory;
using FEMR.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FEMR.WebAPI.Middleware
{
    public static class FemrDependencyInjectionMiddleware
    {
        public static IServiceProvider AddFemrDependencyInjection(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var builder = new ContainerBuilder();

            var coreAssembly = typeof(Aggregate).GetTypeInfo().Assembly;
            var commandsAssembly = typeof(CreateUser).GetTypeInfo().Assembly;
            var queriesAssembly = typeof(GetUserInfo).GetTypeInfo().Assembly;
            var dataAccessAssemply = typeof(EventData).GetTypeInfo().Assembly;
            var webApiAssemply = typeof(Program).GetTypeInfo().Assembly;

            builder
                .RegisterAssemblyTypes(coreAssembly, commandsAssembly, queriesAssembly, webApiAssemply)
                .AsImplementedInterfaces();

            builder
                .RegisterAssemblyTypes(dataAccessAssemply)
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .Register(p => new BCryptPasswordEncryptor(configuration.GetSection("BCrypt").GetValue<int>("WorkFactor")))
                .As<IPasswordEncryptor>();

            builder.Populate(services);

            var applicationContainer = builder.Build();

            return new AutofacServiceProvider(applicationContainer);
        }
    }
}

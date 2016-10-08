using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FEMR.Commands;
using FEMR.Core;
using FEMR.DataAccess.InMemory;
using FEMR.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FEMR.WebAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IContainer ApplicationContainer { get; private set; }
        public IConfigurationRoot Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddRouting(options => options.LowercaseUrls = true);

            return CreateAutoFacDependencyContainer(services);
        }

        private IServiceProvider CreateAutoFacDependencyContainer(IServiceCollection services)
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
                .Register(p => new BCryptPasswordEncryptor(Configuration.GetSection("BCrypt").GetValue<int>("WorkFactor")))
                .As<IPasswordEncryptor>();

            builder.Populate(services);

            this.ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}

using Autofac;
using Autofac.Extensions.DependencyInjection;
using LSH.Infrastructure.Dapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSH.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {

        public static void AddDapperFactory(this IServiceCollection services)
        {
            services.AddSingleton<DapperFactory>();
        }


        public  static IServiceProvider AddAutofac(this IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();
           IContainer container= containerBuilder.Build();

            var serviceProvider = new AutofacServiceProvider(container);
           
            return serviceProvider;
        }

     
    }
}

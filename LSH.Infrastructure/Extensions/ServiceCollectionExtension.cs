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

     
    }
}

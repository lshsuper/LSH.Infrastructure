using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Api_Demo;
using Api_Demo.Extensions;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ConsoleApp53;
using LSH.Infrastructure.Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Extensions.Logging;

namespace WebApplication2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddHttpClient<WeChatProvider>();
            //services.AddAuthorization(options=> {
            //    options.AddPolicy("admin",policy=> policy.AddRequirements(new AdminReqirement(20)));
            //});
            //services.AddSingleton<IAuthorizationHandler,AdminHandler>();


            // services.AddJWTAuthentication();

            services.AddAuthentication(options=> {

                options.AddScheme<WeChatAuthenticationHandler>("DEFAULT_SCHEME_NAME", "Default Wechat Scheme");
                options.DefaultAuthenticateScheme = "DEFAULT_SCHEME_NAME";
                options.DefaultChallengeScheme = "DEFAULT_SCHEME_NAME";

            });
            services.AddSwaggerDocument(new SwaggerSetting()
            {
                SwaggerGroups = new List<SwaggerGroupSetting>() {
                    new SwaggerGroupSetting() {Description="基础模块-API",Title="基础模块",Version="V1",Name="Basic" },
                    new SwaggerGroupSetting() {Description="用户模块-API",Title="用户模块",Version="V1" ,Name="User"},

                },
                ApiXmlName = "Api_Demo.xml"
            });
         
            services.AddOptions();
            services.Configure<JWTSetting>(Configuration.GetSection("JWT"));

         
        
            
            //return AutofacProvider.Current.Build();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //builder.AddNLog();
           // LogManager.LoadConfiguration("nlog.config");
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else if (env.IsProduction())
            {
                app.UseExceptionHandler();
            }
            //启用静态文件中间件
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSwaggerDocument(new SwaggerSetting()
            {
                SwaggerGroups = new List<SwaggerGroupSetting>() {
                    new SwaggerGroupSetting() {Description="基础模块-API",Title="基础模块",Version="V1",Name="Basic" },
                    new SwaggerGroupSetting() {Description="用户模块-API",Title="用户模块",Version="V1" ,Name="User"}
                }
            });
            app.UseMvc();
        }
    }
}

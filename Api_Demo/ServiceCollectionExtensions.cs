using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2
{

    /// <summary>
    /// 服务配置
    /// </summary>
    public  static  class ServiceCollectionExtensions
    {

        #region +JWT Extension
        /// <summary>
        ///添加JWT
        /// </summary>
        /// <param name="services"></param>
        public static void AddJWTAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                //认证middleware配置
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters()
                {

                    ValidIssuer = "LSH",
                    ValidAudience = "HTML",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("qwertyuiopasdfghjklzxcvbnm"))

                };

            });
        }

        /// <summary>
        /// 启用JWT授权
        /// </summary>
        /// <param name="app"></param>
        public static void UseJWTAuthentication(this IApplicationBuilder app)
        {
            app.UseAuthentication();
        } 
        #endregion

        #region +Swagger Extension
        /// <summary>
        /// 添加Swagger
        /// </summary>
        /// <param name="services"></param>
        /// <param name="swaggerSetting"></param>
        public static void AddSwaggerDocument(this IServiceCollection services, SwaggerSetting swaggerSetting)
        {
            services.AddSwaggerGen(c =>
            {

                foreach (var doc in swaggerSetting.SwaggerGroups)
                {
                    c.SwaggerDoc(doc.Name, new OpenApiInfo()
                    {
                        Version = doc.Version,
                        Title = doc.Title,
                        Description = doc.Description,
                    });
                }

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = swaggerSetting.TokenDescription ?? "Authorization format : Bearer {token}",
                    Name = swaggerSetting.TokenName ?? "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"

                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] { }
                        }
                    });
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                if (!string.IsNullOrEmpty(swaggerSetting.ApiXmlName)) {
                    var apiXmlPath = Path.Combine(basePath, swaggerSetting.ApiXmlName);
                    c.IncludeXmlComments(apiXmlPath);
                }


                if (!string.IsNullOrEmpty(swaggerSetting.EntityXmlName)) {
                    var entityXmlPath = Path.Combine(basePath, swaggerSetting.EntityXmlName);
                    c.IncludeXmlComments(entityXmlPath);
                }
               
            });

        }
        /// <summary>
        /// 启用Swagger
        /// </summary>
        /// <param name="app"></param>
        /// <param name="swaggerSetting"></param>
        public static void UseSwaggerDocument(this IApplicationBuilder app, SwaggerSetting swaggerSetting)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var doc in swaggerSetting.SwaggerGroups)
                {
                    c.SwaggerEndpoint($"/swagger/{doc.Name}/swagger.json", doc.Description);
                }

            });
        } 
        #endregion
    }
}

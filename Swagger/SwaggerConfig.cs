using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POC.Bff.Web.Swagger.Filters;
using POC.Shared.Configuration.Documentations;
using Swashbuckle.AspNetCore.Examples;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;

namespace POC.Bff.Web.Swagger
{
    /// <summary>
    ///     Swagger configuration class.
    /// </summary>
    public static class SwaggerConfig
    {
        /// <summary>
        ///     Configure services for swagger.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        public static void ConfigureSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            var swagger = configuration.GetSection(nameof(SwaggerDoc)).Get<SwaggerDoc>() ?? new SwaggerDoc();

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("v1/swagger.json", swagger.EndpointName);
                s.EnableFilter();
                s.RoutePrefix = "swagger";
                s.DocExpansion(DocExpansion.None);
            });
        }

        /// <summary>
        ///     Add swagger configurations services.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var swagger = configuration.GetSection(nameof(SwaggerDoc)).Get<SwaggerDoc>() ?? new SwaggerDoc();

            var realPath = typeof(Startup).Assembly.Location;

            var realPathXmlDoc =
                $"{Path.GetDirectoryName(realPath)}{Path.DirectorySeparatorChar}{Path.GetFileNameWithoutExtension(realPath)}.xml";

            services.AddSwaggerGen(GetSwaggerOptions(swagger, realPathXmlDoc));

            return services;
        }

        private static Action<SwaggerGenOptions> GetSwaggerOptions(SwaggerDoc swagger, string pathXmlDoc)
        {
            return c =>
            {
                c.OperationFilter<OverloadMethodsSameVerb>();
                c.DocumentFilter<DocumentSortFilter>();
                c.DocumentFilter<HostDocumentFilter>();
                c.UseReferencedDefinitionsForEnums();
                c.SwaggerDoc(swagger.Version.ToLower(),
                    new Info
                    {
                        Title = swagger.Title,
                        Version = swagger.Version.ToLower(),
                        Description = swagger.Description
                    });
                c.OperationFilter<ExamplesOperationFilter>();
                c.OperationFilter<AuthorizationFilter>();
                c.IncludeXmlComments(pathXmlDoc);
                c.MapType<Guid>(() => new Schema { Type = "string", Format = "uuid" });
                c.AddSecurityDefinition(
                    "Bearer",
                    new ApiKeyScheme
                    {
                        Description =
                            "Standard Authorization header using the Bearer scheme. Example: bearer {token}",
                        Name = "Authorization",
                        In = "header"
                    });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }}
                });
            };
        }
    }
}
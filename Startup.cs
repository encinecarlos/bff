using AutoMapper;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Refit;
using Serilog;
using POC.Bff.Web.Infrastructure;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Bff.Web.Infrastructure.DelegatingHandlers;
using POC.Bff.Web.Infrastructure.Services.v1.Accounts;
using POC.Bff.Web.Infrastructure.Services.v1.Authorizations;
using POC.Bff.Web.Infrastructure.Services.v1.CalculateInstallments;
using POC.Bff.Web.Infrastructure.Services.v1.CardsBrand;
using POC.Bff.Web.Infrastructure.Services.v1.Comments;
using POC.Bff.Web.Infrastructure.Services.v1.Components;
using POC.Bff.Web.Infrastructure.Services.v1.Configurations;
using POC.Bff.Web.Infrastructure.Services.v1.Coupon;
using POC.Bff.Web.Infrastructure.Services.v1.DistributionCenters;
using POC.Bff.Web.Infrastructure.Services.v1.Loyalty;
using POC.Bff.Web.Infrastructure.Services.v1.Manufacturers;
using POC.Bff.Web.Infrastructure.Services.v1.Medias;
using POC.Bff.Web.Infrastructure.Services.v1.Memorials;
using POC.Bff.Web.Infrastructure.Services.v1.Order;
using POC.Bff.Web.Infrastructure.Services.v1.Organizations;
using POC.Bff.Web.Infrastructure.Services.v1.Products;
using POC.Bff.Web.Infrastructure.Services.v1.Promotions;
using POC.Bff.Web.Infrastructure.Services.v1.Quotations;
using POC.Bff.Web.Infrastructure.Services.v1.Reports;
using POC.Bff.Web.Infrastructure.Services.v1.Shippings;
using POC.Bff.Web.Infrastructure.Services.v1.ShoppingCart;
using POC.Bff.Web.Infrastructure.Services.v1.SicesExpress;
using POC.Bff.Web.Infrastructure.Services.v1.Systems;
using POC.Bff.Web.Infrastructure.Services.v1.Tags;
using POC.Bff.Web.Infrastructure.Services.v1.ZipCodes;
using POC.Bff.Web.Swagger;
using POC.Shared.Resources;
using POC.Shared.Resources.Contracts;
using POC.Shared.Responses;
using POC.Shared.Responses.Contracts;
using System;

namespace POC.Bff.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var applicationServices = Configuration.GetSection("ApplicationSettings:ApplicationServices")
                .Get<ApplicationServices>();

            services.AddCors(options => options.AddPolicy("CorsPolicy", policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            }));

            services
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddTransient<HttpClientAuthorizationDelegatingHandler>()
                .AddTransient<HttpLoggingDelegatingHandler>()
                .AddScoped<IResponseService, ResponseService>()
                .AddScoped<IResourceService, ResourceService>()
                .AddScoped<IAccountService, AccountServiceHandler>()
                .AddScoped<IMediaService, MediaServiceHandler>()
                .AddScoped<IAuthorizationService, AuthorizationServiceHandler>()
                .AddScoped<ICommentService, CommentServiceHandler>()
                .AddScoped<IComponentService, ComponentServiceHandler>()
                .AddScoped<IConfigurationService, ConfigurationServiceHandler>()
                .AddScoped<IDistributionCenterService, DistributionCenterServiceHandler>()
                .AddScoped<IManufacturerService, ManufacturerServiceHandler>()
                .AddScoped<IMemorialService, MemorialServiceHandler>()
                .AddScoped<IOrganizationService, OrganizationServiceHandler>()
                .AddScoped<IQuotationService, QuotationServiceHandler>()
                .AddScoped<IShippingService, ShippingServiceHandler>()
                .AddScoped<ISystemServiceHandler, SystemServiceHandler>()
                .AddScoped<IZipCodeService, ZipCodeServiceHandler>()
                .AddScoped<ILoyaltyService, LoyaltyServiceHandler>()
                .AddScoped<IPromotionService, PromotionServiceHandler>()
                .AddScoped<IReportService, ReportServiceHandler>()
                .AddScoped<IProductService, ProductServiceHandler>()
                .AddScoped<IShoppingCartService, ShoppingCartServiceHandler>()
                .AddScoped<IOrderService, OrderServiceHandler>()
                .AddScoped<ICouponService, CouponServiceHandler>()
                .AddScoped<ICardsBrandService, CardsBrandServiceHandler>()
                .AddScoped<ICalculateInstallmentsService, CalculateInstallmentsServiceHandler>()
                .AddScoped<ISicesExpressService, SicesExpressServiceHandler>()
                .AddScoped<ITagService, TagServiceHandler>()
                .AddSwagger(Configuration)
                .AddSingleton(applicationServices)
                .AddAutoMapper(typeof(Startup))
                .AddApplicationInsightsTelemetry(Configuration);

            ConfigureLogger(Configuration);
            ConfigureBffHealthChecks(services, applicationServices);
            ConfigureExternalClients(services, applicationServices);

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
        }

        public void ConfigureLogger(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHealthChecks("/healthcheck", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseCors("CorsPolicy");
            app.ConfigureSwagger(Configuration);
            app.UseStaticFiles();
            app.UseMvc();
        }

        public void ConfigureBffHealthChecks(IServiceCollection services, ApplicationServices applicationServices)
        {
            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddUrlGroup(new Uri($"{applicationServices.Commercial}/healthcheck"), "commercial-service",
                    tags: new[] { nameof(applicationServices.Commercial) })
                .AddUrlGroup(new Uri($"{applicationServices.Identity}/healthcheck"), "identity-service",
                    tags: new[] { nameof(applicationServices.Identity) })
                .AddUrlGroup(new Uri($"{applicationServices.Component}/healthcheck"), "component-service",
                    tags: new[] { nameof(applicationServices.Component) })
                .AddUrlGroup(new Uri($"{applicationServices.Configuration}/healthcheck"), "configuration-service",
                    tags: new[] { nameof(applicationServices.Configuration) })
                .AddUrlGroup(new Uri($"{applicationServices.Report}/healthcheck"), "report-service",
                    tags: new[] { nameof(applicationServices.Report) })
                .AddUrlGroup(new Uri($"{applicationServices.Express}/healthcheck"), "express-service",
                    tags: new[] { nameof(applicationServices.Express) });
        }

        public void ConfigureExternalClients(IServiceCollection services, ApplicationServices applicationServices)
        {
            services.AddRefitClient<ICommercialClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(applicationServices.Commercial))
                .AddHttpMessageHandler<HttpLoggingDelegatingHandler>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddRefitClient<IConfigurationClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(applicationServices.Configuration))
                .AddHttpMessageHandler<HttpLoggingDelegatingHandler>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddRefitClient<IIdentityClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(applicationServices.Identity))
                .AddHttpMessageHandler<HttpLoggingDelegatingHandler>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddRefitClient<IComponentClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(applicationServices.Component))
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddRefitClient<IReportClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(applicationServices.Report))
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddRefitClient<IExpressClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(applicationServices.Express))
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
        }
    }
}
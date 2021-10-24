using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FitnessCenterManagement.Api.Contexts;
using FitnessCenterManagement.Api.SignalR;
using FitnessCenterManagement.BusinessLogic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FitnessCenterManagement.Api
{
    public partial class Startup
    {
        // private const string JwtIssuerConfigKey = "JwtIssuer"
        // private const string JwtAudienceConfigKey = "JwtAudience"
        // private const string JwtSecretKeyConfigKey = "JwtSecretKey"
        private const string ConnectionStringName = "MainDbConnectionString";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static void ConfigureLocalization(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ru-RU"),
                    new CultureInfo("be-BY"),
                };

                options.DefaultRequestCulture = new RequestCulture(culture: "ru-RU", uiCulture: "ru-RU");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;

                options.AddInitialRequestCultureProvider(new CustomRequestCultureProvider(async context =>
                {
                    var cookiesLang = context.Request.Cookies["Language"];

                    var typedHeaders = context.Request.GetTypedHeaders();

                    if (typedHeaders.AcceptLanguage != null && typedHeaders.AcceptLanguage.Any())
                    {
                        var browserLangCode = typedHeaders.AcceptLanguage?.First()?.Value.ToString()
                                .Split(";").FirstOrDefault()?.Split(",").FirstOrDefault();

                        var browserLang = supportedCultures.FirstOrDefault(lang =>
                        lang.Name.Contains(browserLangCode, StringComparison.InvariantCultureIgnoreCase)).Name;

                        return await Task.FromResult(new ProviderCultureResult(cookiesLang is null ? browserLang : cookiesLang));
                    }

                    return await Task.FromResult(new ProviderCultureResult(cookiesLang is null ? "ru-RU" : cookiesLang));
                }));
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connString = Configuration.GetConnectionString(ConnectionStringName);

            services.AddDbContext<AuthApiDbContext>(options =>
                options.UseSqlServer(connectionString: connString));
            services.AddIdentity<Api.Identity.User, IdentityRole>()
                .AddEntityFrameworkStores<AuthApiDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureBllServices(connString);

            ConfigureAuthentication(services);

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BusinessLogic.Mapper.BusinessLogicMapperFirstProfile());
                mc.AddProfile(new BusinessLogic.Mapper.BusinessLogicMapperSecondProfile());
                mc.AddProfile(new Mapper.PresentationMapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            // services.AddSingleton(typeof(IUserIdProvider), typeof(MessagingUserIdProvider))
            ConfigureLocalization(services);

            services.AddControllers();

            ConfigureSwagger(services);

            services.AddCors(options =>
                options.AddDefaultPolicy(builder =>
                    builder.SetIsOriginAllowed(_ => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()));

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
#pragma warning disable CA1822 // Mark members as static
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
#pragma warning restore CA1822 // Mark members as static
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "FitnessCenterManagement.API v1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRequestLocalization();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHub<MessagingHub>("/messaging");
            });
        }
    }
}

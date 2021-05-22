using System;
using System.IO;
using System.Reflection;
using System.Text;
using AutoMapper;
using FitnessCenterManagement.Api.Contexts;
using FitnessCenterManagement.Api.JwtServices;
using FitnessCenterManagement.BusinessLogic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace FitnessCenterManagement.Api
{
    public class Startup
    {
        private const string JwtIssuerConfigKey = "JwtIssuer";
        private const string JwtAudienceConfigKey = "JwtAudience";
        private const string JwtSecretKeyConfigKey = "JwtSecretKey";
        private const string ConnectionStringName = "MainDbConnectionString";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

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

            var tokenSettings = Configuration.GetSection("JwtTokenSettings");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = tokenSettings[JwtIssuerConfigKey],
                        ValidateAudience = true,
                        ValidAudience = tokenSettings[JwtAudienceConfigKey],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings[JwtSecretKeyConfigKey])),
                        ValidateLifetime = false,
                    };
                });

            services.Configure<JwtTokenSettings>(tokenSettings);
            services.AddScoped<JwtTokenService>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BusinessLogic.Mapper.BusinessLogicMapperFirstProfile());
                mc.AddProfile(new BusinessLogic.Mapper.BusinessLogicMapperSecondProfile());
                mc.AddProfile(new Mapper.PresentationMapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FitnessCenterManagement.API",
                    Version = "v1",
                    Description = "Provides endpoints to interact with services. CI CD processes were configured successfully.",
                });

                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Description = "Allows to attach a JWT token to the request to access the endpoints requiring authorization.",
                    In = ParameterLocation.Header,
                    Name = "JWT Authentication",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme,
                    },
                };
                options.AddSecurityDefinition("Bearer", jwtSecurityScheme);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, System.Array.Empty<string>() },
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            // services.AddMvc(options =>
            //    options.Filters.Add(typeof(SimpleResourceFilter))
            // )
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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

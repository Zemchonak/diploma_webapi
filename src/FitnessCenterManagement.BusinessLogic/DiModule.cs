using FitnessCenterManagement.BusinessLogic.Interfaces;
using FitnessCenterManagement.BusinessLogic.Services;
using FitnessCenterManagement.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessCenterManagement.BusinessLogic
{
    public static class DiModule
    {
        public static void ConfigureBllServices(this IServiceCollection services, string connString)
        {
            services.ConfigurateDalServices(connString);

            services.AddScoped(typeof(IEntityService<>), typeof(GenericEntityService<>));

            services.AddScoped<ILocalizationService, LocalizationService>();
            services.AddScoped<IAbonementsService, AbonementsService>();
            services.AddScoped<IFitnessCatalogsService, FitnessCatalogsService>();
            services.AddScoped<ISchedulesService, SchedulesService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IQrService, QrService>();

            services.AddScoped<IChatService, ChatService>();
        }
    }
}

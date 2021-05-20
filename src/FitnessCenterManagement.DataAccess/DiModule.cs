using FitnessCenterManagement.DataAccess.Contexts;
using FitnessCenterManagement.DataAccess.Interfaces;
using FitnessCenterManagement.DataAccess.Repositories;
using FitnessCenterManagement.DataAccess.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessCenterManagement.DataAccess
{
    public static class DiModule
    {
        public static void ConfigurateDalServices(this IServiceCollection services, string connString)
        {
            services.AddDbContext<MainDbContext>(options =>
                options.UseSqlServer(connString));

            services.AddScoped(typeof(IRepository<>), typeof(GenericEfRepository<>));
            services.AddScoped(typeof(IMessageRepository), typeof(MessageRepository));
        }
    }
}

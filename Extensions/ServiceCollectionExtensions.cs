using CriptoControl.Domain.Interfaces;
using CriptoControl.Infrastructure.Repository;
using CriptoControl.Infrastructure.Repository.IRepository;

namespace CriptoControl.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ICriptoRepository, CriptoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;        
        }
    }
}
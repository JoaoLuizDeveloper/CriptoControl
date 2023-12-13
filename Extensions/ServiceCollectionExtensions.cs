using CriptoControl.Infrastructure.Application;
using CriptoControl.Infrastructure.Repository;
using CriptoControl.Infrastructure.Service.MessageBus;
using CriptoControl.Model.Interfaces.IApplication;
using CriptoControl.Model.Interfaces.IRepository;
using CriptoControl.Model.Interfaces.IServices;

namespace CriptoControl.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ICriptoApplication, CriptoApplication>();
            services.AddScoped<ICriptoRepository, CriptoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMessageBusService, MessageBusService>();
            return services;        
        }
    }
}
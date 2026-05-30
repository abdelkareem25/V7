using V7.Application.Services;
using V7.Domain.Interfaces;
using V7.Domain.Interfaces.Repositories;
using V7.Domain.Interfaces.Services;
using V7.Infrastructure;
using V7.Infrastructure.Repositories;

namespace V7.Api.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IOrderService,OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(config => config.AddProfile(new V7.Api.Mapping.ProductProfile()));
            services.AddAutoMapper(config => config.AddProfile(new V7.Api.Mapping.CategoryProfile()));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}

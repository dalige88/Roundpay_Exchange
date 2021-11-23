using Microsoft.Extensions.DependencyInjection;
using RoundpayFinTech.AppCode.Interfaces;
using RoundpayFinTech.AppCode.MiddleLayer;

namespace RoundpayFinTech.AppCode.Configuration
{
    public static class Services
    {
        public static void AddService(this IServiceCollection service)
        {
            service.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}

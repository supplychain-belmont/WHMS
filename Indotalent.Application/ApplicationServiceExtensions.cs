using Indotalent.Application.GoodReceives;
using Indotalent.Application.Products;
using Indotalent.Application.PurchaseOrders;
using Indotalent.Application.SalesOrders;

using Microsoft.Extensions.DependencyInjection;

namespace Indotalent.Application;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<SalesOrderProcessor>();
        services.AddScoped<PurchaseOrderProcessor>();
        services.AddScoped<AssemblyProcessor>();
        services.AddScoped<ProductProcessor>();
        services.AddScoped<GoodReceiveProcessor>();
        services.AddScoped<PurchaseOrderItemProcessor>();
        return services;
    }
}

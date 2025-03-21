using Indotalent.Applications.Warehouses;
using Indotalent.Domain.Entities;

namespace Indotalent.Data.Demo
{
    public static class DemoWarehouse
    {
        public static async Task GenerateAsync(IServiceProvider services)
        {
            var service = services.GetRequiredService<WarehouseService>();

            await service.AddAsync(new Warehouse { Name = "Tienda 1" });
            await service.AddAsync(new Warehouse { Name = "Garaje" });
            await service.AddAsync(new Warehouse { Name = "8A" });
            await service.AddAsync(new Warehouse { Name = "Ubicación Juana" });
            await service.AddAsync(new Warehouse { Name = "Baulera" });
        }
    }
}

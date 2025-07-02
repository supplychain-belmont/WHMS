using Indotalent.Applications.VendorGroups;
using Indotalent.Domain.Entities;

namespace Indotalent.Data.Demo
{
    public static class DemoVendorGroup
    {
        public static async Task GenerateAsync(IServiceProvider services)
        {
            var service = services.GetRequiredService<VendorGroupService>();

            await service.AddAsync(new VendorGroup { Name = "Fabricante" });
            await service.AddAsync(new VendorGroup { Name = "Proveedor" });
            await service.AddAsync(new VendorGroup { Name = "Proveedor de Servicios" });
            await service.AddAsync(new VendorGroup { Name = "Distribuidor" });
            await service.AddAsync(new VendorGroup { Name = "Freelancer" });
        }
    }
}

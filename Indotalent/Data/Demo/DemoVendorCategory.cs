using Indotalent.Applications.VendorCategories;
using Indotalent.Models.Entities;

namespace Indotalent.Data.Demo
{
    public static class DemoVendorCategory
    {
        public static async Task GenerateAsync(IServiceProvider services)
        {
            var service = services.GetRequiredService<VendorCategoryService>();

            await service.AddAsync(new VendorCategory { Name = "Grande" });
            await service.AddAsync(new VendorCategory { Name = "Mediano" });
            await service.AddAsync(new VendorCategory { Name = "Pequeño" });
            await service.AddAsync(new VendorCategory { Name = "Especialidad" });
            await service.AddAsync(new VendorCategory { Name = "Local" });
            await service.AddAsync(new VendorCategory { Name = "Global" });
        }
    }
}

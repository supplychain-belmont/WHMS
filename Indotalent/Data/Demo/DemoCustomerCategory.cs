using Indotalent.Applications.CustomerCategories;
using Indotalent.Models.Entities;

namespace Indotalent.Data.Demo
{
    public static class DemoCustomerCategory
    {
        public static async Task GenerateAsync(IServiceProvider services)
        {
            var service = services.GetRequiredService<CustomerCategoryService>();

            await service.AddAsync(new CustomerCategory { Name = "Empresa grande" });
            await service.AddAsync(new CustomerCategory { Name = "Empresa mediana" });
            await service.AddAsync(new CustomerCategory { Name = "Empresa pequeña" });
            await service.AddAsync(new CustomerCategory { Name = "Startup" });
            await service.AddAsync(new CustomerCategory { Name = "Microempresa" });
        }
    }
}

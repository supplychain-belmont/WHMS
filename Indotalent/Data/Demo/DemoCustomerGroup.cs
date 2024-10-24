using Indotalent.Applications.CustomerGroups;
using Indotalent.Models.Entities;

namespace Indotalent.Data.Demo
{
    public static class DemoCustomerGroup
    {
        public static async Task GenerateAsync(IServiceProvider services)
        {
            var service = services.GetRequiredService<CustomerGroupService>();

            await service.AddAsync(new CustomerGroup { Name = "Corporativo" });
            await service.AddAsync(new CustomerGroup { Name = "Gobierno" });
            await service.AddAsync(new CustomerGroup { Name = "Fundación" });
            await service.AddAsync(new CustomerGroup { Name = "Militar" });
            await service.AddAsync(new CustomerGroup { Name = "Educación" });
            await service.AddAsync(new CustomerGroup { Name = "Hostelería" });
        }
    }
}

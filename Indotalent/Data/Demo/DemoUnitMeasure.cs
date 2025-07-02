using Indotalent.Applications.UnitMeasures;
using Indotalent.Domain.Entities;

namespace Indotalent.Data.Demo
{
    public static class DemoUnitMeasure
    {
        public static async Task GenerateAsync(IServiceProvider services)
        {
            var service = services.GetRequiredService<UnitMeasureService>();

            await service.AddAsync(new UnitMeasure { Name = "m", Description = "Metro" });
            await service.AddAsync(new UnitMeasure { Name = "kg", Description = "Kilogramo" });
            await service.AddAsync(new UnitMeasure { Name = "h", Description = "Hora" });
            await service.AddAsync(new UnitMeasure { Name = "u", Description = "Unidad" });
        }
    }
}

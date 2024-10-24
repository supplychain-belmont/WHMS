using Indotalent.Applications.NumberSequences;
using Indotalent.Applications.Products;
using Indotalent.Models.Entities;

namespace Indotalent.Data.Demo;

public static class DemoAssemblyProduct
{
    public static async Task GenerateAsync(IServiceProvider services)
    {
        var assemblyProductService = services.GetRequiredService<AssemblyProductService>();
        var numberSequenceService = services.GetRequiredService<NumberSequenceService>();

    }
}

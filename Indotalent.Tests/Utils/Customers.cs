using Indotalent.Models.Entities;

namespace Indotalent.Tests.Utils;

public static class Customers
{
    public static IEnumerable<TestCaseData> GetAll()
    {
        yield return new TestCaseData(new Customer
        {
            Name = "Customer05",
            CustomerGroupId = 0,
            CustomerCategoryId = 0
        }).SetName("test for Customer");
    }
}

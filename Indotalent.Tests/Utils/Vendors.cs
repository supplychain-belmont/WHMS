using Indotalent.Models.Entities;

namespace Indotalent.Tests.Utils;

public static class Vendors
{

    public static IEnumerable<TestCaseData> GetAll()
    {
        yield return new TestCaseData(new Vendor
        {
            Name = "Vendor05",
            VendorGroupId = 0,
            VendorCategoryId = 0
        }).SetName("test for Vendor");
        yield return new TestCaseData(new VendorGroup
        {
            Name = "VendorGroup05"
        }).SetName("test for VendorGroup");
        yield return new TestCaseData(new VendorCategory
        {
            Name = "VendorCategory05"
        }).SetName("test for VendorCategory");
        yield return new TestCaseData(new VendorContact
        {
            Name = "VendorContact05",
            VendorId = 0
        }).SetName("test for VendorContact");
    }
}
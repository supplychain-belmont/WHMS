using Indotalent.Models.Entities;

using static System.Guid;

namespace Indotalent.Tests.Utils;

public static class ItemList
{
    public static List<ApplicationUser> Users =
    [
        new()
        {
            Id = "1",
            UserName = "User1",
            SelectedCompanyId = 1
        },

        new()
        {
            Id = "2",
            UserName = "User2",
            SelectedCompanyId = 2
        },

        new()
        {
            Id = "3",
            UserName = "User3",
            SelectedCompanyId = 3
        },

        new()
        {
            Id = "4",
            UserName = "User4",
            SelectedCompanyId = 4
        }
    ];

    public static List<Company> Companies =
    [
        new Company
        {
            Id = 1,
            TimeZone = "Pacific Standard Time",
            Name = "Company 1",
            Currency = "USD",
        },

        new Company
        {
            Id = 2,
            TimeZone = "Central Standard Time",
            Name = "Company 2",
            Currency = "USD",
        },

        new Company
        {
            Id = 3,
            TimeZone = "Eastern Standard Time",
            Name = "Company 3",
            Currency = "USD",
        },

        new Company
        {
            Id = 4,
            TimeZone = "Mountain Standard Time",
            Name = "Company 4",
            Currency = "USD",
        }
    ];

    public static List<Vendor> Vendors =
    [
        new Vendor
        {
            Id = 1,
            RowGuid = NewGuid(),
            Name = "Vendor01",
            VendorGroupId = 0,
            VendorCategoryId = 0
        },

        new Vendor
        {
            Id = 2,
            RowGuid = NewGuid(),
            Name = "Vendor02",
            VendorGroupId = 0,
            VendorCategoryId = 0
        },

        new Vendor
        {
            Id = 3,
            RowGuid = NewGuid(),
            Name = "Vendor03",
            VendorGroupId = 0,
            VendorCategoryId = 0
        },

        new Vendor
        {
            Id = 4,
            RowGuid = NewGuid(),
            Name = "Vendor04",
            VendorGroupId = 0,
            VendorCategoryId = 0
        }
    ];

    public static List<VendorGroup> VendorGroups =
    [
        new VendorGroup
        {
            Id = 1,
            RowGuid = NewGuid(),
            Name = "VendorGroup01"
        },
        new VendorGroup
        {
            Id = 2,
            RowGuid = NewGuid(),
            Name = "VendorGroup02"
        },
        new VendorGroup
        {
            Id = 3,
            RowGuid = NewGuid(),
            Name = "VendorGroup03"
        },
        new VendorGroup
        {
            Id = 4,
            RowGuid = NewGuid(),
            Name = "VendorGroup04"
        }
    ];

    public static List<VendorCategory> VendorCategories =
    [
        new VendorCategory
        {
            Id = 1,
            RowGuid = NewGuid(),
            Name = "VendorCategory01"
        },

        new VendorCategory
        {
            Id = 2,
            RowGuid = NewGuid(),
            Name = "VendorCategory02"
        },

        new VendorCategory
        {
            Id = 3,
            Name = "VendorCategory03"
        },

        new VendorCategory
        {
            Id = 4,
            RowGuid = NewGuid(),
            Name = "VendorCategory04"
        }
    ];

    public static List<VendorContact> VendorContacts =
    [
        new VendorContact
        {
            Id = 1,
            RowGuid = NewGuid(),
            Name = "VendorContact01",
            VendorId = 0
        },

        new VendorContact
        {
            Id = 2,
            RowGuid = NewGuid(),
            Name = "VendorContact02",
            VendorId = 0
        },

        new VendorContact
        {
            Id = 3,
            RowGuid = NewGuid(),
            Name = "VendorContact03",
            VendorId = 0
        },

        new VendorContact
        {
            Id = 4,
            RowGuid = NewGuid(),
            Name = "VendorContact04",
            VendorId = 0
        }
    ];

    public static List<Customer> Customers =
    [
        new Customer
        {
            Id = 1,
            Name = "Customer1",
            Number = "123456",
            Description = "Description1",
            Street = "Street1",
            City = "City1",
            State = "State1",
            ZipCode = "Zip1",
            Country = "Country1",
            PhoneNumber = "Phone1",
            FaxNumber = "Fax1",
            EmailAddress = "Email1",
            Website = "Website1",
            WhatsApp = "WhatsApp1",
            LinkedIn = "LinkedIn1",
            Facebook = "Facebook1",
            Instagram = "Instagram1",
            TwitterX = "Twitter1",
            TikTok = "TikTok1",
            CustomerGroupId = 1,
            CustomerCategoryId = 1
        },
        new Customer
        {
            Id = 2,
            Name = "Customer2",
            Number = "123456",
            Description = "Description1",
            Street = "Street1",
            City = "City1",
            State = "State1",
            ZipCode = "Zip1",
            Country = "Country1",
            PhoneNumber = "Phone1",
            FaxNumber = "Fax1",
            EmailAddress = "Email1",
            Website = "Website1",
            WhatsApp = "WhatsApp1",
            LinkedIn = "LinkedIn1",
            Facebook = "Facebook1",
            Instagram = "Instagram1",
            TwitterX = "Twitter1",
            TikTok = "TikTok1",
            CustomerGroupId = 1,
            CustomerCategoryId = 1
        },
        new Customer
        {
            Id = 3,
            Name = "Customer3",
            Number = "123456",
            Description = "Description1",
            Street = "Street1",
            City = "City1",
            State = "State1",
            ZipCode = "Zip1",
            Country = "Country1",
            PhoneNumber = "Phone1",
            FaxNumber = "Fax1",
            EmailAddress = "Email1",
            Website = "Website1",
            WhatsApp = "WhatsApp1",
            LinkedIn = "LinkedIn1",
            Facebook = "Facebook1",
            Instagram = "Instagram1",
            TwitterX = "Twitter1",
            TikTok = "TikTok1",
            CustomerGroupId = 1,
            CustomerCategoryId = 1
        }
    ];

    public static List<T>? GetList<T>(Type type) where T : class
    {
        return type switch
        {
            not null when type == typeof(ApplicationUser) => Users as List<T>,
            not null when type == typeof(Company) => Companies as List<T>,
            not null when type == typeof(Vendor) => Vendors as List<T>,
            not null when type == typeof(VendorGroup) => VendorGroups as List<T>,
            not null when type == typeof(VendorCategory) => VendorCategories as List<T>,
            not null when type == typeof(VendorContact) => VendorContacts as List<T>,
            not null when type == typeof(Customer) => Customers as List<T>,
            _ => throw new ArgumentException("Invalid type")
        };
    }
}

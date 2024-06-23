using Indotalent.Data;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Models.Contracts;
using Indotalent.Tests.Utils;

using Microsoft.EntityFrameworkCore;

using MockQueryable.Moq;

using Moq;

using static NUnit.Framework.Assert;

namespace Indotalent.Tests.Infrastructures.Repositories;

[TestFixture]
[TestOf(typeof(AuditColumnTransformer))]
public class AuditColumnTransformerTest
{
    private Mock<ApplicationDbContext> _contextMock;
    private AuditColumnTransformer _auditColumnTransformer;

    [SetUp]
    public void Setup()
    {
        var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Indotalent")
            .Options;
        _contextMock = new Mock<ApplicationDbContext>(contextOptions);
        var mockUsers = ItemList.Users.AsQueryable().BuildMockDbSet();
        var mockCompanies = ItemList.Companies.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.Users).Returns(mockUsers.Object);
        _contextMock.Setup(x => x.Company).Returns(mockCompanies.Object);
        _contextMock.Setup(p =>
                p.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        _auditColumnTransformer = new AuditColumnTransformer();
    }

    [TestCaseSource(typeof(Vendors), nameof(Vendors.GetAll))]
    [TestCaseSource(typeof(Customers), nameof(Customers.GetAll))]
    public async Task ShouldModifyTheContextVariables<T>(T entity) where T : _Base
    {
        entity.CreatedByUserId = "1";
        entity.CreatedAtUtc = DateTime.UtcNow;
        entity.UpdatedByUserId = "1";
        entity.UpdatedAtUtc = DateTime.UtcNow;
        That(entity.UpdatedAtString, Is.Null);
        await _auditColumnTransformer.TransformAsync(entity, _contextMock.Object);
        Multiple(() =>
        {
            That(entity.CreatedByUserName, Is.EqualTo("User1"));
            That(entity.UpdatedAtString, Is.Not.Null);
            That(entity.CreatedAtString, Is.Not.Null);
        });
    }
}

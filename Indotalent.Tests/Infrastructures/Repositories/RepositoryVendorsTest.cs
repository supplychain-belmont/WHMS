using Indotalent.Data;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Models.Contracts;
using Indotalent.Tests.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using static NUnit.Framework.Assert;

namespace Indotalent.Tests.Infrastructures.Repositories;

[TestFixture]
public class RepositoryVendorsTest
{
    private Mock<ApplicationDbContext> _contextMock;
    private Mock<IHttpContextAccessor> _httpContextAccessorMock;
    private Mock<IAuditColumnTransformer> _auditColumnTransformerMock;

    private const string ExceptionMessageRowGuidNull = "Unable to process, row guid is null";
    private const string ExceptionMessageIdNull = "Unable to process, id is null";
    private const string ExceptionMessageEntityNull = "Unable to process, entity is null";

    #region Setup

    [SetUp]
    public void Setup()
    {
        var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Indotalent")
            .Options;

        _contextMock = new Mock<ApplicationDbContext>(contextOptions);
        _contextMock.Setup(x => x.SetModifiedState(It.IsAny<_Base>()));
        _contextMock.Setup(p => p.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        _auditColumnTransformerMock = new Mock<IAuditColumnTransformer>();
    }

    #endregion

    #region TestAllTheVendors

    [TestCaseSource(typeof(Vendors), nameof(Vendors.GetAll))]
    public void ShouldCountAllVendorsDbSet<T>(T entity) where T : _Base
    {
        ConfigureDbContextMock<T>();
        var repository = new Repository<T>(_contextMock.Object, _httpContextAccessorMock.Object,
            _auditColumnTransformerMock.Object);

        That(repository, Is.Not.Null);
        That(repository.GetAll().Count(), Is.Not.EqualTo(0));
    }

    [TestCaseSource(typeof(Vendors), nameof(Vendors.GetAll))]
    public void ShouldCountAllArchivedVendorsDbSet<T>(T entity) where T : _Base
    {
        ConfigureDbContextMock<T>();
        var repository = new Repository<T>(_contextMock.Object, _httpContextAccessorMock.Object,
            _auditColumnTransformerMock.Object);

        That(repository, Is.Not.Null);
        That(repository.GetAllArchive().Count(), Is.EqualTo(0));
    }

    #endregion

    #region TestCreateVendors

    [TestCaseSource(typeof(Vendors), nameof(Vendors.GetAll))]
    public async Task ShouldAddVendorByTable<T>(T entity) where T : _Base
    {
        ConfigureDbContextMock<T>();
        var repository = new Repository<T>(_contextMock.Object, _httpContextAccessorMock.Object,
            _auditColumnTransformerMock.Object);

        await repository.AddAsync(entity);

        _contextMock.Verify(x => x.Set<T>().Add(It.IsAny<T>()), Times.Once);
        _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        That(repository.GetAll().Count(), Is.GreaterThan(4));
    }

    [TestCaseSource(typeof(Vendors), nameof(Vendors.GetAll))]
    public void ShouldThrowExceptionTryToAddNullEntity<T>(T entity) where T : _Base
    {
        ConfigureDbContextMock<T>();
        var repository = new Repository<T>(_contextMock.Object, _httpContextAccessorMock.Object,
            _auditColumnTransformerMock.Object);

        var exception = ThrowsAsync<Exception>(async () => await repository.AddAsync(null));
        That(exception, Is.Not.Null);
        That(exception?.Message, Is.EqualTo(ExceptionMessageEntityNull));
    }

    #endregion

    #region TestByIdVendors

    [TestCaseSource(typeof(Vendors), nameof(Vendors.GetAll))]
    public async Task ShouldReturnAVendorByIdAsync<T>(T entity) where T : _Base
    {
        ConfigureDbContextMock<T>();
        var repository = new Repository<T>(_contextMock.Object, _httpContextAccessorMock.Object,
            _auditColumnTransformerMock.Object);

        await repository.AddAsync(entity);
        var vendor = await repository.GetByIdAsync(entity.Id);

        _contextMock.Verify(x => x.Set<T>().Add(It.IsAny<T>()), Times.Once);
        _auditColumnTransformerMock.Verify(x => x.TransformAsync(It.IsAny<T>(), It.IsAny<ApplicationDbContext>()),
            Times.Once);
        That(vendor, Is.Not.Null);
    }

    [TestCaseSource(typeof(Vendors), nameof(Vendors.GetAll))]
    public async Task ShouldReturnAVendorByRowGuidAsync<T>(T entity) where T : _Base
    {
        ConfigureDbContextMock<T>();
        var repository = new Repository<T>(_contextMock.Object, _httpContextAccessorMock.Object,
            _auditColumnTransformerMock.Object);

        await repository.AddAsync(entity);
        var vendor = await repository.GetByRowGuidAsync(entity.RowGuid);

        _contextMock.Verify(x => x.Set<T>().Add(It.IsAny<T>()), Times.Once);
        _auditColumnTransformerMock.Verify(x => x.TransformAsync(It.IsAny<T>(), It.IsAny<ApplicationDbContext>()),
            Times.Once);
        That(vendor, Is.Not.Null);
    }

    [TestCaseSource(typeof(Vendors), nameof(Vendors.GetAll))]
    public void ShouldThrowExceptionWhenIdIsNull<T>(T entity) where T : _Base
    {
        ConfigureDbContextMock<T>();
        var repository = new Repository<T>(_contextMock.Object, _httpContextAccessorMock.Object,
            _auditColumnTransformerMock.Object);

        var exception = ThrowsAsync<Exception>(async () => await repository.GetByIdAsync(null));
        That(exception, Is.Not.Null);
        That(exception?.Message, Is.EqualTo(ExceptionMessageIdNull));
    }

    [TestCaseSource(typeof(Vendors), nameof(Vendors.GetAll))]
    public void ShouldThrowExceptionWhenRowGuidIsNull<T>(T entity) where T : _Base
    {
        ConfigureDbContextMock<T>();
        var repository = new Repository<T>(_contextMock.Object, _httpContextAccessorMock.Object,
            _auditColumnTransformerMock.Object);

        var exception = ThrowsAsync<Exception>(async () => await repository.GetByRowGuidAsync(null));
        That(exception, Is.Not.Null);
        That(exception?.Message, Is.EqualTo(ExceptionMessageRowGuidNull));
    }

    #endregion

    #region TestUpdateVendors

    [TestCaseSource(typeof(Vendors), nameof(Vendors.GetAll))]
    public async Task ShouldUpdateAVendor<T>(T entity) where T : _Base
    {
        ConfigureDbContextMock<T>();
        var previousDateTime = entity.CreatedAtUtc;

        var repository = new Repository<T>(_contextMock.Object, _httpContextAccessorMock.Object,
            _auditColumnTransformerMock.Object);

        await repository.AddAsync(entity);
        var vendor = await repository.GetByIdAsync(entity.Id);
        That(vendor, Is.Not.Null);

        vendor!.CreatedAtUtc = DateTime.Now;
        await repository.UpdateAsync(vendor);

        _contextMock.Verify(x => x.Set<T>().Update(It.IsAny<T>()), Times.Once);
        _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
        Multiple(() =>
        {
            That(vendor.CreatedAtUtc, Is.Not.EqualTo(previousDateTime));
            That(vendor.UpdatedAtUtc, Is.Not.Null);
        });
    }

    [TestCaseSource(typeof(Vendors), nameof(Vendors.GetAll))]
    public void ShouldThrowExceptionTryToUpdateNullEntity<T>(T entity) where T : _Base
    {
        ConfigureDbContextMock<T>();
        var repository = new Repository<T>(_contextMock.Object, _httpContextAccessorMock.Object,
            _auditColumnTransformerMock.Object);

        var exception = ThrowsAsync<Exception>(async () => await repository.UpdateAsync(null));
        That(exception, Is.Not.Null);
        That(exception?.Message, Is.EqualTo(ExceptionMessageEntityNull));
    }

    #endregion

    #region TestDeleteVendors

    [TestCaseSource(typeof(Vendors), nameof(Vendors.GetAll))]
    public async Task ShouldDeleteAVendorByIdAsync<T>(T entity) where T : _Base
    {
        ConfigureDbContextMock<T>();
        var repository = new Repository<T>(_contextMock.Object, _httpContextAccessorMock.Object,
            _auditColumnTransformerMock.Object);

        var vendor = await repository.GetByIdAsync(new Random().Next(1, 5));
        Multiple(() =>
        {
            That(vendor, Is.Not.Null);
            That(vendor?.IsNotDeleted, Is.True);
        });

        await repository.DeleteByIdAsync(vendor?.Id);

        _contextMock.Verify(x =>
            x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        That(vendor?.IsNotDeleted, Is.False);

        vendor!.IsNotDeleted = true;
        await repository.UpdateAsync(vendor);
    }

    [TestCaseSource(typeof(Vendors), nameof(Vendors.GetAll))]
    public async Task ShouldDeleteAVendorByRowGuidAsync<T>(T entity) where T : _Base
    {
        ConfigureDbContextMock<T>();
        var repository = new Repository<T>(_contextMock.Object, _httpContextAccessorMock.Object,
            _auditColumnTransformerMock.Object);

        var vendor = await repository.GetByIdAsync(new Random().Next(1, 5));
        Multiple(() =>
        {
            That(vendor, Is.Not.Null);
            That(vendor?.IsNotDeleted, Is.True);
        });

        await repository.DeleteByRowGuidAsync(vendor?.RowGuid);

        _contextMock.Verify(x =>
            x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        That(vendor?.IsNotDeleted, Is.False);

        vendor!.IsNotDeleted = true;
        await repository.UpdateAsync(vendor);
    }

    [TestCaseSource(typeof(Vendors), nameof(Vendors.GetAll))]
    public void ShouldThrowExceptionTryToDeleteWithNullId<T>(T entity) where T : _Base
    {
        ConfigureDbContextMock<T>();
        var repository = new Repository<T>(_contextMock.Object, _httpContextAccessorMock.Object,
            _auditColumnTransformerMock.Object);

        var exception = ThrowsAsync<Exception>(async () => await repository.DeleteByIdAsync(null));
        That(exception, Is.Not.Null);
        That(exception?.Message, Is.EqualTo(ExceptionMessageIdNull));
    }

    [TestCaseSource(typeof(Vendors), nameof(Vendors.GetAll))]
    public void ShouldThrowExceptionTryToDeleteWithNullRowGuid<T>(T entity) where T : _Base
    {
        ConfigureDbContextMock<T>();
        var repository = new Repository<T>(_contextMock.Object, _httpContextAccessorMock.Object,
            _auditColumnTransformerMock.Object);

        var exception = ThrowsAsync<Exception>(async () => await repository.DeleteByRowGuidAsync(null));
        That(exception, Is.Not.Null);
        That(exception?.Message, Is.EqualTo(ExceptionMessageRowGuidNull));
    }

    #endregion

    private void ConfigureDbContextMock<T>() where T : _Base
    {
        var list = ItemList.GetList<T>(typeof(T)).BuildMockDbSet();
        list.Setup(x => x.Add(It.IsAny<T>()))
            .Callback<T>(x => ItemList.GetList<T>(typeof(T))?.Add(x));
        _contextMock.Setup(x => x.Set<T>()).Returns(list.Object);
    }
}
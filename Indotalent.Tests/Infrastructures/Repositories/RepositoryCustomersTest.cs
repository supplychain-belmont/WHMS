using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Indotalent.Data;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Models.Contracts;
using Indotalent.Tests.Utils;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using MockQueryable.Moq;

using Moq;

using NUnit.Framework;

using static NUnit.Framework.Assert;

namespace Indotalent.Tests.Infrastructures.Repositories
{
    [TestFixture]
    public class RepositoryCustomersTest
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
            _auditColumnTransformerMock.Setup(x => x.TransformAsync(It.IsAny<_Base>(), It.IsAny<ApplicationDbContext>()))
                .Returns(Task.CompletedTask);
        }

        #endregion

        #region TestAllTheCustomers

        [TestCaseSource(typeof(Customers), nameof(Customers.GetAll))]
        public void ShouldCountAllCustomersDbSet<T>(T entity) where T : _Base
        {
            ConfigureDbContextMock<T>();
            var repository = new Repository<T>(_contextMock.Object, _httpContextAccessorMock.Object,
                _auditColumnTransformerMock.Object);

            That(repository, Is.Not.Null);
            That(repository.GetAll().Count(), Is.Not.EqualTo(0));
        }

        [TestCaseSource(typeof(Customers), nameof(Customers.GetAll))]
        public void ShouldCountAllArchivedCustomersDbSet<T>(T entity) where T : _Base
        {
            ConfigureDbContextMock<T>();
            var repository = new Repository<T>(_contextMock.Object, _httpContextAccessorMock.Object,
                _auditColumnTransformerMock.Object);

            That(repository, Is.Not.Null);
            That(repository.GetAllArchive().Count(), Is.EqualTo(0));
        }

        #endregion

        #region TestCreateCustomers

        [TestCaseSource(typeof(Customers), nameof(Customers.GetAll))]
        public async Task ShouldAddCustomerByTable<T>(T entity) where T : _Base
        {
            ConfigureDbContextMock<T>();
            var repository = new Repository<T>(_contextMock.Object, _httpContextAccessorMock.Object, _auditColumnTransformerMock.Object);

            await repository.AddAsync(entity);

            _contextMock.Verify(x => x.Set<T>().Add(It.IsAny<T>()), Times.Once);
            _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            That(repository.GetAll().Count(), Is.GreaterThan(2));
        }

        [TestCaseSource(typeof(Customers), nameof(Customers.GetAll))]
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

        #region TestByIdCustomers

        [TestCaseSource(typeof(Customers), nameof(Customers.GetAll))]
        public async Task ShouldReturnACustomerByIdAsync<T>(T entity) where T : _Base
        {
            ConfigureDbContextMock<T>();
            var repository = new Repository<T>(_contextMock.Object, _httpContextAccessorMock.Object, _auditColumnTransformerMock.Object);

            await repository.AddAsync(entity);
            var customer = await repository.GetByIdAsync(entity.Id);

            _contextMock.Verify(x => x.Set<T>().Add(It.IsAny<T>()), Times.Once);
            _auditColumnTransformerMock.Verify(x => x.TransformAsync(It.IsAny<T>(), It.IsAny<ApplicationDbContext>()), Times.Once);
            That(customer, Is.Not.Null);
        }
        [TestCaseSource(typeof(Customers), nameof(Customers.GetAll))]
        public async Task ShouldReturnACustomerByRowGuidAsync<T>(T entity) where T : _Base
        {
            ConfigureDbContextMock<T>();
            var repository = new Repository<T>(_contextMock.Object, _httpContextAccessorMock.Object,
                _auditColumnTransformerMock.Object);

            await repository.AddAsync(entity);
            var customer = await repository.GetByRowGuidAsync(entity.RowGuid);

            _contextMock.Verify(x => x.Set<T>().Add(It.IsAny<T>()), Times.Once);
            _auditColumnTransformerMock.Verify(x => x.TransformAsync(It.IsAny<T>(), It.IsAny<ApplicationDbContext>()),
                Times.Once);
            That(customer, Is.Not.Null);
        }

        [TestCaseSource(typeof(Customers), nameof(Customers.GetAll))]
        public void ShouldThrowExceptionWhenIdIsNull<T>(T entity) where T : _Base
        {
            ConfigureDbContextMock<T>();
            var repository = new Repository<T>(_contextMock.Object, _httpContextAccessorMock.Object,
                _auditColumnTransformerMock.Object);

            var exception = ThrowsAsync<Exception>(async () => await repository.GetByIdAsync(null));
            That(exception, Is.Not.Null);
            That(exception?.Message, Is.EqualTo(ExceptionMessageIdNull));
        }

        [TestCaseSource(typeof(Customers), nameof(Customers.GetAll))]
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

        #region TestUpdateCustomers
        [TestCaseSource(typeof(Customers), nameof(Customers.GetAll))]
        public async Task ShouldUpdateACustomer<T>(T entity) where T : _Base
        {
            ConfigureDbContextMock<T>();
            var previousDateTime = entity.CreatedAtUtc;

            var repository = new Repository<T>(_contextMock.Object, _httpContextAccessorMock.Object, _auditColumnTransformerMock.Object);

            await repository.AddAsync(entity);
            var customer = await repository.GetByIdAsync(entity.Id);
            That(customer, Is.Not.Null);

            customer!.CreatedAtUtc = DateTime.Now;
            await repository.UpdateAsync(customer);

            _contextMock.Verify(x => x.Set<T>().Update(It.IsAny<T>()), Times.Once);
            _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
            Multiple(() =>
            {
                That(customer.CreatedAtUtc, Is.Not.EqualTo(previousDateTime));
                That(customer.UpdatedAtUtc, Is.Not.Null);
            });
        }

        [TestCaseSource(typeof(Customers), nameof(Customers.GetAll))]
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

        #region TestRemoveCustomers

        [TestCaseSource(typeof(Customers), nameof(Customers.GetAll))]
        public void ShouldThrowExceptionTryToRemoveNullEntity<T>(T entity) where T : _Base
        {
            ConfigureDbContextMock<T>();
            var repository = new Repository<T>(_contextMock.Object, _httpContextAccessorMock.Object, _auditColumnTransformerMock.Object);

            var exception = ThrowsAsync<Exception>(async () => await repository.DeleteByIdAsync(null));
            That(exception, Is.Not.Null);
            That(exception?.Message, Is.EqualTo(ExceptionMessageIdNull));
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
}

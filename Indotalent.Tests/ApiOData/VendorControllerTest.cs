using System.Linq.Expressions;

using AutoMapper;

using Indotalent.ApiOData;
using Indotalent.Applications.NumberSequences;
using Indotalent.Applications.Vendors;
using Indotalent.Data;
using Indotalent.DTOs;
using Indotalent.Infrastructures.Mapper;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Models.Contracts;
using Indotalent.Models.Entities;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.EntityFrameworkCore;

using MockQueryable.Moq;

using Moq;

namespace Indotalent.Tests.ApiOData;

[TestFixture]
[TestOf(typeof(VendorController))]
public class VendorControllerTest
{
    private MapperConfiguration _config;
    private IMapper _mapper;
    private Mock<VendorService> _vendorService;
    private Mock<ApplicationDbContext> _contextMock;
    private Mock<IHttpContextAccessor> _httpContextAccessorMock;
    private Mock<IAuditColumnTransformer> _auditColumnTransformerMock;
    private Mock<NumberSequenceService> _numberService;
    private VendorController _controller;

    private readonly List<Vendor> vendors =
    [
        new Vendor
        {
            Id = 1,
            Name = "Vendor 1",
            Number = "VND0001",
            VendorGroupId = 1,
            VendorCategoryId = 1,
            VendorGroup = new VendorGroup { Id = 1, Name = "Group 1" },
            VendorCategory = new VendorCategory { Id = 1, Name = "Category 1" },
            RowGuid = Guid.NewGuid()
        },

        new Vendor
        {
            Id = 2,
            Name = "Vendor 2",
            Number = "VND0002",
            VendorGroupId = 2,
            VendorCategoryId = 2,
            VendorGroup = new VendorGroup { Id = 2, Name = "Group 2" },
            VendorCategory = new VendorCategory { Id = 2, Name = "Category 2" },
            RowGuid = Guid.Empty
        }
    ];

    [SetUp]
    public void Setup()
    {
        _config = new MapperConfiguration(cfg
            => cfg.AddProfile<ApplicationProfile>());
        _mapper = _config.CreateMapper();

        var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Indotalent")
            .Options;
        _contextMock = new Mock<ApplicationDbContext>(contextOptions);
        _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        _auditColumnTransformerMock = new Mock<IAuditColumnTransformer>();

        _numberService = new Mock<NumberSequenceService>(_contextMock.Object, _httpContextAccessorMock.Object,
            _auditColumnTransformerMock.Object);

        var vendorsMock = vendors.AsQueryable().BuildMock();
        _vendorService = new Mock<VendorService>(_contextMock.Object, _httpContextAccessorMock.Object,
            _auditColumnTransformerMock.Object, _numberService.Object);
        _vendorService.Setup(x => x.GetAll()).Returns(vendorsMock);
        _vendorService.Setup(x => x.GetByRowGuidAsync(It.IsAny<Guid>(),
                It.IsAny<Expression<Func<Vendor, _Base?>>[]>()
            ))
            .Returns(vendorsMock.Where(x => x.RowGuid == Guid.Empty));
        _vendorService.Setup(x => x.GetByRowGuidAsync(It.IsAny<Guid>()))
            .ReturnsAsync(vendors[1]);
        _vendorService.Setup(x => x.UpdateAsync(It.IsAny<Vendor>()))
            .Returns(() =>
            {
                vendors[1].Name = "Vendor 2 updated";
                vendors[1].VendorGroupId = 4;
                vendors[1].VendorCategoryId = 4;
                return Task.FromResult(0);
            });

        _controller = new VendorController(_vendorService.Object, _mapper);
    }

    [Test]
    public async Task ShouldReturnAllVendorsAsADto()
    {
        var result = _controller.Get();
        Assert.That(result, Is.Not.Null);
        Assert.That(await result.CountAsync(), Is.GreaterThan(0));

        var vendorDtoList = await result.ToListAsync();
        for (var i = 0; i < vendorDtoList.Count; i++)
        {
            Assert.Multiple(() =>
            {
                Assert.That(vendorDtoList[i].Id, Is.EqualTo(vendors[i].Id));
                Assert.That(vendorDtoList[i].Name, Is.EqualTo(vendors[i].Name));
                Assert.That(vendorDtoList[i].Number, Is.EqualTo(vendors[i].Number));
                Assert.That(vendorDtoList[i].VendorGroupId, Is.EqualTo(vendors[i].VendorGroupId));
                Assert.That(vendorDtoList[i].VendorCategoryId, Is.EqualTo(vendors[i].VendorCategoryId));
                Assert.That(vendorDtoList[i].VendorGroup, Is.EqualTo(vendors[i].VendorGroup!.Name));
                Assert.That(vendorDtoList[i].VendorCategory, Is.EqualTo(vendors[i].VendorCategory!.Name));
            });
        }
    }


    [Test]
    public async Task ShouldReturnAMappedVendorByGuid()
    {
        var vendor = await _controller.Get(Guid.Empty);
        Assert.That(vendor, Is.Not.Null);
        Assert.That(vendor.Result, Is.InstanceOf<OkObjectResult>());

        var okObjectResult = vendor.Result as OkObjectResult;
        Assert.That(okObjectResult?.Value, Is.Not.Null);
        Assert.That(okObjectResult?.Value, Is.InstanceOf<VendorDto>());

        VendorDto vendorDto = (VendorDto)okObjectResult.Value;
        Assert.That(vendorDto, Is.InstanceOf<VendorDto>());
        Assert.That(vendorDto.Id, Is.EqualTo(vendors[1].Id));
        Assert.That(vendorDto.Name, Is.EqualTo(vendors[1].Name));
        Assert.That(vendorDto.Number, Is.EqualTo(vendors[1].Number));
        Assert.That(vendorDto.VendorGroupId, Is.EqualTo(vendors[1].VendorGroupId));
        Assert.That(vendorDto.VendorCategoryId, Is.EqualTo(vendors[1].VendorCategoryId));
        Assert.That(vendorDto.VendorGroup, Is.EqualTo(vendors[1].VendorGroup!.Name));
        Assert.That(vendorDto.VendorCategory, Is.EqualTo(vendors[1].VendorCategory!.Name));
    }


    [Test]
    public async Task ShouldCreateAVendorAndReturnCreatedStatus()
    {
        var vendorDto = new VendorDto
        {
            Id = 3,
            Name = "Vendor 3",
            Number = "VND0003",
            VendorGroupId = 3,
            VendorCategoryId = 3,
        };
        var result = await _controller.Post(vendorDto);
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.InstanceOf<CreatedResult>());
    }


    [Test]
    public async Task ShouldUpdateAVendorAndReturnNoContentStatus()
    {
        var vendorToUpdate = new VendorDto
        {
            Id = 2,
            Name = "Vendor 2 updated",
            Number = "VND0002",
            VendorGroupId = 4,
            VendorCategoryId = 4,
            RowGuid = Guid.Empty
        };

        var result = await _controller.Put(Guid.Empty, vendorToUpdate);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.InstanceOf<NoContentResult>());
        Assert.That(vendors[1].Name, Is.EqualTo(vendorToUpdate.Name));
        Assert.That(vendors[1].VendorGroupId, Is.EqualTo(vendorToUpdate.VendorGroupId));
        Assert.That(vendors[1].VendorCategoryId, Is.EqualTo(vendorToUpdate.VendorCategoryId));
    }


    [Test]
    public async Task ShouldThrowAnExceptionWhenNumberIsTriedToUpdate()
    {
        var vendorToUpdate = new VendorDto
        {
            Id = 2,
            Name = "Vendor 2 updated",
            Number = "VND0011",
            VendorGroupId = 4,
            VendorCategoryId = 4,
            RowGuid = Guid.Empty
        };

        var result = await _controller.Put(Guid.Empty, vendorToUpdate);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.InstanceOf<BadRequestODataResult>());

        var error = result.Result as BadRequestODataResult;
        Assert.That(error?.Error.Message, Is.EqualTo("Unable to update vendor"));
        Assert.That(error?.StatusCode, Is.EqualTo(400));
    }

    [Test]
    public async Task ShouldPatchAVendorAndReturnNoContentStatus()
    {
        var vendorToUpdate = new VendorDto { Name = "Vendor 2 updated", VendorGroupId = 4, VendorCategoryId = 4, };

        var delta = new Delta<VendorDto>(typeof(VendorDto));
        delta.TrySetPropertyValue("Name", vendorToUpdate.Name);
        delta.TrySetPropertyValue("VendorGroupId", vendorToUpdate.VendorGroupId);
        delta.TrySetPropertyValue("VendorCategoryId", vendorToUpdate.VendorCategoryId);
        var result = await _controller.Patch(Guid.Empty, delta);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.InstanceOf<UpdatedODataResult<VendorDto>>());
        Assert.That(vendors[1].Name, Is.EqualTo(vendorToUpdate.Name));
        Assert.That(vendors[1].VendorGroupId, Is.EqualTo(vendorToUpdate.VendorGroupId));
        Assert.That(vendors[1].VendorCategoryId, Is.EqualTo(vendorToUpdate.VendorCategoryId));
    }


    [Test]
    public async Task ShouldThrowAnExceptionWhenNumberIsTriedToPatch()
    {
        var vendorToUpdate = new VendorDto
        {
            Name = "Vendor 2 updated",
            Number = "VND0011",
            VendorGroupId = 4,
            VendorCategoryId = 4,
        };

        var delta = new Delta<VendorDto>(typeof(VendorDto));
        delta.TrySetPropertyValue("Name", vendorToUpdate.Name);
        delta.TrySetPropertyValue("VendorGroupId", vendorToUpdate.VendorGroupId);
        delta.TrySetPropertyValue("VendorCategoryId", vendorToUpdate.VendorCategoryId);
        delta.TrySetPropertyValue("Number", vendorToUpdate.Number);
        var result = await _controller.Patch(Guid.Empty, delta);


        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.InstanceOf<BadRequestODataResult>());

        var error = result.Result as BadRequestODataResult;
        Assert.That(error?.Error.Message, Is.EqualTo("Unable to update vendor"));
        Assert.That(error?.StatusCode, Is.EqualTo(400));
    }


    [Test]
    public async Task ShouldDeleteAVendorAndReturnNoContent()
    {
        var result = await _controller.Delete(Guid.Empty);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }
}

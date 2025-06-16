using garbageDetetionApi.Context;
using garbageDetetionApi.Controllers;
using garbageDetetionApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace UnitTests;

public class GarbageControllerTests
{
    private GarbageDbContext GetEmptyDbContext()
    {
        var options = new DbContextOptionsBuilder<GarbageDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new GarbageDbContext(options);
    }

    private GarbageDbContext GetDbContextWithData()
    {
        var context = GetEmptyDbContext();
        context.Garbages.AddRange(
            new Garbage { Id = 1, DetectedObject = "Plastic", TimeStamp = DateTime.UtcNow.AddMinutes(-10) },
            new Garbage { Id = 2, DetectedObject = "Glass", TimeStamp = DateTime.UtcNow }
        );
        context.SaveChanges();
        return context;
    }

    [Test]
    public async Task GetAll_ReturnsNotFound_WhenNoData()
    {
        var context = GetEmptyDbContext();
        var configMock = new Mock<IConfiguration>();
        var controller = new GarbageController(context, configMock.Object);

        var result = await controller.GetAll();

        Assert.That(result.Result, Is.TypeOf<NotFoundObjectResult>());
    }

    [Test]
    public async Task GetByTimeToNow_ReturnsOk_WhenDataExists()
    {
        var context = GetDbContextWithData();
        var configMock = new Mock<IConfiguration>();
        var controller = new GarbageController(context, configMock.Object);

        var time = DateTime.UtcNow.AddMinutes(-15);
        var result = await controller.GetByTimeToNow(time);

        Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That((okResult.Value as IEnumerable<Garbage>).Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task GetByTimeToNow_ReturnsNotFound_WhenNoData()
    {
        var context = GetDbContextWithData();
        var configMock = new Mock<IConfiguration>();
        var controller = new GarbageController(context, configMock.Object);

        var time = DateTime.UtcNow.AddHours(1);
        var result = await controller.GetByTimeToNow(time);

        Assert.That(result.Result, Is.TypeOf<NotFoundObjectResult>());
    }

    [Test]
    public async Task GetByAmount_ReturnsBadRequest_WhenAmountIsZero()
    {
        var context = GetDbContextWithData();
        var configMock = new Mock<IConfiguration>();
        var controller = new GarbageController(context, configMock.Object);

        var result = await controller.GetByAmount(0);

        Assert.That(result.Result, Is.TypeOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task GetByAmount_ReturnsOk_WithCorrectCount()
    {
        var context = GetDbContextWithData();
        var configMock = new Mock<IConfiguration>();
        var controller = new GarbageController(context, configMock.Object);

        var result = await controller.GetByAmount(1);

        Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That((okResult.Value as IEnumerable<Garbage>).Count(), Is.EqualTo(1));
    }

    [Test]
    public async Task GetById_ReturnsOk_WhenIdExists()
    {
        var context = GetDbContextWithData();
        var configMock = new Mock<IConfiguration>();
        var controller = new GarbageController(context, configMock.Object);

        var result = await controller.GetById(1);

        Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That((okResult.Value as IEnumerable<Garbage>).Any(g => g.Id == 1), Is.True);
    }

    [Test]
    public async Task GetById_ReturnsNotFound_WhenIdDoesNotExist()
    {
        var context = GetDbContextWithData();
        var configMock = new Mock<IConfiguration>();
        var controller = new GarbageController(context, configMock.Object);

        var result = await controller.GetById(99);

        Assert.That(result.Result, Is.TypeOf<NotFoundObjectResult>());
    }
}
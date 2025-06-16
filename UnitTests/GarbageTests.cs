using garbageDetetionApi.Context;
using garbageDetetionApi.Controllers;
using garbageDetetionApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;


namespace UnitTests;

public class Tests
{
    private GarbageDbContext GetDbContextWithData()
    {
        var options = new DbContextOptionsBuilder<GarbageDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var context = new GarbageDbContext(options);
        context.Garbages.Add(new Garbage { Id = 1, DetectedObject = "Plastic", TimeStamp = DateTime.UtcNow });
        context.SaveChanges();
        return context;
    }

    [Test]
    public async Task GetAll_ReturnsOk_WhenDataExists()
    {
        var context = GetDbContextWithData();
        var configMock = new Mock<IConfiguration>();
        var controller = new GarbageController(context, configMock.Object);

        var result = await controller.GetAll();

        Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.Value, Is.AssignableTo<IEnumerable<Garbage>>());
        var garbages = okResult.Value as IEnumerable<Garbage>;
        Assert.That(garbages.Count(), Is.EqualTo(1));
    }
}
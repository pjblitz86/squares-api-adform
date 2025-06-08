using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using squares_api_adform.Controllers;
using squares_api_adform.Data;
using squares_api_adform.Models;

namespace SquaresApi.Tests
{
    public class PointsControllerTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task GetPoints_ReturnsAllPoints()
        {
            var context = GetInMemoryDbContext();
            context.Points.AddRange(
                new Point { X = 1, Y = 1 },
                new Point { X = 2, Y = 2 }
            );
            await context.SaveChangesAsync();

            var controller = new PointsController(context);
            var result = await controller.GetPoints();

            var points = Assert.IsAssignableFrom<IEnumerable<Point>>(result.Value);
            Assert.Equal(2, points.Count());
        }

        [Fact]
        public async Task AddPoint_CreatesPoint()
        {
            var context = GetInMemoryDbContext();
            var controller = new PointsController(context);

            var newPoint = new Point { X = 5, Y = 6 };
            var result = await controller.AddPoint(newPoint);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var point = Assert.IsType<Point>(createdResult.Value);
            Assert.Equal(5, point.X);
            Assert.Equal(6, point.Y);
        }

        [Fact]
        public async Task DeletePoint_RemovesPoint_WhenExists()
        {
            var context = GetInMemoryDbContext();
            var point = new Point { X = 3, Y = 4 };
            context.Points.Add(point);
            await context.SaveChangesAsync();

            var controller = new PointsController(context);
            var result = await controller.DeletePoint(3, 4);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeletePoint_ReturnsNotFound_WhenPointDoesNotExist()
        {
            var context = GetInMemoryDbContext();
            var controller = new PointsController(context);

            var result = await controller.DeletePoint(999, 999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task ImportPoints_AddsOnlyNewPoints()
        {
            var context = GetInMemoryDbContext();
            context.Points.Add(new Point { X = 1, Y = 1 });
            await context.SaveChangesAsync();

            var controller = new PointsController(context);
            var result = await controller.ImportPoints(new List<Point>
            {
                new Point { X = 1, Y = 1 },
                new Point { X = 2, Y = 2 }
            });

            var okResult = Assert.IsType<OkObjectResult>(result);
            var insertedProp = okResult.Value?.GetType().GetProperty("Inserted");
            Assert.NotNull(insertedProp);

            var insertedValue = insertedProp!.GetValue(okResult.Value);
            Assert.Equal(1, Convert.ToInt32(insertedValue));
        }
    }
}
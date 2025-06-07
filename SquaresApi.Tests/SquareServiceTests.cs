using Xunit;
using squares_api_adform.Models;
using squares_api_adform.Services;

namespace SquaresApi.Tests
{
    public class SquareServiceTests
    {
        [Fact]
        public void FindSquares_FindsOneSquare_WhenFourSquarePoints()
        {
            var service = new SquareService();
            var points = new List<Point>
            {
                new Point { X = -1, Y = -1 },
                new Point { X = -1, Y = 1 },
                new Point { X = 1, Y = -1 },
                new Point { X = 1, Y = 1 }
            };

            var squares = service.FindSquares(points);

            Assert.Single(squares);
        }

        [Fact]
        public void FindSquares_ReturnsEmpty_WhenNotEnoughPoints()
        {
            var service = new SquareService();
            var points = new List<Point>
            {
                new Point { X = 0, Y = 0 },
                new Point { X = 1, Y = 1 }
            };

            var squares = service.FindSquares(points);

            Assert.Empty(squares);
        }

        [Fact]
        public void FindSquares_DetectsTwoDistinctSquares()
        {
            var service = new SquareService();
            var points = new List<Point>
            {
                new Point { X = 0, Y = 0 },
                new Point { X = 0, Y = 2 },
                new Point { X = 2, Y = 0 },
                new Point { X = 2, Y = 2 },

                new Point { X = 1, Y = 1 },
                new Point { X = 1, Y = 3 },
                new Point { X = 3, Y = 1 },
                new Point { X = 3, Y = 3 }
            };

            var squares = service.FindSquares(points);

            Assert.Equal(4, squares.Count);
        }
    }
}

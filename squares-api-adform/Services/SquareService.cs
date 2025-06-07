using squares_api_adform.Models;

namespace squares_api_adform.Services
{
    public class SquareService
    {
        public List<List<Point>> FindSquares(List<Point> points)
        {
            var pointSet = points.ToHashSet(); // for fast contains lookup
            var squares = new List<List<Point>>();

            // Loop through every unique pair of points. Each pair is treated as a diagonal of a possible square.
            for (int i = 0; i < points.Count; i++)
            {
                for (int j = i + 1; j < points.Count; j++)
                {
                    var p1 = points[i];
                    var p2 = points[j];

                    // midpoint of the diagonal
                    double midX = (p1.X + p2.X) / 2.0;
                    double midY = (p1.Y + p2.Y) / 2.0;

                    // half-diagonal vector between the two points
                    double dx = (p1.X - p2.X) / 2.0;
                    double dy = (p1.Y - p2.Y) / 2.0;

                    // Rotate the half-diagonal 90° to find the other 2 corners of the square (p3, p4).
                    var p3 = new Point { X = (int)(midX - dy), Y = (int)(midY + dx) };
                    var p4 = new Point { X = (int)(midX + dy), Y = (int)(midY - dx) };

                    // Only accept the square if all 4 points exist in the database
                    if (pointSet.Contains(p3) && pointSet.Contains(p4))
                    {
                        var square = new List<Point> { p1, p2, p3, p4 }
                            .OrderBy(p => p.X).ThenBy(p => p.Y).ToList();

                        // Prevent duplicates by checking if this square already exists
                        if (!squares.Any(s =>
                            s.OrderBy(p => p.X).ThenBy(p => p.Y)
                             .SequenceEqual(square)))
                        {
                            squares.Add(square);
                        }
                    }
                }
            }

            return squares;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace squares_api_adform.Models

{
    [Index(nameof(X), nameof(Y), IsUnique = true)]
    public class Point : IEquatable<Point>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int X { get; set; }

        [Required]
        public int Y { get; set; }
        
        // Custom equality logic to allow value-based comparisons
        public bool Equals(Point? other)
        {
            return other is not null && X == other.X && Y == other.Y;
        }

        public override bool Equals(object? obj) => Equals(obj as Point);
        public override int GetHashCode() => HashCode.Combine(X, Y);
    }
}

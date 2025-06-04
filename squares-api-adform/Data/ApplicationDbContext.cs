using Microsoft.EntityFrameworkCore;
using squares_api_adform.Models;

namespace squares_api_adform.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Point> Points { get; set; }
    }
}

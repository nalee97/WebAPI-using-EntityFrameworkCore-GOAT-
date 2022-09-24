using Microsoft.EntityFrameworkCore;

namespace GOAT.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Goats> Goats { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {
        }

        public DbSet<DebetCard> DebetCards { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

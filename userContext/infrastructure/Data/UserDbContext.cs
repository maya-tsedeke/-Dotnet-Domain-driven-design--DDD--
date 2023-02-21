using Microsoft.EntityFrameworkCore;
using userContext.domain.entities;

namespace infrastructure.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }
        }
        public DbSet<User> Users { get; set; }
    }
}



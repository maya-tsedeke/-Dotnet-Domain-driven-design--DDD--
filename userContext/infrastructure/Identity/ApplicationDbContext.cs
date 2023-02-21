using infrastructure.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Identity
{
   
        public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {
            }

            protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder);

                // Add any customizations to the Identity model here
            }
        }
    
}

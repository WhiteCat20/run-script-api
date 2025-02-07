using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace run_script.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var guestId = "d44dc1fc-6c29-43b0-9b2f-0a7402bc64d4";
            var adminId = "f503a0cb-b385-400b-8f26-fe17ed706f0f";

            var roles = new List<IdentityRole> { 
                new IdentityRole { 
                    Id = guestId,
                    ConcurrencyStamp = guestId,
                    Name = "Guest",
                    NormalizedName = "Guest".ToUpper()
                },
                new IdentityRole {
                    Id = adminId,
                    ConcurrencyStamp = adminId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}

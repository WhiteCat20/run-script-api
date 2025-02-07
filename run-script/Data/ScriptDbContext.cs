using Microsoft.AspNetCore.Identity;
using System.Data;
using Microsoft.EntityFrameworkCore;
using run_script.Models;

namespace run_script.Data
{
    public class ScriptDbContext : DbContext
    {
        public ScriptDbContext(DbContextOptions<ScriptDbContext> options) : base(options)
        {
            
        }

        public DbSet<Script> Scripts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            var scripts = new List<Script>() {
                new Script()
                {
                    Id = 1,
                    Name="Hello World",
                    ScriptContent = "echo \"Hello\" ",
                    TimesAccessed = 0,
                },
                new Script()
                {
                    Id= 2,
                    Name="faiz",
                    ScriptContent = "docker start nginxfaiz",
                    TimesAccessed = 0,
                }
            };

            modelBuilder.Entity<Script>().HasData(scripts);

        }
    }
}

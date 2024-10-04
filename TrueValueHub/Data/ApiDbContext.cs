using Microsoft.EntityFrameworkCore;
using TrueValueHub.Models;

namespace TrueValueHub.Data
{
    public class ApiDbContext:DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Manufacturing> Manufacturings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Part>()
                .HasMany(p => p.Manufacturings)
                .WithOne(m => m.Part)
                .HasForeignKey(m => m.PartId);
        }

    }
}

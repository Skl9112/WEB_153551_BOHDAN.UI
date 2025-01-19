using WEB_153551_BOHDAN.UI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace WEB_153551_BOHDAN.API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Category> Categories { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Dish>()
                .HasOne(d => d.Category)
                .WithMany(c => c.Dishes)
                .HasForeignKey(d => d.CategoryId);
        }
    }
}

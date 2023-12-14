using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ASM1670.Models;
using Microsoft.EntityFrameworkCore;

namespace ASM1670.Data
{
    public class ApplicationDBContext : IdentityDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<ApplicationUser> applicationUsers {  get; set; } 
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 4, Name = "Science", Description = "So difficult", DisplayOrder = 4 }
            );
        }
    }
}

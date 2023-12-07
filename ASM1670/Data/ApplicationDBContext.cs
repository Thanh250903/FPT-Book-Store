using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ASM1670.Models;
using Microsoft.EntityFrameworkCore;

namespace ASM1670.Data
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}

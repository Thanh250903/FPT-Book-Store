using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ASM1670.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ASM1670.Data
{
    public class ApplicationDBContext : IdentityDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderShip> OrderShips { get; set; }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
            //Anh em vui lòng đọc hướng dẫn tạo account Admin
            //create a new account
            //sau đó vào SQL tạo querry mới rồi gõ DELETE FORM UserRoles where User = '..' sau đó chạy
            //tiếp theo gõ INSERT INTO UserRoles Values('cái id của user','cái id của role Admin') sau đó chạy 
            //rồi logout rồi login lại là xong

        }
    }
}

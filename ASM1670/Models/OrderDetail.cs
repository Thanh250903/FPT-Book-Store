using System.ComponentModel.DataAnnotations;

namespace ASM1670.Models
{
    public class OrderDetail
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public virtual ICollection<Book>? Books { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }
}

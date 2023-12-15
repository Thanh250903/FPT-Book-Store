using System.ComponentModel.DataAnnotations;

namespace ASM1670.Models
{
    public class OrderShip
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required, MaxLength(10)]
        public int PhoneNumber { get; set; }
        public List<OrderDetail>? OrderDetails { get; set; }
    }
}

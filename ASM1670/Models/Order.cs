using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ASM1670.Models
{
    public class Order
    {
        [Required]
        public int Id { get; set; }
        [ValidateNever]
        public Book? Book { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required, MaxLength(10)]
        public int PhoneNumber { get; set; }
        [Required]
        public string? Mail { get; set; }
        public List<OrderDetail>? OrderDetails { get; set; }
    }
}

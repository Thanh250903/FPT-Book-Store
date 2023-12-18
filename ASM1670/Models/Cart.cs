using Microsoft.AspNetCore.Identity;

namespace ASM1670.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public Book book { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public DateTime AddedTime { get; set; }
        public string AddedByUserEmail { get; set; }

    }
}

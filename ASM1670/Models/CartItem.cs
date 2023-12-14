using System;

namespace ASM1670.Models
{
    [Serializable]
    public class CartItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }
        public Book book { get; set; }
    }
}

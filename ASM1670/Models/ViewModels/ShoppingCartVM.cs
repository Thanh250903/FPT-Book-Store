using ASM1670.Models;

namespace ASM1670.Models.ViewModels;

public class ShoppingCartVM
{
    public IEnumerable<Cart> ListCarts { get; set; }
    public Order Order { get; set; }
}
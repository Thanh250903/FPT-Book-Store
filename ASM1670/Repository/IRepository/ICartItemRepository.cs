using ASM1670.Models;

namespace ASM1670.Repository.IRepository
{
    public interface ICartItemRepository : IRepository<CartItem>
    {
        void Update(CartItem entity);
    }
}

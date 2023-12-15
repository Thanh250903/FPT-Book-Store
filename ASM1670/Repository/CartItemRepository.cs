using ASM1670.Data;
using ASM1670.Models;
using ASM1670.Repository.IRepository;
using System.Linq.Expressions;

namespace ASM1670.Repository
{
    public class CartItemRepository : Repository<CartItem>, ICartItemRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public CartItemRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }

        public void Update(CartItem entity)
        {
            _dbContext.CartItems.Update(entity);
        }




    }
}

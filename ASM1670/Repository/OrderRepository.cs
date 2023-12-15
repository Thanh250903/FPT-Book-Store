using ASM1670.Data;
using ASM1670.Models;
using ASM1670.Repository.IRepository;

namespace ASM1670.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public OrderRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }
        public void Update(Order entity)
        {
            _dbContext.Orders.Update(entity);
        }
    }
}

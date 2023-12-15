using ASM1670.Data;
using ASM1670.Models;
using ASM1670.Repository.IRepository;

namespace ASM1670.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public OrderDetailRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }
        public void Update(OrderDetail entity)
        {
            _dbContext.OrderDetails.Update(entity);
        }
    }
}

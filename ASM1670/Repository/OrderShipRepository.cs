using ASM1670.Data;
using ASM1670.Models;
using ASM1670.Repository.IRepository;

namespace ASM1670.Repository
{
    public class OrderShipRepository : Repository<OrderShip>, IOrderShipRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public OrderShipRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }
        public void Update(OrderShip entity)
        {
            _dbContext.OrderShips.Update(entity);
        }
    }
}

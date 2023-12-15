using ASM1670.Models;

namespace ASM1670.Repository.IRepository
{
    public interface IOrderShipRepository : IRepository<OrderShip>
    {
        void Update(OrderShip entity);
    }
}

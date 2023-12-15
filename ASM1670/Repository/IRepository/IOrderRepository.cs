using ASM1670.Models;

namespace ASM1670.Repository.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        void Update(Order entity);
    }
}

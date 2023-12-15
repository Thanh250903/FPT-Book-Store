using ASM1670.Models;

namespace ASM1670.Repository.IRepository
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        void Update(OrderDetail entity);
    }
}

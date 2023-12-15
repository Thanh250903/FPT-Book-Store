using ASM1670.Data;
using ASM1670.Models;
using ASM1670.Repository.IRepository;

namespace ASM1670.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _dbContext;
        public ICategoryRepository CategoryRepository { get; private set; }

        public IBookRepository BookRepository { get; private set; }
        
        public ICartItemRepository CartItemRepository { get; private set; }
        public IOrderRepository OrderRepository { get; private set; }
        public IOrderDetailRepository OrderDetailRepository { get; private set; }

        public IOrderShipRepository OrderShipRepository { get; private set; }
        public UnitOfWork(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
            CategoryRepository = new CategoryRepository(dBContext);
            BookRepository = new BookRepository(dBContext);
            CartItemRepository = new CartItemRepository(dBContext);
            OrderRepository = new OrderRepository(dBContext);
            OrderDetailRepository = new OrderDetailRepository(dBContext);
            OrderShipRepository = new OrderShipRepository(dBContext);
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}

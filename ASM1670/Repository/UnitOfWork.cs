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
        

        public UnitOfWork(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
            CategoryRepository = new CategoryRepository(dBContext);
            BookRepository = new BookRepository(dBContext);
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}

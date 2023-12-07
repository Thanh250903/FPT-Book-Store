using ASM1670.Models;

namespace ASM1670.Repository.IRepository
{
    public interface IBookRepository : IRepository<Book>
    {
        void Update(Book entity);
    }
}

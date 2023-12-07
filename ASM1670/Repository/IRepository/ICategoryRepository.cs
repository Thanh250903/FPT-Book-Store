using ASM1670.Models;

namespace ASM1670.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category entity);
    }
}

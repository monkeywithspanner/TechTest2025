using TechTest2025.Models;

namespace TechTest2025.Repositories
{
    public interface IPersonRepository
    {
        IQueryable<Person> GetAll();
    }
}

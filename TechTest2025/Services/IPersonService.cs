using TechTest2025.Models;

namespace TechTest2025.Services
{
    public interface IPersonService
    {
        IQueryable<Person> GetAllPersons();
    }
}

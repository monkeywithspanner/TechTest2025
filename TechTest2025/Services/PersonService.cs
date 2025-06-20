using TechTest2025.Models;
using TechTest2025.Repositories;

namespace TechTest2025.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;

        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
        }

        public IQueryable<Person> GetAllPersons()
        {
            return _repository.GetAll();
        }
    }
}

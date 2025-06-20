using TechTest2025.Models;

namespace TechTest2025.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly string _filePath;

        public PersonRepository(string filePath)
        {
            _filePath = filePath;
        }

        public IQueryable<Person> GetAll()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Person>().AsQueryable();
            }

            var persons = File.ReadAllLines(_filePath)
                .Select((line, index) => ParsePerson(line, index))
                .Where(p => p != null)
                .ToList();

            return persons.AsQueryable();
        }

        private Person ParsePerson(string line, int index)
        {
            try
            {
                var parts = line.Split(',');
                return new Person
                {
                    Id = index + 1,
                    Name = parts[0],
                    Address = parts[1],
                    DateOfBirth = DateTime.Parse(parts[2])
                };
            }
            catch
            {
                return null; // skip invalid lines
            }
        }
    }
}

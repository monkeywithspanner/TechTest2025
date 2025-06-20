namespace TechTest2025.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using TechTest2025.Services;

    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var persons = _personService.GetAllPersons().ToList();
            return Ok(persons);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Model;
using PeopleManager.Services;

namespace PeopleManager.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PersonService _personService;

        public PeopleController(PersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public IActionResult Find()
        {
            var people = _personService.Find();
            return Ok(people);
        }

        [HttpGet("{id:int}", Name = "GetPersonRoute")]
        public IActionResult Get(int id)
        {
            var people = _personService.Get(id);
            return Ok(people);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Person model)
        {
            var person = _personService.Create(model);
            if (person is null)
            {
                return NotFound();
            }
            return CreatedAtRoute("GetPersonRoute", new {id = person.Id}, person);
        }

        [HttpPut("{id:int}")]
        public IActionResult Edit(int id, Person model)
        {
            var person = _personService.Update(id, model);
            if (person is null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _personService.Delete(id);

            return Ok();
        }
    }
}

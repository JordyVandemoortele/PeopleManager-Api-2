using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PeopleManager.APIservices;
using PeopleManager.Model;
using System.Text;

namespace PeopleManager.Ui.Mvc.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PeopleApiService _peopleApiService;

        public PeopleController(PeopleApiService peopleApiService)
        {
            _peopleApiService = peopleApiService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var people = await _peopleApiService.GetAll();
            return View(people);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }

            await _peopleApiService.Create(person);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var person = await _peopleApiService.GetById(id);
            if (person is null)
            {
                return RedirectToAction("Index");
            }

            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromForm]Person person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }
            await _peopleApiService.Edit(id, person);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await _peopleApiService.GetById(id);

            if (person is null)
            {
                return RedirectToAction("Index");
            }

            return View(person);
        }

        [HttpPost("People/Delete/{id:int?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _peopleApiService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

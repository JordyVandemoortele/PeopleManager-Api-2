using Microsoft.AspNetCore.Mvc;
using PeopleManager.APIservices;
using PeopleManager.Ui.Mvc.Models;
using System.Diagnostics;
using System.Net.Http;

namespace PeopleManager.Ui.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly PeopleApiService _peopleApiService;

        public HomeController(PeopleApiService peopleApiService)
        {
            _peopleApiService = peopleApiService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _peopleApiService.GetAll());
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var person = _peopleApiService.GetById(id);

            if (person is null)
            {
                return RedirectToAction("Index");
            }

            return View("PersonDetail", person);
        }
    }
}
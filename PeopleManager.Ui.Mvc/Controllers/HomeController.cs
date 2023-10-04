using Microsoft.AspNetCore.Mvc;
using PeopleManager.Model;
using PeopleManager.Ui.Mvc.Models;
using System.Diagnostics;
using System.Net.Http;

namespace PeopleManager.Ui.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var HttpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/People";
            var response = await HttpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();
            var people = await response.Content.ReadFromJsonAsync<IList<Person>>();
            return View(people);
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
            var HttpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/People/" + id;
            var response = await HttpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();
            var person = await response.Content.ReadFromJsonAsync<Person>(); ;

            if (person is null)
            {
                return RedirectToAction("Index");
            }

            return View("PersonDetail", person);
        }
    }
}
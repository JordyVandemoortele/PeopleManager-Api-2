using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PeopleManager.APIservices;
using PeopleManager.Model;
using System.Net.Http;
using System.Text;

namespace PeopleManager.Ui.Mvc.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly VehicleApiService _vehicleApiService;
        private readonly PeopleApiService _peopleApiService;

        public VehiclesController(VehicleApiService vehicleApiService, PeopleApiService peopleApiService)
        {
            _vehicleApiService = vehicleApiService;
            _peopleApiService = peopleApiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var people = await _vehicleApiService.GetAll();
            return View(people);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return await CreateEditView("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return await CreateEditView("Create", vehicle);
            }
            await _vehicleApiService.Create(vehicle);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var vehicle = await _vehicleApiService.GetById(id);
            if (vehicle is null)
            {
                return RedirectToAction("Index");
            }
            return await CreateEditView("Edit", vehicle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return View(vehicle);
            }
            await _vehicleApiService.Edit(id, vehicle);
            return RedirectToAction("Index");
        }

        private async Task<IActionResult> CreateEditView([AspMvcView] string viewName, Vehicle? vehicle = null)
        {
            var people = await _peopleApiService.GetAll();

            ViewBag.People = people;

            return View(viewName, vehicle);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var vehicle = await _vehicleApiService.GetById(id);

            if (vehicle is null)
            {
                return RedirectToAction("Index");
            }

            return View(vehicle);
        }

        [HttpPost("Vehicles/Delete/{id:int?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vehicleApiService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

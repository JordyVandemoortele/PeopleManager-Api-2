using Newtonsoft.Json;
using PeopleManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PeopleManager.APIservices
{
    public class VehicleApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public VehicleApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IList<Vehicle>> GetAll()
        {
            var HttpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/Vehicle";
            var response = await HttpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();
            var people = await response.Content.ReadFromJsonAsync<IList<Vehicle>>();
            return people ?? new List<Vehicle>();
        }
        public async Task<Vehicle> GetById(int id)
        {
            var HttpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/Vehicle/" + id;
            var response = await HttpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();
            var vehicle = await response.Content.ReadFromJsonAsync<Vehicle>();
            if(vehicle == null)
            {
                return new Vehicle { LicensePlate = ""};
            }
            return vehicle;
        }
        public async Task Create (Vehicle vehicle)
        {
            var HttpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/Vehicle";
            var content = new StringContent(JsonConvert.SerializeObject(vehicle), Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync(route, content);
            response.EnsureSuccessStatusCode();
        }
        public async Task Edit(int id, Vehicle vehicle)
        {
            var HttpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/Vehicle/" + id;
            var content = new StringContent(JsonConvert.SerializeObject(vehicle), Encoding.UTF8, "application/json");
            var response = await HttpClient.PutAsync(route, content);
            response.EnsureSuccessStatusCode();
        }
        public async Task Delete(int id)
        {
            var HttpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/Vehicle/" + id;
            var response = await HttpClient.DeleteAsync(route);
            response.EnsureSuccessStatusCode();
        }
    }
}

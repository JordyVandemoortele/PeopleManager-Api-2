using Newtonsoft.Json;
using PeopleManager.Dto.Requests;
using PeopleManager.Dto.Results;
using System.Net.Http.Json;
using System.Text;

namespace PeopleManager.APIservices
{
    public class VehicleApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public VehicleApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IList<VehicleResult>> GetAll()
        {
            var HttpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/Vehicle";
            var response = await HttpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();
            var people = await response.Content.ReadFromJsonAsync<IList<VehicleResult>>();
            return people ?? new List<VehicleResult>();
        }
        public async Task<VehicleResult> GetById(int id)
        {
            var HttpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/Vehicle/" + id;
            var response = await HttpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();
            var vehicle = await response.Content.ReadFromJsonAsync<VehicleResult>();
            if(vehicle == null)
            {
                return new VehicleResult { LicensePlate = ""};
            }
            return vehicle;
        }
        public async Task Create (VehicleRequest vehicle)
        {
            var HttpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/Vehicle";
            var content = new StringContent(JsonConvert.SerializeObject(vehicle), Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync(route, content);
            response.EnsureSuccessStatusCode();
        }
        public async Task Edit(int id, VehicleRequest vehicle)
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

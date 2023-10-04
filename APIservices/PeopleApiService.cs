using System.Net.Http.Json;
using PeopleManager.Dto.Requests;
using PeopleManager.Dto.Results;

namespace PeopleManager.APIservices
{

    public class PeopleApiService
    {    
        private readonly IHttpClientFactory _httpClientFactory;

        public PeopleApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IList<PersonResult>> GetAll()
        {
            var HttpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/People";
            var response = await HttpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();
            var people = await response.Content.ReadFromJsonAsync<IList<PersonResult>>();
            return people ?? new List<PersonResult>();
        }
        public async Task<PersonResult> GetById(int id)
        {
            var HttpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/People/" + id;
            var response = await HttpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();
            var person = await response.Content.ReadFromJsonAsync<PersonResult>();
            if (person == null)
            {
                return new PersonResult { Email="", FirstName="", LastName=""};
            }
            return person;
        }
        public async Task Create(PersonRequest person)
        {
            var HttpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/People";
            var response = await HttpClient.PostAsJsonAsync(route, person);
            response.EnsureSuccessStatusCode();
        }
        public async Task Edit(int id, PersonRequest person)
        {
            var HttpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/People/" + id;
            var response = await HttpClient.PutAsJsonAsync(route, person);
            response.EnsureSuccessStatusCode();
        }
        public async Task Delete(int id)
        {
            var HttpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/People/" + id;
            var response = await HttpClient.DeleteAsync(route);
            response.EnsureSuccessStatusCode();
        }
    }
}

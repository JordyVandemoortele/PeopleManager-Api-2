using PeopleManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PeopleManager.APIservices
{

    public class PeopleApiService
    {    
        private readonly IHttpClientFactory _httpClientFactory;

        public PeopleApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IList<Person>> GetAll()
        {
            var HttpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/People";
            var response = await HttpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();
            var people = await response.Content.ReadFromJsonAsync<IList<Person>>();
            return people ?? new List<Person>();
        }
        public async Task<Person> GetById(int id)
        {
            var HttpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/People/" + id;
            var response = await HttpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();
            var person = await response.Content.ReadFromJsonAsync<Person>();
            if (person == null)
            {
                return new Person { Email="", FirstName="", LastName=""};
            }
            return person;
        }
        public async Task Create(Person person)
        {
            var HttpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/People";
            var content = new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync(route, content);
            response.EnsureSuccessStatusCode();
        }
        public async Task Edit(int id, Person person)
        {
            var HttpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/People/" + id;
            var content = new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, "application/json");
            var response = await HttpClient.PutAsync(route, content);
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

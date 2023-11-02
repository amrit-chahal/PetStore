using PetStore.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PetStore.Services
{
    public class PetStoreApiClient:IApiClient
    {
        private readonly HttpClient _httpClient;
        public PetStoreApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<PetStoreModel>> GetPetsAsync(string apiFilter)
        {
            var pets = await _httpClient.GetFromJsonAsync<List<PetStoreModel>>(apiFilter);     
            return pets ?? new List<PetStoreModel>();
           
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using VetClinicManager.Model;

namespace VetClinicManager.DataServices
{
    internal class ClientService : IClientService
    {
        private readonly HttpClient _httpClient;

        public ClientService(string baseAddress)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            var clients = await _httpClient.GetFromJsonAsync<List<Client>>("api/clients");
            return clients;
        }

        public async Task<Client> CreateClientAsync(Client client)
        {
            var response = await _httpClient.PostAsJsonAsync("api/clients", client);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Client>();
        }

        public async Task UpdateClientAsync(Client updatedClient)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/clients/", updatedClient);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteClientAsync(int clientId)
        {
            var response = await _httpClient.DeleteAsync($"api/clients/{clientId}");
            response.EnsureSuccessStatusCode();
        }
    }

}

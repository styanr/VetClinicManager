using VetClinicManager.Model;

namespace VetClinicManager.DataServices;

internal interface IClientService
{
    Task<List<Client>> GetClientsAsync();
    Task<Client> CreateClientAsync(Client client);
    Task UpdateClientAsync(Client updatedClient);
    Task DeleteClientAsync(int clientId);
}
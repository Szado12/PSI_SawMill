using CSharpFunctionalExtensions;
using OrderMicroservice.ModelViews;

namespace OrderMicroservice.Services.Interfaces
{
    public interface IClientService
    {
        Result<List<ClientDetails>> GetClients();
        Result<ClientDetails> GetClient(int clientId);
        Result<bool> DeleteClient(int clientId);
        Result<ClientDetails> EditClient(int clientId, ClientDetails data);
        Result<ClientDetails> AddClient(ClientDetails data);
    }
}

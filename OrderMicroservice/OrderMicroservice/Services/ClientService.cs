using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using OrderMicroservice.Models;
using OrderMicroservice.ModelViews.Client;
using OrderMicroservice.Services.Interfaces;

namespace OrderMicroservice.Services
{
    public class ClientService : DefaultService, IClientService
    {
        IEncryptionService _encryptionService;
        public ClientService(ClientOrderContext clientOrderContext, IEncryptionService encryptionService) : base(clientOrderContext)
        {
            _encryptionService = encryptionService;
        }

        public Result<ClientDetails> AddClient(ClientDetails data)
        {
            var clientToAdd = Mapper.Map<ClientDetails, Client>(data);
            ClientOrderContext.Add(clientToAdd);
            if (ClientOrderContext.SaveChanges() > 0)
                return GetClient(clientToAdd.ClientId);
            else
                return Result.Failure<ClientDetails>("Adding client failed.");
        }

        public Result<bool> DeleteClient(int clientId)
        {
            var clientToDelete = ClientOrderContext.Clients.Where(x => x.ClientId == clientId).FirstOrDefault();
            if (clientToDelete == null)
                return Result.Failure<bool>($"Client with id {clientId} not found.");

            clientToDelete.IsArchived = true;
            if (ClientOrderContext.SaveChanges() > 0)
                return Result.Success(true);
            else
                return Result.Failure<bool>($"Deleting client with id {clientId} failed.");

        }

        public Result<ClientDetails> EditClient(int clientId, ClientDetails data)
        {
            var clientToEdit = ClientOrderContext.Clients.Where(x => x.ClientId == clientId).Include(x => x.Address).FirstOrDefault();
            if (clientToEdit == null)
                return Result.Failure<ClientDetails>($"Client with id {clientId} not found.");

            clientToEdit.IsArchived = data.IsArchived ?? false;
            clientToEdit.FirstName = _encryptionService.EncryptData(data.FirstName);
            clientToEdit.LastName = _encryptionService.EncryptData(data.LastName);
            clientToEdit.Nip = data.NIP;
            clientToEdit.CompanyName = data.CompanyName;

            clientToEdit.Address.City = data.Address.City;
            clientToEdit.Address.PostalCode = data.Address.PostalCode;
            clientToEdit.Address.Street = data.Address.Street;

            if (ClientOrderContext.SaveChanges() > 0)
                return GetClient(clientId);
            else
                return Result.Failure<ClientDetails>($"Editing client with id {clientId} failed.");
        }

        public Result<ClientDetails> GetClient(int clientId)
        {
            var result = ClientOrderContext.Clients.Where(x => x.ClientId == clientId).Include(x => x.Address).Select(Mapper.Map<Client, ClientDetails>).FirstOrDefault();
            if (result != null)
                return Result.Success(result);
            else
                return Result.Failure<ClientDetails>($"Fetching client data with id {clientId} failed.");
        }

        public Result<List<ClientDetails>> GetClients()
        {
            var result = ClientOrderContext.Clients.Where(x => !x.IsArchived).Include(x => x.Address).Select(Mapper.Map<Client, ClientDetails>).ToList();
            if (result != null)
                return Result.Success(result);
            else
                return Result.Failure<List<ClientDetails>>($"Fetching clients data failed.");
        }
    }
}

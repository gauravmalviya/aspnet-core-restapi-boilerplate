using System.Collections.Generic;
using System.Threading.Tasks;
using CWG.API.Workflow.ViewModels.ClientModule;

namespace CWG.API.Workflow.ServiceContracts.ClientModule
{
    public interface IClientService
    {
        Task<List<ClientListViewModel>> GetClients();
        Task<List<ClientListViewModel>> GetClientsForUser(int userId);
        Task<ClientDetailViewModel> GetClient(int clientId);
        Task<bool> InsertClient(ClientDetailViewModel inputData);
        Task<bool> UpdateClient(ClientDetailViewModel inputData);
        Task<bool> DeleteClient(int clientId);
    }
}
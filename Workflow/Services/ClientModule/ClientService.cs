using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CWG.API.Workflow.Data;
using CWG.API.Workflow.Data.Entities.ClientModule;
using CWG.API.Workflow.ServiceContracts.ClientModule;
using CWG.API.Workflow.ViewModels.ClientModule;

namespace CWG.API.Workflow.Services.ClientModule
{
    public class ClientService : IClientService
    {
        private readonly IMapper _mapper;
        private readonly CWGDbContext _context;
        public ClientService(CWGDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper; //injected automapper
        }

        public async Task<List<ClientListViewModel>> GetClients()
        {
            List<ClientListViewModel> lstClient = new List<ClientListViewModel>();
            var _clients = await _context.Clients.ToListAsync();

            foreach(var item in _clients)
            {
                var oClient = new ClientListViewModel();
                oClient.ClientId = item.ClientId;
                oClient.Address = item.Address + item.City + item.StateName + item.Pincode;
                oClient.Effective_Date = item.Effective_Date;
                oClient.IsActive = item.IsActive;
                oClient.LogoUrl = item.LogoUrl;
                oClient.ContactNumber = item.Mobile +item.OfficeNo;
                oClient.Name = item.Name;
                
                lstClient.Add(oClient);
            }
            return lstClient;
        }

        public async Task<List<ClientListViewModel>> GetClientsForUser(int userId)
        {
            List<ClientListViewModel> lstClient = new List<ClientListViewModel>();
            var _clients = await _context.Clients.ToListAsync();

            foreach(var item in _clients)
            {
                var oClient = new ClientListViewModel();
                oClient.ClientId = item.ClientId;
                oClient.Address = item.Address + item.City + item.StateName + item.Pincode;
                oClient.Effective_Date = item.Effective_Date;
                oClient.IsActive = item.IsActive;
                oClient.LogoUrl = item.LogoUrl;
                oClient.ContactNumber = item.Mobile +item.OfficeNo;
                oClient.Name = item.Name;
                
                lstClient.Add(oClient);
            }
            return lstClient;
        }

        public async Task<ClientDetailViewModel> GetClient(int clientId)
        {
            var _client = await _context.Clients.FirstOrDefaultAsync(x => x.ClientId == clientId);
            if (_client == null)
                return null;

            var oClient = new ClientDetailViewModel();
            oClient.ClientId = _client.ClientId;
            oClient.Address = _client.Address;
            oClient.City = _client.City;
            oClient.Country = _client.Country;
            oClient.Effective_Date = _client.Effective_Date;
            oClient.IsActive = _client.IsActive;
            oClient.LogoUrl = _client.LogoUrl;
            oClient.Mobile = _client.Mobile;
            oClient.Name = _client.Name;
            oClient.OfficeNo = _client.OfficeNo;
            oClient.Pincode = _client.Pincode;
            oClient.StateName = _client.StateName;
            oClient.Termination_Date = _client.Termination_Date;
            oClient.LastUpdatedBy = _client.ModifiedBy != null ? _client.ModifiedBy : _client.CreatedBy;
            oClient.LastUpdatedOn = _client.ModifiedOn != null ? _client.ModifiedOn : _client.CreatedOn;

            return oClient;
        }

        public async Task<bool> InsertClient(ClientDetailViewModel inputData)
        {
            bool retVal= true;
            var _client = new Client();

            _client.Address = inputData.Address;
            _client.City = inputData.City;
            _client.Country = inputData.Country;
            _client.Effective_Date = inputData.Effective_Date;
            _client.IsActive = inputData.IsActive;
            _client.LogoUrl = inputData.LogoUrl;
            _client.Mobile = inputData.Mobile;
            _client.Name = inputData.Name;
            _client.OfficeNo = inputData.OfficeNo;
            _client.Pincode = inputData.Pincode;
            _client.StateName = inputData.StateName;
            _client.Termination_Date = inputData.Termination_Date;
            _client.ModifiedBy = inputData.LastUpdatedBy;
            _client.ModifiedOn = inputData.LastUpdatedOn;
            _context.Clients.Add(_client);
            await _context.SaveChangesAsync();

            return retVal;
        }

        public async Task<bool> UpdateClient(ClientDetailViewModel inputData)
        {
            bool retVal= true;
            var _client = await _context.Clients.FirstOrDefaultAsync(x => x.ClientId == inputData.ClientId);
            if (_client == null)
                return false;

            _client.Address = inputData.Address;
            _client.City = inputData.City;
            _client.Country = inputData.Country;
            _client.Effective_Date = inputData.Effective_Date;
            _client.IsActive = inputData.IsActive;
            _client.LogoUrl = inputData.LogoUrl;
            _client.Mobile = inputData.Mobile;
            _client.Name = inputData.Name;
            _client.OfficeNo = inputData.OfficeNo;
            _client.Pincode = inputData.Pincode;
            _client.StateName = inputData.StateName;
            _client.Termination_Date = inputData.Termination_Date;
            _client.ModifiedBy = inputData.LastUpdatedBy;
            _client.ModifiedOn = inputData.LastUpdatedOn;
            await _context.SaveChangesAsync();

            return retVal;
        }

        public async Task<bool> DeleteClient(int clientId)
        {
            bool retVal= true;
            var _client = await _context.Clients.FirstOrDefaultAsync(x => x.ClientId == clientId);
            if (_client == null)
                return false;

            _client.IsDeleted = true;
            _client.DeletedBy = 0;
            _client.DeletedOn = DateTime.UtcNow;

            
            await _context.SaveChangesAsync();
            
            return retVal;
        }
    }
}
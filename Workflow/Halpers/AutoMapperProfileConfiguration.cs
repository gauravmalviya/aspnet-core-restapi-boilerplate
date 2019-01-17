using AutoMapper;
using CWG.API.Workflow.Data.Entities.ClientModule;
using CWG.API.Workflow.ViewModels.ClientModule;

namespace CWG.API.Workflow.Halpers
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        : this("MyProfile")
        {
        }
        protected AutoMapperProfileConfiguration(string profileName)
        : base(profileName)
        {
            CreateMap<Client, ClientDetailViewModel>();
            CreateMap<ClientDetailViewModel, Client>();
            CreateMap<Client, ClientListViewModel>();
            CreateMap<ClientListViewModel, Client>();
          

        }
    }
}
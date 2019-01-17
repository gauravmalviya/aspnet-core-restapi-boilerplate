using System;

namespace CWG.API.Workflow.ViewModels.ClientModule
{
    public class ClientListViewModel : BaseViewModel
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string LogoUrl { get; set; }
        public DateTime? Effective_Date { get; set; }
        public bool? IsActive { get; set; }
    }


   
}
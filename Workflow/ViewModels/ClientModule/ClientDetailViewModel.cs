using System;
using System.ComponentModel.DataAnnotations;

namespace CWG.API.Workflow.ViewModels.ClientModule
{
    public class ClientDetailViewModel : BaseViewModel
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string StateName { get; set; }
        public string Country { get; set; }
        public string Pincode { get; set; }
        public string OfficeNo { get; set; }
        public string Mobile { get; set; }
        public string LogoUrl { get; set; }
        public DateTime? Effective_Date { get; set; }
        public DateTime? Termination_Date { get; set; }
        public bool? IsActive { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
    }
}
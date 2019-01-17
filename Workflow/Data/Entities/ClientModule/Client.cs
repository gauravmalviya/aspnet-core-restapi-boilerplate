using System;
using System.ComponentModel.DataAnnotations;

namespace CWG.API.Workflow.Data.Entities.ClientModule
{
    public class Client : BaseEntity
    {
        [Key]
        public int ClientId { get; set; }
        [Required, MaxLength(256)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(100)]
        public string StateName { get; set; }
        [MaxLength(100)]
        public string Country { get; set; }
        [MaxLength(10)]
        public string Pincode { get; set; }
        [MaxLength(15)]
        public string OfficeNo { get; set; }
        [Required,MaxLength(15)]
        public string Mobile { get; set; }
        [MaxLength(500)]
        public string LogoUrl { get; set; }
        public DateTime? Effective_Date { get; set; }
        public DateTime? Termination_Date { get; set; }
    }
}
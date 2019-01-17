using System;
using System.ComponentModel.DataAnnotations;

namespace CWG.API.Workflow.Data.Entities.Auth
{
    public class AuthUser : BaseEntity
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public byte[] SecurityStamp { get; set; }
        public byte[] PasswordHash { get; set; }
        public string PasswordResetCode { get; set; }
        public bool? IsFirstTimeLogin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public  bool? PhoneConfirmed { get; set; }
        public string PhoneConfirmedCode { get; set; }
        public bool? IsTwoFactoredEnabled { get; set; }
        public  string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string EmailConfirmedCode { get; set; }
        public string PictureUrl { get; set; }
        public DateTime? LastLoginTime { get; set; }
    }
}
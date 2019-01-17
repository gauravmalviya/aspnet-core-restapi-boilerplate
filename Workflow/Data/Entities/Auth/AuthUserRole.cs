using System.ComponentModel.DataAnnotations;

namespace CWG.API.Workflow.Data.Entities.Auth
{
    public class AuthUserRole : BaseEntity
    {
        [Key]
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
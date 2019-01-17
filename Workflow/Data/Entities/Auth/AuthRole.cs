using System.ComponentModel.DataAnnotations;

namespace CWG.API.Workflow.Data.Entities.Auth
{
    public class AuthRole : BaseEntity
    {
        [Key]
        public int RoleId { get; set; }
        public string Name { get; set; }
    }
}
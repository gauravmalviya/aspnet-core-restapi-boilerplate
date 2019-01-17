using System.Threading.Tasks;
using CWG.API.Workflow.ViewModels.Auth;

namespace CWG.API.Workflow.ServiceContracts.Auth
{
    public interface IAuthService
    {
         Task<RegisterViewModel> Register(RegisterViewModel user, string passowrd);
         Task<ProfileViewModel> Login(string username,string password);
         Task<bool> UserExists(string username);
    }
}
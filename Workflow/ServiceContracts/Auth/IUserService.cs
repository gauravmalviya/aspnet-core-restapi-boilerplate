using System.Threading.Tasks;
using CWG.API.Workflow.ViewModels.Auth;

namespace CWG.API.Workflow.ServiceContracts.Auth
{
    public interface IUserService
    {
         Task<ProfileViewModel> GetUserInfo(int userId);
    }
}
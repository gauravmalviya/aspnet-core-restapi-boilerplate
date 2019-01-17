using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CWG.API.Workflow.Data;
using CWG.API.Workflow.ServiceContracts.Auth;
using CWG.API.Workflow.ViewModels.Auth;

namespace CWG.API.Workflow.Services.Auth
{
    public class UserService : IUserService 
    {
         private readonly CWGDbContext _context;
        public UserService(CWGDbContext context)
        {
            _context = context;
        }
        public async Task<ProfileViewModel> GetUserInfo(int userId)
        {
            var user = await _context.AuthUsers.FirstOrDefaultAsync(x=>x.UserId == userId);
            if(user==null)
                return null;
                
            var profileViewModel = new ProfileViewModel();
            profileViewModel.UserName = user.UserName;
            profileViewModel.FirstName = user.FirstName;
            profileViewModel.LastName = user.LastName;
            profileViewModel.Email = user.Email;
            profileViewModel.UserId = user.UserId;
          
            return profileViewModel;
        }

        
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CWG.API.Workflow.Data;
using CWG.API.Workflow.Data.Entities.Auth;
using CWG.API.Workflow.ServiceContracts.Auth;
using CWG.API.Workflow.ViewModels.Auth;

namespace CWG.API.Workflow.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly CWGDbContext _context;
        public AuthService(CWGDbContext context)
        {
            _context = context;
        }
        public async Task<ProfileViewModel> Login(string username, string password)
        {
            var user = await _context.AuthUsers.FirstOrDefaultAsync(x=>x.UserName ==username);
            if(user==null)
                return null;
            if(!VerifyPasswordHash(password,user.PasswordHash,user.SecurityStamp))
                return null;

                var profileViewModel = new ProfileViewModel();
                profileViewModel.UserName = user.UserName;
                profileViewModel.FirstName = user.FirstName;
                profileViewModel.LastName = user.LastName;
                profileViewModel.Email = user.Email;
                profileViewModel.UserId = user.UserId;

                return profileViewModel;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] securityStamp)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(securityStamp))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0;i<computeHash.Length;i++)
                {
                    if(computeHash[i]!=passwordHash[i])
                        return false;
                }
                return true;

            }
        }

        public async Task<RegisterViewModel> Register(RegisterViewModel user, string passowrd)
        {
            var authUser = new AuthUser();
            authUser.UserName = user.UserName;
            authUser.FirstName = user.FirstName;
            authUser.LastName = user.LastName;
            authUser.Email = user.Email;
            authUser.IsActive = true;
            authUser.IsDeleted = false;
            authUser.Phone = user.Mobile;
            authUser.PictureUrl = user.PictureUrl;
           
            authUser.CreatedBy = -1;
            authUser.CreatedOn = DateTime.Now;

            byte[] passwordHash, SecurityStamp;
            CreatePasswordHash(passowrd,out passwordHash,out SecurityStamp);
            authUser.PasswordHash = passwordHash;
            authUser.SecurityStamp = SecurityStamp;

            await _context.AuthUsers.AddAsync(authUser);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] securityStamp)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                securityStamp = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.AuthUsers.AnyAsync(x=>x.UserName==username))
                return true;
            return false;
        }
    
    }
}
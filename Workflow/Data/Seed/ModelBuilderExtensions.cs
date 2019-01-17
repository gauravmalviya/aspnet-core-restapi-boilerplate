using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CWG.API.Workflow.Data.Entities.Auth;
using CWG.API.Workflow.Data.Entities.ClientModule;

namespace CWG.API.Workflow.Data.Seed
{   public static class ModelBuilderExtensions
    {
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] securityStamp)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                securityStamp = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public static void Seed(this ModelBuilder modelBuilder)
        {
            byte[] passwordHash, securityStamp;
            CreatePasswordHash("Admin#12345",out passwordHash,out securityStamp);
            
            modelBuilder.Entity<AuthRole>().HasData(
                new AuthRole{RoleId = 1, Name = "Super Admin"}
            );
            modelBuilder.Entity<AuthUser>().HasData(
                new AuthUser{UserId = 1,UserName = "sysadmin",Email = "info@codewithgaurav.com", EmailConfirmed = true, 
                Phone = "999999999", PhoneConfirmed = true,IsFirstTimeLogin = true,IsTwoFactoredEnabled = false, 
                FirstName="System", LastName = "Admin",CreatedOn = DateTime.UtcNow,IsActive = true,IsDeleted = false,
                PasswordHash = passwordHash, SecurityStamp = securityStamp }
            
            );

            modelBuilder.Entity<AuthUserRole>().HasData(
                new AuthUserRole{UserRoleId = 1, UserId = 1, RoleId = 1,IsActive = true, IsDeleted = false}
            );



           

            
        }

        
    }


}
using Authentication.Application.Common.Exceptions;
using Authentication.Application.Common.Interfaces;
using Authentication.Infra.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Authentication.Infra.Services
{
    public class IdentityService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
        : IIdentityService
    {
        public async Task<(bool isSucceed, string userId)> CreateUserAsync(string userName,
            string password,
            string email, string fullName, string phoneNumber)
        {
            var user = new ApplicationUser()
            {
                FullName = fullName,
                UserName = userName,
                Email = email,
                PhoneNumber = phoneNumber
            };

            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors);
            }

            return (result.Succeeded, user.Id);
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            if (user.UserName == "system" || user.UserName == "admin")
            {
                throw new Exception("You can not delete system or admin user");
            }

            var result = await userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<List<(string id, string fullName, string userName, string email, string phoneNumber)>>
            GetAllUsersAsync()
        {
            var users = await userManager.Users.Select(x => new
            {
                x.Id,
                x.FullName,
                x.UserName,
                x.Email,
                x.PhoneNumber
            }).ToListAsync();

            return users.Select(user => (user.Id, user.FullName, user.UserName, user.Email, user.PhoneNumber)).ToList();
        }

        public async Task<(string userId, string fullName, string UserName, string email)> GetUserDetailsAsync(
            string userId)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            return (user.Id, user.FullName, user.UserName, user.Email);
        }

        public async Task<(string userId, string fullName, string UserName, string email)>
            GetUserDetailsByUserNameAsync(string userName)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            return (user.Id, user.FullName, user.UserName, user.Email);
        }

        public async Task<string> GetUserIdAsync(string userName)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            return await userManager.GetUserIdAsync(user);
        }

        // public async Task<string> GetUserNameAsync(string userId)
        // {
        //     var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
        //     if (user == null)
        //     {
        //         throw new NotFoundException("User not found");
        //     }
        //
        //     return await userManager.GetUserNameAsync(user);
        // }

        public async Task<bool> SigninUserAsync(string userName, string password)
        {
            var result = await signInManager.PasswordSignInAsync(userName, password, true, false);
            return result.Succeeded;
        }

        public async Task<bool> UpdateUserProfile(string id, string fullName, string email, string phoneNumber)
        {
            var user = await userManager.FindByIdAsync(id);
            user.FullName = fullName;
            user.Email = email;
            user.PhoneNumber = phoneNumber;
            var result = await userManager.UpdateAsync(user);

            return result.Succeeded;
        }
    }
}
namespace Authentication.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        // User section
        Task<(bool isSucceed, string userId)> CreateUserAsync(string userName, string password, string email,
            string fullName, string phoneNumber);

        Task<bool> SigninUserAsync(string userName, string password);
        Task<string> GetUserIdAsync(string userName);
        Task<(string userId, string fullName, string UserName, string email)> GetUserDetailsAsync(string userId);

        Task<(string userId, string fullName, string UserName, string email)> GetUserDetailsByUserNameAsync(
            string userName);

        // Task<string> GetUserNameAsync(string userId);
        Task<bool> DeleteUserAsync(string userId);
        Task<List<(string id, string fullName, string userName, string email, string phoneNumber)>> GetAllUsersAsync();
        Task<bool> UpdateUserProfile(string id, string fullName, string email, string phoneNumber);
    }
}
using System.Security.Claims;
using Core.Entities;

namespace Core.Interfaces.Services;

public interface IUserService
{
    Task<string> RegisterUserAsync(string email, string username, string password, bool rememberMe);
    Task<string> LoginUserAsync(string email, string password, bool rememberMe);
    Task<string> DeleteUserAsync(string email);
    Task<User> GetUserByEmailAsync(string email);
    Task<User> GetUserByUsernameAsync(string userName);
    Task<IEnumerable<User>> GetUsersByRoleAsync(string role, string? searchTerm = null,
    string? statusFilter = null, string? sortOrder = null, int? skip = null, int? take = null);
    Task LogoutUserAsync();
    Task<string> UpdateUserProfileAsync(User user, string userName, string profilePicture);
    Task<int> GetUsersCountByRoleAsync(string role, string? searchTerm = null, string? statusFilter = null);
    Task AddClaimsAsync(User user, IEnumerable<Claim> claims);
    Task RemoveClaimAsync(User user, string claimType);
    Task<User> GetUserByIdAsync(Guid userId);
    Task<string> GetUsername(User user);
    string GetUserPfp(User user);
    Task<string> GetUserIdByUsername(string userName);
    Task<bool> IsUserAdmin(string userName);
}
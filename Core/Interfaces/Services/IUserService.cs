using Core.Entities;
using Core.Models;

namespace Core.Interfaces.Services;

public interface IUserService
{
    Task<ServiceResult<User>> RegisterUserAsync(string email, string username, string password, bool rememberMe);
    Task<ServiceResult<User>> LoginUserAsync(string emailOrUsername, string password, bool rememberMe);
    Task<ServiceResult<User>> DeleteUserAsync(string email);
    Task<ServiceResult<User>> GetUserByEmailAsync(string email);
    Task<ServiceResult<User>> GetUserByUsernameAsync(string username);
    Task<ServiceResult<bool>> LogoutUserAsync();
    Task<ServiceResult<User>>  UpdateUserProfileAsync(User user, string username, string profilePicture);
    Task<ServiceResult<User>> GetUserByIdAsync(Guid userId);
    Task<ServiceResult<bool>> IsUserAdmin(string username);
}
using System.Security.Claims;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Logic.Services;

public class UserService(SignInManager<User> signInManager, UserManager<User> userManager)
{
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly UserManager<User> _userManager = userManager;

    public async Task<ServiceResult<User>> RegisterUserAsync(string email, string username, string password,
        bool rememberMe)
    {
        var user = new User { UserName = username, Email = email };
        var result = await _userManager.CreateAsync(user, password);
        var serviceResult = new ServiceResult<User> { Succeeded = result.Succeeded };

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, rememberMe);
            serviceResult.Data = user;
        }
        else
        {
            serviceResult.Errors = result.Errors.Select(e => e.Description).ToList();
        }

        return serviceResult;
    }

    public async Task<ServiceResult<User>> LoginUserAsync(string emailOrUsername, string password, bool rememberMe)
    {
        var user = await _userManager.FindByEmailAsync(emailOrUsername) ??
                   await _userManager.FindByNameAsync(emailOrUsername);
        var serviceResult = new ServiceResult<User>();

        if (user == null)
        {
            serviceResult.Succeeded = false;
            serviceResult.Errors.Add("User not found.");

            return serviceResult;
        }

        var result = await _signInManager.PasswordSignInAsync(user, password, rememberMe, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            serviceResult.Succeeded = true;
            serviceResult.Data = user;
        }
        else
        {
            serviceResult.Succeeded = false;
            if (result.IsLockedOut)
                serviceResult.Errors.Add("User account is locked out.");
            else if (result.IsNotAllowed)
                serviceResult.Errors.Add("User is not allowed to sign in.");
            else
                serviceResult.Errors.Add("Invalid credentials.");
        }

        return serviceResult;
    }

    public async Task<ServiceResult<User>> DeleteUserAsync(string email)
    {
        var serviceResult = new ServiceResult<User>();

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            serviceResult.Succeeded = false;
            serviceResult.Errors.Add("User not found.");

            return serviceResult;
        }

        var result = await _userManager.DeleteAsync(user);

        if (result.Succeeded)
        {
            serviceResult.Succeeded = true;
            serviceResult.Data = user;
        }
        else
        {
            serviceResult.Succeeded = false;
            serviceResult.Errors.AddRange(result.Errors.Select(e => e.Description));
        }

        return serviceResult;
    }

    public async Task<ServiceResult<List<User>>> GetUsersByRoleAsync(string roleName)
    {
        var serviceResult = new ServiceResult<List<User>>();

        var users = await _userManager.GetUsersInRoleAsync(roleName);

        serviceResult.Succeeded = true;
        serviceResult.Data = users.ToList();

        return serviceResult;
    }

    public async Task<ServiceResult<User>> GetUserByEmailAsync(string email)
    {
        var serviceResult = new ServiceResult<User>();

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            serviceResult.Succeeded = false;
            serviceResult.Errors.Add("User not found.");
        }
        else
        {
            serviceResult.Succeeded = true;
            serviceResult.Data = user;
        }

        return serviceResult;
    }
    
    public async Task<ServiceResult<User>> GetUserByUsernameAsync(string userName)
    {
        var serviceResult = new ServiceResult<User>();

        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
        {
            serviceResult.Succeeded = false;
            serviceResult.Errors.Add("User not found.");
        }
        else
        {
            serviceResult.Succeeded = true;
            serviceResult.Data = user;
        }

        return serviceResult;
    }
    
    public async Task<ServiceResult<bool>> LogoutUserAsync()
    {
        var serviceResult = new ServiceResult<bool>();

        try
        {
            await _signInManager.SignOutAsync();
            serviceResult.Succeeded = true;
            serviceResult.Data = true;
        }
        catch (Exception ex)
        {
            serviceResult.Succeeded = false;
            serviceResult.Data = false;
            serviceResult.Errors.Add(ex.Message);
        }

        return serviceResult;
    }
    
    public async Task<ServiceResult<User>> UpdateUserProfileAsync(User user, string userName, string profilePicture)
    {
        var serviceResult = new ServiceResult<User>();

        user.UserName = userName;
        user.ProfilePicture = profilePicture;

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            serviceResult.Succeeded = true;
            serviceResult.Data = user;
        }
        else
        {
            serviceResult.Succeeded = false;
            serviceResult.Errors.AddRange(result.Errors.Select(e => e.Description));
        }

        return serviceResult;
    }
    
    public async Task<ServiceResult<User>> GetUserByIdAsync(Guid userId)
    {
        var serviceResult = new ServiceResult<User>();

        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            serviceResult.Succeeded = false;
            serviceResult.Errors.Add("User not found.");
        }
        else
        {
            serviceResult.Succeeded = true;
            serviceResult.Data = user;
        }

        return serviceResult;
    }
    
    public async Task<ServiceResult<bool>> IsUserAdmin(string userName)
    {
        var serviceResult = new ServiceResult<bool>();

        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
        {
            serviceResult.Succeeded = false;
            serviceResult.Data = false;
            serviceResult.Errors.Add("User not found.");
            return serviceResult;
        }

        var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
        serviceResult.Succeeded = true;
        serviceResult.Data = isAdmin;

        return serviceResult;
    }
}

using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.Interfaces.Repositories;

public interface IUserRepository : IRepository<User, Guid>
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUsernameAsync(string userName);
    Task<IEnumerable<User>> GetByRoleAsync(string role, string? searchTerm = null,
    string? statusFilter = null, string? sortOrder = null, int? skip = null, int? take = null);
    Task<IdentityResult> AddAsync(User user, string password);
    Task<IdentityResult> UpdateAsync(User user, string password);
    Task<string> DeleteAsync(User user);
    Task<int> GetUsersCountByRoleAsync(string role, string? searchTerm = null, string? statusFilter = null);

}

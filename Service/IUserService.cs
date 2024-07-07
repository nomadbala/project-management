using ProjectManagement.Contracts;
using ProjectManagement.Model;

namespace ProjectManagement.Service;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<Guid> CreateUserAsync(CreateUserContract contract);
    Task<User> GetUserByIdAsync(Guid id);
    Task<User> UpdateUserAsync(Guid id, UpdateUserContract contract);
    Task DeleteByIdAsync(Guid id);
    Task<IEnumerable<Assignment>> GetAllTasks(Guid id);
    Task<IEnumerable<User>> FindByFullName(string fullName);
    Task<User> FindByEmail(string email);
}
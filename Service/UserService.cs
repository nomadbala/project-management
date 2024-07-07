using ProjectManagement.Contracts;
using ProjectManagement.Model;
using ProjectManagement.Repository;

namespace ProjectManagement.Service;

public class UserService(IUserRepository repository) : IUserService
{
    private readonly IUserRepository _repository = repository;

    public async Task<IEnumerable<User>> GetAllUsersAsync() =>
        await _repository.GetAllUsersAsync();

    public async Task<Guid> CreateUserAsync(CreateUserContract contract) =>
        await _repository.CreateUserAsync(contract);

    public async Task<User> GetUserByIdAsync(Guid id) =>
        await _repository.GetUserByIdAsync(id);

    public async Task<User> UpdateUserAsync(Guid id, UpdateUserContract contract) =>
        await _repository.UpdateUserAsync(id, contract);  

    public async Task DeleteByIdAsync(Guid id) =>
        await _repository.DeleteByIdAsync(id);

    public async Task<IEnumerable<Assignment>> GetAllTasks(Guid id) =>
        await _repository.GetAllTasks(id);

    public async Task<IEnumerable<User>> FindByFullName(string fullName) => 
        await _repository.FindByFullName(fullName);

    public async Task<User> FindByEmail(string email) => 
        await _repository.FindByEmail(email);
}
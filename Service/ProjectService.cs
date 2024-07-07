using ProjectManagement.Contracts;
using ProjectManagement.Model;
using ProjectManagement.Repository;

namespace ProjectManagement.Service;

public class ProjectService(IProjectRepository repository) : IProjectService
{
    private readonly IProjectRepository _repository = repository;

    public async Task<IEnumerable<Project>> GetAllProjectAsync() =>
        await _repository.GetAllProjectAsync();

    public async Task<Guid> CreateProjectAsync(CreateProjectContract contract) =>
        await _repository.CreateProjectAsync(contract);

    public async Task<Project> GetByIdAsync(Guid id) =>
        await _repository.GetByIdAsync(id);

    public async Task<Project> UpdateProjectAsync(Guid id, UpdateProjectContract contract) =>
        await _repository.UpdateProjectAsync(id, contract);

    public async Task DeleteByIdAsync(Guid id) =>
        await _repository.DeleteByIdAsync(id);

    public async Task<IEnumerable<Assignment>> GetAllAssignments(Guid id) =>
        await _repository.GetAllAssignments(id);

    public async Task<IEnumerable<Project>> GetByTitleAsync(string title) =>
        await _repository.GetByTitleAsync(title);


    public async Task<IEnumerable<Project>> GetByManagerId(Guid id) =>
        await _repository.GetByManagerId(id);
}
using ProjectManagement.Contracts;
using ProjectManagement.Model;

namespace ProjectManagement.Service;

public interface IProjectService
{
    Task<IEnumerable<Project>> GetAllProjectAsync();
    Task<Guid> CreateProjectAsync(CreateProjectContract contract);
    Task<Project> GetByIdAsync(Guid id);
    Task<Project> UpdateProjectAsync(Guid id, UpdateProjectContract contract);
    Task DeleteByIdAsync(Guid id);
    Task<IEnumerable<Assignment>> GetAllAssignments(Guid id);
    Task<IEnumerable<Project>> GetByTitleAsync(string title);
    Task<IEnumerable<Project>> GetByManagerId(Guid id);
}
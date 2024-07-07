using ProjectManagement.Contracts;
using ProjectManagement.Model;

namespace ProjectManagement.Service;

public interface IAssignmentService
{
    Task<IEnumerable<Assignment>> GetAllAssignmentsAsync();
    Task<Guid> CreateAssignmentAsync(CreateAssignmentContract contract);
    Task<Assignment> GetByIdAsync(Guid id);
    Task<Assignment> UpdateAssignmentAsync(Guid id, UpdateAssignmentContract contract);
    Task DeleteByIdAsync(Guid id);
    Task<IEnumerable<Assignment>> GetByTitleAsync(string title);
    Task<IEnumerable<Assignment>> GetByStatusAsync(AssignmentState state);
    Task<IEnumerable<Assignment>> GetByPriorityAsync(AssignmentPriority priority);
    Task<IEnumerable<Assignment>> GetByAssigneeAsync(Guid assigneeId);
    Task<IEnumerable<Assignment>> GetByProjectAsync(Guid projectId);
}
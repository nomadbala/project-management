using ProjectManagement.Contracts;
using ProjectManagement.Model;
using ProjectManagement.Repository;

namespace ProjectManagement.Service;

public class AssignmentService(IAssignmentRepository repository) : IAssignmentService
{
    private readonly IAssignmentRepository _repository = repository;

    public async Task<IEnumerable<Assignment>> GetAllAssignmentsAsync() =>
        await _repository.GetAllAssignmentsAsync();

    public async Task<Guid> CreateAssignmentAsync(CreateAssignmentContract contract) =>
        await _repository.CreateAssignmentAsync(contract);

    public async Task<Assignment> GetByIdAsync(Guid id) =>
        await _repository.GetByIdAsync(id);

    public async Task<Assignment> UpdateAssignmentAsync(Guid id, UpdateAssignmentContract contract) =>
        await _repository.UpdateAssignmentAsync(id, contract);

    public async Task DeleteByIdAsync(Guid id) =>
        await _repository.DeleteByIdAsync(id);

    public async Task<IEnumerable<Assignment>> GetByTitleAsync(string title) =>
        await _repository.GetByTitleAsync(title);

    public async Task<IEnumerable<Assignment>> GetByStatusAsync(AssignmentState state) =>
        await _repository.GetByStatusAsync(state);

    public async Task<IEnumerable<Assignment>> GetByPriorityAsync(AssignmentPriority priority) =>
        await _repository.GetByPriorityAsync(priority);


    public async Task<IEnumerable<Assignment>> GetByAssigneeAsync(Guid assigneeId) =>
        await _repository.GetByAssigneeAsync(assigneeId);

    public async Task<IEnumerable<Assignment>> GetByProjectAsync(Guid projectId) =>
        await _repository.GetByProjectAsync(projectId);
}
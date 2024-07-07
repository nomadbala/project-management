using Microsoft.EntityFrameworkCore;
using ProjectManagement.Contracts;
using ProjectManagement.Exceptions;
using ProjectManagement.Model;

namespace ProjectManagement.Repository;

public class AssignmentRepository(ApplicationDbContext context) : IAssignmentRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<Assignment>> GetAllAssignmentsAsync()
    {
        return await _context.Assignments.ToListAsync();
    }

    public async Task<Guid> CreateAssignmentAsync(CreateAssignmentContract contract)
    {
        var assignee = await _context.Users.FirstOrDefaultAsync(u => u.Id == contract.AssigneeId);

        if (assignee == null)
            throw new ElementNotFoundException($"Assignee with id {contract.AssigneeId} not found");

        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == contract.ProjectId);

        if (project == null)
            throw new ElementNotFoundException($"Project with id ${contract.ProjectId} not found");
        
        var assignment = new Assignment
        {
            Id = Guid.NewGuid(),
            Title = contract.Title,
            Description = contract.Description,
            Priority = contract.Priority,
            State = contract.State,
            AssigneeId = assignee.Id,
            Assignee = assignee,
            ProjectId = project.Id,
            Project = project,
            CreationDate = DateTime.UtcNow
        };

        await _context.AddAsync(assignment);
        await _context.SaveChangesAsync();

        return assignment.Id;
    }

    public async Task<Assignment> GetByIdAsync(Guid id)
    {
        var assignment = await _context.Assignments.FirstOrDefaultAsync(a => a.Id == id);

        return assignment ?? throw new ElementNotFoundException($"Assignment with id {id} not found");
    }

    public async Task<Assignment> UpdateAssignmentAsync(Guid id, UpdateAssignmentContract contract)
    {
        var assignment = await _context.Assignments.FirstOrDefaultAsync(a => a.Id == id);

        if (assignment == null)
            throw new ElementNotFoundException($"Assignment with id {id} not found");
        
        var assignee = await _context.Users.FirstOrDefaultAsync(u => u.Id == contract.AssigneeId);

        if (assignee == null)
            throw new ElementNotFoundException($"Assignee with id {contract.AssigneeId} not found");

        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == contract.ProjectId);

        if (project == null)
            throw new ElementNotFoundException($"Project with id ${contract.ProjectId} not found");

        assignment.Title = contract.Title;
        assignment.Description = contract.Description;
        assignment.Priority = contract.Priority;
        assignment.State = contract.State;
        assignment.AssigneeId = assignee.Id;
        assignment.Assignee = assignee;
        assignment.ProjectId = project.Id;
        assignment.Project = project;

        if (contract.CompletionDate != null)
            assignment.CompletionDate = contract.CompletionDate;

        await _context.SaveChangesAsync();

        return assignment;
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        if (!await _context.Assignments.AnyAsync(a => a.Id == id))
            throw new ElementNotFoundException($"Assignment with id {id} not found");

        await _context.Assignments
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<IEnumerable<Assignment>> GetByTitleAsync(string title)
    {
        var assignments = await _context.Assignments
            .Where(a => a.Title == title)
            .ToListAsync();

        return assignments;
    }

    public async Task<IEnumerable<Assignment>> GetByStatusAsync(AssignmentState state)
    {
        var assignments = await _context.Assignments
            .Where(a => a.State == state)
            .ToListAsync();

        return assignments;
    }

    public async Task<IEnumerable<Assignment>> GetByPriorityAsync(AssignmentPriority priority)
    {
        var assignments = await _context.Assignments
            .Where(a => a.Priority == priority)
            .ToListAsync();

        return assignments;
    }

    public async Task<IEnumerable<Assignment>> GetByAssigneeAsync(Guid assigneeId)
    {
        var assignments = await _context.Assignments
            .Where(a => a.AssigneeId == assigneeId)
            .ToListAsync();

        return assignments;
    }

    public async Task<IEnumerable<Assignment>> GetByProjectAsync(Guid projectId)
    {
        var assignments = await _context.Assignments
            .Where(a => a.ProjectId == projectId)
            .ToListAsync();

        return assignments;
    }
}
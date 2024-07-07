using Microsoft.EntityFrameworkCore;
using ProjectManagement.Contracts;
using ProjectManagement.Exceptions;
using ProjectManagement.Model;

namespace ProjectManagement.Repository;

public class ProjectRepository(ApplicationDbContext context) : IProjectRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<Project>> GetAllProjectAsync()
    {
        return await _context.Projects.ToListAsync();
    }

    public async Task<Guid> CreateProjectAsync(CreateProjectContract contract)
    {
        var manager = await _context.Users.FirstOrDefaultAsync(u => u.Id == contract.ManagerId);

        if (manager == null || manager.Role != Roles.Manager)
            throw new ElementNotFoundException($"Manager with id {contract.ManagerId} not found");

        var project = new Project
        {
            Id = Guid.NewGuid(),
            Title = contract.Title,
            Description = contract.Description,
            StartDate = DateTime.UtcNow,
            ManagerId = manager.Id,
            Manager = manager
        };

        await _context.AddAsync(project);
        await _context.SaveChangesAsync();

        return project.Id;
    }

    public async Task<Project> GetByIdAsync(Guid id)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);

        return project ?? throw new ElementNotFoundException($"Project with id {id} not found");
    }

    public async Task<Project> UpdateProjectAsync(Guid id, UpdateProjectContract contract)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);

        if (project == null)
            throw new ElementNotFoundException($"Project with id {id} not found");

        var manager = await _context.Users.FirstOrDefaultAsync(u => u.Id == contract.ManagerId);

        if (manager == null)
            throw new ElementNotFoundException($"Manager with id {id} not found");

        project.Title = contract.Title;
        project.Description = contract.Description;

        if (contract.EndDate != null)
            project.EndDate = contract.EndDate;

        project.ManagerId = manager.Id;
        project.Manager = manager;

        await _context.SaveChangesAsync();

        return project;
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        if (!await _context.Projects.AnyAsync(p => p.Id == id))
            throw new ElementNotFoundException($"Project with id {id} not found");

        await _context.Projects
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<IEnumerable<Assignment>> GetAllAssignments(Guid id)
    {
        var project = await this.GetByIdAsync(id);

        if (project == null)
            throw new ElementNotFoundException($"Project with id {id} not found");

        var assignments = await _context.Assignments
            .Where(a => a.ProjectId == project.Id)
            .ToListAsync();

        return assignments;
    }

    public async Task<IEnumerable<Project>> GetByTitleAsync(string title)
    {
        var projects = await _context.Projects
            .Where(p => p.Title == title)
            .ToListAsync();

        return projects;
    }

    public async Task<IEnumerable<Project>> GetByManagerId(Guid id)
    {
        var projects = await _context.Projects
            .Where(p => p.ManagerId == id)
            .ToListAsync();

        return projects;
    }
}
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Contracts;
using ProjectManagement.Exceptions;
using ProjectManagement.Model;
using ProjectManagement.Service;

namespace ProjectManagement.Controller;

[ApiController]
[Route("api/projects")]
public class ProjectController(IProjectService service) : ControllerBase
{
    private readonly IProjectService _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectController>>> GetAllProjectsAsync()
    {
        try
        {
            var projects = await _service.GetAllProjectAsync();

            return StatusCode(200, projects);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateProjectAsync([FromBody] CreateProjectContract contract)
    {
        if (!ModelState.IsValid)
            return StatusCode(400, ModelState);

        try
        {
            var id = await _service.CreateProjectAsync(contract);

            return StatusCode(200, id);
        }
        catch (ElementNotFoundException e)
        {
            return StatusCode(404, e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Project>> GetByIdAsync(Guid id)
    {
        try
        {
            var project = await _service.GetByIdAsync(id);

            return StatusCode(200, project);
        }
        catch (ElementNotFoundException e)
        {
            return StatusCode(404, e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Project>> UpdateProjectAsync(Guid id, [FromBody] UpdateProjectContract contract)
    {
        if (!ModelState.IsValid)
            return StatusCode(400, ModelState);

        try
        {
            var project = await _service.UpdateProjectAsync(id, contract);

            return StatusCode(200, project);
        }
        catch (ElementNotFoundException e)
        {
            return StatusCode(404, e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteByIdAsync(Guid id)
    {
        try
        {
            await _service.DeleteByIdAsync(id);

            return StatusCode(200);
        }
        catch (ElementNotFoundException e)
        {
            return StatusCode(404, e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id}/tasks")]
    public async Task<ActionResult<IEnumerable<Assignment>>> GetAllAssignments(Guid id)
    {
        try
        {
            var assignments = await _service.GetAllAssignments(id);

            return StatusCode(200, assignments);
        }
        catch (ElementNotFoundException e)
        {
            return StatusCode(404, e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Project>>> SearchProjectsAsync(
        [FromQuery] string? title = null,
        [FromQuery] Guid? manager = null)
    {
        try
        {
            IEnumerable<Project> projects;

            if (!string.IsNullOrEmpty(title))
            {
                projects = await _service.GetByTitleAsync(title);
            }
            else if (manager.HasValue)
            {
                projects = await _service.GetByManagerId(manager.Value);
            }
            else
            {
                return StatusCode(400, "No valid search parameters provided.");
            }

            return StatusCode(200, projects);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}
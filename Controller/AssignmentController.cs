using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Contracts;
using ProjectManagement.Exceptions;
using ProjectManagement.Model;
using ProjectManagement.Service;

namespace ProjectManagement.Controller;

[ApiController]
[Route("api/tasks")]
public class AssignmentController(IAssignmentService service) : ControllerBase
{
    private readonly IAssignmentService _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Assignment>>> GetAllAssignmentsAsync()
    {
        try
        {
            return StatusCode(200, await _service.GetAllAssignmentsAsync());
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Assignment>> CreateAssignmentAsync([FromBody] CreateAssignmentContract contract)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(400, ModelState);
        }
        
        try
        {
            var id = await _service.CreateAssignmentAsync(contract);

            return StatusCode(201, id);
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
    public async Task<ActionResult<Assignment>> GetByIdAsync(Guid id)
    {
        try
        {
            var assignment = await _service.GetByIdAsync(id);

            return StatusCode(200, assignment);
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
    public async Task<ActionResult<Assignment>> UpdateAssignmentAsync(Guid id,
        [FromBody] UpdateAssignmentContract contract)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(400, ModelState);
        }

        try
        {
            var assignment = await _service.UpdateAssignmentAsync(id, contract);

            return StatusCode(200, assignment);
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

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Assignment>>> SearchAssignmentsAsync(
        [FromQuery] string? title = null,
        [FromQuery] AssignmentState? status = null,
        [FromQuery] AssignmentPriority? priority = null,
        [FromQuery] Guid? assignee = null,
        [FromQuery] Guid? project = null)
    {
        try
        {
            IEnumerable<Assignment> assignments;

            if (!string.IsNullOrEmpty(title))
            {
                assignments = await _service.GetByTitleAsync(title);
            }
            else if (status.HasValue)
            {
                assignments = await _service.GetByStatusAsync(status.Value);
            }
            else if (priority.HasValue)
            {
                assignments = await _service.GetByPriorityAsync(priority.Value);
            }
            else if (assignee.HasValue)
            {
                assignments = await _service.GetByAssigneeAsync(assignee.Value);
            }
            else if (project.HasValue)
            {
                assignments = await _service.GetByProjectAsync(project.Value);
            }
            else
            {
                return StatusCode(400, "No valid search parameters provided.");
            }

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
}

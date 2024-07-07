using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Contracts;
using ProjectManagement.Exceptions;
using ProjectManagement.Model;
using ProjectManagement.Service;

namespace ProjectManagement.Controller
{
    [ApiController]
    [Route("api/users")]
    public class UserController(IUserService service) : ControllerBase
    {
        private readonly IUserService _service = service;
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsersAsync()
        {
            try
            {
                return StatusCode(200, await _service.GetAllUsersAsync());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateUserAsync([FromBody] CreateUserContract contract)
        {
            if (!ModelState.IsValid)
                return StatusCode(400, ModelState);

            try
            {
                var id = await _service.CreateUserAsync(contract);
                return StatusCode(201, id);
            }
            catch (ElementAlreadyExistsException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserByIdAsync(Guid id)
        {
            try
            {
                var user = await _service.GetUserByIdAsync(id);
                return StatusCode(200, user);
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
        public async Task<ActionResult<User>> UpdateUserAsync(Guid id, [FromBody] UpdateUserContract contract)
        {
            try
            {
                var user = await _service.UpdateUserAsync(id, contract);
                return StatusCode(200, user);
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
        public async Task<ActionResult<IEnumerable<Assignment>>> GetAllTasks(Guid id)
        {
            try
            {
                var assignments = await _service.GetAllTasks(id);
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
        public async Task<ActionResult<IEnumerable<User>>> SearchUsersAsync(
            [FromQuery] string? name = null,
            [FromQuery] string? email = null)
        {
            try
            {
                IEnumerable<User> users;

                if (!string.IsNullOrEmpty(name))
                {
                    users = await _service.FindByFullName(name);
                }
                else if (!string.IsNullOrEmpty(email))
                {
                    var user = await _service.FindByEmail(email);
                    users = new List<User> { user };
                }
                else
                {
                    return StatusCode(400, "No valid search parameters provided.");
                }

                return StatusCode(200, users);
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
}

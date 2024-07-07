using Microsoft.EntityFrameworkCore;
using ProjectManagement.Contracts;
using ProjectManagement.Exceptions;
using ProjectManagement.Model;

namespace ProjectManagement.Repository;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<Guid> CreateUserAsync(CreateUserContract contract)
    {
        if (await _context.Users.AnyAsync(u => u.Email.Equals(contract.Email)))
            throw new ElementAlreadyExistsException($"User with email {contract.Email} already exists");

        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = contract.FullName,
            Email = contract.Email,
            RegistrationDate = DateTime.UtcNow,
            Role = contract.Role
        };

        await _context.AddAsync(user);
        await _context.SaveChangesAsync();

        return user.Id;
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        return user ?? throw new ElementNotFoundException($"User with id {id} not found");
    }

    public async Task<User> UpdateUserAsync(Guid id, UpdateUserContract contract)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            throw new ElementNotFoundException($"User with id {id} not found");

        user.FullName = contract.FullName;
        
        user.Email = contract.Email;
        
        user.Role = contract.Role;

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        if (!await _context.Users.AnyAsync(u => u.Id == id))
            throw new ElementNotFoundException($"User with id {id} not found");

        await _context.Users
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<IEnumerable<Assignment>> GetAllTasks(Guid id)
    {
        if (!await _context.Users.AnyAsync(u => u.Id == id))
            throw new ElementNotFoundException($"User with id {id} not found");
        
        var assignments = await _context.Assignments
            .Where(a => a.AssigneeId == id)
            .ToListAsync();

        return assignments;
    }

    public async Task<IEnumerable<User>> FindByFullName(string fullName)
    {
        var users = await _context.Users
            .Where(u => u.FullName.Equals(fullName))
            .ToListAsync();

        return users ?? throw new ElementNotFoundException($"Users with fullname {fullName} not found");
    }

    public async Task<User> FindByEmail(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));

        return user ?? throw new ElementNotFoundException($"User with email {email} not found");
    }
}
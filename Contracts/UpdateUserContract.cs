using ProjectManagement.Model;

namespace ProjectManagement.Contracts;

public class UpdateUserContract(string fullName, string email, Roles role)
{
    public string FullName { get; } = fullName;

    public string Email { get; } = email;

    public Roles Role { get; } = role;
}
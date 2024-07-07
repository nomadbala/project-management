namespace ProjectManagement.Model;

public enum Roles
{
    Administrator = 0,
    Manager = 1,
    Developer = 2
}

public class User
{
    public Guid Id { get; set; }

    public string FullName { get; set; }
    
    public string Email { get; set; }

    public DateTime RegistrationDate { get; set; }
    
    public Roles Role { get; set; }
}
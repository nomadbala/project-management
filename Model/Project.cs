namespace ProjectManagement.Model;

public class Project
{
    public Guid Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    public Guid ManagerId { get; set; }
    
    public User Manager { get; set; }
}
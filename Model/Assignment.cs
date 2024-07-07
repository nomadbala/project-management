namespace ProjectManagement.Model;

public enum AssignmentPriority
{
    Low = 0,
    Medium = 1,
    High = 2
}

public enum AssignmentState
{
    New = 0,
    InProcess = 1,
    Done = 2
}

public class Assignment
{
    public Guid Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public AssignmentPriority Priority { get; set; }
    
    public AssignmentState State { get; set; }
    
    public Guid AssigneeId { get; set; }
    
    public User Assignee { get; set; }
    
    public Guid ProjectId { get; set; }
    
    public Project Project { get; set; }
    
    public DateTime CreationDate { get; set; }
    
    public DateTime? CompletionDate { get; set; }
}
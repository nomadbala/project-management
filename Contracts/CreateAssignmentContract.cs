using System.ComponentModel.DataAnnotations;
using ProjectManagement.Model;

namespace ProjectManagement.Contracts;

public class CreateAssignmentContract(string title, string description, AssignmentPriority priority, AssignmentState state, Guid assigneeId, Guid projectId)
{
    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; } = title;

    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; } = description;

    [Required(ErrorMessage = "Priority is required.")]
    public AssignmentPriority Priority { get;  } = priority;

    [Required(ErrorMessage = "State is required")]
    public AssignmentState State { get;  } = state;

    [Required(ErrorMessage = "Assignee is required.")]
    public Guid AssigneeId { get;  } = assigneeId;

    [Required(ErrorMessage = "Project is required.")]
    public Guid ProjectId { get;  } = projectId;
}
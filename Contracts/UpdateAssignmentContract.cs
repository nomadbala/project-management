using ProjectManagement.Model;

namespace ProjectManagement.Contracts;

public class UpdateAssignmentContract(
    string title,
    string description,
    AssignmentPriority priority,
    AssignmentState state,
    Guid assigneeId,
    Guid projectId,
    DateTime? completionDate)
{
    public string Title { get; } = title;

    public string Description { get; } = description;

    public AssignmentPriority Priority { get; } = priority;

    public AssignmentState State { get; } = state;

    public Guid AssigneeId { get; } = assigneeId;

    public Guid ProjectId { get; } = projectId;

    public DateTime? CompletionDate { get; } = completionDate;
}
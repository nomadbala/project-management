namespace ProjectManagement.Contracts;

public class UpdateProjectContract(string title, string description, DateTime? endDate, Guid managerId)
{
    public string Title { get; } = title;

    public string Description { get; } = description;

    public DateTime? EndDate { get; } = endDate;

    public Guid ManagerId { get; } = managerId;
}
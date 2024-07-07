using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Contracts;

public class CreateProjectContract(string title, string description, Guid managerId)
{
    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; } = title;

    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; } = description;

    [Required(ErrorMessage = "Manager is required.")]
    public Guid ManagerId { get; } = managerId;
}
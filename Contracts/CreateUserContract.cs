using System.ComponentModel.DataAnnotations;
using ProjectManagement.Model;
using System.Text.Json.Serialization;

namespace ProjectManagement.Contracts
{
    public class CreateUserContract
    {
        [Required(ErrorMessage = "Full name is required.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Roles Role { get; set; }
    }
}

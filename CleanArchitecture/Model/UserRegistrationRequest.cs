using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace CleanArchitecture.Model

{
    public class UserRegistrationRequest
    {
        [Required(ErrorMessage = "Name is mandatory.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Email is mandatory.")]
        [EmailAddress(ErrorMessage ="Email is not valid.")]
        public required string Email { get; set; }
    }
}

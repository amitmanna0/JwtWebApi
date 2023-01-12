using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAPIProject.Models
{
    public class UserCredential
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;
        
        [PasswordPropertyText]
        public string Password { get; set; } = string.Empty;
    }
}

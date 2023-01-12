using Microsoft.Build.Framework;
using System.ComponentModel;

namespace WebAPIProject.Models
{
    public class User
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [PasswordPropertyText]
        public string Password { get; set; } = string.Empty;
       
    }
}

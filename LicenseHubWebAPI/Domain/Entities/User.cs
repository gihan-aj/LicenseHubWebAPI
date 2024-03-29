using System.ComponentModel.DataAnnotations;

namespace LicenseHubWebAPI.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string UserEmail { get; set; } = string.Empty;

        [Required]
        public string PasswordHashed { get; set; } = string.Empty;
    }
}

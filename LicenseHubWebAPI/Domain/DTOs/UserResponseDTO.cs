using System.Reflection.Metadata;

namespace LicenseHubWebAPI.Domain.DTOs
{
    public class UserResponseDTO
    {
        public required string UserEmail { get; set; }
        public string Username { get; set; } = string.Empty;
        public string SessionToken { get; set; } = string.Empty;
    }
}

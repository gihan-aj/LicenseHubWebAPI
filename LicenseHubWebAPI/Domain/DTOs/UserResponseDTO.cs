using System.Reflection.Metadata;

namespace LicenseHubWebAPI.Domain.DTOs
{
    public class UserResponseDTO
    {
        public required string UserName { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

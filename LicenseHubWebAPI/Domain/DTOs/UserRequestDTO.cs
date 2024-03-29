namespace LicenseHubWebAPI.Domain.DTOs
{
    public class UserRequestDTO
    {
        public required string UserEmail { get; set; }
        public required string Password { get; set; }
    }
}

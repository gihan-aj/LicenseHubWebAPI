namespace LicenseHubWebAPI.Domain.DTOs
{
    public class UserRegisterDTO
    {
        public required string UserName { get; set; }
        public required string UserEmail { get; set; }
        public required string Password { get; set; }
    }
}

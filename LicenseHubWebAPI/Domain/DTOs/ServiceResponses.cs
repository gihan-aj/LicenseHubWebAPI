namespace LicenseHubWebAPI.Domain.DTOs
{
    public class ServiceResponses
    {
        public record class TestResponse(bool flag, string message);
        public record class LoginResponse(bool flag,string message, string? token, UserResponseDTO? user);
        public record class LogoutResponse(bool flag,string message);
        public record class VerificationResponse(bool flag,string message);

    }
}

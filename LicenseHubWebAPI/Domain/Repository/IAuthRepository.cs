using LicenseHubWebAPI.Domain.DTOs;
using LicenseHubWebAPI.Domain.Entities;

namespace LicenseHubWebAPI.Domain.Repository
{
    public interface IAuthRepository
    {
        Task<UserResponseDTO?> Register(UserRegisterDTO request);
        Task<UserResponseDTO?> Login(UserRequestDTO request);
        Task<UserResponseDTO?> GetUser(string userEmail);
    }
}

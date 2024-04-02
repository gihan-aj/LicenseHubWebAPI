using Azure.Core;
using LicenseHubWebAPI.Domain.DTOs;
using LicenseHubWebAPI.Domain.Entities;
using LicenseHubWebAPI.Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static LicenseHubWebAPI.Domain.DTOs.ServiceResponses;

namespace LicenseHubWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register([FromBody]UserRegisterDTO request)
        {
            try
            {
                var user = await _authRepository.Register(request);

                if (user == null)
                {
                    return BadRequest("User is not created.");
                }

                return CreatedAtAction(nameof(GetUser), new { userEmail = user.UserName }, user);
                //return Ok(user);

            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while processing the request.\n{e}");
            }
        }

        [AllowAnonymous]
        [HttpGet("User")]
        public async Task<ActionResult<UserResponseDTO>> GetUser(string userEmail)
        {
            try
            {
                var user = await _authRepository.GetUser(userEmail);
                if (user == null)
                {
                    return NotFound("Account not found.");
                }

                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while processing the request.\n{e}");
            }

        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login(UserRequestDTO request)
        {
            try
            {
                //var user = await _authRepository.Login(request);
                //if(user == null)
                //{
                //    return BadRequest("Username or password is wrong.");
                //}
                var user = new UserResponseDTO() { UserName = request.UserEmail, Name = "Admin"};


                return Ok(new LoginResponse(true,"Login successful", "1234567890", user));

            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while processing the request.\n{e}");
            }
        }
    }
}

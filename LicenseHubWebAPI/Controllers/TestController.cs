using LicenseHubWebAPI.Domain.DTOs;
using LicenseHubWebAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static LicenseHubWebAPI.Domain.DTOs.ServiceResponses;

namespace LicenseHubWebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private List<User> _users = new List<User>();
        private string _token = string.Empty;
        private List<Client> _clients = new List<Client>();
        private List<Package> _packages = new List<Package>();
        public TestController()
        {
            _users.Add(new User()
            {
                Id = 1,
                UserEmail = "user01@email.com",
                Username = "User01",
                PasswordHashed = "1234"
            });
            _users.Add(new User()
            {
                Id = 2,
                UserEmail = "user02@email.com",
                Username = "User02",
                PasswordHashed = "1234"
            });

            _token = "hduikKhuGj56l4l36lsKK";

            _clients.Add(new Client { ClientCode = "C123", ClientName = "ABC Corporation" });
            _clients.Add(new Client { ClientCode = "C456", ClientName = "XYZ Ltd." });
            _clients.Add(new Client { ClientCode = "C789", ClientName = "123 Industries" });
            _clients.Add(new Client { ClientCode = "C999", ClientName = "Acme Corp" });

            _packages.Add(new Package { PackageCode = "BMS001", PackageName = "Temperature Control", Description = "Controls heating and cooling systems for optimal temperature management." });
            _packages.Add(new Package { PackageCode = "BMS002", PackageName = "Security System", Description = "onitors and controls access, surveillance, and alarms for building security." });
            _packages.Add(new Package { PackageCode = "BMS003", PackageName = "Lighting Control", Description = "Manages lighting systems to enhance energy efficiency and occupant comfort." });
            _packages.Add(new Package { PackageCode = "BMS004", PackageName = "HVAC System", Description = "Regulates heating, ventilation, and air conditioning for optimal indoor air quality." });
            _packages.Add(new Package { PackageCode = "BMS005", PackageName = "Energy Monitoring", Description = "Tracks and optimizes energy consumption to reduce costs and environmental impact." });


        }
        [HttpGet("test")]
        public TestResponse Test()
        {
            return new TestResponse(true, "Connection successful.");
        }

        [HttpPost("login")]
        public LoginResponse Login([FromBody]UserRequestDTO request)
        {
            var user = _users.Find(u => u.UserEmail == request.UserEmail);
            if(user == null)
            {
                return new LoginResponse(false, "User not found",null, null);
            }
            else
            {
                if(user.PasswordHashed == request.Password)
                {
                    var userDetails = new UserResponseDTO() { UserName = user.UserEmail, Name = user.Username };
                    return new LoginResponse(true, "Login successful",_token, userDetails);
                }
                else
                {
                    return new LoginResponse(false, "Wrong password",null, null);
                }
            }
        }

        [HttpGet("logout")]
        public LogoutResponse Logout()
        {
            _token = string.Empty;
            return new LogoutResponse(true, "Logout successful");
        }

        [HttpGet("verify-token/{token}")]
        public ActionResult<VerificationResponse> VerifyToken(string token)
        {
            if(token == _token)
            {
                return Ok(new VerificationResponse(true, "Verfivation successful"));
            }
            else
            {
                return Unauthorized( new VerificationResponse(false, "Verfivation failed"));
            }
        }

        [HttpGet("clients")]
        public ActionResult<List<Client>> GetClients()
        {
            return Ok(_clients);
        }

        [HttpGet("packages")]
        public ActionResult<List<Package>> GetPackages()
        {
            return Ok(_packages);
        }
    }
}

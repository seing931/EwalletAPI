using EWallet.Api.Models.Dtos;
using EWallet.Api.Models.Entities;
using EWallet.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;


namespace EWallet.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var user = new User
                {
                    Username = request.username,
                    FullName = request.full_name,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.password),
                    MobileNumber = request.Mobile,
                    SourceMercId = request.source_merchant_id,
                    Gender = request.gender,
                    IdentificationType = request.identification_type,
                    IdentificationId = request.identification_id
                };

                var result = await _userService.RegisterUserAsync(user, request.password);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Register API failed | Username: {Username} | NRIC: {NRIC} | Error: {ErrorMessage}",request.username,request.identification_id,ex.Message);
                return BadRequest(new{success = false,message = ex.Message});
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var token = await _userService.LoginUserAsync(request.username, request.password, request.source_merchant_id);
                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login API failed | Username: {Username} | Error: {ErrorMessage}", request.username, ex.Message);
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("profile/{token}")]
        public async Task<IActionResult> Profile(string token)
        {
            try
            {
                var user = await _userService.GetProfileAsync(token);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Retrieve profile failed | token: {token} | Error: {ErrorMessage}", token, ex.Message);
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("profile/{token}")]
        public async Task<IActionResult> UpdateProfile(string token, [FromBody] ProfileRequest request)
        {
            try
            {
                var myProfile = new ProfileLog
                {
                    FullName = request.full_name,
                    MobileNumber = request.mobile_number,
                    Email = request.email,
                    DateOfBirth = request.date_of_birth,
                    UserRace = request.user_race,
                    Address = request.address,
                    City = request.city,
                    State = request.state,
                    Postcode = request.postcode,
                    Country = request.country,
                    FaxNumber= request.fax_number,
                    Otp = request.otp,
                    Token = token
                };

                var user = await _userService.UpdateProfileAsync(token, myProfile);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update profile failed | token: {token} | Error: {ErrorMessage}", token, ex.Message);
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}

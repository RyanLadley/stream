using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ReelStream.auth.Enum;
using ReelStream.auth.Logic;
using ReelStream.auth.Logic.Interfaces;
using ReelStream.auth.Models.Buisness;
using ReelStream.auth.Models.DataTransfer.Form;
using ReelStream.auth.Settings;
using ReelStream.data.Repositories.IRepositories;
using ReelStream.auth.Models.DataTransfer.Response;
using ReelStream.core.Models.DataTransfer.Response;

namespace ReelStream.api.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
        
        IUserRegistrar _registrar;
        IOptions<AuthSettings> _settings;
        IUserRepository _userRepository;
        IVideoFileRepository _videFileRepository;

        public UserController(IUserRegistrar userRegist, IUserRepository userRepo, IVideoFileRepository videoRepo, IOptions<AuthSettings> settings)
        {
            _registrar = userRegist;
            _settings = settings;
            _userRepository = userRepo;
            _videFileRepository = videoRepo;
        }


        [HttpGet("settings")]
        [Authorize(Policy = "GeneralUser")]
        public IActionResult Get()
        {
            var userId = TokenManager.ExtractUserId(User.Claims);
            var user = _userRepository.Get(userId);
            var fileSize = _videFileRepository.TotalFileSizeForUser(userId);

            var userSettingsResponse = UserSettingsResponse.MapFromObject(user, fileSize);
            return Ok(userSettingsResponse);
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegistrationForm registration)
        {

            var registrationStatus = _registrar.RegisterUser(registration);

            if (registrationStatus == AuthStatus.InvalidPassword)
                return BadRequest("The provided password is in an invalid format");

            return Login(new LoginCredentialsForm {Username = registration.Username, Passphrase = registration.Passphrase });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginCredentialsForm credentials)
        {
            var user = _registrar.ValidateLogin(credentials);

            if(user == null)
                return BadRequest(new ErrorResponse("Incorect Username or Password"));

            TokenManager tokenizer = new TokenManager();
            var response = tokenizer.CreateToken(user, _settings.Value.TokenOptions);

            return Ok(response);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok();
        }

        /// <summary>
        /// Serves to verify a user is still logged in and refreshes their token
        /// </summary>
        /// <returns></returns>
        [HttpGet("token")]
        [Authorize(Policy = "GeneralUser")]
        public IActionResult ValidateToken()
        {
            var userId = TokenManager.ExtractUserId(User.Claims);
            var user = _userRepository.Get(userId);

            if (user == null)
                return BadRequest();
            
            TokenManager tokenizer = new TokenManager();
            var response = tokenizer.CreateToken(user, _settings.Value.TokenOptions);
            return Ok(response);
        }

    }
}

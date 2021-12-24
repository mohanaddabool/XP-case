using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using XPAssignment.authentication;
using XPAssignment.PostData.Login;

namespace XPAssignment.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IAuthentication Authentication { get; }

        public LoginController(IAuthentication authentication)
        {
            Authentication = authentication;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "please use test@test.nl as email address" +
                                    "123 as password")]
        public IActionResult Login([FromBody] Login login)
        {
            var token = Authentication.Authenticate(login.EmailAddress, login.Password);
            if (string.IsNullOrWhiteSpace(token))
            {
                return Unauthorized();
            }

            return Ok($"Bearer {token}");
        }

    }
}

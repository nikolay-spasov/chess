namespace Chess.Web.Controllers
{
    using System;
    using System.Web.Http;

    using Chess.Core.Authentication;
    using Chess.Core.Logging;
    using Chess.Web.Models;
    using Chess.Web.App_Start;

    public class AuthenticationController : ApiController
    {
        private readonly IUserManager _userManager;
        //private readonly ILogger _logger;

        public AuthenticationController(IUserManager userManager)// ILogger logger)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }

            //if (logger == null)
            //{
            //    throw new ArgumentNullException("logger");
            //}

            _userManager = userManager;
            //_logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/authentication/login")]
        public IHttpActionResult Login(LoginModel credentials)
        {
            if (credentials == null) return BadRequest("Invalid Data");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = _userManager.Validate(credentials.Username, credentials.Password);

            if (user == null) //todo: try moving to resource files
            {
                ModelState.AddModelError("Error", "Wrong username or password");
                return BadRequest(ModelState);
            }

            var ticket = _userManager.GetAuthenticationTicket(user, Startup.OAuthOptions.AuthenticationType);
            var token = Startup.OAuthOptions.AccessTokenFormat.Protect(ticket);
            return Ok(new
            {
                Token = token,
                UserId = user.Id
            });
        }
    }
}

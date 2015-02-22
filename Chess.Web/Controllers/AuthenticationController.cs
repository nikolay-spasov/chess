using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Chess.Core.Authentication;
using Chess.Core.Logging;
using Chess.Web.Models;

namespace Chess.Web.Controllers
{
    public class AuthenticationController : ApiController
    {
        private readonly IUserManager _userManager;
        private readonly ILogger _logger;

        public AuthenticationController(IUserManager userManager, ILogger logger)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }

            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            _userManager = userManager;
            _logger = logger;
        }

        public IHttpActionResult Login(LoginModel credentials)
        {
            _logger.Log("Logging example!");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _userManager.Validate(credentials.Username, credentials.Password);

            if (user == null)
            {
                return BadRequest("Wrong username or password");
            }

            var token = user.GetAuthenticationToken();

            return Ok(new { Token = token });
        }
    }
}

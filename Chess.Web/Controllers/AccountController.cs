namespace Chess.Web.Controllers
{
    using System;
    using System.Web.Http;

    using Chess.Core.Repository;
    using Chess.Web.Models;

    public class AccountController : ApiController
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException("userRepository");
            }

            _userRepository = userRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/account/register")]
        public IHttpActionResult Register(RegisterModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                ModelState.AddModelError("error", "Entered incorrect data.");
                return BadRequest(ModelState);
            }

            try
            {
                var userByUsername = _userRepository.GetByUsername(model.Username);
                if (userByUsername != null)
                {
                    ModelState.AddModelError("error", "User already exists.");
                    return BadRequest(ModelState);
                }

                var createdUser = _userRepository.CreateUser(model.Username, model.Password, model.Email);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Route("api/check-unique-email")]
        [AllowAnonymous]
        public IHttpActionResult CheckUniqueEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Ok(false);
            }

            try
            {
                var isUnique = _userRepository.IsEmailExists(email);
                return Ok(isUnique);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

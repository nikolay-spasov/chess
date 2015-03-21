namespace Chess.Web.Controllers
{
    using System;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;

    using Chess.Core.Repository;
    using Chess.Web.Models;

    public class UserController : ApiController
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException("userRepository");
            }

            _userRepository = userRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/user/{id}")]
        public IHttpActionResult GetUserById(int id)
        {
            try
            {
                var userId = this.User.Identity.GetUserId();
                if (userId != id.ToString())
                {
                    ModelState.AddModelError("error", "Forbidden access to other users information!");
                    return BadRequest(ModelState);
                }

                var currentUser = _userRepository.GetById(userId);

                if (currentUser == null)
                {
                    ModelState.AddModelError("error", "User does not exist.");
                    return BadRequest(ModelState);
                }

                return Ok(currentUser);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/user/{id}")]
        public IHttpActionResult UpdateUser(int id, UserModel model)
        {
            try
            {
                var userId = this.User.Identity.GetUserId();
                if (userId != id.ToString())
                {
                    ModelState.AddModelError("error", "Forbidden access to other users information!");
                    return BadRequest(ModelState);
                }

                if (model == null)
                {
                    return BadRequest("Invalid user information.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var currentUser = _userRepository.GetById(userId);
                if (currentUser == null)
                {
                    ModelState.AddModelError("error", "User does not exist!");
                    return BadRequest(ModelState);
                }

                //_userRepository.UpdateUser(id, model.FirstName, model.LastName)

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

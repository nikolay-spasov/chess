namespace Chess.Web.Controllers
{
    using System;
    using System.Web.Http;

    using Chess.Core.Repository;
    using Chess.Web.Models;

    public class GameController : ApiController
    {
        private readonly IGameRepository _gameRepository;

        public GameController(IGameRepository gameRepository)
        {
            if (gameRepository == null)
            {
                throw new ArgumentNullException("gameRepository");    
            }

            _gameRepository = gameRepository;
        }

        [HttpPost]
        [Route("api/searchgame")]
        public IHttpActionResult SearchGame(SearchGameModel model)
        {
            return Ok();
        }
    }
}

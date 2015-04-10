namespace Chess.Infrastructure.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Dapper;

    using Chess.Core.Models;
    using Chess.Core.Repository;
    using Chess.Infrastructure.Database;
    using Chess.Infrastructure.Database.Entities;
    
    public class GameRepository : IGameRepository
    {
        private readonly IDatabaseConnectionProvider _connectionProvider;

        public GameRepository(IDatabaseConnectionProvider connectionProvider)
        {
            if (connectionProvider == null)
            {
                throw new ArgumentNullException("connectionProvider");
            }

            _connectionProvider = connectionProvider;
        }

        public IEnumerable<Game> GetAll()
        {
            IEnumerable<DbGame> games = null;
            using (var connection = _connectionProvider.GetOpenConnection())
            {
                games = connection.Query<DbGame>("select * from DbGames");
            }

            return Mapper.Map<IEnumerable<Game>>(games);
        }

        public Game GetById(object id)
        {
            DbGame game = null;
            using (var connection = _connectionProvider.GetOpenConnection())
            {
                game = connection.Query<DbGame>("select * from DbGames where Id = @id", new { id = id }).First();
            }

            return Mapper.Map<Game>(game);
        }
    }
}

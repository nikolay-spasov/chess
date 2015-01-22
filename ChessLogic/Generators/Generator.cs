namespace ChessLogic.Generators
{
    using System;
    using System.Collections.Generic;

    public abstract class Generator
    {
        protected Generator(Game game)
        {
            if (game == null)
            {
                throw new ArgumentNullException("game");
            }

            this.Game = game;
        }

        public abstract HashSet<Move> GenerateFor(int row, int col);

        protected Game Game { get; set; }
    }
}

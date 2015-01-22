namespace ChessLogic.Generators
{
    using System;
    using System.Collections.Generic;

    public class KnightGenerator : Generator
    {
        private static readonly int[] rows = { -1, -2, -2, -1, 1, 2, 2, 1 };
        private static readonly int[] cols = { -2, -1, 1, 2, 2, 1, -1, -2 };

        public KnightGenerator(Game game)
            : base(game)
        {

        }

        public override HashSet<Move> GenerateFor(int row, int col)
        {
            this.Game.EnsurePositionIsOnTheBoard(row, col);

            var moves = new HashSet<Move>();

            for (int i = 0; i < rows.Length; i++)
            {
                var currentRow = row + rows[i];
                var currentCol = col + cols[i];

                if (this.Game.IsOnTheBoard(currentRow, currentCol))
                {
                    if (!Game.IsAllyPiece(currentRow, currentCol))
                    {
                        var move = new Move(row, col, currentRow, currentCol);

                        moves.Add(move);
                    }
                }
            }

            return moves;
        }
    }
}

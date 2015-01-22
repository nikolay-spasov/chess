namespace ChessLogic.Generators
{
    using System.Collections.Generic;

    public class DiagonalGenerator : Generator
    {
        private static readonly int[] rows = { -1, -1, 1, 1 };
        private static readonly int[] cols = { -1, 1, -1, 1 };

        public DiagonalGenerator(Game game)
            : base(game)
        {

        }

        public override HashSet<Move> GenerateFor(int row, int col)
        {
            this.Game.EnsurePositionIsOnTheBoard(row, col);

            var moves = new HashSet<Move>();

            for (int i = 0; i < 4; i++)
            {
                int r = rows[i];
                int c = cols[i];

                var currentRow = row;
                var currentCol = col;

                while (true)
                {
                    currentRow += r;
                    currentCol += c;
                    if (this.Game.IsOnTheBoard(currentRow, currentCol))
                    {
                        if (Game.IsEmpty(currentRow, currentCol))
                        {
                            var move = new Move(row, col, currentRow, currentCol);

                            moves.Add(move);
                        }
                        else if (Game.IsEnemyPieceAt(currentRow, currentCol))
                        {
                            var move = new Move(row, col, currentRow, currentCol);

                            moves.Add(move);
                            break;
                        }
                        else break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return moves;
        }
    }
}

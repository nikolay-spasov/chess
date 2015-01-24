namespace ChessLogic.Generators
{
    using System.Collections.Generic;

    public class KingGenerator : Generator
    {
        private int initialRow;
        private int initialCol = 4;
        private int kingSideCol = 6;
        private int queenSideCol = 2;

        private char allyRook;

        public KingGenerator(Game game)
            :base(game)
        {
            SetupInitials();
        }

        public override HashSet<Move> GenerateFor(int row, int col)
        {
            var moves = new HashSet<Move>();
            for (int r = -1; r <= 1; r++)
            {
                for (int c = -1; c <= 1; c++)
                {
                    if (r == 0 && c == 0)
                    {
                        continue;
                    }

                    int currentRow = row + r;
                    int currentCol = col + c;

                    if (this.Game.IsOnTheBoard(currentRow, currentCol))
                    {
                        if (!Game.IsAllyPiece(currentRow, currentCol))
                        {
                            var moveInfo = new MoveInfo
                            {
                                KingMoved = true
                            };

                            var move = new Move(row, col, currentRow, currentCol, moveInfo);

                            moves.Add(move);
                        }
                    }
                }
            }

            moves.UnionWith(AddCastles(row, col));

            return moves;
        }

        public HashSet<Move> GenerateFor(int row, int col, bool includeCastles)
        {
            var result = GenerateFor(row, col);
            result.ExceptWith(AddCastles(row, col));

            return result;
        }

        private HashSet<Move> AddCastles(int row, int col)
        {
            var moves = new HashSet<Move>();
            if (row == initialRow && col == initialCol)
            {
                if (Game.CanCastleKingSide && 
                    Game.IsEmpty(initialRow, initialCol + 1) &&
                    Game.IsEmpty(initialRow, initialCol + 2) &&
                    Game.GetPieceAt(initialRow, 7) == allyRook)
                {
                    var moveInfo = new MoveInfo
                    {
                        KingSideCastle = true,
                    };

                    var move = new Move(row, col, initialRow, kingSideCol, moveInfo);

                    moves.Add(move);
                }

                if (Game.CanCastleQueenSide &&
                    Game.IsEmpty(initialRow, initialCol - 1) &&
                    Game.IsEmpty(initialRow, initialCol - 2) &&
                    Game.GetPieceAt(initialRow, 0) == allyRook)
                {
                    var moveInfo = new MoveInfo
                    {
                        QueenSideCastle = true,
                    };

                    var move = new Move(row, col, initialRow, queenSideCol, moveInfo);

                    moves.Add(move);
                }
            }

            return moves;
        }

        private void SetupInitials()
        {
            if (Game.WhiteTurn)
            {
                initialRow = 7;
                allyRook = BoardConstants.WhiteRook;
            }
            else
            {
                initialRow = 0;
                allyRook = BoardConstants.BlackRook;
            }
        }
    }
}

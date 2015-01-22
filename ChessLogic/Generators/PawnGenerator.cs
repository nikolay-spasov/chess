namespace ChessLogic.Generators
{
    using System.Collections.Generic;

    public class PawnGenerator : Generator
    {
        private int initialRow;
        private int enPassantRow;
        private int direction;
        private char enemyPawn;

        public PawnGenerator(Game game)
            : base(game)
        {
            SetupInitials();
        }

        public override HashSet<Move> GenerateFor(int row, int col)
        {
            var moves = new HashSet<Move>();

            var destRow = row + direction;
            if (Game.IsOnTheBoard(destRow, col) &&
                Game.IsEmpty(destRow, col))
            {
                var move = new Move(row, col, destRow, col);

                moves.Add(move);
            }

            if (row == initialRow &&
                Game.IsEmpty(destRow, col) &&
                Game.IsEmpty(destRow + direction, col))
            {
                var move = new Move(row, col, destRow + direction, col);

                moves.Add(move);
            }

            if (Game.IsOnTheBoard(destRow, col - 1) &&
                Game.IsEnemyPieceAt(destRow, col - 1))
            {
                var move = new Move(row, col, destRow, col - 1);

                moves.Add(move);
            }

            if (Game.IsOnTheBoard(destRow, col + 1) &&
                Game.IsEnemyPieceAt(destRow, col + 1))
            {
                var move = new Move(row, col, destRow, col + 1);

                moves.Add(move);
            }

            AddEnPassantMoves(moves, row, col);

            return moves;
        }

        private void AddEnPassantMoves(HashSet<Move> moves, int row, int col)
        {
            var destRow = row + direction;
            if (row == enPassantRow &&
                Game.IsOnTheBoard(destRow, col - 1) &&
                Game.IsEmpty(destRow, col - 1) &&
                Game.GetPieceAt(destRow, col) == enemyPawn)
            {
                var info = new MoveInfo
                {
                    EnPassant = true
                };

                var move = new Move(row, col, destRow, col - 1, info);
            }

            if (row == enPassantRow &&
                Game.IsOnTheBoard(destRow, col + 1) &&
                Game.IsEmpty(destRow, col + 1) &&
                Game.GetPieceAt(destRow, col) == enemyPawn)
            {
                var info = new MoveInfo
                {
                    EnPassant = true
                };

                var move = new Move(row, col, destRow, col + 1, info);
            }
        }

        private void SetupInitials()
        {
            if (Game.WhiteTurn)
            {
                initialRow = 6;
                enPassantRow = 3;
                direction = -1;
                enemyPawn = BoardConstants.BlackPawn;
            }
            else
            {
                initialRow = 1;
                enPassantRow = 4;
                direction = 1;
                enemyPawn = BoardConstants.WhitePawn;
            }
        }
    }
}

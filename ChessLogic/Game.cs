namespace ChessLogic
{
    using System;
    using System.Linq;

    using ChessLogic.Generators;

    public class Game
    {
        public Game(string board, bool whiteTurn, bool canCastleKingSide, bool canCastleQueenSide, Move lastMove)
        {
            this.Board = board;
            this.WhiteTurn = whiteTurn;
            this.CanCastleKingSide = canCastleKingSide;
            this.CanCastleQueenSide = canCastleQueenSide;
            this.LastMove = lastMove;
        }

        public char GetPieceAt(int row, int col)
        {
            EnsurePositionIsOnTheBoard(row, col);

            return Board[ToStringIndex(row, col)];
        }

        public bool IsEnemyPieceAt(int row, int col)
        {
            EnsurePositionIsOnTheBoard(row, col);

            char piece = GetPieceAt(row, col);

            if (piece != BoardConstants.Empty)
            {
                if (WhiteTurn && Char.IsLower(piece))
                {
                    return true;
                }

                if (!WhiteTurn && Char.IsUpper(piece))
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsEmpty(int row, int col)
        {
            EnsurePositionIsOnTheBoard(row, col);

            return Board[ToStringIndex(row, col)] == BoardConstants.Empty;
        }

        public bool IsAllyPiece(int row, int col)
        {
            EnsurePositionIsOnTheBoard(row, col);

            char piece = GetPieceAt(row, col);

            if (piece != BoardConstants.Empty)
            {
                if (WhiteTurn && Char.IsUpper(piece))
                {
                    return true;
                }

                if (!WhiteTurn && Char.IsLower(piece))
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsOnTheBoard(int row, int col)
        {
            return row >= 0 && row < 8 && col >= 0 && col < 8;
        }

        public bool FieldIsUnderAttack(int row, int col)
        {
            EnsurePositionIsOnTheBoard(row, col);

            var result = new LineGenerator(this).GenerateFor(row, col)
                .Any(x => GetPieceAt(x.Destination.Row, x.Destination.Col) == OpponentRook() || 
                    GetPieceAt(x.Destination.Row, x.Destination.Col) == OpponentQueen());
            var s = new DiagonalGenerator(this).GenerateFor(row, col);
            result = result || s.Any(x => GetPieceAt(x.Destination.Row, x.Destination.Col) == OpponentBishop() ||
                        GetPieceAt(x.Destination.Row, x.Destination.Col) == OpponentQueen());
            result = result || new PawnGenerator(this).GenerateFor(row, col).Any(x => GetPieceAt(x.Destination.Row, x.Destination.Col) == OpponentPawn());
            result = result || new KnightGenerator(this).GenerateFor(row, col).Any(x => GetPieceAt(x.Destination.Row, x.Destination.Col) == OpponentKnight());;
            result = result || new KingGenerator(this).GenerateFor(row, col).Any(x => GetPieceAt(x.Destination.Row, x.Destination.Col) == OpponentKing());

            return result;
        }

        public void EnsurePositionIsOnTheBoard(int row, int col)
        {
            if (row < 0 || row >= 8)
            {
                throw new IndexOutOfRangeException("row");
            }

            if (col < 0 || col >= 8)
            {
                throw new IndexOutOfRangeException("col");
            }
        }

        public Game Clone()
        {
            var game = this.MemberwiseClone() as Game;
            if (this.LastMove != null)
            {
                game.LastMove = this.LastMove.Clone();
            }

            return game;
        }

        private int ToStringIndex(int row, int col)
        {
            EnsurePositionIsOnTheBoard(row, col);

            return row * 8 + col;
        }

        private char OpponentQueen()
        {
            return WhiteTurn ? BoardConstants.BlackQueen : BoardConstants.WhiteQueen;
        }

        private char OpponentRook()
        {
            return WhiteTurn ? BoardConstants.BlackRook : BoardConstants.WhiteRook;
        }

        private char OpponentBishop()
        {
            return WhiteTurn ? BoardConstants.BlackBishop : BoardConstants.WhiteBishop;
        }

        private char OpponentPawn()
        {
            return WhiteTurn ? BoardConstants.BlackPawn : BoardConstants.WhitePawn;
        }

        private char OpponentKnight()
        {
            return WhiteTurn ? BoardConstants.BlackKnight : BoardConstants.WhiteKnight;
        }

        private char OpponentKing()
        {
            return WhiteTurn ? BoardConstants.BlackKing : BoardConstants.WhiteKing;
        }

        #region Properties

        public string Board { get; set; }

        public bool WhiteTurn { get; set; }

        public bool CanCastleKingSide { get; set; }

        public bool CanCastleQueenSide { get; set; }

        public Move LastMove { get; set; }

        #endregion
    }
}

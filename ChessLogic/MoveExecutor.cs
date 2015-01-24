namespace ChessLogic.Generators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class MoveExecutor
    {
        private Game game;
        private AllMovesGenerator generator;
        private Game backup;

        public MoveExecutor(Game game)
        {
            if (game == null)
            {
                throw new ArgumentNullException("game");
            }

            this.game = game;
            this.backup = game.Clone();
            this.generator = new AllMovesGenerator(game);
        }

        public Game ExecuteMove(InputMove move)
        {
            if (move == null)
            {
                throw new ArgumentNullException("move");
            }

            var m = GenerateCorrespondingBoardMove(move);

            if (m != null)
            {
                return Execute(m);
            }
            else
            {
                return game.Clone();
            }
        }

        public bool HasValidMoves(Game game)
        {
            var validMoves = generator.Generate();

            foreach (var move in validMoves)
            {
                var b = false;
                var g = Execute(move);

                if (g.WhiteTurn != game.WhiteTurn)
                {
                    return true;
                }
            }

            return false;
        }

        private Game Execute(Move move)
        {
            // TODO: Refactor this

            var game = this.game.Clone();
            char sourcePiece = game.GetPieceAt(move.Source.Row, move.Source.Col);

            var builder = new StringBuilder(game.Board);

            if (move.Info == null)
            {
                if (CanPromotePiece(game, move))
                {
                    MovePiece(move, sourcePiece, builder);
                    builder[move.Destination.Row * 8 + move.Destination.Col] = GetPromotionPiece(game, move);
                    game.Board = builder.ToString();
                }
                else
                {
                    MovePiece(move, sourcePiece, builder);
                    game.Board = builder.ToString();
                }
            }
            else if (move.Info.PromotePiece.HasValue)
            {
                MovePiece(move, sourcePiece, builder);
                builder[move.Destination.Row * 8 + move.Destination.Col] = GetPromotionPiece(game, move);
                game.Board = builder.ToString();
            }
            else if (move.Info.EnPassant &&
                game.LastMove != null &&
                game.LastMove.EnPassantPossible)
            {
                MovePiece(move, sourcePiece, builder);
                builder[game.LastMove.Destination.Row * 8 + game.LastMove.Destination.Col] = BoardConstants.Empty;
                game.Board = builder.ToString();
            }
            else if (move.Info.KingMoved)
            {
                if (!game.FieldIsUnderAttack(move.Destination.Row, move.Destination.Col))
                {
                    game.CanCastleKingSide = false;
                    game.CanCastleQueenSide = false;
                    MovePiece(move, sourcePiece, builder);
                    game.Board = builder.ToString();
                }
                else
                {
                    return backup.Clone();
                }
            }
            else if (move.Info.KingSideCastle)
            {
                if (!game.FieldIsUnderAttack(move.Source.Row, move.Source.Col) &&
                    !game.FieldIsUnderAttack(move.Source.Row, move.Source.Col + 1) &&
                    !game.FieldIsUnderAttack(move.Source.Row, move.Source.Col + 2))
                {
                    builder[move.Source.Row * 8 + 7] = BoardConstants.Empty;
                    builder[move.Destination.Row * 8 + 5] = GetAllyRook();

                    game.CanCastleKingSide = false;
                    game.CanCastleQueenSide = false;

                    MovePiece(move, sourcePiece, builder);
                    game.Board = builder.ToString();
                }
            }
            else if (move.Info.QueenSideCastle)
            {
                if (!game.FieldIsUnderAttack(move.Source.Row, move.Source.Col) &&
                    !game.FieldIsUnderAttack(move.Source.Row, move.Source.Col - 1) &&
                    !game.FieldIsUnderAttack(move.Source.Row, move.Source.Col - 2))
                {
                    builder[move.Source.Row * 8] = BoardConstants.Empty;
                    builder[move.Destination.Row * 8 + 2] = GetAllyRook();

                    game.CanCastleKingSide = false;
                    game.CanCastleQueenSide = false;

                    MovePiece(move, sourcePiece, builder);
                    game.Board = builder.ToString();
                }
            }
            else if (move.Info.KingRookMovedFromInitial)
            {
                MovePiece(move, sourcePiece, builder);
                game.Board = builder.ToString();
                game.CanCastleKingSide = false;
            }
            else if (move.Info.QueenRookMovedFromInitial)
            {
                MovePiece(move, sourcePiece, builder);
                game.Board = builder.ToString();
                game.CanCastleQueenSide = false;
            }
            else
            {
                return backup.Clone();
            }

            if (KingIsUnderAttack(game))
            {
                return backup.Clone();
            }

            game.Board = builder.ToString();
            game.WhiteTurn = !game.WhiteTurn;
            game.LastMove = move.Clone();
            return game.Clone();
        }

        public bool KingIsUnderAttack(Game game)
        {
            var king = GetKingPosition(game);
            return game.FieldIsUnderAttack(king.Row, king.Col);
        }

        private void MovePiece(Move move, char sourcePiece, StringBuilder builder)
        {
            builder[move.Source.Row * 8 + move.Source.Col] = BoardConstants.Empty;
            builder[move.Destination.Row * 8 + move.Destination.Col] = sourcePiece;
        }

        private Move GenerateCorrespondingBoardMove(InputMove move)
        {
            var generated = generator.Generate();

            var m = generated.ToList().FirstOrDefault(
                x => x.Source.Row == move.SourceRow &&
                x.Source.Col == move.SourceCol &&
                x.Destination.Row == move.DestinationRow &&
                x.Destination.Col == move.DestinationCol);

            if (move.PromotePiece.HasValue)
            {
                m.Info = new MoveInfo
                {
                    PromotePiece = move.PromotePiece
                };
            }

            return m;
        }

        private char GetAllyRook()
        {
            if (game.WhiteTurn)
            {
                return BoardConstants.WhiteRook;
            }
            else
            {
                return BoardConstants.BlackRook;
            }
        }

        private Square GetKingPosition(Game game)
        {
            var king = game.WhiteTurn
                ? BoardConstants.WhiteKing
                : BoardConstants.BlackKing;

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (game.GetPieceAt(row, col) == king)
                    {
                        return new Square(row, col);
                    }
                }
            }

            throw new Exception();
        }

        private bool CanPromotePiece(Game game, Move move)
        {
            var promotionRow = game.WhiteTurn ? 0 : 7;
            var myPawn = game.WhiteTurn ? BoardConstants.WhitePawn : BoardConstants.BlackPawn;
            if (game.GetPieceAt(move.Source.Row, move.Source.Col) == myPawn &&
                move.Destination.Row == promotionRow)
            {
                return true;
            }

            return false;
        }

        private char GetPromotionPiece(Game game, Move move)
        {
            string pieces = game.WhiteTurn ? "QBNR" : "qbnr";
            if (move.Info != null && move.Info.PromotePiece.HasValue)
            {
                if (pieces.Contains(move.Info.PromotePiece.Value))
                {
                    return move.Info.PromotePiece.Value;
                }
            }

            return pieces[0];
        }
    }
}

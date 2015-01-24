namespace ChessLogic.Generators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AllMovesGenerator
    {
        private Game game;

        public AllMovesGenerator(Game game)
        {
            this.game = game;
        }

        public List<Move> Generate()
        {
            var moves = new HashSet<Move>();

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (game.IsAllyPiece(row, col))
                    {
                        var generators = CreateGenerators(row, col);
                        foreach (var gen in generators)
                        {
                            moves.UnionWith(gen.GenerateFor(row, col));
                        }
                    }
                }
            }

            return moves.ToList();
        }

        private List<Generator> CreateGenerators(int row, int col)
        {
            var result = new List<Generator>();

            switch (game.GetPieceAt(row, col))
            {
                case BoardConstants.WhitePawn:
                case BoardConstants.BlackPawn:
                    result.Add(new PawnGenerator(game));
                    break;
                case BoardConstants.WhiteRook:
                case BoardConstants.BlackRook:
                    result.Add(new LineGenerator(game));
                    break;
                case BoardConstants.WhiteKnight:
                case BoardConstants.BlackKnight:
                    result.Add(new KnightGenerator(game));
                    break;
                case BoardConstants.WhiteBishop:
                case BoardConstants.BlackBishop:
                    result.Add(new DiagonalGenerator(game));
                    break;
                case BoardConstants.WhiteQueen:
                case BoardConstants.BlackQueen:
                    result.Add(new LineGenerator(game));
                    result.Add(new DiagonalGenerator(game));
                    break;
                case BoardConstants.WhiteKing:
                case BoardConstants.BlackKing:
                    result.Add(new KingGenerator(game));
                    break;
                default:
                    throw new ArgumentException();
            }

            return result;
        }
    }
}

namespace ChessLogic.Parsers
{
    using System;
    using System.Text;

    public static class FenParser
    {
        private const char sep = '/';

        public static Game FromFEN(string representation)
        {
            throw new NotImplementedException();
        }

        public static string ToFEN(Game game)
        {
            var builder = new StringBuilder();

            for (int row = 0; row < 8; row++)
            {
                var countEmpty = 0;
                for (int col = 0; col < 8; col++)
                {
                    var currentPiece = game.GetPieceAt(row, col);
                    if (currentPiece != BoardConstants.Empty)
                    {
                        if (countEmpty > 0)
                        {
                            builder.Append(countEmpty);
                            countEmpty = 0;
                        }
                        builder.Append(currentPiece);
                    }
                    else
                    {
                        countEmpty++;
                    }
                }

                if (countEmpty > 0)
                {
                    builder.Append(countEmpty);
                }
                
                builder.Append(sep);
            }

            builder.Length--;

            // Add turn info
            builder.Append(game.WhiteTurn ? " w" : " b");

            return builder.ToString();
        }
    }
}

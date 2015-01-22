namespace ChessLogic.Tests
{
    public abstract class TestBase
    {
        protected static Game CreateGame(string board, bool whiteTurn, bool canCastleKingSide, bool canCastleQueenSide, Move lastMove)
        {
            return new Game(board, whiteTurn, canCastleKingSide, canCastleQueenSide, lastMove);
        }

        protected static Game CreateGame(string board, bool whiteTurn)
        {
            return CreateGame(board, whiteTurn, false, false, null);
        }
    }
}

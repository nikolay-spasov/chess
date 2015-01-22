namespace ChessLogic
{
    using ChessLogic.Generators;

    public class Engine
    {
        public static ValidationResult Execute(InputMove move)
        {
            var currentPlayerExecutor = new MoveExecutor(move.Game);
            var newState = currentPlayerExecutor.ExecuteMove(move);

            if (newState.WhiteTurn == move.Game.WhiteTurn)
            {
                return new ValidationResult
                {
                    Game = move.Game,
                    EndGameState = null,
                    IsValid = false
                };
            }

            var opponentExecutor = new MoveExecutor(newState);
            
            if (opponentExecutor.HasValidMoves(newState))
            {
                return new ValidationResult
                {
                    Game = newState,
                    EndGameState = null,
                    IsValid = true
                };
            }
            else if (opponentExecutor.KingIsUnderAttack(newState))
            {
                // Mate
                return new ValidationResult
                {
                    Game = newState,
                    EndGameState = move.Game.WhiteTurn ? EndGameState.WhiteWin : EndGameState.BlackWin,
                    IsValid = true
                };
            }
            else
            {
                // Stealmate
                return new ValidationResult
                {
                    Game = newState,
                    EndGameState = EndGameState.Draw,
                    IsValid = true
                };
            }
        }
    }
}

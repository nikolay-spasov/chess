namespace ChessLogic
{
    public class ValidationResult
    {
        public Game Game { get; set; }
        public bool IsValid { get; set; }
        public EndGameState? EndGameState { get; set; }
    }

    public enum EndGameState
    {
        WhiteWin, BlackWin, Draw
    }
}

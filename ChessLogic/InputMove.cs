namespace ChessLogic
{
    public class InputMove
    {
        public int SourceRow { get; set; }
        public int SourceCol { get; set; }
        public int DestinationRow { get; set; }
        public int DestinationCol { get; set; }
        public char? PromotePiece { get; set; }
        public Game Game { get; set; }
    }
}

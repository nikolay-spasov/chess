namespace Chess.Core.Models
{
    public class Game
    {
        public Game()
        {
            
        }

        public int Id { get; set; }
        public string Board { get; set; }
        public bool WhiteCanCastleKingSide { get; set; }
        public bool WhiteCanCastleQueenSide { get; set; }
        public bool BlackCanCastleKingSide { get; set; }
        public bool BlackCanCastleQueenSide { get; set; }
        public bool WhiteTurn { get; set; }
    }
}

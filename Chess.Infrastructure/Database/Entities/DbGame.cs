namespace Chess.Infrastructure.Database.Entities
{
    public class DbGame
    {
        public int Id { get; set; }
        public string Board { get; set; }
        public bool WhiteCanCastleKingSide { get; set; }
        public bool WhiteCanCastleQueenSide { get; set; }
        public bool BlackCanCastleKingSide { get; set; }
        public bool BlackCanCastleQueenSide { get; set; }
        public bool WhiteTurn { get; set; }
        public int GameState { get; set; }
    }
}

namespace ChessLogic
{
    public class MoveInfo
    {
        public char? PromotePiece { get; set; }
        public bool KingSideCastle { get; set; }
        public bool QueenSideCastle { get; set; }
        public bool EnPassant { get; set; }
        public bool KingRookMovedFromInitial { get; set; }
        public bool QueenRookMovedFromInitial { get; set; }
        public bool KingMoved { get; set; }

        public MoveInfo Clone()
        {
            return this.MemberwiseClone() as MoveInfo;
        }
    }
}

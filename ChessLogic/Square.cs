namespace ChessLogic
{
    public struct Square
    {
        public Square(int row, int col)
            :this()
        {
            Row = row;
            Col = col;
        }

        public int Row { get; set; }
        public int Col { get; set; }
    }
}

namespace ChessLogic
{
    using System;

    public class Move
    {
        public Move(int srcRow, int srcCol, int destRow, int destCol, MoveInfo info = null)
            :this(new Square(srcRow, srcCol), new Square(destRow, destCol), info)
        {
            
        }

        public Move(Square source, Square dest, MoveInfo info = null)
        {
            Source = source;
            Destination = dest;
            Info = info;
        }

        public Square Source { get; set; }

        public Square Destination { get; set; }

        public MoveInfo Info { get; set; }

        public override string ToString()
        {
            return String.Format("[{0} {1}] [{2} {3}]",
                Source.Row, Source.Col,
                Destination.Row, Destination.Col);
        }

        public Move Clone()
        {
            var result = this.MemberwiseClone() as Move;
            if (this.Info != null)
            {
                result.Info = this.Info.Clone();
            }

            return result;
        }

        public bool EnPassantPossible
        {
            get
            {
                return (Source.Row == 1 && Destination.Row == 3) ||
                    (Source.Row == 6 && Destination.Row == 4);
            }
        }
    }
}

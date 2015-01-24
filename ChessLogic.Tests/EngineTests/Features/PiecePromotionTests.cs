namespace ChessLogic.Tests.EngineTests.Features
{
    using ChessLogic.Generators;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PiecePromotionTests
    {
        [TestMethod]
        public void WhiteCanPromoteToDefaultQueen()
        {
            var board =
                "EEEkEEEE" +
                "PEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEKEEEE";
            var game = new Game(board, true, false, false, null);

            var result = Engine.Execute(new InputMove()
                {
                    Game = game,
                    SourceRow = 1,
                    SourceCol = 0,
                    DestinationRow = 0,
                    DestinationCol = 0,
                });

            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(BoardConstants.WhiteQueen, result.Game.GetPieceAt(0, 0));
            Assert.AreEqual(BoardConstants.Empty, result.Game.GetPieceAt(1, 0));
        }

        [TestMethod]
        public void BlackCanPromoteToDefaultQueen()
        {
            var board =
                "EEEkEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "pEEEEEEE" +
                "EEEKEEEE";
            var game = new Game(board, false, false, false, null);

            var result = Engine.Execute(new InputMove()
            {
                Game = game,
                SourceRow = 6,
                SourceCol = 0,
                DestinationRow = 7,
                DestinationCol = 0,
            });

            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(BoardConstants.BlackQueen, result.Game.GetPieceAt(7, 0));
            Assert.AreEqual(BoardConstants.Empty, result.Game.GetPieceAt(6, 0));
        }

        [TestMethod]
        public void WhiteCanPromoteToKnight()
        {
            var board =
                "EEEkEEEE" +
                "PEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEKEEEE";
            var game = new Game(board, true, false, false, null);

            var result = Engine.Execute(new InputMove()
            {
                Game = game,
                SourceRow = 1,
                SourceCol = 0,
                DestinationRow = 0,
                DestinationCol = 0,
                PromotePiece = BoardConstants.WhiteKnight
            });

            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(BoardConstants.WhiteKnight, result.Game.GetPieceAt(0, 0));
            Assert.AreEqual(BoardConstants.Empty, result.Game.GetPieceAt(1, 0));
        }

        [TestMethod]
        public void BlackCanPromoteToKnight()
        {
            var board =
                "EEEkEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "pEEEEEEE" +
                "EEEKEEEE";
            var game = new Game(board, false, false, false, null);

            var result = Engine.Execute(new InputMove()
            {
                Game = game,
                SourceRow = 6,
                SourceCol = 0,
                DestinationRow = 7,
                DestinationCol = 0,
                PromotePiece = BoardConstants.BlackKnight
            });

            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(BoardConstants.BlackKnight, result.Game.GetPieceAt(7, 0));
            Assert.AreEqual(BoardConstants.Empty, result.Game.GetPieceAt(6, 0));
        }
    }
}

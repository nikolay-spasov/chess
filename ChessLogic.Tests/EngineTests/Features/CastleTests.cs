namespace ChessLogic.Tests.EngineTests.Features
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CastleTests : TestBase
    {
        [TestMethod]
        public void WhiteCanCastleKingSide()
        {
            var board =
                "EEEEkEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEKEER";

            var game = CreateGame(board, true, true, true, null);

            var result = Engine.Execute(new InputMove()
                {
                    Game = game,
                    SourceRow = 7,
                    SourceCol = 4,
                    DestinationRow = 7,
                    DestinationCol = 6
                });

            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(BoardConstants.Empty, result.Game.GetPieceAt(7, 4));
            Assert.AreEqual(BoardConstants.WhiteKing, result.Game.GetPieceAt(7, 6));
            Assert.AreEqual(BoardConstants.WhiteRook, result.Game.GetPieceAt(7, 5));
            Assert.AreEqual(BoardConstants.Empty, result.Game.GetPieceAt(7, 7));
            Assert.IsFalse(result.Game.CanCastleKingSide);
            Assert.IsFalse(result.Game.CanCastleQueenSide);
        }

        [TestMethod]
        public void WhiteCanCastleQueenSide()
        {
            var board =
                "EEEEkEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "REEEKEEE";

            var game = CreateGame(board, true, true, true, null);

            var result = Engine.Execute(new InputMove()
                {
                    Game = game,
                    SourceRow = 7,
                    SourceCol = 4,
                    DestinationRow = 7,
                    DestinationCol = 2
                });

            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(BoardConstants.Empty, result.Game.GetPieceAt(7, 4));
            Assert.AreEqual(BoardConstants.WhiteKing, result.Game.GetPieceAt(7, 2));
            Assert.AreEqual(BoardConstants.WhiteRook, result.Game.GetPieceAt(7, 3));
            Assert.AreEqual(BoardConstants.Empty, result.Game.GetPieceAt(7, 0));
            Assert.IsFalse(result.Game.CanCastleKingSide);
            Assert.IsFalse(result.Game.CanCastleQueenSide);
        }

        [TestMethod]
        public void BlackCanCastleKingSide()
        {
            var board =
                "EEEEkEEr" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEKEEE";

            var game = CreateGame(board, false, true, true, null);

            var result = Engine.Execute(new InputMove()
            {
                Game = game,
                SourceRow = 0,
                SourceCol = 4,
                DestinationRow = 0,
                DestinationCol = 6
            });

            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(BoardConstants.Empty, result.Game.GetPieceAt(0, 4));
            Assert.AreEqual(BoardConstants.BlackKing, result.Game.GetPieceAt(0, 6));
            Assert.AreEqual(BoardConstants.BlackRook, result.Game.GetPieceAt(0, 5));
            Assert.AreEqual(BoardConstants.Empty, result.Game.GetPieceAt(0, 7));
            Assert.IsFalse(result.Game.CanCastleKingSide);
            Assert.IsFalse(result.Game.CanCastleQueenSide);
        }

        [TestMethod]
        public void BlackCanCastleQueenSide()
        {
            var board =
                "rEEEkEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEKEEE";

            var game = CreateGame(board, false, true, true, null);

            var result = Engine.Execute(new InputMove()
            {
                Game = game,
                SourceRow = 0,
                SourceCol = 4,
                DestinationRow = 0,
                DestinationCol = 2
            });

            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(BoardConstants.Empty, result.Game.GetPieceAt(0, 4));
            Assert.AreEqual(BoardConstants.BlackKing, result.Game.GetPieceAt(0, 2));
            Assert.AreEqual(BoardConstants.BlackRook, result.Game.GetPieceAt(0, 3));
            Assert.AreEqual(BoardConstants.Empty, result.Game.GetPieceAt(0, 0));
            Assert.IsFalse(result.Game.CanCastleKingSide);
            Assert.IsFalse(result.Game.CanCastleQueenSide);
        }
    }
}

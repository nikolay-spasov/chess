namespace ChessLogic.Tests.EngineTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EngineTests : TestBase
    {
        [TestMethod]
        public void CanDetectStealmate()
        {
            var board =
                "EEEkEEEE" +
                "EEEPEEEE" +
                "EEEEEEEE" +
                "EEEKEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE";

            var game = CreateGame(board, true);

            var result = Engine.Execute(new InputMove
            {
                SourceRow = 3,
                SourceCol = 3,
                DestinationRow = 2,
                DestinationCol = 3,
                Game = game
            });

            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(EndGameState.Draw, result.EndGameState);
        }

        [TestMethod]
        public void CanDetectMate()
        {
            var board =
                "EEEkEEEE" +
                "QEEEEEEE" +
                "EEEKEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE";

            var game = CreateGame(board, true);

            var result = Engine.Execute(new InputMove
            {
                SourceRow = 1,
                SourceCol = 0,
                DestinationRow = 1,
                DestinationCol = 3,
                Game = game
            });

            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(EndGameState.WhiteWin, result.EndGameState);
        }

        [TestMethod]
        public void CanDetectTwoMoveMate()
        {
            string board =
                "rnbqkbnr" +
                "pppppEEp" +
                "EEEEEEEE" +
                "EEEEEppE" +
                "EEEEPEEE" +
                "PEEEEEEE" +
                "EPPPEPPP" +
                "RNBQKBNR";

            var game = new Game(board, true, true, true, null);

            var result = Engine.Execute(new InputMove
                {
                    SourceRow = 7,
                    SourceCol = 3,
                    DestinationRow = 3,
                    DestinationCol = 7,
                    Game = game
                });

            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(EndGameState.WhiteWin, result.EndGameState);
        }

        [TestMethod]
        public void CanExecuteNonSpecialMove()
        {
            string board =
                "rnbqkbnr" +
                "pppppppp" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "PPPPPPPP" +
                "RNBQKBNR";

            var game = new Game(board, true, true, true, null);

            var result = Engine.Execute(new InputMove
            {
                SourceRow = 6,
                SourceCol = 4,
                DestinationRow = 5,
                DestinationCol = 4,
                Game = game
            });

            Assert.IsTrue(result.IsValid);
            Assert.IsNull(result.EndGameState);
        }

        [TestMethod]
        public void CanEscapeFromCheck()
        {
            string board =
                "rnbqkbnr" +
                "pppppQEp" +
                "EEEEEEEE" +
                "EEEEEppE" +
                "EEEEPEEE" +
                "PEEEEEEE" +
                "EPPPEPPP" +
                "RNBEKBNR";

            var game = new Game(board, false, true, true, null);

            var result = Engine.Execute(new InputMove
            {
                SourceRow = 0,
                SourceCol = 4,
                DestinationRow = 1,
                DestinationCol = 5,
                Game = game
            });

            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(null, result.EndGameState);
        }

        [TestMethod]
        public void IssueWithLoseOnCheck()
        {
            var board = 
                "rEbEEbnr" + 
                "EEpnEkpE" +
                "ppEEEEEp" +
                "EEBpppEE" +
                "EEEEPEPP" +
                "NEEPEEEN" +
                "PPPqEPER" +
                "REEQKBEE";
            var game = new Game(board, true, true, true, null);

            var result = Engine.Execute(new InputMove
            {
                SourceRow = 7,
                SourceCol = 3,
                DestinationRow = 6,
                DestinationCol = 3,
                Game = game
            });

            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(null, result.EndGameState);
        }

        [TestMethod]
        public void IssueWithBlackLoseOnCheck()
        {
            var board =
                "rEbEEbnr" +
                "EEpnEkpE" +
                "ppEEEEEp" +
                "EEBpppNE" +
                "EEEEPEPP" +
                "NEEPEEEE" +
                "PPPqEPER" +
                "REEQKBEE";
            var game = new Game(board, false, true, true, null);

            var result = Engine.Execute(new InputMove
            {
                SourceRow = 1,
                SourceCol = 5,
                DestinationRow = 2,
                DestinationCol = 6,
                Game = game
            });

            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(null, result.EndGameState);
        }

        [TestMethod]
        public void IssueWithLoseOnCheck1()
        {
            var board = "EEEEEEEkEEREEPEEPEEEEEEPEEEEEEPEEEEpEEEEpEEPEpENEEEEEEEErKEEEEEE";

            var game = new Game(board, true, false, false, null);

            var result = Engine.Execute(new InputMove
            {
                SourceRow = 7,
                SourceCol = 1,
                DestinationRow = 7,
                DestinationCol = 0,
                Game = game
            });

            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(null, result.EndGameState);
        }

        [TestMethod]
        public void IssueWithLoseOnCheck2()
        {
            var board = "EEEEEBEEEEEKEEEEEkEEEEEEEEEEREPEpEEEEEEEPEEEEEEEEEEEEEEEEpEEEEEE";

            var game = new Game(board, false, false, false, null);

            var result = Engine.Execute(new InputMove
            {
                SourceRow = 2,
                SourceCol = 1,
                DestinationRow = 2,
                DestinationCol = 0,
                Game = game
            });

            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(null, result.EndGameState);
        }

        [TestMethod]
        public void WhiteRightCanTakeWithEnPassant()
        {
            var board =
                "EEEkEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEPpEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEKEEEE";
            var game = new Game(board, true, true, true, null);
            game.LastMove = new Move(new Square(1, 3), new Square(3, 3));

            var result = Engine.Execute(new InputMove
            {
                SourceRow = 3,
                SourceCol = 2,
                DestinationRow = 2,
                DestinationCol = 3,
                Game = game
            });

            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(null, result.EndGameState);
            Assert.AreEqual(BoardConstants.Empty, result.Game.GetPieceAt(3, 2));
            Assert.AreEqual(BoardConstants.WhitePawn, result.Game.GetPieceAt(2, 3));
            Assert.AreEqual(BoardConstants.Empty, result.Game.GetPieceAt(3, 3));
        }

        [TestMethod]
        public void WhiteLeftCanTakeWithEnPassant()
        {
            var board =
                "EEEkEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EpPEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEKEEEE";
            var game = new Game(board, true, true, true, null);
            game.LastMove = new Move(new Square(1, 1), new Square(3, 1));

            var result = Engine.Execute(new InputMove
            {
                SourceRow = 3,
                SourceCol = 2,
                DestinationRow = 2,
                DestinationCol = 1,
                Game = game
            });

            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(null, result.EndGameState);
            Assert.AreEqual(BoardConstants.Empty, result.Game.GetPieceAt(3, 2));
            Assert.AreEqual(BoardConstants.WhitePawn, result.Game.GetPieceAt(2, 1));
            Assert.AreEqual(BoardConstants.Empty, result.Game.GetPieceAt(3, 1));
        }

        [TestMethod]
        public void BlackRightCanTakeWithEnPassant()
        {
            var board =
                "EEEkEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEpPEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEKEEEE";
            var game = new Game(board, false, true, true, null);
            game.LastMove = new Move(new Square(6, 3), new Square(4, 3));

            var result = Engine.Execute(new InputMove
            {
                SourceRow = 4,
                SourceCol = 2,
                DestinationRow = 5,
                DestinationCol = 3,
                Game = game
            });

            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(null, result.EndGameState);
            Assert.AreEqual(BoardConstants.Empty, result.Game.GetPieceAt(6, 3));
            Assert.AreEqual(BoardConstants.BlackPawn, result.Game.GetPieceAt(5, 3));
            Assert.AreEqual(BoardConstants.Empty, result.Game.GetPieceAt(4, 3));
        }

        [TestMethod]
        public void BlackLeftCanTakeWithEnPassant()
        {
            var board =
                "EEEkEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EPpEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEKEEEE";
            var game = new Game(board, false, true, true, null);
            game.LastMove = new Move(new Square(6, 1), new Square(4, 1));

            var result = Engine.Execute(new InputMove
            {
                SourceRow = 4,
                SourceCol = 2,
                DestinationRow = 5,
                DestinationCol = 1,
                Game = game
            });

            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(null, result.EndGameState);
            Assert.AreEqual(BoardConstants.Empty, result.Game.GetPieceAt(6, 3));
            Assert.AreEqual(BoardConstants.BlackPawn, result.Game.GetPieceAt(5, 1));
            Assert.AreEqual(BoardConstants.Empty, result.Game.GetPieceAt(4, 1));
        }
    }
}

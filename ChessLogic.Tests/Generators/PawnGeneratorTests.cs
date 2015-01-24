namespace ChessLogic.Tests.Generators
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ChessLogic.Generators;

    [TestClass]
    public class PawnGeneratorTests : GeneratorsTestBase
    {
        [TestMethod]
        public void PawnCanMove2ForwardIfInitialPosition()
        {
            var board =
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEPEEEE" +
                "EEEEEEEE";

            var game = CreateGame(board, true);
            var generator = new PawnGenerator(game);

            var result = generator.GenerateFor(6, 3);

            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void PawnCannotMove2ForwardIfNotInitialPosition()
        {
            var board =
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEPEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE";

            var game = CreateGame(board, true);
            var generator = new PawnGenerator(game);

            var result = generator.GenerateFor(5, 3);

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void PawnCanTakeEnemyPiece()
        {
            var board =
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEpPpEEE" +
                "EEEPEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE";

            var game = CreateGame(board, true);
            var generator = new PawnGenerator(game);

            var result = generator.GenerateFor(5, 3);

            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void PawnCannotJumpOverAPiece()
        {
            var board =
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEpEEEE" +
                "EEEPEEEE" +
                "EEEEEEEE";

            var game = CreateGame(board, true);
            var generator = new PawnGenerator(game);

            var result = generator.GenerateFor(6, 3);

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void PawnCannotMoveDiagonallyIfNotTakingAPiece()
        {
            var board =
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEnEEEE" +
                "EEEPEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE";

            var game = CreateGame(board, true);
            var generator = new PawnGenerator(game);

            var result = generator.GenerateFor(4, 3);

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void CanGenerateWhiteRightEnPassant()
        {
            var board =
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEPpEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE";

            var game = CreateGame(board, true);
            game.LastMove = new Move(new Square(1, 4), new Square(3, 4));
            var generator = new PawnGenerator(game);

            var result = generator.GenerateFor(3, 3);

            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void CanGenerateWhiteLeftEnPassant()
        {
            var board =
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEpPEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE";

            var game = CreateGame(board, true);
            game.LastMove = new Move(new Square(1, 2), new Square(3, 2));
            var generator = new PawnGenerator(game);

            var result = generator.GenerateFor(3, 3);

            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void CanGenerateBlackRightEnPassant()
        {
            var board =
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEpPEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE";

            var game = CreateGame(board, false);
            game.LastMove = new Move(new Square(6, 5), new Square(4, 5));
            var generator = new PawnGenerator(game);

            var result = generator.GenerateFor(4, 4);

            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void CanGenerateBlackLeftEnPassant()
        {
            var board =
                 "EEEEEEEE" +
                 "EEEEEEEE" +
                 "EEEEEEEE" +
                 "EEEEEEEE" +
                 "EEEPpEEE" +
                 "EEEEEEEE" +
                 "EEEEEEEE" +
                 "EEEEEEEE";

            var game = CreateGame(board, false);
            game.LastMove = new Move(new Square(6, 3), new Square(4, 3));
            var generator = new PawnGenerator(game);

            var result = generator.GenerateFor(4, 4);

            Assert.AreEqual(2, result.Count);
        }
    }
}

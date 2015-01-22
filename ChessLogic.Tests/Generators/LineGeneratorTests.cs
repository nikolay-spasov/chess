namespace ChessLogic.Tests.Generators
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ChessLogic.Generators;

    [TestClass]
    public class LineGeneratorTests : GeneratorsTestBase
    {
        [TestMethod]
        public void GeneratesProperMovesCount()
        {
            var board =
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEREEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE";

            var game = CreateGame(board, true);
            var generator = new LineGenerator(game);

            var result = generator.GenerateFor(3, 3);

            Assert.AreEqual(14, result.Count);
        }

        [TestMethod]
        public void RookCannotJumpOverAPiece()
        {
            var board =
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEPEEEE" +
                "EEPRPEEE" +
                "EEEPEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE";

            var game = CreateGame(board, true);
            var generator = new LineGenerator(game);

            var result = generator.GenerateFor(3, 3);

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void RookCannotJumpOverAnEnemyPiece()
        {
            var board =
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEPEEEE" +
                "EEpRpEEE" +
                "EEEPEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE";

            var game = CreateGame(board, true);
            var generator = new LineGenerator(game);

            var result = generator.GenerateFor(3, 3);

            Assert.AreEqual(2, result.Count);
        }
    }
}

namespace ChessLogic.Tests.Generators
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ChessLogic.Generators;

    [TestClass]
    public class DiagonalGeneratorTests : GeneratorsTestBase
    {
        [TestMethod]
        public void GeneratesProperMovesCount()
        {
            var board =
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEBEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE";

            var game = CreateGame(board, true);
            var generator = new DiagonalGenerator(game);

            var result = generator.GenerateFor(3, 3);

            Assert.AreEqual(13, result.Count);
        }

        [TestMethod]
        public void BishopCannotJumpOverAPiece()
        {
            var board =
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEPEPEEE" +
                "EEEBEEEE" +
                "EEPEPEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE";

            var game = CreateGame(board, true);
            var generator = new DiagonalGenerator(game);

            var result = generator.GenerateFor(3, 3);

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void BishopCannotJumpOverAnEnemyPiece()
        {
            var board =
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEPEpEEE" +
                "EEEBEEEE" +
                "EEPEpEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE";

            var game = CreateGame(board, true);
            var generator = new DiagonalGenerator(game);

            var result = generator.GenerateFor(3, 3);

            Assert.AreEqual(2, result.Count);
        }
    }
}

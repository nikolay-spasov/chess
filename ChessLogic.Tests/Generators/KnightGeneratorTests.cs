namespace ChessLogic.Tests.Generators
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ChessLogic.Generators;

    [TestClass]
    public class KnightGeneratorTests : GeneratorsTestBase
    {
        [TestMethod]
        public void GeneratesProperMovesCount()
        {
            var board =
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEENEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE";

            var game = CreateGame(board, true);
            var generator = new KnightGenerator(game);

            var result = generator.GenerateFor(3, 3);

            Assert.AreEqual(8, result.Count);
        }

        [TestMethod]
        public void KnightCannotLandOnAnAllyPiece()
        {
            var board =
                "EEEEEEEE" +
                "EEPEPEEE" +
                "EPEEEPEE" +
                "EEENEEEE" +
                "EPEEEPEE" +
                "EEPEPEEE" +
                "EEEEEEEE" +
                "EEEEEEEE";

            var game = CreateGame(board, true);
            var generator = new KnightGenerator(game);

            var result = generator.GenerateFor(3, 3);

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void KnightCanTakeEnemyPiece()
        {
            var board =
                "EEEEEEEE" +
                "EEPEPEEE" +
                "EPEEEPEE" +
                "EEENEEEE" +
                "EpEEEPEE" +
                "EEPEPEEE" +
                "EEEEEEEE" +
                "EEEEEEEE";

            var game = CreateGame(board, true);
            var generator = new KnightGenerator(game);

            var result = generator.GenerateFor(3, 3);

            Assert.AreEqual(1, result.Count);
        }
    }
}

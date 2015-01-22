namespace ChessLogic.Tests.Generators
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ChessLogic.Generators;

    [TestClass]
    public class KingGeneratorTests : GeneratorsTestBase
    {
        [TestMethod]
        public void GeneratesProperMovesCount()
        {
            var board =
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEKEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE";

            var game = CreateGame(board, true);
            var generator = new KingGenerator(game);

            var result = generator.GenerateFor(3, 3);

            Assert.AreEqual(8, result.Count);
        }

        [TestMethod]
        public void CanGenerateCastleMoves()
        {
            var board =
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "REEEKEER";

            var game = CreateGame(board, true, true, true, null);
            var generator = new KingGenerator(game);

            var result = generator.GenerateFor(7, 4);

            Assert.AreEqual(7, result.Count);
        }

        [TestMethod]
        public void CannotCastleIfBlocked()
        {
            var board =
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEPPPEE" +
                "EEEPKPEE";

            var game = CreateGame(board, true, true, true, null);
            var generator = new KingGenerator(game);

            var result = generator.GenerateFor(7, 4);

            Assert.AreEqual(0, result.Count);
        }
    }
}

namespace ChessLogic.Tests.Generators
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ChessLogic.Generators;

    [TestClass]
    public class InitialBoardGenerator
    {
        [TestMethod]
        public void CanGenerateInitialMoves()
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
            var gen = new AllMovesGenerator(game);

            var result = gen.Generate();

            Assert.AreEqual(20, result.Count);
        }
    }
}

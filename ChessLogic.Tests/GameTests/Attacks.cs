namespace ChessLogic.Tests.GameTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Attacks : TestBase
    {
        [TestMethod]
        public void CanTellIfFieldIsUnderAttackByQueen()
        {
            var board =
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEqEEEEE" +
                "EEEEEEEE" +
                "EEEEKEEE";

            var game = CreateGame(board, true);

            var result = game.FieldIsUnderAttack(7, 4);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanTellIfFieldIsUnderAttackByPawn()
        {
            var board =
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEpEE" +
                "EEEEKEEE";

            var game = CreateGame(board, true);

            var result = game.FieldIsUnderAttack(7, 4);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanTellIfFieldIsUnderAttackByKnight()
        {
            var board =
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEnEE" +
                "EEEEEEEE" +
                "EEEEKEEE";

            var game = CreateGame(board, true);

            var result = game.FieldIsUnderAttack(7, 4);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanTellIfFieldIsUnderAttackByKing()
        {
            var board =
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEkEEE" +
                "EEEEKEEE";

            var game = CreateGame(board, true);

            var result = game.FieldIsUnderAttack(7, 4);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FieldCannotBeAttackedByCastling()
        {
            var board =
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEkEEE" +
                "EEEEKEpE";

            var game = CreateGame(board, true);

            var result = game.FieldIsUnderAttack(7, 6);

            Assert.IsFalse(result);
        }
    }
}

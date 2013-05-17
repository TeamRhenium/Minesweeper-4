using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Minesweeper.Tests
{
    [TestClass]
    public class GameFieldTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "The rows must be between 2 and 10.")]
        public void TestGameField_WithZeroRows()
        {
            GameField gameField = new GameField(0, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "The cols must be between 2 and 10.")]
        public void TestGameField_WithElevenCols()
        {
            GameField gameField = new GameField(4, 11);
        }

        [TestMethod]
        public void TestGameField_Correct()
        {
            GameField gameField = new GameField(10, 10);

            Assert.AreEqual(10, gameField.FieldCols);
            Assert.AreEqual(10, gameField.FieldRows);
        }

        [TestMethod]
        public void TestCreate_FiveRowsFiveCols()
        {
            GameField gameField = new GameField(5, 5);

            char[,] expected = { 
                                   {'?', '?', '?', '?', '?'},
                                   {'?', '?', '?', '?', '?'},
                                   {'?', '?', '?', '?', '?'},
                                   {'?', '?', '?', '?', '?'},
                                   {'?', '?', '?', '?', '?'},
                               };

            for (int i = 0; i < gameField.FieldRows; i++)
            {
                for (int j = 0; j < gameField.FieldCols; j++)
                {
                    Assert.AreEqual(expected[i,j], gameField.Create()[i,j]);
                }
            }
        }
    }
}

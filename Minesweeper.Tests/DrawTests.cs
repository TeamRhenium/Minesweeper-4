using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.IO;

namespace Minesweeper.Tests
{
    [TestClass]
    public class DrawTests
    {
        [TestMethod]
        public void TestDrawPlayingFiel_WithTenRowsAndTenCols()
        {
            GameField gameField = new GameField(10, 10);
            char[,] board = gameField.Create();

            StringBuilder expectedOutput = new StringBuilder();
            StringBuilder actualOutput = new StringBuilder();

            expectedOutput.AppendFormat("    0 1 2 3 4 5 6 7 8 9{0}", Environment.NewLine);
            expectedOutput.AppendFormat("   ---------------------{0}", Environment.NewLine);
            expectedOutput.AppendFormat("0 | ? ? ? ? ? ? ? ? ? ? |{0}", Environment.NewLine);
            expectedOutput.AppendFormat("1 | ? ? ? ? ? ? ? ? ? ? |{0}", Environment.NewLine);
            expectedOutput.AppendFormat("2 | ? ? ? ? ? ? ? ? ? ? |{0}", Environment.NewLine);
            expectedOutput.AppendFormat("3 | ? ? ? ? ? ? ? ? ? ? |{0}", Environment.NewLine);
            expectedOutput.AppendFormat("4 | ? ? ? ? ? ? ? ? ? ? |{0}", Environment.NewLine);
            expectedOutput.AppendFormat("5 | ? ? ? ? ? ? ? ? ? ? |{0}", Environment.NewLine);
            expectedOutput.AppendFormat("6 | ? ? ? ? ? ? ? ? ? ? |{0}", Environment.NewLine);
            expectedOutput.AppendFormat("7 | ? ? ? ? ? ? ? ? ? ? |{0}", Environment.NewLine);
            expectedOutput.AppendFormat("8 | ? ? ? ? ? ? ? ? ? ? |{0}", Environment.NewLine);
            expectedOutput.AppendFormat("9 | ? ? ? ? ? ? ? ? ? ? |{0}", Environment.NewLine);
            expectedOutput.AppendFormat("   ---------------------{0}", Environment.NewLine);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Draw.PlayingField(board);
                Assert.AreEqual<string>(expectedOutput.ToString(), sw.ToString());
            }
        }

        [TestMethod]
        public void TestDrawPlayingFiel_WithTwoRowsAndTwoCols()
        {
            GameField gameField = new GameField(2,2);
            char[,] board = gameField.Create();

            StringBuilder expectedOutput = new StringBuilder();
            StringBuilder actualOutput = new StringBuilder();

            expectedOutput.AppendFormat("    0 1 2 3 4 5 6 7 8 9{0}", Environment.NewLine);
            expectedOutput.AppendFormat("   ---------------------{0}", Environment.NewLine);
            expectedOutput.AppendFormat("0 | ? ? |{0}", Environment.NewLine);
            expectedOutput.AppendFormat("1 | ? ? |{0}", Environment.NewLine);
            expectedOutput.AppendFormat("   ---------------------{0}", Environment.NewLine);
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Draw.PlayingField(board);
                Assert.AreEqual(expectedOutput.ToString(), sw.ToString());
            }
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.IO;
using System.Collections.Generic;

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
            GameField gameField = new GameField(2, 2);
            char[,] board = gameField.Create();

            StringBuilder expectedOutput = new StringBuilder();

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

        [TestMethod]
        public void TestDrawScoreBoard_WithEmptyScoreBoard()
        {
            List<Player> topPlayers = new List<Player>();

            StringBuilder expectedOutput = new StringBuilder();
            expectedOutput.AppendLine("Points:");
            expectedOutput.AppendLine("Empty score board.");

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Draw.ScoreBoard(topPlayers);
                Assert.AreEqual(expectedOutput.ToString(), sw.ToString());
            }
        }

        [TestMethod]
        public void TestDrawScoreBoard_WithThreeUsers()
        {
            List<Player> topPlayers = new List<Player>();

            Player[] players = {
                                   new Player("John Doe", 15),
                                   new Player("Jane Doe", 25),
                                   new Player("Jimmy Doe", 8),
                               };
            foreach (var player in players)
            {
                topPlayers.Add(player);
            }

            StringBuilder expectedOutput = new StringBuilder();
            expectedOutput.AppendFormat("Points:{0}", Environment.NewLine);
            expectedOutput.AppendFormat("1. John Doe --> 15 points{0}", Environment.NewLine);
            expectedOutput.AppendFormat("2. Jane Doe --> 25 points{0}", Environment.NewLine);
            expectedOutput.AppendFormat("3. Jimmy Doe --> 8 points{0}", Environment.NewLine);
            expectedOutput.AppendLine();
            expectedOutput.AppendFormat("The game will begin in 5 seconds.{0}", Environment.NewLine);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Draw.ScoreBoard(topPlayers);
                Assert.AreEqual(expectedOutput.ToString(), sw.ToString());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Minesweeper.Tests
{
    [TestClass]
    public class EngineTests
    {
        [TestMethod]
        public void TestParseInputCommand_WithExitCommand()
        {
            Engine game = new Engine();
            GameField gameField = new GameField(10, 10);

            char[,] fieldWithQuestionMarks = gameField.Create();

            string actual = game.ParseInputCommand("exit", fieldWithQuestionMarks);
            string expected = "exit";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestParseInputCommand_WithRowsAndColsCommand()
        {
            Engine game = new Engine();
            GameField gameField = new GameField(10, 10);

            char[,] fieldWithQuestionMarks = gameField.Create();

            string actual = game.ParseInputCommand("5x5", fieldWithQuestionMarks);
            string expected = "turn";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestExecuteCommand_WithTopCommand()
        {
            Engine game = new Engine();
            GameField gameField = new GameField(10, 10);

            char[,] fieldWithQuestionMarks = gameField.Create();
            char[,] fieldWithBombs = gameField.PlaceBombs();

            int maxScore = (gameField.FieldCols * gameField.FieldCols) -
                           (gameField.FieldCols + gameField.FieldCols);

            string inputCommand = "top";

            List<Player> topPlayers = new List<Player>();

            StringBuilder expectedOutput = new StringBuilder();
            expectedOutput.AppendLine("Points:");
            expectedOutput.AppendLine("Empty score board.");

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                game.ExecuteCommand(inputCommand, gameField, fieldWithQuestionMarks, fieldWithBombs, maxScore);
                Assert.AreEqual<string>(expectedOutput.ToString(), sw.ToString());
            }
        }

        [TestMethod]
        public void TestExecuteCommand_WithRestartCommand()
        {
            Engine game = new Engine();
            GameField gameField = new GameField(2, 2);

            char[,] fieldWithQuestionMarks = gameField.Create();
            char[,] fieldWithBombs = gameField.PlaceBombs();

            int maxScore = (gameField.FieldCols * gameField.FieldCols) -
                           (gameField.FieldCols + gameField.FieldCols);

            string inputCommand = "restart";

            StringBuilder expectedOutput = new StringBuilder();
            expectedOutput.AppendFormat("    0 1 2 3 4 5 6 7 8 9{0}", Environment.NewLine);
            expectedOutput.AppendFormat("   ---------------------{0}", Environment.NewLine);
            expectedOutput.AppendFormat("0 | ? ? |{0}", Environment.NewLine);
            expectedOutput.AppendFormat("1 | ? ? |{0}", Environment.NewLine);
            expectedOutput.AppendFormat("   ---------------------{0}", Environment.NewLine);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                game.ExecuteCommand(inputCommand, gameField, fieldWithQuestionMarks, fieldWithBombs, maxScore);
                Assert.AreEqual<string>(expectedOutput.ToString(), sw.ToString());
            }
        }

        [TestMethod]
        public void TestExecuteCommand_WithExitCommand()
        {
            Engine game = new Engine();
            GameField gameField = new GameField(10, 10);

            char[,] fieldWithQuestionMarks = gameField.Create();
            char[,] fieldWithBombs = gameField.PlaceBombs();

            int maxScore = (gameField.FieldCols * gameField.FieldCols) -
                           (gameField.FieldCols + gameField.FieldCols);

            string inputCommand = "exit";

            StringBuilder expectedOutput = new StringBuilder();
            expectedOutput.AppendFormat("Good bye.{0}", Environment.NewLine);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                game.ExecuteCommand(inputCommand, gameField, fieldWithQuestionMarks, fieldWithBombs, maxScore);
                Assert.AreEqual<string>(expectedOutput.ToString(), sw.ToString());
            }
        }

        [TestMethod]
        public void TestExecuteCommand_WithWrongCommand()
        {
            Engine game = new Engine();
            GameField gameField = new GameField(10, 10);

            char[,] fieldWithQuestionMarks = gameField.Create();
            char[,] fieldWithBombs = gameField.PlaceBombs();

            int maxScore = (gameField.FieldCols * gameField.FieldCols) -
                           (gameField.FieldCols + gameField.FieldCols);

            string inputCommand = "15 x -1";

            StringBuilder expectedOutput = new StringBuilder();
            expectedOutput.AppendFormat("Wrong command: {0}{1}", inputCommand, Environment.NewLine);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                game.ExecuteCommand(inputCommand, gameField, fieldWithQuestionMarks, fieldWithBombs, maxScore);
                Assert.AreEqual<string>(expectedOutput.ToString(), sw.ToString());
            }
        }
    }


}

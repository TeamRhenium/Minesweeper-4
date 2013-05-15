// <copyright file="Engine.cs" company="Telerik Academy">
// Telerik Academy - High Quality Code Team Project. Team Rhenium.
// </copyright>
namespace Minesweeper
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Initializes a new instance of the <seealso cref="Engine"/> class.
    /// </summary>
    public class Engine
    {
        public static List<Player> ScoreBoardTopPlayers = new List<Player>();
        public static bool IsNewGame = true;

        private int personalScore = 0;

        private int rowToCheckForBomb;
        private int colToCheckForBomb;

        /// <summary>
        /// Parses and returns the input command.
        /// </summary>
        /// <param name="inputCommand">The command to parse.</param>
        /// <param name="fieldWithQuestionmarks">The field to validate before the command is parsed.</param>
        /// <returns>Returns the parsed input command.</returns>
        public string ParseInputCommand(string inputCommand, char[,] fieldWithQuestionmarks)
        {
            Console.Write("Enter row and column: ");
            inputCommand = Console.ReadLine().Trim();

            if (inputCommand.Length == 3)
            {
                if (int.TryParse(inputCommand[0].ToString(), out this.rowToCheckForBomb) &&
                    int.TryParse(inputCommand[2].ToString(), out this.colToCheckForBomb) &&
                    this.rowToCheckForBomb <= fieldWithQuestionmarks.GetLength(0) &&
                    this.colToCheckForBomb <= fieldWithQuestionmarks.GetLength(1))
                {
                    inputCommand = "turn";
                }
            }

            return inputCommand;
        }

        /// <summary>
        /// Executes a parsed command.
        /// </summary>
        /// <param name="inputCommand">The parsed command to execute.</param>
        /// <param name="gameField">The game field dimensions.</param>
        /// <param name="fieldWithQuestionmarks">The unrevealed game field.(with question marks)</param>
        /// <param name="fieldWithBombs">The bombs field.(the places of the bombs)</param>
        /// <param name="maxScore">The max score formula.
        ///     Calculated by the formula (gameField.FieldCols * gameField.FieldCols) - (gameField.FieldCols + gameField.FieldCols)
        /// </param>
        public void ExecuteCommand(string inputCommand, GameField gameField, char[,] fieldWithQuestionmarks, char[,] fieldWithBombs, int maxScore) 
        {
            switch (inputCommand)
            {
                case "top":
                    Draw.ScoreBoard(ScoreBoardTopPlayers);
                    break;

                case "restart":
                    fieldWithQuestionmarks = gameField.Create();
                    fieldWithBombs = gameField.PlaceBombs();

                    Draw.PlayingField(fieldWithQuestionmarks);

                    IsNewGame = false;
                    break;

                case "exit":
                    Console.WriteLine("Good bye.");
                    break;

                case "turn":
                    if (fieldWithBombs[this.rowToCheckForBomb, this.colToCheckForBomb] != '*')
                    {
                        IsNewGame = false;
                        this.SetSurroundingBombsCount(fieldWithQuestionmarks, fieldWithBombs, this.rowToCheckForBomb, this.colToCheckForBomb);
                        this.personalScore++;

                        if (maxScore == this.personalScore)
                        {
                            Console.WriteLine("Congrats! You won the game!");

                            Draw.PlayingField(fieldWithBombs);

                            this.EnterScoreToScoreBoard();

                            Draw.ScoreBoard(ScoreBoardTopPlayers);

                            fieldWithQuestionmarks = gameField.Create();
                            fieldWithBombs = gameField.PlaceBombs();

                            this.personalScore = 0;

                            IsNewGame = true;
                        }
                        else
                        {
                            Draw.PlayingField(fieldWithQuestionmarks);
                        }
                    }
                    else
                    {
                        Draw.PlayingField(fieldWithBombs);

                        Console.WriteLine("You just hit a bomb. Sorry.");

                        EnterScoreToScoreBoard();
                        
                        Draw.ScoreBoard(ScoreBoardTopPlayers);

                        fieldWithQuestionmarks = gameField.Create();
                        fieldWithBombs = gameField.PlaceBombs();

                        this.personalScore = 0;

                        IsNewGame = true;
                    }

                    break;

                default:
                    Console.WriteLine("Wrong command.");
                    break;
            }
        }

        /// <summary>
        /// Sets the number of the surrounding bombs near a non-bomb cell which is picked.
        /// </summary>
        /// <param name="fieldWithQuestionmarks">The unrevealed game field.(with question marks)</param>
        /// <param name="fieldWithBombs">The bombs field.(the places of the bombs)</param>
        /// <param name="bombFieldRow">The current row to set the number.</param>
        /// <param name="bombFieldCol">The current column to set the number.</param>
        private void SetSurroundingBombsCount(char[,] fieldWithQuestionmarks, char[,] fieldWithBombs, int bombFieldRow, int bombFieldCol)
        {
            char surroundingBombs = this.GetSurroundingBombsCount(fieldWithBombs, bombFieldRow, bombFieldCol);
            fieldWithBombs[bombFieldRow, bombFieldCol] = surroundingBombs;
            fieldWithQuestionmarks[bombFieldRow, bombFieldCol] = surroundingBombs;
        }

        /// <summary>
        /// Gets the number of the surrounding bombs.
        /// </summary>
        /// <param name="bombField">The field with placed bombs.</param>
        /// <param name="row">The current row to get the number.</param>
        /// <param name="col">The current column to get the number.</param>
        /// <returns>The number of surrounding bombs near the cell.</returns>
        private char GetSurroundingBombsCount(char[,] bombField, int row, int col)
        {
            int bombsCount = 0;
            int rowsCount = bombField.GetLength(0);
            int colsCount = bombField.GetLength(1);

            if (row - 1 >= 0)
            {
                if (bombField[row - 1, col] == '*')
                {
                    bombsCount++;
                }
            }

            if (row + 1 < rowsCount)
            {
                if (bombField[row + 1, col] == '*')
                {
                    bombsCount++;
                }
            }

            if (col - 1 >= 0)
            {
                if (bombField[row, col - 1] == '*')
                {
                    bombsCount++;
                }
            }

            if (col + 1 < colsCount)
            {
                if (bombField[row, col + 1] == '*')
                {
                    bombsCount++;
                }
            }

            if ((row - 1 >= 0) && (col - 1 >= 0))
            {
                if (bombField[row - 1, col - 1] == '*')
                {
                    bombsCount++;
                }
            }

            if ((row - 1 >= 0) && (col + 1 < colsCount))
            {
                if (bombField[row - 1, col + 1] == '*')
                {
                    bombsCount++;
                }
            }

            if ((row + 1 < rowsCount) && (col - 1 >= 0))
            {
                if (bombField[row + 1, col - 1] == '*')
                {
                    bombsCount++;
                }
            }

            if ((row + 1 < rowsCount) && (col + 1 < colsCount))
            {
                if (bombField[row + 1, col + 1] == '*')
                {
                    bombsCount++;
                }
            }

            return char.Parse(bombsCount.ToString());
        }

        /// <summary>
        /// Enter the score to the scoreboard when a round is finished.
        /// </summary>
        private void EnterScoreToScoreBoard()
        {
            Console.WriteLine("Enter your nickname for the score board: ");
            string playerNickname = Console.ReadLine();
            Player playerCurrentScore = new Player(playerNickname, this.personalScore);

            ScoreBoardTopPlayers.Add(playerCurrentScore);

            this.personalScore = 0;

            IsNewGame = true;
        }
    }
}

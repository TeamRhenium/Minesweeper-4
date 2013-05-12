namespace Minesweeper
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Engine
    {
        public static List<Player> ScoreBoardTopPlayers = new List<Player>(6);
        public static bool IsNewGame = true;

        private int personalScore = 0;

        private int rowToCheckForBomb;
        private int colToCheckForBomb;

        public string ParseInputCommand(string inputCommand, char[,] fieldWithQuestionmarks)
        {
            Console.Write("Enter row and column : ");
            inputCommand = Console.ReadLine().Trim();

            if (inputCommand.Length == 3)
            {
                if (int.TryParse(inputCommand[0].ToString(), out rowToCheckForBomb) &&
                    int.TryParse(inputCommand[2].ToString(), out colToCheckForBomb) &&
                    rowToCheckForBomb <= fieldWithQuestionmarks.GetLength(0) &&
                    colToCheckForBomb <= fieldWithQuestionmarks.GetLength(1))
                {
                    inputCommand = "turn";
                }
            }

            return inputCommand;
        }

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
                    if (fieldWithBombs[rowToCheckForBomb, colToCheckForBomb] != '*')
                    {
                        IsNewGame = false;
                        SetSurroundingBombsCount(fieldWithQuestionmarks, fieldWithBombs, rowToCheckForBomb, colToCheckForBomb);
                        personalScore++;

                        if (maxScore == personalScore)
                        {
                            Console.WriteLine("Congrats! You won the game!");

                            Draw.PlayingField(fieldWithBombs);

                            EnterScoreToScoreBoard();

                            Draw.ScoreBoard(ScoreBoardTopPlayers);

                            fieldWithQuestionmarks = gameField.Create();
                            fieldWithBombs = gameField.PlaceBombs();

                            personalScore = 0;

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

                        personalScore = 0;

                        IsNewGame = true;
                    }
                    break;

                default:
                    Console.WriteLine("Wrong command.");
                    break;
            }
        }

        private void SetSurroundingBombsCount(char[,] fieldWithBombs, char[,] allBombs, int bombFieldRow, int bombFieldCol)
        {
            char surroundingBombs = GetSurroundingBombsCount(allBombs, bombFieldRow, bombFieldCol);
            allBombs[bombFieldRow, bombFieldCol] = surroundingBombs;
            fieldWithBombs[bombFieldRow, bombFieldCol] = surroundingBombs;
        }

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

        private void EnterScoreToScoreBoard()
        {
            Console.WriteLine("Enter your nickname for the score board: ");
            string playerNickname = Console.ReadLine();
            Player playerCurrentScore = new Player(playerNickname, personalScore);

            ScoreBoardTopPlayers.Add(playerCurrentScore);

            personalScore = 0;

            IsNewGame = true;
        }
    }
}

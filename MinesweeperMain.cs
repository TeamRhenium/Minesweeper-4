namespace Minesweeper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Mines
    {
        public static void Main(string[] args)
        {
            string inputCommand = string.Empty;

            GameField gameField = new GameField(10,10);

            char[,] playingField = gameField.Create();
            char[,] bombsField = gameField.PlaceBombs();

            int personalScore = 0;

            bool isBombHit = false;

            List<Player> scoreBoardTopPlayers = new List<Player>(6);

            int row = 0;
            int col = 0;

            bool isNewGame = true;
            bool isWon = false;

            int maxScore = (gameField.FieldCols * gameField.FieldCols) -
                           (gameField.FieldCols + gameField.FieldCols);

            do
            {
                if (isNewGame)
                {
                   isNewGame = Draw.GameLoad(playingField);
                }

                Console.Write("Enter row and column : ");
                inputCommand = Console.ReadLine().Trim();

                if (inputCommand.Length >= 3)
                {
                    if (int.TryParse(inputCommand[0].ToString(), out row) &&
                        int.TryParse(inputCommand[2].ToString(), out col) &&
                        row <= playingField.GetLength(0) &&
                        col <= playingField.GetLength(1))
                    {
                        inputCommand = "turn";
                    }
                }

                switch (inputCommand)
                {
                    case "top":
                        ShowScoreBoard(scoreBoardTopPlayers);
                        break;
                    case "restart":
                        playingField = gameField.Create();

                        bombsField = gameField.PlaceBombs();

                        Draw.PlayingField(playingField);

                        isBombHit = false;
                        isNewGame = false;
                        break;
                    case "exit":
                        Console.WriteLine("Good bye.");
                        break;
                    case "turn":
                        if (bombsField[row, col] != '*')
                        {
                            if (bombsField[row, col] == '-')
                            {
                                SetSurroundingBombsCount(playingField, bombsField, row, col);
                                personalScore++;
                            }

                            if (maxScore == personalScore)
                            {
                                isWon = true;
                            }
                            else
                            {
                                Draw.PlayingField(playingField);
                            }
                        }
                        else
                        {
                            isBombHit = true;
                        }

                        break;
                    default:
                        Console.WriteLine("Wrong command.");
                        break;
                }

                if (isBombHit)
                {
                    Draw.PlayingField(bombsField);

                    Console.WriteLine("You just hit a bomb. Sorry.");
                    Console.WriteLine("Enter your nickname for the score board: ", personalScore);

                    string nickname = Console.ReadLine();
                    Player playerPersonalScore = new Player(nickname, personalScore);

                    if (scoreBoardTopPlayers.Count < 5)
                    {
                        scoreBoardTopPlayers.Add(playerPersonalScore);
                    }
                    else
                    {
                        for (int i = 0; i < scoreBoardTopPlayers.Count; i++)
                        {
                            if (scoreBoardTopPlayers[i].PlayerPoints < playerPersonalScore.PlayerPoints)
                            {
                                scoreBoardTopPlayers.Insert(i, playerPersonalScore);
                                scoreBoardTopPlayers.RemoveAt(scoreBoardTopPlayers.Count - 1);
                                break;
                            }
                        }
                    }

                    scoreBoardTopPlayers.Sort((Player firstPlayer, Player secondPlayer) => secondPlayer.PlayerName.CompareTo(firstPlayer.PlayerName));
                    scoreBoardTopPlayers.Sort((Player firstPlayer, Player secondPlayer) => secondPlayer.PlayerPoints.CompareTo(firstPlayer.PlayerPoints));
                    ShowScoreBoard(scoreBoardTopPlayers);

                    playingField = gameField.Create();
                    bombsField = gameField.PlaceBombs();

                    personalScore = 0;

                    isBombHit = false;
                    isNewGame = true;
                }

                if (isWon)
                {
                    Console.WriteLine("Congrats! You won the game!");

                    Draw.PlayingField(bombsField);

                    Console.WriteLine("Enter your nickname for the score board: ");
                    string playerNickname = Console.ReadLine();

                    Player playerCurrentScore = new Player(playerNickname, personalScore);
                    scoreBoardTopPlayers.Add(playerCurrentScore);
                    ShowScoreBoard(scoreBoardTopPlayers);

                    playingField = gameField.Create();
                    bombsField = gameField.PlaceBombs();
                    personalScore = 0;

                    isWon = false;
                    isNewGame = true;
                }
            }
            while (inputCommand != "exit");

            Console.WriteLine("Press any key to exit the game.");
            Console.Read();
        }

        private static void ShowScoreBoard(List<Player> allScores)
        {
            Console.WriteLine("Points:");

            if (allScores.Count > 0)
            {
                for (int i = 0; i < allScores.Count; i++)
                {
                    Console.WriteLine("{0}. {1} --> {2} points", i + 1, allScores[i].PlayerName, allScores[i].PlayerPoints);
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Empty score board.");
            }
        }

        private static void SetSurroundingBombsCount(char[,] bombsField, char[,] allBombs, int bombFieldRow, int bombFieldCol)
        {
            char surroundingBombs = GetSurroundingBombsCount(allBombs, bombFieldRow, bombFieldCol);
            allBombs[bombFieldRow, bombFieldCol] = surroundingBombs;
            bombsField[bombFieldRow, bombFieldCol] = surroundingBombs;
        }

        private static char GetSurroundingBombsCount(char[,] bombField, int row, int col)
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
    }
}

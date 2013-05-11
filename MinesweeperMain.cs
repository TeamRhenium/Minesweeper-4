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

            char[,] playingField = CreatePlayingField();
            char[,] bombsField = PlaceBombsOnField();

            int personalScore = 0;

            bool isBombHit = false;

            List<Player> scoreBoardTopPlayers = new List<Player>(6);

            int row = 0;
            int col = 0;

            bool isNewGame = true;
            bool isWon = false;

            const int MaxScore = 35;

            do
            {
                //in Draw.cs -> Rules()
                if (isNewGame)
                {
                    Console.WriteLine("Let's play some Minesweeper! ");
                    Console.WriteLine("Find the cells without bombsField. If you hit a bomb the game ends.");
                    Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                    Console.WriteLine("Menu:");
                    Console.WriteLine("'top' - show the score board");
                    Console.WriteLine("'restart' - start a new game");
                    Console.WriteLine("'exit' - exit the game");
                    Console.WriteLine("'4x7' - example for entering row and col");
                    Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                    Console.WriteLine();

                    DrawPlayingField(playingField);

                    isNewGame = false;
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

                //in Engine.cs -> ExecuteGameCommands()
                switch (inputCommand)
                {
                    case "top":
                        ShowScoreBoard(scoreBoardTopPlayers);
                        break;
                    case "restart":
                        playingField = CreatePlayingField();

                        bombsField = PlaceBombsOnField();

                        DrawPlayingField(playingField);

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

                            if (MaxScore == personalScore)
                            {
                                isWon = true;
                            }
                            else
                            {
                                DrawPlayingField(playingField);
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
                    DrawPlayingField(bombsField);
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

                    playingField = CreatePlayingField();
                    bombsField = PlaceBombsOnField();

                    personalScore = 0;

                    isBombHit = false;
                    isNewGame = true;
                }

                if (isWon)
                {
                    Console.WriteLine("Congrats! You won the game!");

                    DrawPlayingField(bombsField);

                    Console.WriteLine("Enter your nickname for the score board: ");
                    string playerNickname = Console.ReadLine();

                    Player playerCurrentScore = new Player(playerNickname, personalScore);
                    scoreBoardTopPlayers.Add(playerCurrentScore);
                    ShowScoreBoard(scoreBoardTopPlayers);

                    playingField = CreatePlayingField();
                    bombsField = PlaceBombsOnField();
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

        private static void DrawPlayingField(char[,] board)
        {
            int playingFieldRows = board.GetLength(0);
            int playingFieldCols = board.GetLength(1);

            Console.WriteLine("    0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("   ---------------------");

            for (int i = 0; i < playingFieldRows; i++)
            {
                Console.Write("{0} | ", i);

                for (int j = 0; j < playingFieldCols; j++)
                {
                    Console.Write(string.Format("{0} ", board[i, j]));
                }

                Console.Write("|");
                Console.WriteLine();
            }

            Console.WriteLine("   ---------------------");
        }

        private static char[,] CreatePlayingField()
        {
            int boardRows = 5;
            int boardColumns = 10;

            char[,] board = new char[boardRows, boardColumns];

            for (int i = 0; i < boardRows; i++)
            {
                for (int j = 0; j < boardColumns; j++)
                {
                    board[i, j] = '?';
                }
            }

            return board;
        }

        private static char[,] PlaceBombsOnField()
        {
            int bombFieldrows = 5;
            int bombFieldCols = 10;

            char[,] bombField = new char[bombFieldrows, bombFieldCols];

            for (int i = 0; i < bombFieldrows; i++)
            {
                for (int j = 0; j < bombFieldCols; j++)
                {
                    bombField[i, j] = '-';
                }
            }

            List<int> bombMap = new List<int>();

            while (bombMap.Count < 15)
            {
                Random randomInteger = new Random();
                int randomBombLocation = randomInteger.Next(50);
                if (!bombMap.Contains(randomBombLocation))
                {
                    bombMap.Add(randomBombLocation);
                }
            }

            foreach (int bombLocation in bombMap)
            {
                int bombLocationCol = bombLocation / bombFieldCols;
                int bombLocationRow = bombLocation % bombFieldCols;
                if (bombLocationRow == 0 && bombLocation != 0)
                {
                    bombLocationCol--;
                    bombLocationRow = bombFieldCols;
                }
                else
                {
                    bombLocationRow++;
                }

                bombField[bombLocationCol, bombLocationRow - 1] = '*';
            }

            return bombField;
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

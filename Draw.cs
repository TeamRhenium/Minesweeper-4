namespace Minesweeper
{
    using System;
    using System.Collections.Generic;

    public static class Draw
    {

        public static bool GameLoad(char[,] board)
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

            PlayingField(board);

            return false;
        }


        public static void PlayingField(char[,] board)
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

        public static void ScoreBoard(List<Player> allScores)
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
    }
}

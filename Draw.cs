// <copyright file="Draw.cs" company="Telerik Academy">
// Telerik Academy - High Quality Code Team Project. Team Rhenium.
// </copyright>
namespace Minesweeper
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Draws game's console.
    /// </summary>
    public static class Draw
    {

       /// <summary>
       /// Draws the game's rules and commands.
       /// </summary>
        public static void GameLoad()
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
        }

        /// <summary>
        /// Draws the game's playing field.
        /// </summary>
        /// <param name="board">The playing field to be drawn on the console.</param>
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

        /// <summary>
        /// Draws the game's score board.
        /// </summary>
        /// <param name="players">List with players. Each player has name and scores</param>
        public static void ScoreBoard(List<Player> players)
        {
            Console.WriteLine("Points:");

            if (players.Count > 0)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    Console.WriteLine("{0}. {1} --> {2} points", i + 1, players[i].PlayerName, players[i].PlayerPoints);
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

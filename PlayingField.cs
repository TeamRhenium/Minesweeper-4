using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minesweeper
{
    public static class PlayingField
    {

        public static char[,] Create()
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

        public static char[,] PlaceBombs()
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
    }
}

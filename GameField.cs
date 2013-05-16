// <copyright file="GameField.cs" company="Telerik Academy">
// Telerik Academy - High Quality Code Team Project. Team Rhenium.
// </copyright>
namespace Minesweeper
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Initializes a new instance of the <seealso cref="GameField"/> class.
    /// </summary>
    public class GameField
    {
        private readonly int fieldRows;
        private readonly int fieldCols;

        /// <summary>
        /// GameField class constructor.
        /// </summary>
        /// <param name="fieldRows">Game's field number of rows given as parameter.</param>
        /// <param name="fieldCols">Game's field number of columns given as parameter.</param>
        public GameField(int fieldRows, int fieldCols)
        {
            if (fieldRows <= 1 || fieldRows > 10)
            {
                throw new ArgumentOutOfRangeException("The rows must be between 2 and 10.");
            }
            if (fieldCols <= 1 || fieldCols > 10)
            {
                throw new ArgumentOutOfRangeException("The cols must be between 2 and 10.");
            }

            this.fieldRows = fieldRows;
            this.fieldCols = fieldCols;
        }

        /// <summary>
        /// GameField class property. Returns the number of game's field rows.
        /// </summary>
        public int FieldRows
        {
            get
            {
                return this.fieldRows;
            }
        }

        /// <summary>
        /// GameField class property. Returns the number of game's field columns.
        /// </summary>
        public int FieldCols
        {
            get
            {
                return this.fieldCols;
            }
        }

        /// <summary>
        /// Creates new game field depending on it's number of rows and columns.
        /// </summary>
        /// <returns>Returns game field</returns>
        public char[,] Create()
        {
            char[,] board = new char[this.fieldRows, this.fieldCols];

            for (int i = 0; i < this.fieldRows; i++)
            {
                for (int j = 0; j < this.fieldCols; j++)
                {
                    board[i, j] = '?';
                }
            }

            return board;
        }


        /// <summary>
        /// Places bombs on the game field. The number of bombs is equal to the sum of game's field number of rows and columns.
        /// The bombs are placed randomly on the game field.
        /// </summary>
        /// <returns>Returns a game field with bombs placed on it.</returns>
        public char[,] PlaceBombs()
        {
            char[,] bombField = new char[this.fieldRows, this.fieldCols];

            int totalCellsCount = fieldCols * fieldRows;
            int totalBombsCount = fieldCols + fieldRows;

            for (int i = 0; i < this.fieldRows; i++)
            {
                for (int j = 0; j < this.fieldCols; j++)
                {
                    bombField[i, j] = '-';
                }
            }

            List<int> bombsMap = new List<int>();

            while (bombsMap.Count < totalBombsCount)
            {
                Random randomInteger = new Random();
                int randomBombLocation = randomInteger.Next(totalCellsCount);

                if (!bombsMap.Contains(randomBombLocation))
                {
                    bombsMap.Add(randomBombLocation);
                }
            }

            foreach (int bombLocation in bombsMap)
            {
                int bombLocationCol = bombLocation / this.fieldCols;
                int bombLocationRow = bombLocation % this.fieldCols;

                if (bombLocationRow == 0 && bombLocation != 0)
                {
                    bombLocationCol--;
                    bombLocationRow = this.fieldCols;
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

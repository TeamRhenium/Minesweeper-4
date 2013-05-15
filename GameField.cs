﻿// <copyright file="GameField.cs" company="Telerik Academy">
// Telerik Academy - High Quality Code Team Project. Team Rhenium.
// </copyright>
namespace Minesweeper
{
    using System;
    using System.Collections.Generic;

    public class GameField
    {
        private readonly int fieldRows;
        private readonly int fieldCols;

        public GameField(int fieldRows, int fieldCols)
        {
            if (fieldRows <= 0 || fieldRows > 10)
            {
                throw new ArgumentOutOfRangeException("The rows must be between 1 and 10.");
            }
            if (fieldCols <= 0 || fieldCols > 10)
            {
                throw new ArgumentOutOfRangeException("The cols must be between 1 and 10.");
            }

            this.fieldRows = fieldRows;
            this.fieldCols = fieldCols;
        }

        public int FieldRows
        {
            get
            {
                return this.fieldRows;
            }
        }

        public int FieldCols
        {
            get
            {
                return this.fieldCols;
            }
        }

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

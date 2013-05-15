// <copyright file="Player.cs" company="Telerik Academy">
// Telerik Academy - High Quality Code Team Project. Team Rhenium.
// </copyright>
namespace Minesweeper
{
    using System;

    public class Player
    {
        private string playerName;
        private int playerPoints;

        public Player()
        {
        }

        public Player(string playerName, int playerPoints)
        {
            this.PlayerName = playerName;
            this.PlayerPoints = playerPoints;
        }

        public string PlayerName
        {
            get { return this.playerName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Player's name must not be empty!");
                }
                else
                {
                    this.playerName = value;
                }
            }
        }

        public int PlayerPoints
        {
            get { return this.playerPoints; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Player's score must not be negative value!");
                }
                else
                {
                    this.playerPoints = value;
                }
            }
        }
    }
}

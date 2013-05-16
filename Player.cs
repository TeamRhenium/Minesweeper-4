// <copyright file="Player.cs" company="Telerik Academy">
// Telerik Academy - High Quality Code Team Project. Team Rhenium.
// </copyright>
namespace Minesweeper
{
    using System;

    /// <summary>
    /// Initializes a new instance of the <seealso cref="Player"/> class.
    /// </summary>
    public class Player
    {
        private string playerName;
        private int playerPoints;

        /// <summary>
        /// Player class constructor.
        /// </summary>
        /// <param name="playerName">Player's name given as parameter.</param>
        /// <param name="playerPoints">Player's points given as parameter.</param>
        public Player(string playerName, int playerPoints)
        {
            this.PlayerName = playerName;
            this.PlayerPoints = playerPoints;
        }

        /// <summary>
        /// Player class property. Validates the given playerName parameter in the constructor.
        /// Returns the player's name.
        /// </summary>
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

        /// <summary>
        /// Player class property. Validates the given playerPoints parameter in the constructor.
        /// Returns the player's points.
        /// </summary>
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

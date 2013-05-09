namespace Minesweeper
{
    using System;

    public class Score
    {
        private string playerName;
        private int playerPoints;

        public Score()
        {
        }

        public Score(string playerName, int playerPoints)
        {
            this.PlayerName = playerName;
            this.PlayerPoints = playerPoints;
        }

        public string PlayerName
        {
            get { return this.playerName; }
            set { this.playerName = value; }
        }

        public int PlayerPoints
        {
            get { return this.playerPoints; }
            set { this.playerPoints = value; }
        }
    }
}

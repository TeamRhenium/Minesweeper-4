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
            set { this.playerName = value; }
        }

        public int PlayerPoints
        {
            get { return this.playerPoints; }
            set { this.playerPoints = value; }
        }

        public void AddScore(Player playerPersonalScore)
        {
            if (Engine.ScoreBoardTopPlayers.Count < 5)
            {
                Engine.ScoreBoardTopPlayers.Add(playerPersonalScore);
            }
            else
            {
                for (int i = 0; i < Engine.ScoreBoardTopPlayers.Count; i++)
                {
                    if (Engine.ScoreBoardTopPlayers[i].PlayerPoints < playerPersonalScore.PlayerPoints)
                    {
                        Engine.ScoreBoardTopPlayers.Insert(i, playerPersonalScore);
                        Engine.ScoreBoardTopPlayers.RemoveAt(Engine.ScoreBoardTopPlayers.Count - 1);
                        break;
                    }
                }
            }

            Engine.ScoreBoardTopPlayers.Sort((Player firstPlayer, Player secondPlayer) => secondPlayer.PlayerName.CompareTo(firstPlayer.PlayerName));
            Engine.ScoreBoardTopPlayers.Sort((Player firstPlayer, Player secondPlayer) => secondPlayer.PlayerPoints.CompareTo(firstPlayer.PlayerPoints));
        }
    }
}

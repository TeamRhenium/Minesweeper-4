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

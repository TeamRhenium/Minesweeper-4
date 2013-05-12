namespace Minesweeper
{
    using System;

    public class MinesweeperDemo
    {
         public static void Main(string[] args)
        {
            string inputCommand = string.Empty;

            GameField gameField = new GameField(10,10);

            char[,] playingField = gameField.Create();
            char[,] bombsField = gameField.PlaceBombs();

            int maxScore = (gameField.FieldCols * gameField.FieldCols) -
                           (gameField.FieldCols + gameField.FieldCols);

            Engine engine = new Engine();

            do
            {
                if (Engine.IsNewGame)
                {
                    playingField = gameField.Create();
                    bombsField = gameField.PlaceBombs();

                    Draw.GameLoad(playingField);
                }

                inputCommand = engine.ParseInputCommand(inputCommand, playingField);

                engine.ExecuteCommand(inputCommand, gameField, playingField, bombsField, maxScore);

            }
            while (inputCommand != "exit");

            Console.WriteLine("Press any key to exit the game.");
            Console.Read();
        }
    }
}

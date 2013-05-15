// <copyright file="MinesweeperDemo.cs" company="Telerik Academy">
// Telerik Academy - High Quality Code Team Project. Team Rhenium.
// </copyright>
namespace Minesweeper
{
    using System;

    /// <summary>
    /// The entry point of the game. 
    /// </summary>
    public class MinesweeperDemo
    {
         public static void Main(string[] args)
        {
            GameField gameField = new GameField(10,10);

            char[,] playingField = gameField.Create();
            char[,] bombsField = gameField.PlaceBombs();

            int maxScore = (gameField.FieldCols * gameField.FieldCols) -
                           (gameField.FieldCols + gameField.FieldCols);

            Engine engine = new Engine();
            
            while (true)
            {
                if (Engine.IsNewGame)
                {
                    playingField = gameField.Create();
                    bombsField = gameField.PlaceBombs();

                    Draw.GameLoad();
                    Draw.PlayingField(playingField);
                }

                Console.Write("Enter row and column: ");
                string inputCommand = Console.ReadLine();

                if (inputCommand == "exit")
                {
                    break;
                }

                inputCommand = engine.ParseInputCommand(inputCommand, playingField);

                engine.ExecuteCommand(inputCommand, gameField, playingField, bombsField, maxScore);
            }            

            Console.WriteLine("Press any key to exit the game.");
            Console.Read();
        }
    }
}

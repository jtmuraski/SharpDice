using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarkleSharp
{
    public static class Game
    {

        public static void MainMenu()
        {
            AnsiConsole.Clear();
            AnsiConsole.Markup("[green]" + FiggleFonts.Standard.Render("Farkle Sharp!") + "[/]");
            var mainMenuSelection = AnsiConsole.Prompt(
                   new SelectionPrompt<string>()
                          .Title("Welcome to Farkle Sharp! Please select an option from below to continue:")
                          .PageSize(10)
                          .AddChoices(new[] { "Play a Game", "View Instructions", "Quit" }));

            switch (mainMenuSelection)
            {
                case "Play a Game":
                    PlayGame();
                    MainMenu();
                    break;
                case "View Instructions":
                    ViewInstructions();
                    MainMenu();
                    break;
                case "Quit":
                    return;
                default:
                    AnsiConsole.MarkupLine("[red]Invalid selection. Please try again.[/]");
                    break;
            }

        }

        public static void PlayGame()
        {
            FarkleGame farkleGame = GameServices.SetUpGame();
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("The game is ready! Let's Play! Press any key to start the game.");
            Console.Read();
            bool gameIsWon = false;
            int currentGameTurn = 1;
            int currentPlayerTurn = 1;
            do
            {
                AnsiConsole.Clear();
                var currentPlayer = farkleGame.Players.Single(p => p.TurnOrderNbr == currentPlayerTurn);
                // ---Game Layout---
                var layout = new Layout("Root")
                                .SplitRows(
                                          new Layout("Top").SplitColumns(new Layout("Left").MinimumSize(8),
                                                                        new Layout("Right")).MinimumSize(8),
                                          new Layout("DiceRoll"));
                layout["Top"]["Left"].Update(new Panel(
                                                    Align.Left(new Markup($"[green]Current Player: {currentPlayer.Name}[/]"),
                                                    VerticalAlignment.Top)));

                layout["Top"]["Right"].Update(DrawBottomRightPanel(farkleGame));

                layout["DiceRoll"].Update(new Panel(GameServices.DrawDice(1)).Expand());
                AnsiConsole.Write(layout);
                var answer = AnsiConsole.Ask<int>("Roll the dice?");
                Console.Read();
            } while (!gameIsWon);
            return;
        }

        private static void ViewInstructions()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("Here are the instructions on how to play...");
            AnsiConsole.MarkupLine("Press any key to return to the menu");
            Console.ReadLine();
            return;
        }

        private static Panel DrawBottomRightPanel(FarkleGame farkelGame)
        {
            var table = new Table();
            table.AddColumn("Player");
            table.AddColumn("Score");
            foreach(var player in farkelGame.Players)
            {
                table.AddRow(player.Name, player.Score.ToString());
            }
           return new Panel(Align.Center(table, VerticalAlignment.Top)).Header("Current Scores");

        }

        
    }
}

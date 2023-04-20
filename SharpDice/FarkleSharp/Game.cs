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
            bool gameIsReady = false;
            FarkleGame farkleGame = new FarkleGame();

            // ---Game Set Up---
            do
            {
                AnsiConsole.Clear();
                AnsiConsole.Markup("Let's set up the game. How many players will be playing? ");
                var playerCount = AnsiConsole.Ask<int>("Please enter a number between 2 and 6: ");
                if (playerCount < 2 || playerCount > 6)
                {
                    AnsiConsole.MarkupLine("[red]Invalid selection. Please try again.[/]");
                    PlayGame();
                }
                else
                {
                    AnsiConsole.MarkupLine($"[green]You selected {playerCount} players.[/]");
                }

                for (int i = 0; i < playerCount; i++)
                {
                    var name = AnsiConsole.Ask<string>($"What is Player {i + 1}'s name?");
                    farkleGame.Players.Add(new Player(name, true, i+1, i+1));
                }

                var score = AnsiConsole.Ask<int>("What score would you like to play to?");
                farkleGame.SetScoreToWin(score);

                // --Confirm Game Setup--
                AnsiConsole.MarkupLine("[blue]These are our players for this game:[/]");
                int count = 1;
                foreach (var player in farkleGame.Players)
                {
                    AnsiConsole.MarkupLine($"Player {count}: {player.Name}");
                }
                AnsiConsole.MarkupLine($"The score that we will play to: {farkleGame.ScoreToWin}");
                if (AnsiConsole.Confirm("Are these settings correct?"))
                    gameIsReady = true;
                else
                    farkleGame.Reset();

            } while (!gameIsReady);
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("The game is ready! Let's Play! Press any key to start the game.");
            Console.Read();
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
    }
}

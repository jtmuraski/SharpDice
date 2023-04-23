using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace FarkleSharp
{
    public static class GameServices
    {
        public static FarkleGame SetUpGame()
        {
            bool gameIsReady = false;
            FarkleGame farkleGame = new FarkleGame();
            // ---Game Set Up---
            do
            {
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("Let's set up our game!");
                var playerCount = AnsiConsole.Prompt(new TextPrompt<int>("Please enter a number between 2 and 6: ")
                    .ValidationErrorMessage("Yoy did not enter a valid number of players")
                    .Validate(age => 
                        { return age >= 2 && age <= 6; }));

                for (int i = 0; i < playerCount; i++)
                {
                    AnsiConsole.MarkupLine($"Please enter the name of player {i + 1}: ");
                    var playerName = AnsiConsole.Ask<string>("");
                    farkleGame.AddPlayer(playerName, true, i +1, i +1);
                }

                farkleGame.SetScoreToWin(AnsiConsole.Prompt(new TextPrompt<int>("What score would you like to play to?")
                                                            .ValidationErrorMessage("[red]Yor did not enter a valid score[/]")
                                                            .Validate(score =>
                                                            {
                                                                return score switch
                                                                {
                                                                    <= 5000 => ValidationResult.Error("[red]You must enter a minimum score of 5000[/]"),
                                                                    >= 15000 => ValidationResult.Error("[red]You must enter a maximum score of 15000[/]"),
                                                                    _ => ValidationResult.Success()
                                                                };
                                                            })));

                AnsiConsole.MarkupLine($"[green]You have selected the following players:[/]");
                foreach (var player in farkleGame.Players)
                {
                    AnsiConsole.MarkupLine($"[green]{player.Name}[/]");
                }
                AnsiConsole.MarkupLine($"[green]The winning score is: {farkleGame.ScoreToWin}[/]");
                var confirmPlayers = AnsiConsole.Prompt(
                                       new SelectionPrompt<string>()
                                           .Title("Are these the players you would like to play with?")
                                           .PageSize(10)
                                           .AddChoices(new[] { "Yes", "No" }));
                gameIsReady = confirmPlayers == "Yes" ? true : false;
            } while (!gameIsReady);
            return farkleGame;      
        }
    }
}

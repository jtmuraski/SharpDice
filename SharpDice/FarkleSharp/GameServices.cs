using FarkleSharp.Components;
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
        // ---Class Variables---

        // --Dice Point Coordinates--
        private static List<Point> DiceOne = new List<Point>()
        {
            new Point(5,5),
            new Point(5,6),
            new Point(6,5),
            new Point(6,6)
        };
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
                    .ValidationErrorMessage("You did not enter a valid number of players")
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

        public static Canvas DrawDice(int roll)
        {
            var canvas = new Canvas(12, 12);
            switch (roll)
            {
                case 1:
                    for(int i = 0; i < canvas.Width; i++)
                    {
                        for(int h = 0; h < canvas.Height; h++)
                        {
                            Point pixel = new Point(i, h);
                            bool isDot = false;
                            foreach(var p in DiceOne)
                            {
                                if ((pixel.X == p.X) && (pixel.Y == p.Y))
                                    isDot = true;
                            }

                            if (isDot)
                                canvas.SetPixel(i, h, Color.Black);
                            else
                                canvas.SetPixel(i, h, Color.White);
                        }
                        
                    }
                    break;
                case 2:
                    canvas = new Canvas(5, 5)
                        .SetPixel(1, 1, Color.Red)
                        .SetPixel(3, 3, Color.Red);
                    break;
                case 3:
                    canvas = new Canvas(5, 5)
                        .SetPixel(1, 1, Color.Red)
                        .SetPixel(2, 2, Color.Red)
                        .SetPixel(3, 3, Color.Red);
                    break;
                case 4:
                    canvas = new Canvas(5, 5)
                        .SetPixel(1, 1, Color.Red)
                        .SetPixel(3, 1, Color.Red)
                        .SetPixel(1, 3, Color.Red)
                        .SetPixel(3, 3, Color.Red);
                    break;
                case 5:
                    canvas = new Canvas(5, 5)
                        .SetPixel(1, 1, Color.Red)
                        .SetPixel(3, 1, Color.Red)
                        .SetPixel(1, 3, Color.Red)
                        .SetPixel(3, 3, Color.Red)
                        .SetPixel(2, 2, Color.Red);
                    break;
                case 6:
                    canvas = new Canvas(5, 5)
                        .SetPixel(1, 1, Color.Red)
                        .SetPixel(3, 1, Color.Red)
                        .SetPixel(1, 3, Color.Red)
                        .SetPixel(3, 3, Color.Red)
                        .SetPixel(1, 2, Color.Red)
                        .SetPixel(3, 2, Color.Red);
                    break;
                default:
                    break;
            }
            return canvas;
        }
    }
}

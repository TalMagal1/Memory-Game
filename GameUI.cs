using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ex02
{
    internal class GameUI
    {
        private static string[] errorMessages =
        {
            "Empty input. Please enter a card coordinates .",
            "Invalid input. Please enter digits followed by a letter",
            "Invalid input. Please enter a coordinates in the board bounds",
            "Invalid input. This spot is taken"
        };

        private const int NO = 0;
        private const int YES = 1;

        public static int[] GetAndValidateCardCoordinates(BoardData i_GameBoard)
        {
            string input = null;
            int errorNumber;
            do
            {
                Console.Write("Enter a digit followed by a letter (e.g., 2A): ");
                input = Console.ReadLine();
                if (QuitGame(input))
                {
                    Console.WriteLine("You have asked to quit in the middle of the game, it a shame..");
                    Environment.Exit(1);
                }
                errorNumber = GameLogics.ValidateCardCoordinatesInput(input, i_GameBoard);
                if (errorNumber != GameLogics.VALID_INPUT)
                {
                    PrintError(errorNumber);
                }
            } while (errorNumber != GameLogics.VALID_INPUT);

            return GameLogics.InputToBoardCoordinates(input);
        }

        public static void PrintError(int i_ErrorNumber)
        {
            Console.WriteLine(errorMessages[i_ErrorNumber]);
        }

        public static void HandlePlayersInputs(Player io_P1, Player io_P2)
        {
            HandlePlayerOneInput(io_P1);
            HandlePlayerTwoInput(io_P2);
        }

        public static BoardData BoardBuilder()
        {
            int widthInput;
            int heightInput;
            do
            {
                Console.WriteLine("Please Enter Board Width: ");
                widthInput = int.Parse(Console.ReadLine());
                Console.WriteLine("Please Enter Board Height: ");
                heightInput = int.Parse(Console.ReadLine());
                if (!GameLogics.ValidateBoardDimensions(widthInput, heightInput))
                {
                    Console.WriteLine("Input Invalid! Please Enter At Least One Even Dimension");
                }
            } while (!GameLogics.ValidateBoardDimensions(widthInput, heightInput));

            BoardData o_GameBoard = new BoardData(heightInput, widthInput);
            return o_GameBoard;
        }

        public static void HandlePlayerOneInput(Player io_p1)
        {
            io_p1.Name = GetNameInput();
        }

        public static void HandlePlayerTwoInput(Player io_p2)
        {
            Console.WriteLine("Who Is Your Rival? : \n 1 - Human \n 2 - PC ");
            string p2TypeInput = Console.ReadLine();
            io_p2.Type = int.Parse(p2TypeInput);
            if (io_p2.Type == Player.HUMAN)
            {
                io_p2.Name = GetNameInput();
            }
            else
            {
                io_p2.Name = "PC";
            }
        }

        public static string GetNameInput()
        {
            Console.WriteLine("Please Enter Player Name: ");
            string playerName = Console.ReadLine();
            return playerName;
        }

        public static void PrintBoard(BoardData i_board)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            Console.Write("   ");
            for (int i = 0; i < i_board.Width; i++)
            {
                Console.Write("  " + (BoardData.eCards)i + " ");
            }

            Console.WriteLine("");
            for (int i = 0; i < i_board.Width; i++)
            {
                Console.Write("=====");
            }

            Console.WriteLine();
            for (int i = 0; i < i_board.Height; i++)
            {
                Console.Write(" " + (i + 1) + " |");
                for (int j = 0; j < i_board.Width; j++)
                {
                    if (i_board.RevealedCards[i, j] == true)
                    {
                        Console.Write(" " + i_board.Board[i, j] + " |");
                    }
                    else
                    {
                        Console.Write("   |");
                    }
                }

                Console.WriteLine("");
                for (int k = 0; k < i_board.Width; k++)
                {
                    Console.Write("=====");
                }

                Console.WriteLine("");
            }
        }

        public static int GameOver(Player i_player1, Player i_player2)
        {
            string endingChoice;
            string winnerName;
            if (i_player1.Points > i_player2.Points)
            {
                winnerName = i_player1.Name;
            }
            else if (i_player1.Points < i_player2.Points)
            {
                winnerName = i_player2.Name;
            }
            else
            {
                winnerName = "Its A Tie";
            }

            string msg = string.Format(
                @"Congrats {0}! Game Over.
Stats :
{1} has {2} points.
{3} has {4} points.",
                winnerName, i_player1.Name, i_player1.Points,
                i_player2.Name, i_player2.Points);
            Console.WriteLine(msg);
            Console.WriteLine("Do you wish to start a new game?( 1 = YES /0 = NO )");
            endingChoice = Console.ReadLine();
            return int.Parse(endingChoice);
        }

        public static void PcMoveGreeting()
        {
            Console.WriteLine("PC is thinking");
            Thread.Sleep(1000);
        }

        public static bool QuitGame(string input)
        {
            return input.Contains('Q');
        }
    }
}


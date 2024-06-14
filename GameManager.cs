using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ex02
{
    internal static class GameManager
    {
        public static int HandleGameFlow()
        {
            bool gameInProccess = true;
            int startANewGame = 0;
            int errorNumber = -1;
            Player p1 = new Player();
            Player p2 = new Player();
            GameUI.HandlePlayersInputs(p1, p2);
            BoardData gameBoard = GameUI.BoardBuilder();
            GameUI.PrintBoard(gameBoard);
            while (gameInProccess)
            {
                turn(p1, gameBoard);
                if (gameBoard.IsGameOver())
                {
                    gameInProccess = false;
                    startANewGame = GameUI.GameOver(p1, p2);
                    break;
                }

                turn(p2, gameBoard);
                if (gameBoard.IsGameOver())
                {
                    gameInProccess = false;
                    startANewGame = GameUI.GameOver(p1, p2);
                }
            }
            return startANewGame;
        }

        private static void turn(Player i_Player, BoardData i_gameBoard)
        {
            Console.WriteLine("This is " + i_Player.Name + " turn now");
            int errorNumber = -1;
            int[] FirstCardCoordinats = new int[2];
            int[] SecondCardCoordinats = new int[2];

            if (i_Player.Name == "PC")
            {
                GameUI.PcMoveGreeting();
                FirstCardCoordinats = i_Player.AIGenrateMove(i_gameBoard);
            }
            else
            {
                FirstCardCoordinats = GameUI.GetAndValidateCardCoordinates(i_gameBoard);
            }

            errorNumber = i_Player.CardReveal(FirstCardCoordinats[0], FirstCardCoordinats[1], i_gameBoard);
            while (errorNumber != GameLogics.VALID_INPUT)
            {
                GameUI.PrintError(errorNumber);
                FirstCardCoordinats = GameUI.GetAndValidateCardCoordinates(i_gameBoard);
                errorNumber = i_Player.CardReveal(FirstCardCoordinats[0], FirstCardCoordinats[1], i_gameBoard);
            }

            GameUI.PrintBoard(i_gameBoard);


            if (i_Player.Name == "PC")
            {

                GameUI.PcMoveGreeting();
                SecondCardCoordinats = i_Player.AIGenrateMove(i_gameBoard);
            }
            else
            {
                SecondCardCoordinats = GameUI.GetAndValidateCardCoordinates(i_gameBoard);
            }

            errorNumber = i_Player.CardReveal(SecondCardCoordinats[0], SecondCardCoordinats[1], i_gameBoard);
            while (errorNumber != GameLogics.VALID_INPUT)
            {
                GameUI.PrintError(errorNumber);
                SecondCardCoordinats = GameUI.GetAndValidateCardCoordinates(i_gameBoard);
                errorNumber = i_Player.CardReveal(SecondCardCoordinats[0], SecondCardCoordinats[1], i_gameBoard);
            }

            if (i_gameBoard.IsCardsMatching(FirstCardCoordinats[0], FirstCardCoordinats[1],
                    SecondCardCoordinats[0], SecondCardCoordinats[1]))
            {
                i_gameBoard.RevealCard(SecondCardCoordinats[0], SecondCardCoordinats[1]);
                i_Player.Points++;
            }
            else
            {
                i_gameBoard.RevealCard(SecondCardCoordinats[0], SecondCardCoordinats[1]);
                GameUI.PrintBoard(i_gameBoard);
                Thread.Sleep(2000);
                i_gameBoard.UnrvealCards(FirstCardCoordinats[0], FirstCardCoordinats[1],
                    SecondCardCoordinats[0], SecondCardCoordinats[1]);
            }
            GameUI.PrintBoard(i_gameBoard);
        }


    }
}

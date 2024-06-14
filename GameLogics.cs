using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    internal static class GameLogics
    {
        public const int VALID_INPUT = -1;
        public static int ValidateCardCoordinatesInput(string i_cardCoordinates, BoardData i_GameBoard)
        {
            int errorMessageIndex = VALID_INPUT;
            errorMessageIndex = IsInputContainNumberAndLetter(i_cardCoordinates);
            if (errorMessageIndex == VALID_INPUT)
            {
                errorMessageIndex = validateCardInputCoordinatesInBoard(i_cardCoordinates, i_GameBoard);
            }
            return errorMessageIndex;
        }
        private static int validateCardInputCoordinatesInBoard(string i_CardInput, BoardData i_GameBoard)
        {
            int errorMessageIndex;
            int[] RowAndCol = InputToBoardCoordinates(i_CardInput);
            int heightNumericValue = RowAndCol[0];
            int widthNumericValue = RowAndCol[1] + 1;
            if (heightNumericValue <= i_GameBoard.Height &&
                heightNumericValue >= 0 &&
                widthNumericValue <= i_GameBoard.Width &&
                widthNumericValue >= 0)
            {
                Console.WriteLine("valid input");
                errorMessageIndex = -1;
            }
            else
            {
                Console.WriteLine("invalid input");
                errorMessageIndex = 2;
            }
            return errorMessageIndex;
        }
        public static bool  ValidateBoardDimensions(int i_Width, int i_Height)
        {
            return ((i_Height * i_Width)%2 == 0);
        }
        public static int[] InputToBoardCoordinates(string i_CardInput)
        {
            string numbers ="";
            string chars = "";
            foreach (char c in i_CardInput)
            {
                if (Char.IsDigit(c))
                {
                    numbers = numbers + c;
                }
                else
                {
                    chars += c;
                }
            }

            int[] coordinates = new int[2];
            coordinates[0] = int.Parse(numbers);
            coordinates[1] = (int)Char.ToUpper(chars[0]) - (int)('A');
            return coordinates;
        }
        public static int IsInputContainNumberAndLetter(string i_Input)
        {
            int errorMessageNumber = VALID_INPUT;
            if (string.IsNullOrEmpty(i_Input) || i_Input.Length == 1)
            {
                errorMessageNumber = 0; // empty input
            }

            int length = i_Input.Length;

            if (!Char.IsLetter(i_Input[length - 1]))
            {
                errorMessageNumber = 1;
            }

            for (int i = 0; i < length - 1; i++)
            {
                if (!Char.IsDigit(i_Input[i]))
                {
                    errorMessageNumber = 1;
                }
            }

            return errorMessageNumber;
        }
    }

}
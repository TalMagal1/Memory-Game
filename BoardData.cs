using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ex02
{
    internal class BoardData
    {
        private eCards[,] m_Board;
        private bool[,] m_RevealedCards;
        private readonly int r_Width;
        private readonly int r_Height;
        private readonly int r_NumOfCards;

        public enum eCards
        {
            A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z
        }

        public BoardData(int i_height, int i_width)
        {
            r_Width = i_width;
            r_Height = i_height;
            r_NumOfCards = i_height * i_width;
            m_Board = new eCards[i_height, i_width];
            m_RevealedCards = new bool[i_height, i_width];
            assignCardsToBoard();
        }

        public eCards[,] Board
        {
            get { return m_Board; }
        }

        public bool[,] RevealedCards
        {
            get { return m_RevealedCards; }
        }

        public int Width
        {
            get { return r_Width; }
        }

        public int Height
        {
            get { return r_Height; }
        }

        public int NumOfCards
        {
            get { return r_NumOfCards; }
        }

        public bool IsFreeSpot(int i_row, int i_col)
        {
            return !m_RevealedCards[i_row - 1, i_col];
        }
        public void RevealCard(int i_row, int i_col)
        {
            m_RevealedCards[i_row - 1, i_col] = true;
        }
        public bool IsCardsMatching(int i_PrevRow, int i_PrevCol, int i_NewRow, int i_NewCol)
        {
            return m_Board[i_NewRow - 1, i_NewCol] == m_Board[i_PrevRow - 1, i_PrevCol];
        }
        public void UnrvealCards(int i_PrevRow, int i_PrevCol, int i_NewRow, int i_NewCol)
        {
            m_RevealedCards[i_PrevRow - 1, i_PrevCol] = false;
            m_RevealedCards[i_NewRow - 1, i_NewCol] = false;
        }
        public int ValidateSpotAvailabilty(int i_Row, int i_Col)
        {
            int errorMessageIndex;

            if (this.IsFreeSpot(i_Row, i_Col))
            {
                errorMessageIndex = -1;
            }
            else
            {
                errorMessageIndex = 3;
            }

            return errorMessageIndex;

        }
        private void assignCardsToBoard()
        {
            int [] cardsStorage = new int[r_NumOfCards];
            fillCardsStorageWithValues(cardsStorage);

            for (int i = 0; i < r_Height; i++)
            {
                for (int j = 0; j < r_Width; j++)
                {
                    m_Board[i, j] = getCardToAssign(cardsStorage);
                }
            }
        }
        private eCards getCardToAssign(int[] io_cardsStorage)
        {
            eCards resCard = eCards.A; //initial value
            var rand = new Random();
            bool cardFound = false;
            while (!cardFound)
            {
                int randomIndex = rand.Next(r_NumOfCards);
                if (io_cardsStorage[randomIndex] != -1)
                {
                    resCard = (eCards)io_cardsStorage[randomIndex];
                    io_cardsStorage[randomIndex] = -1;
                    cardFound = true;
                }
            }

            return resCard;
        }
        private void fillCardsStorageWithValues(int[] io_cardsStorage)
        {
            int index = 0;

            for (int i = 0; i < r_NumOfCards / 2; i++)
            {
                io_cardsStorage[index++] = i;
                io_cardsStorage[index++] = i;
            }
        }
        public bool IsGameOver()
        {
            bool gameOver = true;
            for (int i = 0; i < r_Height && gameOver; i++)
            {
                for (int j = 0; j < r_Width && gameOver; j++)
                {
                    if (m_RevealedCards[i, j] == false)
                    {
                        gameOver = false;
                        
                    }
                }
            }

            return gameOver;
        }
    }
}

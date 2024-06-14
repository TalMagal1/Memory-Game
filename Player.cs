using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    internal class Player
    {
        public const int HUMAN = 1;
        public const int PC = 2;
        public static int s_NumOfPlayers = 0;

        private string m_Name;
        private int m_Type; 
        private readonly int r_Id;
        private int m_PointsEarned = 0;
        public Player()
        {
            s_NumOfPlayers++;
            r_Id = s_NumOfPlayers;
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        public int Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }
        public int Points
        {
            get { return m_PointsEarned; }
            set { m_PointsEarned = value; }
        }

        public int CardReveal(int i_Row, int i_Col, BoardData i_GameBoard)
        {
            int errorNumber = i_GameBoard.ValidateSpotAvailabilty(i_Row, i_Col);
            if (errorNumber == GameLogics.VALID_INPUT)
            {
                 i_GameBoard.RevealCard(i_Row, i_Col);
            }

            return errorNumber;
        } // 

        public int[] AIGenrateMove(BoardData i_Board)
        {
            int[] pcMove = new int[2];
            var rand = new Random();
            int randomWidthIndex;
            int radomHeightIndex;
            do
            {
                radomHeightIndex = rand.Next(1, i_Board.Height + 1);
                randomWidthIndex = rand.Next(1, i_Board.Width);
            } while (!i_Board.IsFreeSpot(radomHeightIndex, randomWidthIndex));
            pcMove[0] = radomHeightIndex; 
            pcMove[1] = randomWidthIndex;
            return pcMove;
        }
    }
}
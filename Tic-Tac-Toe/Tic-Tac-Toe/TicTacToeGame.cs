using System;
using System.Collections.Generic;
using System.Text;

namespace Tic_Tac_Toe
{
    public delegate void PlayerHandler(int player);
    public delegate void PlayerXHandler(int x, int player);

    public class TicTacToeGame
    {
        // int player:
        // -1 - None
        // 0 - X
        // 1 - O
        public PlayerXHandler BoxChangeHandler;
        public PlayerHandler PlayerTurnHandler;
        public PlayerHandler WinnerHandler;
        private int BoardPieceMask = 0;
        private int BoardPlayerMask = 0;
        private int Turn = 0;
        private int Winner = -1;
        private int[] WinningStates = {
            0b000000111,
            0b000111000,
            0b111000000,
            0b001001001,
            0b010010010,
            0b100100100,
            0b100010001,
            0b001010100,
        };

        public void Reset()
        {
            BoardPieceMask = 0;
            BoardPlayerMask = 0;
            Turn = 0;
            Winner = -1;
            for (int x = 0; x < 9; x++)
                BoxChangeHandler.Invoke(x, -1);
            PlayerTurnHandler.Invoke(Turn);
        }

        public void PlacePiece(int x)
        {
            if (((BoardPieceMask >> x) & 1) == 1 || Winner != -1)
                return;
            BoardPieceMask |= 1 << x;
            BoardPlayerMask |= Turn << x;
            BoxChangeHandler.Invoke(x, Turn);
            Winner = GetWinner();
            if (Winner != -1)
                WinnerHandler.Invoke(Winner);
            else
            {
                Turn ^= 1;
                PlayerTurnHandler.Invoke(Turn);
            }
        }

        public int GetWinner()
        {
            foreach (int winState in WinningStates)
            {
                bool arePiecesLinedUp = (BoardPieceMask & winState) == winState;
                int playerCheck = BoardPlayerMask & winState;
                bool hasPlayerWon = playerCheck == 0 || playerCheck == winState;
                if (arePiecesLinedUp && hasPlayerWon)
                    return (playerCheck == 0) ? 0 : 1;
            }
            return -1;
        }
    }
}

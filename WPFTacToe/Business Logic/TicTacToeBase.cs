using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTacToe.Business_Logic
{
    public enum Token
    {
        Empty = 0,
        Player1 = 1,
        Player2 = 2,
    }

    class TicTacToeBase
    {
        protected Token[][] playerInputs;

        public TicTacToeBase()
        {
            playerInputs = new Token[3][]
            {
                new Token[3],
                new Token[3],
                new Token[3],
            };

            Reset();
        }

        public Token CurrentPlayer { get; private set; }
        public int RoundsLeft { get; private set; }
        public Token this[int x, int y]
        {
            get { return playerInputs[y][x]; }
            set
            {
                if (playerInputs[y][x] != Token.Empty)
                {
                    throw new InvalidOperationException("A field cannot be set twice");
                }

                if (value != CurrentPlayer)
                {
                    throw new InvalidOperationException("The same player cannot set two tokens in a row");
                }

                playerInputs[y][x] = value;
                RoundsLeft--;

                if (CurrentPlayer == Token.Player1)
                {
                    CurrentPlayer = Token.Player2;
                }
                else if (CurrentPlayer == Token.Player2)
                {
                    CurrentPlayer = Token.Player1;
                }
            }
        }

        public bool CheckWinCondition(Token player)
        {
            if (CheckRow(player) || CheckColumn(player) || CheckDiagonal(player))
            {
                return true;
            }

            return false;
        }

        public void Reset()
        {
            CurrentPlayer = Token.Player1;
            RoundsLeft = 9;

            for (int y = 0; y < playerInputs.Length; y++)
            {
                for (int x = 0; x < playerInputs[y].Length; x++)
                {
                    playerInputs[y][x] = Token.Empty;
                }
            }
        }

        private bool CheckRow(Token player)
        {
            for (int y = 0; y < playerInputs.Length; y++)
            {
                bool wholeRow = true;

                for (int x = 0; x < playerInputs[y].Length && wholeRow; x++)
                {
                    if (playerInputs[y][x] != player)
                    {
                        wholeRow = false;
                    }
                }

                if (wholeRow)
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckColumn(Token player)
        {
            for (int x = 0; x < playerInputs.Length; x++)
            {
                bool wholeColumn = true;

                for (int y = 0; y < playerInputs[x].Length && wholeColumn; y++)
                {
                    if (playerInputs[y][x] != player)
                    {
                        wholeColumn = false;
                    }
                }

                if (wholeColumn)
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckDiagonal(Token player)
        {
            bool wholeDiaognal = true;

            for (int i = 0; i < playerInputs[0].Length && wholeDiaognal; i++)
            {
                if ((playerInputs[i][i] != player))
                {
                    wholeDiaognal = false;
                }
            }

            if (wholeDiaognal)
            {
                return true;
            }

            wholeDiaognal = true;

            for (int i = 0; i < playerInputs[0].Length && wholeDiaognal; i++)
            {
                if ((playerInputs[i][playerInputs.Length - 1 - i] != player))
                {
                    wholeDiaognal = false;
                }
            }

            if (wholeDiaognal)
            {
                return true;
            }

            return false;
        }
    }
}
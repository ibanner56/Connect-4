using System;
using System.Collections.Generic;

namespace Connect4Server
{
    enum Checker
    {
        None = 0,
        Red = 1,
        Blue = 2
    }

    class Entry
    {
        private int[] chains;

        public Entry(Checker checker)
        {
            this.Color = checker;
            chains = new int[4];
        }

        public Checker Color { get; private set; }

        public int GetChain(int position)
        {
            if (position < 0 || position > 3)
                throw new ArgumentException("Position must be within 0 and 5");
            return chains[position];
        }

        public void SetChain(int position, Entry neighbor)
        {
            if(neighbor == null || neighbor.Color != this.Color)
                return;
            
            if (position < 0 || position > 3)
                throw new ArgumentException("Position must be within 0 and 5");
            chains[position] = neighbor.GetChain(position) + 1;
        }
    }

    class Player
    {
        public Player(Checker checker)
        {
            this.Color = checker;
            this.PId = Guid.NewGuid();
            this.GId = Guid.Empty;
        }

        public Player(Checker checker, Guid gId) : this(checker)
        {
            this.GId = gId;
        }

        public Guid GId { get; private set; }
        public Guid PId { get; private set; }
        public Checker Color { get; private set; }

        public void JoinGame(Guid GId)
        {
            if(this.GId == Guid.Empty)
            {
                this.GId = GId;
            }
        }
    }

    class Game
    {
        public static Dictionary<Guid, Player> players = new Dictionary<Guid, Player>();
        public static Dictionary<Guid, Game> games = new Dictionary<Guid, Game>();

        private Entry[,] board;
        private Player red;
        private Player blue;

        private Guid gId;
        
        public Game()
        {
            this.gId = Guid.NewGuid();
            this.board = new Entry[6,7];
            this.Fullboard = false;

            games.Add(gId, this);
        }

        public Player Red
        {
            get { return this.red; }

            set
            {
                value.JoinGame(this.gId);
                players.Add(value.PId, value);
                this.red = value;
            }
        }

        public Player Blue
        {
            get { return this.blue; }

            set
            {
                value.JoinGame(this.gId);
                players.Add(value.PId, value);
                this.blue = value;
            }
        }

        public bool Fullboard { get; private set; }

        public Checker Move(Checker checker, int column)
        {
            if (board[0, column] != null)
                throw new ArgumentException("Column is full");

            for(int i = 5; i >= 0; i--)
            {
                if(board[i,column] == null)
                {
                    board[i, column] = new Entry(checker);
                    break;
                }
            }

            return this.ValidateBoard();
        }

        private Checker ValidateBoard()
        {
            this.Fullboard = true;
            for(int i = 5; i >= 0; i--)
            {
                for(int j = 0; j <= 6; j++)
                {
                    if (board[i, j] == null)
                    {
                        this.Fullboard = false;
                        continue;
                    }

                    if (j > 0)
                        board[i, j].SetChain(0, board[i, j - 1]);
                    if (i < 5)
                    {
                        if (j > 0)
                            board[i, j].SetChain(1, board[i + 1, j - 1]);
                        board[i, j].SetChain(2, board[i + 1, j]);
                        if (j < 6)
                            board[i, j].SetChain(3, board[i + 1, j + 1]);
                    }

                    for(int k = 0; k <= 3; k++)
                    {
                        if(board[i,j].GetChain(k) >= 3)
                        {
                            return board[i, j].Color;
                        }
                    }
                }
            }



            return Checker.None;
        }
    }
}

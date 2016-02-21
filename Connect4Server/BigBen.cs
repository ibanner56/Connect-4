using System;
using System.Collections.Generic;

namespace Connect4Server
{
    class BigBen : Player 
    {
	public BigBen(Checker checker) : base(checker)
	{
	}

	public BigBen(Checker checker, Guid gId) : base(checker, gId)
	{
	}

	public int GetMove() 
	{
	    Entry[,] board = Game.games.Get(this.gId).Board;
	    
	    // Check for any imminent Wins or Losses
	    int win = -1;
	    int loss = -1;

	    for(int i = 5; i >= 0; i--)
	    {
	    	for(int j = 0; j < 7; j++)
		{
		    if(board[i, j] == null)
		    	continue;

		    for(int k = 0; k < 4; k++)
		    {
			if(board[i, j].GetChain(k) != 3)
			    continue;

			switch(k)
			{
			    case(0):
			    	if(j <= 0)
				    break;
				break;
			}
		    }
		}
	    }
	}
    } 
}

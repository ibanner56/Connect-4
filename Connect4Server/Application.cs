using System;

namespace Connect4Server
{
    class Application
    {
        static void Main(string[] args)
        {
            CLI letsgo = new CLI();
            letsgo.play();
        }
    }

    class Server
    {

    }

    class CLI
    {
        public void play()
        {
            Player p1 = new Player(Checker.Red);
            Player p2 = new Player(Checker.Blue);
            Game game = new Game();
            game.Red = p1;
            game.Blue = p2;

            Checker winner = Checker.None;
            bool redTurn = true;

            while(winner == Checker.None && !game.Fullboard)
            {
                try
                {
                    int column = redTurn ? p1.GetMove() : p2.GetMove();
		    Checker move = redTurn ? Checker.Red : Checker.Blue;
		    winner = game.Move(move, column);
                    redTurn = !redTurn;
		    Console.Write(game.GetBoard());
                }
                catch (ArgumentException ex)
                {
                    Console.Write("Chosen column is full. ");
                }
            }

            Console.WriteLine(winner + " wins!");
            Console.ReadKey();
        }
    }
}

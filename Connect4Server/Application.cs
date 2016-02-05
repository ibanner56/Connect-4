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
                Console.Write("Enter a column 1-7: ");
                try
                {
                    int column = int.Parse(Console.ReadLine()) - 1;
                    Checker move = redTurn ? Checker.Red : Checker.Blue;

                    winner = game.Move(move, column);
                    redTurn = !redTurn;
                }
                catch (ArgumentException ex)
                {
                    Console.Write("Chosen column is full. ");
                }
                catch (FormatException ex)
                {
                    Console.Write("Invalid column number. ");
                }
            }

            Console.WriteLine(winner + " wins!");
            Console.ReadKey();
        }
    }
}

namespace typeFighter
{
    class Program
    {
        static void Main()
        {
            bool running = true;
            int maxTurns;

            while (running)
            {
                Console.WriteLine("Enter Name \n");
                string? playerName = Console.ReadLine();

                Console.WriteLine("How many rounds ?\n");

                string? maxTurnsInput = Console.ReadLine();

                if (playerName == null) playerName = "player";

                if (maxTurnsInput == null)
                {
                    maxTurns = 5;
                }
                else
                {
                    try
                    {
                        maxTurns = int.Parse(maxTurnsInput);
                    }
                    catch (FormatException)
                    {

                        Console.WriteLine("Invalid input, defauting maxTurns to 5\n");
                        maxTurns = 5;
                    }
                };

                Game game = new Game(playerName, maxTurns, "./quotes.txt");
                game.Play();


                Console.WriteLine("Enter 'z' to exit");

                if (Console.ReadLine() == "z") running = false;
            }

        }

    }
}

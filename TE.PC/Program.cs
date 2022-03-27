using System;

namespace TE.PC
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Console.WriteLine("Game Started");

            using (var game = new Game())
                game.Run();
        }
    }
}
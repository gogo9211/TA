using Microsoft.Xna.Framework;
using System;

namespace TE.PC
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Console.WriteLine("Game Started\n");

            using (Game game = new TEGame())
                game.Run();
        }
    }
}
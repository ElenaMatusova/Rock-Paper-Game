using System;
using System.Linq;

namespace Rock_Paper
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            { Console.WriteLine("No elements to play!"); return; }
            else if (args.Length % 2 == 0)
            { Console.WriteLine("Number of elements to play must be odd!"); return; }
            else if (args.Length < 3)
            { Console.WriteLine("Number of elements should be at least 3!"); return; }
            else if (args.Length != args.Distinct().Count())
            { Console.WriteLine("Elements to play should be unique"); return; }
            else { var gameStart = new Gameplay(args.ToList()); gameStart.Start(); }

            
        
     
            Console.ReadKey();
        }
    }
}

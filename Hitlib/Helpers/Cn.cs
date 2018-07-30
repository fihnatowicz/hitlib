using System;
using System.Collections.Generic;
using System.Text;

namespace Hitlib.Helpers
{
    public class Cn
    {
        public static void Write(string message, ConsoleColor fg, ConsoleColor? bg = null)
        {
            Console.ForegroundColor = fg;
            if (bg.HasValue)
                Console.BackgroundColor = bg.Value;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void WriteLine(string message, ConsoleColor fg, ConsoleColor? bg = null)
        {
            Console.ForegroundColor = fg;
            if (bg.HasValue)
                Console.BackgroundColor = bg.Value;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}

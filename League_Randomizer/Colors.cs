using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League_Randomizer
{
    public class Colors
    {
        public static void SetColor(string color)
            //'color' strings are passed to SetColor, which change the console color based on input
        {
            if (color == "Green")
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }
            else if (color == "LightGreen")
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (color == "Red" || color == "Orange")
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (color == "Cyan" || color == "Silver" || color == "Light Blue" || color == "Tan")
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            else if (color == "Yellow")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (color == "Magenta" || color == "Pink")
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
            }
            else if (color == "Purple")
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
            }
            else if (color == "White")
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (color == "Black")
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
        }
    }
}

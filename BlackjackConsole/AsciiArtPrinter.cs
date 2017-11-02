using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackConsole {

    public static class AsciiArtPrinter {

        public static void Draw(string asciiArt, int posX, int posY, ConsoleColor color) {

            if (asciiArt == null)
                return;

            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;

            string[] asciiRows = asciiArt.Split(new char[] { '\n' });

            for (int y = 0; y < asciiRows.Length; y++) {

                Console.SetCursorPosition(posX, posY + y);
                Console.Write(asciiRows[y]);

            }

            Console.ForegroundColor = originalColor;

        }

        public static void Draw(IAsciiArt asciiArt, int posX, int posY, ConsoleColor color) {

            if (asciiArt == null)
                return;

            Draw(asciiArt.ToAsciiArt(), posX, posY, color);

        }

    }

}

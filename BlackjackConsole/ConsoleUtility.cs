using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackConsole {

    public static class ConsoleUtility {

        public static void DrawAsciiArt(string asciiArt, int posX, int posY, ConsoleColor color) {

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

        public static void DrawAsciiArt(IAsciiArt asciiArt, int posX, int posY, ConsoleColor color) {

            if (asciiArt == null)
                return;

            DrawAsciiArt(asciiArt.ToAsciiArt(), posX, posY, color);

        }

        public static void WriteNewLines(int count) {

            for (int i = 0; i < count; i++)
                Console.WriteLine();

        }

        public static void ForceSize(int width, int height) {

            Console.WindowWidth = MathUtility.Clamp(width, 1, Console.LargestWindowWidth);
            Console.WindowHeight = MathUtility.Clamp(height, 1, Console.LargestWindowHeight);

            if (Console.BufferWidth < width)
                Console.BufferWidth = width;

            if (Console.BufferHeight < height)
                Console.BufferHeight = height;

        }

        /// <summary>
        /// Flushes the keyboard buffer by consuming every keystroke currently in the buffer.
        /// </summary>
        public static void FlushKeyboardBuffer() {

            while (Console.KeyAvailable)
                Console.ReadKey(true);

        }

        public static void ClearArea(int posX, int posY, int width, int height) {

            Console.SetCursorPosition(posX, posY);

            for (int i = 0; i < height; i++) {

                for (int j = 0; j < width; j++)
                    Console.Write(" ");

                Console.CursorLeft = posX;
                Console.CursorTop++;

            }

        }

    }

}

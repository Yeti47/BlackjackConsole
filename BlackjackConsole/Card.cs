using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackConsole {

    public abstract class Card : IAsciiArt {

        #region Properties

        public abstract int Value { get;  }
        public abstract int MinValue { get; }

        public CardSuit Suit { get; }
        public string SuitAsString => char.ConvertFromUtf32((int)Suit);

        public virtual string Rank => Value.ToString();

        public bool IsHidden { get; set; }

        #endregion

        #region Constructors

        public Card(CardSuit suit) {

            Suit = suit;

        }

        #endregion

        #region Methods

        public override string ToString() => $"{Rank} of {SuitAsString}";

        public virtual string ToAsciiArt() {

            string asciiArt = "┌─────────┐\n";

            if (!IsHidden) {

                asciiArt += AsciiFirstLine(11)
                         + "│.........│\n"
                         + "│.........│\n"
                         + "│.........│\n"
                         +$"│....{SuitAsString}....│\n"
                         + "│.........│\n"
                         + "│.........│\n"
                         + "│.........│\n"
                         + AsciiLastLine(11);
            }
            else {

                for (int i = 0; i < 9; i++)
                    asciiArt += "│xxxxxxxxx│\n";

            }

            asciiArt += "└─────────┘\n";

            return asciiArt;

        }

        private string AsciiFirstLine(int width) {

            string ascii = "│." + Rank;

            while (ascii.Length < width - 1)
                ascii += '.';

            ascii += "│\n";

            return ascii;

        }

        private string AsciiLastLine(int width) {

            string ascii = Rank + ".│";

            while (ascii.Length < width-1)
                ascii = "." + ascii;

            ascii = "│" + ascii + "\n";

            return ascii;

        }

        #endregion


    }

}

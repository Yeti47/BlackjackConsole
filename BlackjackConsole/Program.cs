using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackConsole {

    class Program {

        static void Main(string[] args) {

            List<Card> cards = new List<Card>();
            cards.Add(new NumericCard(CardSuit.Spade, 5) { IsHidden = true });
            cards.Add(new RoyalCard(CardSuit.Club, RoyalType.Queen));
            cards.Add(new RoyalCard(CardSuit.Diamond, RoyalType.Ace));

            Hand hand = new Hand();

            foreach (Card c in cards)
                hand.AddCard(c);

            Console.OutputEncoding = Encoding.Unicode;
            Console.Title = "Blackjack for Noobs";

            for(int x = 0; x < cards.Count; x++) {

                string cardAscii = cards[x].ToAsciiArt();
                string[] cardAsciiLines = cardAscii.Split(new char[] { '\n' });

                for(int y = 0; y < cardAsciiLines.Length; y++) {

                    Console.SetCursorPosition(x * 7, y);
                    Console.Write(cardAsciiLines[y]);

                }

            }

            Console.CursorLeft = 0;

            Console.WriteLine("Visible Value: " + hand.GetVisibleValue(1));
            Console.WriteLine("Actual Value: " + hand.Value);

            Console.ReadLine();

        }
    }
}

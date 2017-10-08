using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackConsole {

    public abstract class Card {

        public CardSuit Suit { get; }

        public abstract int Value { get;  }

        public Card(CardSuit suit) {

            Suit = suit;

        }

    }

}

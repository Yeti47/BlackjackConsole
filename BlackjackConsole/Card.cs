using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackConsole {

    public abstract class Card {

        #region Properties

        public abstract int Value { get;  }
        public abstract int MinValue { get; }

        public CardSuit Suit { get; }
        public string SuitAsString => char.ConvertFromUtf32((int)Suit);

        public bool IsHidden { get; set; }

        #endregion

        #region Constructors

        public Card(CardSuit suit) {

            Suit = suit;

        }

        #endregion

        #region Methods

        public override string ToString() => $"{Value} of {SuitAsString}";

        #endregion


    }

}

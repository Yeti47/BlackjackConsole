using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackConsole {

    public class RoyalCard : Card {

        #region Fields

        private const int ROYAL_VALUE = 10;

        #endregion

        #region Properties

        public RoyalType RoyalType { get; }

        public override int Value => ROYAL_VALUE;

        #endregion

        #region Constructors

        public RoyalCard(CardSuit suit, RoyalType royalType) : base(suit) {

            RoyalType = royalType;

        }

        #endregion

    }

}

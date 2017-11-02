using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackConsole {

    public class RoyalCard : Card {

        #region Constants

        public const int ROYAL_VALUE = 10;
        public const int ACE_MIN_VALUE = 1;
        public const int ACE_MAX_VALUE = 11;

        #endregion

        #region Properties

        public RoyalTypes RoyalType { get; }

        public override int Value => IsAce ? ACE_MAX_VALUE : ROYAL_VALUE;
        public override int MinValue => IsAce ? ACE_MIN_VALUE : ROYAL_VALUE;

        public bool IsAce => RoyalType == RoyalTypes.Ace;

        public override string Rank {

            get {

                char c = (char)RoyalType;
                return c.ToString();

            }

        }

        #endregion

        #region Constructors

        public RoyalCard(CardSuits suit, RoyalTypes royalType) : base(suit) {

            RoyalType = royalType;

        }

        #endregion

        #region Methods

        

        #endregion

    }

}

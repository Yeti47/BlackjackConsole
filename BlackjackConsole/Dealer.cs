using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackConsole {

    public class Dealer : IPlayer {

        #region Constants

        public const int STAND_VALUE = 17;

        #endregion

        #region Fields

        private Blackjack _blackjack;

        #endregion

        #region Properties

        public Hand Hand { get; private set; } = new Hand();

        #endregion

        #region Constructors

        public Dealer(Blackjack blackjack) {

            _blackjack = blackjack;

        }

        #endregion

        #region Methods

        public void PlayTurn() {

            while(Hand.Value < STAND_VALUE) {

                Card cardDrawn = _blackjack.DealNextCard(false);
                Hand.AddCard(cardDrawn);
                
            }

        }

        #endregion

    }

}

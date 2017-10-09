using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackConsole {

    public class Deck {

        #region Fields

        private Stack<Card> _cards = new Stack<Card>();

        #endregion

        #region Properties

        public int NumberRemainingCards => _cards.Count;

        #endregion

        #region Constructors

        public Deck(bool doShuffle = false) {

            Initialize();

            if (doShuffle)
                Shuffle();

        }

        #endregion

        #region Methods

        public void Initialize() {

            if (_cards == null)
                _cards = new Stack<Card>();
            else
                _cards.Clear();

            foreach(CardSuit suit in Enum.GetValues(typeof(CardSuit))) {

                for(int i = NumericCard.MIN_NUMBER; i <= NumericCard.MAX_NUMBER; i++) {

                    _cards.Push(new NumericCard(suit, i));

                }

                _cards.Push(new RoyalCard(suit, RoyalType.Jack));
                _cards.Push(new RoyalCard(suit, RoyalType.Queen));
                _cards.Push(new RoyalCard(suit, RoyalType.King));
                _cards.Push(new RoyalCard(suit, RoyalType.Ace));

            }

        }

        public void Shuffle() {

            if (_cards == null)
                return;

            List<Card> cards = new List<Card>(_cards);

            Random random = new Random();

            int count = cards.Count;

            for (int i = 0; i < count - 1; i++) {

                int j = random.Next(i, count);

                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;

            }

            _cards = new Stack<Card>(cards);

        }

        public Card Draw() {

            Card card = null;

            if (NumberRemainingCards > 0)
                card = _cards.Pop();

            return card;

        }

        #endregion

    }

}

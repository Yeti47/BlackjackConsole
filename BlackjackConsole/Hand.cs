using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackConsole {

    public class Hand {

        #region Constants

        public const int MAX_VALUE = 21;

        #endregion

        #region Fields

        private List<Card> _cards = new List<Card>();

        #endregion

        #region Properties

        public IReadOnlyList<Card> Cards => _cards;

        public IEnumerable<Card> VisibleCards => _cards.Where(c => !c.IsHidden);

        public int NumberCards => _cards.Count;

        public int Value {

            get {
                
                int sum = _cards.Sum(c => c.Value);

                foreach(Card card in _cards.Where(c => c.Value != c.MinValue)) {

                    if (sum <= MAX_VALUE)
                        break;

                    sum -= (card.Value - card.MinValue);

                }
                
                return sum;

            }

        }

        /// <summary>
        /// Whether or not the cards in this <see cref="Hand"/> are a Blackjack.
        /// </summary>
        public bool IsBlackjack => _cards.Count == 2 && Value == MAX_VALUE;

        #endregion

        #region Constructors

        public Hand() {
            
        }

        #endregion

        #region Methods

        public void AddCard(Card card) => _cards.Add(card);

        /// <summary>
        /// Removes all cards from this <see cref="Hand"/> and returns a collection of the cards discarded.
        /// </summary>
        /// <returns>The cards that were discarded from this Hand.</returns>
        public IEnumerable<Card> DiscardAll() {

            IEnumerable<Card> cardsDropped = new List<Card>(_cards);

            foreach (Card card in cardsDropped)
                card.IsHidden = true;

            _cards.Clear();
            return cardsDropped;

        }

        public void Show() {

            foreach (Card card in _cards)
                card.IsHidden = false;

        }

        /// <summary>
        /// Calculates the total value of all visible <see cref="Card"/>s in this <see cref="Hand"/>.
        /// </summary>
        /// <param name="assumedHiddenVal">The value to assume for each hidden card.</param>
        /// <returns>The total value of the Cards in this Hand.</returns>
        public int GetVisibleValue(int assumedHiddenVal) {

            int sum = VisibleCards.Sum(c => c.Value);

            // The assumed total value of all hidden cards
            int totalHiddenVal = _cards.Where(c => c.IsHidden).Count() * assumedHiddenVal;

            foreach (Card card in _cards.Where(c => c.Value != c.MinValue)) {

                // if there are any hidden cards, we have to subtract their assumed total value from the MAX_VALUE
                if (sum <= MAX_VALUE - totalHiddenVal)
                    break;

                sum -= (card.Value - card.MinValue);

            }

            return sum;
            
        }

        #endregion


    }

}

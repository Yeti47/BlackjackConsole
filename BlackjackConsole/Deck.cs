using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackConsole {

    public class Deck : IAsciiArt {

        #region Fields

        private Stack<Card> _cards = new Stack<Card>();

        #endregion

        #region Properties

        /// <summary>
        /// The number of <see cref="Card"/>s in this Deck.
        /// </summary>
        public int NumberRemainingCards => _cards.Count;

        /// <summary>
        /// Whether or not this <see cref="Deck"/> is currently empty, i. e. there are no <see cref="Card"/>s in this Deck.
        /// </summary>
        public bool IsEmpty => _cards.Count <= 0;

        /// <summary>
        /// An enumeration of all the <see cref="Card"/>s in this Deck.
        /// </summary>
        public IEnumerable<Card> Cards => _cards;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="Deck"/>. If <paramref name="isEmpty"/> is set to true, no cards will be added to this Deck. 
        /// If false, the Deck will be populated with a standard set of 52 <see cref="Card"/>s.
        /// </summary>
        /// <param name="isEmpty">Whether or not the new Deck should be empty.</param>
        public Deck(bool isEmpty = false) {

            if(!isEmpty)
                Initialize();

        }

        /// <summary>
        /// Creates a new <see cref="Deck"/> and initializes it with the given collection of <see cref="Card"/>s.
        /// </summary>
        /// <param name="cards">The <see cref="Card"/>s to use in this Deck.</param>
        public Deck(IEnumerable<Card> cards) {

            Push(cards);

        }

        #endregion

        #region Methods

        /// <summary>
        /// Populates this <see cref="Deck"/> with a standard set of 52 <see cref="Card"/>s.
        /// </summary>
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

        /// <summary>
        /// Shuffles this <see cref="Deck"/> using a simple implementation of the Fisher-Yates shuffling algorithm.
        /// </summary>
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

        /// <summary>
        /// Removes the <see cref="Card"/> on top of this <see cref="Deck"/> and returns it.
        /// </summary>
        /// <returns>The Card on top of this Deck.</returns>
        public Card Draw() {

            Card card = null;

            if (NumberRemainingCards > 0)
                card = _cards.Pop();

            return card;

        }

        /// <summary>
        /// Puts the given <see cref="Card"/> on top of this <see cref="Deck"/>. If null is passed, nothing will be added.
        /// </summary>
        /// <param name="card">The card to put on top of this Deck.</param>
        public void Push(Card card) {

            if (card == null)
                return;

            _cards.Push(card);
            
        }

        /// <summary>
        /// Puts all of the <see cref="Card"/>s in the given enumeration on top of this <see cref="Deck"/>. If null is passed, nothing will be added. #
        /// All null references in the enumeration will be skipped.
        /// </summary>
        /// <param name="cards">An enumeration of Cards to put on top of this Deck.</param>
        public void Push(IEnumerable<Card> cards) {

            if (cards == null)
                return;

            foreach (Card card in cards) {

                if (card != null)
                    _cards.Push(card);

            }
                        
        }

        /// <summary>
        /// Removes all <see cref="Card"/>s from this <see cref="Deck"/>.
        /// </summary>
        public void Clear() => _cards?.Clear();

        public string ToAsciiArt() {

            string top    = "┌---------┐";
            string middle = ":.........:";
            string bottom = "└---------┘";

            if(NumberRemainingCards > 1) {

                top    = "┌─────────╖";
                middle = "│xxxxxxxxx║";
                bottom = "╘═════════╝";

            }
            else if(NumberRemainingCards > 0) {

                top    = "┌─────────┐";
                middle = "│xxxxxxxxx│";
                bottom = "└─────────┘";

            }

            string ascii = top + "\n";

            for (int i = 0; i < 9; i++)
                ascii += middle + "\n";

            ascii += bottom + "\n";

            return ascii;

        }

        #endregion

    }

}

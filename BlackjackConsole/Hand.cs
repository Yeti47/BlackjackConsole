using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackConsole {

    public class Hand {

        public const int MAX_VALUE = 21;

        private List<Card> _cards = new List<Card>();

        public IReadOnlyList<Card> Cards => _cards;

        public int NumberCards => _cards.Count;

        public int Value {

            get {

                int sum = _cards.Sum(c => c.Value);

                IEnumerable<Ace> aces = _cards.OfType<Ace>();

                foreach(Ace ace in aces) {

                    if (sum <= MAX_VALUE)
                        break;

                    sum -= ace.Value;
                    sum += ace.MinValue;

                }
                
                return sum;

            }

        }

        public Hand() {

        }

        public void AddCard(Card card) => _cards.Add(card);

        public void Reset() {

            _cards.Clear();

        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackConsole {

    public class Player : IPlayer {

        #region Constants

        public const string DEFAULT_PLAYER_NAME = "Player";
        public const int MAX_BALANCE = 999999;

        #endregion

        #region Fields

        private Blackjack _blackjack;

        private int _currentBet;
        private int _balance;
        private string _name = DEFAULT_PLAYER_NAME;

        #endregion

        #region Properties

        public int CurrentBet {

            get => _currentBet;
            // TUTORIAL NOTE: Maybe split this into multiple statements and use a regular body for the setter for clarification
            set => _currentBet = MathUtility.Clamp(value, _blackjack.MinBet, Math.Min(_blackjack.MaxBet, _balance)); 

        }
        public int Balance {

            get => _balance;
            set {
                _balance = MathUtility.Clamp(value, 0, MAX_BALANCE);
                _currentBet = MathUtility.Clamp(_currentBet, _blackjack.MinBet, Math.Min(_blackjack.MaxBet, _balance));
            }

        }

        public string Name {
            get => _name;
            set => _name = string.IsNullOrWhiteSpace(value) ? DEFAULT_PLAYER_NAME : value;
        }

        public Hand Hand { get; private set; } = new Hand();

        public bool CanDouble => Hand?.CardsCount == 2 && Balance >= _currentBet * 2;

        public bool HasDoubled { get; internal set; }

        #endregion

        #region Constructors

        public Player(Blackjack blackjack) {

            _blackjack = blackjack;

        }

        #endregion

        #region Methods

        #endregion

    }

}

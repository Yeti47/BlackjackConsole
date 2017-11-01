using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackConsole {

    public class Player : IPlayer {

        #region Constants

        public const string DEFAULT_PLAYER_NAME = "Player";

        #endregion

        #region Fields

        private int _currentBet;
        private string _name;

        #endregion

        #region Properties

        public int CurrentBet {

            get => _currentBet;
            set => _currentBet = MathUtility.Clamp(value, 0, Balance);

        }
        public int Balance { get; set; }

        public string Name {
            get => _name;
            set => _name = string.IsNullOrWhiteSpace(value) ? DEFAULT_PLAYER_NAME : value;
        }

        public Hand Hand { get; private set; } = new Hand();

        public bool CanDouble => Hand?.CardsCount == 2 && Balance >= _currentBet * 2;

        public bool HasDoubled { get; internal set; }

        #endregion

        #region Constructor

        #endregion

        #region Methods



        #endregion

    }

}

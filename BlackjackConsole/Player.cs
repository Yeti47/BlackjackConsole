using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackConsole {

    public class Player {

        #region Constants

        public const string DEFAULT_PLAYER_NAME = "Player";

        #endregion

        #region Fields

        private double _currentBet;

        #endregion

        #region Properties

        public double CurrentBet {

            get => _currentBet;
            set => _currentBet = MathUtility.Clamp(value, 0, Balance);

        }
        public double Balance { get; set; }

        public string Name { get; set; }

        public Hand Hand { get; private set; }

        #endregion

        #region Constructor

        public Player(string name = DEFAULT_PLAYER_NAME) {

            if (string.IsNullOrWhiteSpace(name))
                name = DEFAULT_PLAYER_NAME;

            Name = name;

        }

        #endregion

        #region Methods



        #endregion

    }

}

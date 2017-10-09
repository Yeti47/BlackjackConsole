using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackConsole {

    public class NumericCard : Card {

        #region Constants

        public const int MIN_NUMBER = 2;
        public const int MAX_NUMBER = 10;

        #endregion

        #region Fields

        private readonly int _value;

        #endregion

        #region Properties

        public override int Value => _value;

        #endregion

        #region Constructors

        public NumericCard(CardSuit suit, int value) : base(suit){

            _value = value;

        }

        #endregion

        #region Methods

        #endregion
        
    }

}

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
        public override int MinValue => _value;

        #endregion

        #region Constructors

        public NumericCard(CardSuits suit, int value) : base(suit){

            if (value < MIN_NUMBER || value > MAX_NUMBER)
                throw new ArgumentOutOfRangeException("value", $"The value of the card must not be less than {MIN_NUMBER} or greater than {MAX_NUMBER}.");

            _value = value;

        }

        #endregion

        #region Methods

        #endregion
        
    }

}

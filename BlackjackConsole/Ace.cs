﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackConsole {

    public class Ace : Card {

        private const int ACE_VALUE = 11;
        private const int ACE_MIN_VALUE = 1;

        public override int Value => ACE_VALUE;

        public int MinValue => ACE_MIN_VALUE;

        public Ace(CardSuit suit) : base(suit) {

        }

        public override string ToString() => "A " + SuitAsString;

    }
}

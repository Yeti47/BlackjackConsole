using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackConsole {

    public class Player {

        #region Properties

        public double Balance { get; set; }
        public string Name { get; set; } = "Player";

        public Hand Hand { get; private set; }

        #endregion

    }

}

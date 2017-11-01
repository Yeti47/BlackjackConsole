using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackConsole {

    public class PlayerLostEventArgs {

        public bool IsGameOver { get; }

        public PlayerLostEventArgs(bool isGameOver) {

            IsGameOver = isGameOver;

        }

    }

}

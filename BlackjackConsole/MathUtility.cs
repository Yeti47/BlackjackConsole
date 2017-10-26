using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackConsole {

    public static class MathUtility {

        public static T Clamp<T>(T value, T min, T max) where T : IComparable {

            if (value.CompareTo(min) < 0)
                return min;

            if (value.CompareTo(max) > 0)
                return max;

            return value;

        }

    }

}

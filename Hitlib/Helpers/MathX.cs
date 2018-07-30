using System;
using System.Collections.Generic;
using System.Text;

namespace Hitlib.Helpers
{
    public class MathX
    {
        public static decimal Diff(decimal a, decimal b, int accuracy = 2)
        {
            if (a == 0)
                return 0;
            return Math.Round((b / a - 1) * 100, accuracy);
        }

        /// <summary>
        /// Floor to quantity 
        /// </summary>
        /// <param name="x">value</param>
        /// <param name="qi">Quantity Increment</param>
        /// <returns></returns>
        public static decimal Floor(decimal x, decimal qi)
        {
            return Math.Floor(x / qi) * qi;
        }
    }
}

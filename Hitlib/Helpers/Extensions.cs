using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static Hitlib.Entities.Candle;

namespace Hitlib
{
    public static class Extensions
    {
        /// <summary>
        /// returns best ask key value pair
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static KeyValuePair<decimal, decimal> Ask(this SortedDictionary<decimal, decimal> dict)
        {
            if (dict.Count == 0)
                return new KeyValuePair<decimal, decimal>(0, 0);
            return dict.First();
        }

        /// <summary>
        /// returns best bid key value pair
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static KeyValuePair<decimal,decimal> Bid(this SortedDictionary<decimal,decimal> dict)
        {
            if (dict.Count == 0)
                return new KeyValuePair<decimal, decimal>(0, 0);
            return dict.Last();
        }

        public static double Median(this IEnumerable<double> source)
        {
            if (source.Count() == 0)
            {
                throw new InvalidOperationException("Cannot compute median for an empty set.");
            }

            var sortedList = from number in source
                             orderby number
                             select number;

            int itemIndex = (int)sortedList.Count() / 2;

            if (sortedList.Count() % 2 == 0)
            {
                // Even number of items.  
                return (sortedList.ElementAt(itemIndex) + sortedList.ElementAt(itemIndex - 1)) / 2;
            }
            else
            {
                // Odd number of items.  
                return sortedList.ElementAt(itemIndex);
            }
        }

        public static decimal MovingAverage<T>(this IEnumerable<T> records, Func<T, decimal> selector, int step)
        {
            var count = records.Count();
            if (count == 0)
                throw new InvalidOperationException("Cannot find MA on an empty set.");

            if (step > count)
                step = count;

            return records.TakeLast(step).Select(selector).Average();
        }
    }
}

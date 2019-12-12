using Hitlib.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hitlib.CustomEntities
{
    public class OrderbookExt
    {
        public string Symbol { get; set; }
        public SortedDictionary<decimal, decimal> Ask { get; set; }
        public SortedDictionary<decimal, decimal> Bid { get; set; }
        public OrderbookExt()
        {

        }
        public OrderbookExt(Orderbook orderbook)
        {
            Symbol = orderbook.Symbol;
            Ask = new SortedDictionary<decimal, decimal>();
            Bid = new SortedDictionary<decimal, decimal>();
            AddOrUpdate(orderbook);
        }

        public void AddOrUpdate(Orderbook orderbook)
        {
            foreach (var book in orderbook.Ask)
            {
                if (book.Size == 0M)
                    Ask.Remove(book.Price);
                else
                    Ask[book.Price] = book.Size;
            }
            foreach (var book in orderbook.Bid)
            {
                if (book.Size == 0M)
                    Bid.Remove(book.Price);
                else
                    Bid[book.Price] = book.Size;
            }
        }
    }

}

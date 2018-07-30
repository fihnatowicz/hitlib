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
            Symbol = orderbook.symbol;
            Ask = new SortedDictionary<decimal, decimal>();
            Bid = new SortedDictionary<decimal, decimal>();
            AddOrUpdate(orderbook);
        }

        public void AddOrUpdate(Orderbook orderbook)
        {
            foreach (var book in orderbook.ask)
            {
                if (book.size == 0M)
                    Ask.Remove(book.price);
                else
                    Ask[book.price] = book.size;
            }
            foreach (var book in orderbook.bid)
            {
                if (book.size == 0M)
                    Bid.Remove(book.price);
                else
                    Bid[book.price] = book.size;
            }
        }
    }

}

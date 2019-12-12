using Hitlib.App;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hitlib.Entities
{
    public class Orderbook
    {
        public List<Book> Ask { get; set; }
        public List<Book> Bid { get; set; }
        public string Symbol { get; set; }
        public long Sequence { get; set; }

        public static void Subscribe(string symbol)
        {
            var request = new
            {
                method = "subscribeOrderbook",
                @params = new
                {
                    symbol = symbol
                },
                id = "107"
            };
            var json = JsonConvert.SerializeObject(request);
            Socket.Send(json);
        }

        public static event EventHandler<Orderbook> OnOrderbookReceived;
        public static void ProcessOrderbook(dynamic @params)
        {
            if(OnOrderbookReceived != null)
            {
                var orderbook = JsonConvert.DeserializeObject<Orderbook>(@params.ToString());
                OnOrderbookReceived.Invoke(null, orderbook);
            }            
        }

        public class Book
        {
            public decimal Price { get; set; }
            public decimal Size { get; set; }
        }
    }

}

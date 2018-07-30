using Hitlib.App;
using Hitlib.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hitlib.Entities
{
    public class Trade
    {
        public List<TradeItem> data { get; set; }
        public string symbol { get; set; }

        public static void Subscribe(string symbol)
        {
            var request = new Request
            {
                method = "subscribeTrades",
                @params = new
                {
                    symbol = symbol
                },
                id = "133"
            };
            var json = JsonConvert.SerializeObject(request);
            Socket.Send(json);
        }

        public static event EventHandler<Trade> OnTradesReceived;
        public static void ProcessTrades(dynamic @params)
        {
            if (OnTradesReceived != null)
            {
                var trades = JsonConvert.DeserializeObject<Trade>(@params.ToString());
                OnTradesReceived.Invoke(null, trades);
            }
        }

        public class TradeItem
        {
            public long id { get; set; }
            public decimal price { get; set; }
            public decimal quantity { get; set; }
            public Side side { get; set; }
            public DateTime timestamp { get; set; }

            public override string ToString()
            {
                return string.Format("{0} | [{1}] | p: {2} a: {3} @{4}", this.id, this.timestamp.ToString("yyyy-MM-dd HH:mm:ss.ffffff"), this.price, this.quantity, this.side);
            }
        }

        
    }
}

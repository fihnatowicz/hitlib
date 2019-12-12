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
        public List<TradeItem> Data { get; set; }
        public string Symbol { get; set; }

        public static void Subscribe(string symbol)
        {
            var request = new
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
            public long Id { get; set; }
            public decimal Price { get; set; }
            public decimal Quantity { get; set; }
            public Side Side { get; set; }
            public DateTime Timestamp { get; set; }

            public override string ToString()
            {
                return string.Format("{0} | [{1}] | p: {2} a: {3} @{4}", this.Id, this.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.ffffff"), this.Price, this.Quantity, this.Side);
            }
        }

        
    }
}

using Hitlib.App;
using Hitlib.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hitlib.Entities
{
    public class Candle
    {
        public List<Candlestick> data { get; set; }
        public string symbol { get; set; }
        public Period period { get; set; }

        public static void Subscribe(string symbol, Period period)
        {
            var request = new Request
            {
                method = "subscribeCandles",
                @params = new
                {
                    symbol = symbol,
                    period = period.ToString()
                },
                id = "105",
            };
            var json = JsonConvert.SerializeObject(request);
            Socket.Send(json);
        }

        public static event EventHandler<Candle> OnCandleReceived;
        public static event EventHandler<Candle> OnCandlesSnapshotted;

        public static void ProcessCandles(dynamic @params, bool snapshot = false)
        {
            if(snapshot && OnCandlesSnapshotted != null)
            {
                var candle = JsonConvert.DeserializeObject<Candle>(@params.ToString());
                OnCandlesSnapshotted.Invoke(null, candle);
            }
            if(OnCandleReceived != null)
            {
                var candle = JsonConvert.DeserializeObject<Candle>(@params.ToString());
                OnCandleReceived.Invoke(null, candle);
            }
        }

        public class Candlestick
        {
            public decimal? close { get; set; }
            public decimal? max { get; set; }
            public decimal? min { get; set; }
            public decimal? open { get; set; }
            public decimal volume { get; set; }
            public decimal volumeQuote { get; set; }
            public DateTime timestamp { get; set; }
        }
    }
}

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
        public List<Candlestick> Data { get; set; }
        public string Symbol { get; set; }
        public Period Period { get; set; }

        public static void Subscribe(string symbol, Period period)
        {
            var request = new
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
            public decimal? Close { get; set; }
            public decimal? Max { get; set; }
            public decimal? Min { get; set; }
            public decimal? Open { get; set; }
            public decimal Volume { get; set; }
            public decimal VolumeQuote { get; set; }
            public DateTime Timestamp { get; set; }
        }
    }
}

using Hitlib.App;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hitlib.Entities
{
    public class Ticker
    {
        public decimal? Ask { get; set; }
        public decimal? Bid { get; set; }
        public decimal? Last { get; set; }
        public decimal? Open { get; set; }
        public decimal? Low { get; set; }
        public decimal? High { get; set; }
        public decimal Volume { get; set; }
        public decimal VolumeQuote { get; set; }
        public DateTime Timestamp { get; set; }
        public string Symbol { get; set; }

        public override string ToString()
        {
            return string.Format("[{0}] {1} : ask: {2} | bid: {3} | last: {4} | open: {5} | low: {6} | high: {7} | volume: {8} | volumeQuote: {9}", 
                this.Timestamp, this.Symbol, this.Ask, this.Bid, this.Last, this.Open, this.Low, this.High, this.Volume, this.VolumeQuote);
        }

        public static void Subscribe(string symbol)
        {
            var request = new
            {
                method = "subscribeTicker",
                @params = new
                {
                    symbol = symbol
                },
                id = "106"
            };
            var json = JsonConvert.SerializeObject(request);
            Socket.Send(json);
        }
        public static void Unsubscribe(string symbol)
        {
            var request = new
            {
                method = "unsubscribeTicker",
                @params = new
                {
                    symbol = symbol
                },
                id = "0"
            };
            var json = JsonConvert.SerializeObject(request);
            Socket.Send(json);
        }

        public static event EventHandler<Ticker> OnTickerDataReceived;

        public static void ProcessTicker(dynamic @params)
        {
            if(OnTickerDataReceived != null)
            {
                var ticker = JsonConvert.DeserializeObject<Ticker>(@params.ToString());
                OnTickerDataReceived.Invoke(null, ticker);
            }
        }
    }
}

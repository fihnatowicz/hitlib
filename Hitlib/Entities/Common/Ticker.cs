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
        public decimal? ask { get; set; }
        public decimal? bid { get; set; }
        public decimal? last { get; set; }
        public decimal? open { get; set; }
        public decimal? low { get; set; }
        public decimal? high { get; set; }
        public decimal volume { get; set; }
        public decimal volumeQuote { get; set; }
        public DateTime timestamp { get; set; }
        public string symbol { get; set; }

        public override string ToString()
        {
            return string.Format("[{0}] {1} : ask: {2} | bid: {3} | last: {4} | open: {5} | low: {6} | high: {7} | volume: {8} | volumeQuote: {9}", 
                this.timestamp, this.symbol, this.ask, this.bid, this.last, this.open, this.low, this.high, this.volume, this.volumeQuote);
        }

        public static void Subscribe(string symbol)
        {
            var request = new Request
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
            var request = new Request
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

using Hitlib.App;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hitlib.Entities
{
    public class Symbol
    {
        public string id { get; set; }
        public string baseCurrency { get; set; }
        public string quoteCurrency { get; set; }
        public decimal quantityIncrement { get; set; }
        public decimal tickSize { get; set; }
        public decimal takeLiquidityRate { get; set; }
        public decimal provideLiquidityRate { get; set; }
        public string feeCurrency { get; set; }


        public static event EventHandler<List<Symbol>> OnSymbolsReceived;
        public static event EventHandler<Symbol> OnSymbolReceived;

        public static void GetSymbols()
        {
            var request = new Request
            {
                method = "getSymbols",
                id = "102"
            };
            var json = JsonConvert.SerializeObject(request);
            Socket.Send(json);
        }

        public static void GetSymbol(string symbol)
        {
            var request = new Request
            {
                method = "getSymbol",
                @params = new
                {
                    symbol = symbol
                },
                id = "103"
            };
            var json = JsonConvert.SerializeObject(request);
            Socket.Send(json);
        }

        public static void ProcessSymbols(dynamic @params)
        {
            if(OnSymbolsReceived != null)
            {
                var symbols = JsonConvert.DeserializeObject<List<Symbol>>(@params.ToString());
                OnSymbolsReceived.Invoke(null, symbols);
            }
        }

        public static void ProcessSymbol(dynamic @params)
        {
            if(OnSymbolReceived != null)
            {
                var symbol = JsonConvert.DeserializeObject<Symbol>(@params.ToString());
                OnSymbolReceived.Invoke(null, symbol);
            }
        }
    }
}

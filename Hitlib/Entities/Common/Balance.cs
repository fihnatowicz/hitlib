using Hitlib.App;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hitlib.Entities
{
    public class Balance
    {
        public string Currency { get; set; }
        public decimal Available { get; set; }
        public decimal Reserved { get; set; }

        public static void Subscribe()
        {
            var request = new
            {
                method = "getTradingBalance",
                @params = new
                {
                    
                },
                id = "101"
            };
            var json = JsonConvert.SerializeObject(request);
            Socket.Send(json);
        }

        public static event EventHandler<List<Balance>> OnTradingBalanceReceived;

        public static void ProcessTradingBalance(dynamic @params)
        {
            if(OnTradingBalanceReceived != null)
            {
                var balance = JsonConvert.DeserializeObject<List<Balance>>(@params.ToString());
                OnTradingBalanceReceived.Invoke(null, balance);
            }
        }
    }
}

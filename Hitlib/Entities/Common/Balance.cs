using Hitlib.App;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hitlib.Entities
{
    public class Balance
    {
        public string currency { get; set; }
        public decimal available { get; set; }
        public decimal reserved { get; set; }

        public static void Subscribe()
        {
            var request = new Request
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

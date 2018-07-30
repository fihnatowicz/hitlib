using Hitlib.App;
using Hitlib.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hitlib.Entities
{
    public class Order
    {
        public long id { get; set; }
        public string clientOrderId { get; set; }
        public string symbol { get; set; }
        public string side { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public string timeInForce { get; set; }
        public decimal quantity { get; set; }
        public decimal price { get; set; }
        public decimal cumQuantity { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        public string reportType { get; set; }
        public string originalRequestClientOrderId { get; set; }

        public static string PlaceOrder(string symbol, Side side, decimal quantity, decimal price, TimeInForce timeInForce = TimeInForce.GTC, OrderType type = OrderType.limit)
        {
            var clientOrderId = Guid.NewGuid().ToString("N");
            var request = new Request
            {
                method = "newOrder",
                @params = new
                {
                    clientOrderId,
                    symbol,
                    side = side.ToString(),
                    type = type.ToString(),
                    timeInForce = timeInForce.ToString(),
                    quantity,
                    price,
                    strictValidate = false
                },
                id = "199"
            };
            var json = JsonConvert.SerializeObject(request);

            Socket.Send(json);

            return clientOrderId;
        }

        public static void CancelOrder(string clientOrderId)
        {
            var request = new Request
            {
                method = "cancelOrder",
                @params = new
                {
                    clientOrderId = clientOrderId
                },
                id = "198"
            };
            var json = JsonConvert.SerializeObject(request);
            Socket.Send(json);
        }

        public static void GetOrders()
        {
            var request = new Request
            {
                method = "getOrders",
                @params = new
                {

                },
                id = "197"
            };
            var json = JsonConvert.SerializeObject(request);
            Socket.Send(json);
        }
    }
}

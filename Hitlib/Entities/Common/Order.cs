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
        public long Id { get; set; }
        public string ClientOrderId { get; set; }
        public string Symbol { get; set; }
        public string Side { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string TimeInForce { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal CumQuantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string ReportType { get; set; }
        public string OriginalRequestClientOrderId { get; set; }

        public static string PlaceOrder(string symbol, Side side, decimal quantity, decimal price, TimeInForce timeInForce = Types.TimeInForce.GTC, OrderType type = OrderType.Limit)
        {
            var clientOrderId = Guid.NewGuid().ToString("N");
            var request = new
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
            var request = new
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
            var request = new
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

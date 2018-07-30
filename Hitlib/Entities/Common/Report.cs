using Hitlib.App;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hitlib.Entities
{
    public class Report
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

        public static void Subscribe()
        {
            var request = new Request
            {
                method = "subscribeReports"
            };
            var json = JsonConvert.SerializeObject(request);
            Socket.Send(json);
        }

        public static event EventHandler<List<Report>> OnActiveOrdersReceived;

        public static void ProcessActiveOrders(dynamic @params)
        {
            if(OnActiveOrdersReceived != null)
            {
                var reports = JsonConvert.DeserializeObject<List<Report>>(@params.ToString());
                OnActiveOrdersReceived.Invoke(null, reports);
            }
        }
    }


    public class TradeReport : Report
    {
        public decimal tradeQuantity { get; set; }
        public decimal tradePrice { get; set; }
        public long tradeId { get; set; }
        public decimal tradeFee { get; set; }

        public static event EventHandler<TradeReport> OnReportReceived;
        public static event EventHandler<TradeReport> OnOrderFilled;
        public static event EventHandler<TradeReport> OnOrderPartiallyFilled;
        public static event EventHandler<TradeReport> OnOrderCancelled;

        public static void ProcessTradeReport(dynamic @params)
        {
            TradeReport report = JsonConvert.DeserializeObject<TradeReport>(@params.ToString());

            if (OnReportReceived != null)
                OnReportReceived.Invoke(null, report);

            if (OnOrderFilled != null && report.status == "filled")
                OnOrderFilled.Invoke(null, report);

            if (OnOrderPartiallyFilled != null && report.status == "partiallyFilled")
                OnOrderPartiallyFilled.Invoke(null, report);

            if (OnOrderCancelled != null && report.status == "canceled")
                OnOrderCancelled.Invoke(null, report);
        }
    }
}

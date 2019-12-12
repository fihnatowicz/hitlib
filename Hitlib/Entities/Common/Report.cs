using Hitlib.App;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hitlib.Entities
{
    public class Report
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

        public static void Subscribe()
        {
            var request = new
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
        public decimal TradeQuantity { get; set; }
        public decimal TradePrice { get; set; }
        public long TradeId { get; set; }
        public decimal TradeFee { get; set; }

        public static event EventHandler<TradeReport> OnReportReceived;
        public static event EventHandler<TradeReport> OnOrderFilled;
        public static event EventHandler<TradeReport> OnOrderPartiallyFilled;
        public static event EventHandler<TradeReport> OnOrderCancelled;

        public static void ProcessTradeReport(dynamic @params)
        {
            TradeReport report = JsonConvert.DeserializeObject<TradeReport>(@params.ToString());

            if (OnReportReceived != null)
                OnReportReceived.Invoke(null, report);

            if (OnOrderFilled != null && report.Status == "filled")
                OnOrderFilled.Invoke(null, report);

            if (OnOrderPartiallyFilled != null && report.Status == "partiallyFilled")
                OnOrderPartiallyFilled.Invoke(null, report);

            if (OnOrderCancelled != null && report.Status == "canceled")
                OnOrderCancelled.Invoke(null, report);
        }
    }
}

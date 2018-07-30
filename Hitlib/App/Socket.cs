using Hitlib.Entities;
using Hitlib.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using WebSocket4Net;

namespace Hitlib.App
{
    public static class Socket
    {
        #region Constructor
        static Socket()
        {
            ws.Opened += new EventHandler(onOpen);
            ws.Error += new EventHandler<ErrorEventArgs>(onError);
            ws.Closed += new EventHandler(onClose);
            ws.MessageReceived += new EventHandler<MessageReceivedEventArgs>(onMessage);
        }
        #endregion

        #region Members
        private static bool isConnectionOpened = false;
        public static WebSocket ws = new WebSocket("wss://api.hitbtc.com/api/2/ws", null, WebSocketVersion.Rfc6455);
        #endregion

        #region Methods
        public static void Login(string apiKey, string secretKey)
        {
            var obj = new
            {
                method = "login",
                @params = new
                {
                    algo = "BASIC",
                    pKey = apiKey,
                    sKey = secretKey
                }
            };
            var loginJson = JsonConvert.SerializeObject(obj);
            Socket.Send(loginJson);
        }


        public static void Send(string message, int attemps = 5)
        {
            if (isConnectionOpened)
            {
                ws.Send(message);
            }
            else
            {
                for (var i = 0; i < attemps; i++)
                {
                    Thread.Sleep(1000);
                    if (isConnectionOpened)
                    {
                        ws.Send(message);
                        break;
                    }
                    else
                        continue;
                }
            }
        }

        public static void OpenConnection()
        {
            if (!isConnectionOpened)
            {
                ws.Open();
                Network.IPCfg.Ping("api.hitbtc.com");
            }
        }

        #endregion

        #region Events
        private static void onMessage(object sender, MessageReceivedEventArgs e)
        {
            dynamic obj = JObject.Parse(e.Message);
            if (obj.error != null)
            {
                Error.ProcessError(obj.error);
                return;
            }

            if (obj.method == null)
            {
                if (obj.id == null)
                    return;

                switch (obj.id.ToString())
                {
                    case "101": Balance.ProcessTradingBalance(obj.result); break;
                    case "102": Symbol.ProcessSymbols(obj.result); break;
                    case "103": Symbol.ProcessSymbol(obj.result); break;
                    case "197": Report.ProcessActiveOrders(obj.result); break;
                    case "198":
                    case "199":
                        TradeReport.ProcessTradeReport(obj.result); break;
                        //default: Console.WriteLine("[{1}] Unknown id: {0}", DateTime.UtcNow, obj.id.ToString()); break;
                }
            }
            else
            {
                switch (obj.method.ToString())
                {
                    case "updateOrderbook":
                    case "snapshotOrderbook":
                        Orderbook.ProcessOrderbook(obj.@params); break;
                    case "report":
                        TradeReport.ProcessTradeReport(obj.@params); break;
                    case "activeOrders":
                        Report.ProcessActiveOrders(obj.@params); break;
                    case "ticker":
                        Ticker.ProcessTicker(obj.@params); break;
                    case "snapshotCandles":
                        Candle.ProcessCandles(obj.@params,true); break;
                    case "updateCandles":
                        Candle.ProcessCandles(obj.@params); break;
                    case "snapshotTrades":
                    case "updateTrades":
                        Trade.ProcessTrades(obj.@params); break;
                    default:
                        Console.WriteLine("[{1}] Unknown method: {0}", DateTime.UtcNow, obj.method.ToString()); break;
                }
            }

        }

        private static void onClose(object sender, EventArgs e)
        {
            isConnectionOpened = false;
            Cn.WriteLine("Connection closed", ConsoleColor.Red);
        }

        private static void onError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine(e.Exception.Message);
        }

        private static void onOpen(object sender, EventArgs e)
        {
            isConnectionOpened = true;
            Console.WriteLine("Connection opened");
        }
        #endregion
    }
}

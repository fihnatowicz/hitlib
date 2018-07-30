using Hitlib;
using Hitlib.CustomEntities;
using Hitlib.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;


namespace PerfMonitor
{
    class Program
    {
        public const string ApiKey = "abbcfe27b6a440f65f2bf5d03bef30d1";
        public const string SecretKey = "f00279ba21749a8f00c7783335b99896";


        static void Main(string[] args)
        {
            Hitlib.App.Socket.OpenConnection();
            //Hitlib.App.Socket.Login(ApiKey, SecretKey);
            Candle.Subscribe("BANCABTC", Hitlib.Types.Period.M1);
            Candle.OnCandleReceived += Candle_OnCandleReceived;
            
            Console.ReadLine();
        }

        private static List<Candle> Candles = new List<Candle>();

        private static void Candle_OnCandleReceived(object sender, Candle e)
        {
            
        }
    }
}

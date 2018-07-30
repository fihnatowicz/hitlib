using System;
using System.Collections.Generic;
using System.Text;

namespace Hitlib.Types
{
    public enum TimeInForce
    {
        GTC, // Good till cancel. GTC order won't close until it is filled. 
        IOC,  // An immediate or cancel order is an order to buy or sell that must be executed immediately, and any portion of the order that cannot be immediately filled is cancelled. 
        FOK, // Fill or kill is a type of time-in-force designation used in securities trading that instructs a brokerage to execute a transaction immediately and completely or not at all. 
        Day, // keeps the order active until the end of the trading day in UTC.       
        GTD, // Good till date specified in expireTime.
    }
}

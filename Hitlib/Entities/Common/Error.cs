using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Hitlib.Entities
{
    public class Error
    {
        public int code { get; set; }
        public string message { get; set; }
        public string description { get; set; }

        public static event EventHandler<Error> OnErrorReceived;

        public static void ProcessError(dynamic @params)
        {
            if (OnErrorReceived != null)
            {
                var error = JsonConvert.DeserializeObject<Error>(@params.ToString());
                OnErrorReceived.Invoke(null, error);
            }
        }

        public override string ToString()
        {
            return string.Format("Code: {0}, Message: {1}, Description: {2}", this.code, this.message, this.description);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Hitlib.Entities
{
    public class Request
    {
        public string method { get; set; }
        public object @params { get; set; }
        public string id { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Hitlib.Entities
{
    public class Request
    {
        public string Method { get; set; }
        public object Params { get; set; }
        public string Id { get; set; }
    }
}

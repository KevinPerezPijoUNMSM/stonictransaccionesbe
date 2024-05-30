using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stonictransaccionesbe.model
{
    public class ReturnValue
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public string Argument { get; set; }
        public object Data { get; set; }
    }
}

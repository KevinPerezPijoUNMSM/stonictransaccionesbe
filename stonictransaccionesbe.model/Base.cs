using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stonictransaccionesbe.model
{
    public abstract class Base
    {
        public DateTime AudFechaRegistro { get; set; }
        public DateTime AudFechaModifico { get; set; }
    }
}

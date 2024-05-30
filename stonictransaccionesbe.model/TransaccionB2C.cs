using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stonictransaccionesbe.model
{
    public class TransaccionB2C
    {
        public int IdTransaccionB2C { get; set; }
        public int IdComprador { get; set; }
        public int IdVendedor { get; set; }
        public int IdSucursal { get; set; }
        public int Numero { get; set; }
        public DateTime AudFechaRegistro { get; set; }
        public DateTime AudFechaModifico { get; set; }
        public decimal CostoTotal { get; set; }
    }
}

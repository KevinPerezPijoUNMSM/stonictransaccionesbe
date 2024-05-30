using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stonictransaccionesbe.model
{
    public class TransaccionB2B
    {
        public int IdTransaccionB2B { get; set; }
        public int IdComprador { get; set; }
        public int IdVendedor { get; set; }
        public int IdSucursal { get; set; }
        public int Numero { get; set; }
        public DateTime AudFechaRegistro { get; set; }
        public DateTime AudFechaModifico { get; set; }
        public decimal CostoTotal { get; set; }
    }
}
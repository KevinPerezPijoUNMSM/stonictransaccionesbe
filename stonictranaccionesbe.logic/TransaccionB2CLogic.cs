using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using stonictransaccionesbe.data;
using stonictransaccionesbe.model;


namespace stonictranaccionesbe.logic
{
    public class TransaccionB2CLogic
    {
        private TransaccionB2CData oTransaccionB2CData;

        public TransaccionB2CLogic()
        {
            oTransaccionB2CData = new TransaccionB2CData();
        }

        public ReturnValue Registrar(TransaccionB2C item)
        {
            ReturnValue oReturn = new ReturnValue();
            try
            {
                oReturn = oTransaccionB2CData.Registrar(item);
            }
            catch (Exception ex)
            {
                oReturn.Success = false;
                oReturn.Message = "Error no controlado consulte con el administrador. "+ex;
            }
            return oReturn;
        }
    }
}
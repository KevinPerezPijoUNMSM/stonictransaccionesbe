using Npgsql;
using stonictransaccionesbe.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stonictransaccionesbe.data
{
    public class TransaccionB2CData
    {
        public ReturnValue Registrar(TransaccionB2C item)
        {
            ReturnValue oReturn = new ReturnValue();

            try
            {
                using (var cnn = new NpgsqlConnection(HelpData.ConnectionString()))
                {
                    using (var cmd = new NpgsqlCommand("tra_man_transaccionb2c_ins", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros de entrada
                        cmd.Parameters.AddWithValue("p_idcomprador", item.IdComprador);
                        cmd.Parameters.AddWithValue("p_idvendedor", item.IdVendedor);
                        cmd.Parameters.AddWithValue("p_idsucursal", item.IdSucursal);
                        cmd.Parameters.AddWithValue("p_costototal", item.CostoTotal);

                        // Agregar parámetros de salida
                        var pMessage = new NpgsqlParameter("p_message", NpgsqlTypes.NpgsqlDbType.Varchar, 200)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(pMessage);

                        var pCode = new NpgsqlParameter("p_code", NpgsqlTypes.NpgsqlDbType.Integer)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(pCode);

                        cnn.Open();
                        cmd.ExecuteNonQuery();

                        // Obtener los valores de los parámetros de salida
                        oReturn.Message = pMessage.Value.ToString();
                        oReturn.Success = Convert.ToInt32(pCode.Value) == 1;

                        cnn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                oReturn.Success = false;
                oReturn.Message = $"Error: {ex.Message}";
            }

            return oReturn;
        }
    }
}

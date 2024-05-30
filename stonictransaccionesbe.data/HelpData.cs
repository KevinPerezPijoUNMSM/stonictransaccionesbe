using System;
using System.Collections.Generic;
using Npgsql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace stonictransaccionesbe.data
{
    public static class HelpData
    {
        private static IConfiguration Configuration { get; }

        static HelpData()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        internal static string ConnectionString
        {
            get
            {
                return Configuration.GetConnectionString("DefaultConnection");
            }
        }

        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(ConnectionString);
        }

        internal static NpgsqlParameter NpgsqlParameter(string name, NpgsqlTypes.NpgsqlDbType type, object valor, int size = 0)
        {
            var param = size > 0 ? new NpgsqlParameter(name, type, size) : new NpgsqlParameter(name, type);
            param.Direction = System.Data.ParameterDirection.Input;
            param.Value = valor ?? DBNull.Value;

            if (type == NpgsqlTypes.NpgsqlDbType.Varchar || type == NpgsqlTypes.NpgsqlDbType.Text)
            {
                param.Value = valor ?? "";
            }
            else
            {
                param.Value = valor ?? DBNull.Value;
            }

            return param;
        }

        internal static NpgsqlParameter NpgsqlParameterOutput(string name, NpgsqlTypes.NpgsqlDbType type, int size)
        {
            NpgsqlParameter param = new NpgsqlParameter(name, type, size);
            param.Direction = System.Data.ParameterDirection.Output;
            return param;
        }

        internal static NpgsqlParameter NpgsqlParameterOutput(string name, NpgsqlTypes.NpgsqlDbType type)
        {
            NpgsqlParameter param = new NpgsqlParameter(name, type)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            return param;
        }

        internal static List<T> Mapper<T>(NpgsqlDataReader lector)
        {
            try
            {
                if (lector != null)
                {
                    if (lector.HasRows)
                    {
                        PropertyInfo[] propiedades = typeof(T).GetProperties();
                        Dictionary<string, int> ubicacion = new Dictionary<string, int>();

                        foreach (PropertyInfo propiedad in propiedades)
                        {
                            for (int indice = 0; indice < lector.FieldCount; indice++)
                                if ((propiedad.Name.ToUpper() == lector.GetName(indice).ToUpper()) && (propiedad.CanWrite))
                                    ubicacion.Add(propiedad.Name, indice);
                        }

                        List<T> respuesta = new List<T>();

                        while (lector.Read())
                        {
                            T instancia = (T)Activator.CreateInstance<T>();
                            foreach (PropertyInfo propiedad in propiedades)
                            {
                                if (ubicacion.TryGetValue(propiedad.Name, out int columna))
                                {
                                    try
                                    {
                                        if (!lector.IsDBNull(columna))
                                        {
                                            if (lector[columna].GetType().Name == "BitArray")
                                            {
                                                propiedad.SetValue(instancia, lector.GetBoolean(columna), null);
                                            }
                                            else
                                            {
                                                propiedad.SetValue(instancia, lector.GetValue(columna), null);
                                            }
                                        }
                                    }
                                    catch { }
                                }
                            }
                            respuesta.Add(instancia);
                        }
                        lector.Close();
                        return respuesta;
                    }
                    else
                    {
                        lector.Close();
                        return new List<T>();
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                /* Manejo de error */
                return null;
            }
        }

        internal static T MapperUniq<T>(NpgsqlDataReader lector)
        {
            T respuesta = (T)Activator.CreateInstance<T>();
            try
            {
                if (lector != null)
                {
                    if (lector.HasRows)
                    {
                        PropertyInfo[] propiedades = typeof(T).GetProperties();
                        Dictionary<string, int> ubicacion = new Dictionary<string, int>();

                        foreach (PropertyInfo propiedad in propiedades)
                        {
                            for (int indice = 0; indice < lector.FieldCount; indice++)
                                if ((propiedad.Name.ToUpper() == lector.GetName(indice).ToUpper()) && (propiedad.CanWrite))
                                    ubicacion.Add(propiedad.Name, indice);
                        }

                        while (lector.Read())
                        {
                            foreach (PropertyInfo propiedad in propiedades)
                            {
                                if (ubicacion.TryGetValue(propiedad.Name, out int columna))
                                {
                                    try
                                    {
                                        if (!lector.IsDBNull(columna))
                                        {
                                            if (lector[columna].GetType().Name == "BitArray")
                                            {
                                                propiedad.SetValue(respuesta, lector.GetBoolean(columna), null);
                                            }
                                            else
                                            {
                                                propiedad.SetValue(respuesta, lector.GetValue(columna), null);
                                            }
                                        }
                                    }
                                    catch { }
                                }
                            }
                        }
                        lector.Close();
                        return respuesta;
                    }
                    else
                    {
                        lector.Close();
                        return (T)Activator.CreateInstance<T>();
                    }
                }
                else
                {
                    return respuesta;
                }
            }
            catch (Exception ex)
            {
                /* Manejo de error */
                return respuesta;
            }
        }
    }
}

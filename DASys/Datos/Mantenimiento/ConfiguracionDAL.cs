using Acces;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Datos
{
    public class ConfiguracionDAL: IConfiguracion
    {
        public List<ConfiguracionPaginationDto> PaginadoConfiguracion(PaginationParameter objPaginationParameter)
        {
            List<ConfiguracionPaginationDto> retList = new List<ConfiguracionPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ConfiguracionPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new ConfiguracionPaginationDto
                    {
                        IdConfiguracion = lector.IsDBNull(lector.GetOrdinal("IdConfiguracion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdConfiguracion")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        TiempoColor = lector.IsDBNull(lector.GetOrdinal("TiempoColor")) ? default(byte) : lector.GetByte(lector.GetOrdinal("TiempoColor")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        TiempoEntreMarcaciones = lector.IsDBNull(lector.GetOrdinal("TiempoEntreMarcaciones")) ? default(string) : lector.GetString(lector.GetOrdinal("TiempoEntreMarcaciones")),
                        TiempoRELAY = lector.IsDBNull(lector.GetOrdinal("TiempoRELAY")) ? default(byte) : lector.GetByte(lector.GetOrdinal("TiempoRELAY")),
                        Tipo = lector.IsDBNull(lector.GetOrdinal("Tipo")) ? default(string) : lector.GetString(lector.GetOrdinal("Tipo")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad")),
                    });
                }
            }
            return retList;
        }

        public List<ConfiguracionConsultaDto> ListadoConfiguracion(string Configuracion)
        {
            List<ConfiguracionConsultaDto> retList = new List<ConfiguracionConsultaDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Configuracion });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ConfiguracionListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new ConfiguracionConsultaDto
                    {
                        IdConfiguracion = lector.IsDBNull(lector.GetOrdinal("IdConfiguracion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdConfiguracion")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        TiempoColor = lector.IsDBNull(lector.GetOrdinal("TiempoColor")) ? default(byte) : lector.GetByte(lector.GetOrdinal("TiempoColor")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        TiempoEntreMarcaciones = lector.IsDBNull(lector.GetOrdinal("TiempoEntreMarcaciones")) ? default(string) : lector.GetString(lector.GetOrdinal("TiempoEntreMarcaciones")),
                        TiempoRELAY = lector.IsDBNull(lector.GetOrdinal("TiempoRELAY")) ? default(byte) : lector.GetByte(lector.GetOrdinal("TiempoRELAY")),
                        Tipo = lector.IsDBNull(lector.GetOrdinal("Tipo")) ? default(string) : lector.GetString(lector.GetOrdinal("Tipo")),


                    });
                }
            }
            return retList;
        }

        public ConfiguracionConsultaDto ConsultaConfiguracion(ConfiguracionConsultaDto objConfiguracion)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConfiguracion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objConfiguracion.IdConfiguracion });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ConfiguracionConsulta", listaParams.ToArray());
                while (lector.Read())
                {
                    objConfiguracion.IdConfiguracion = lector.IsDBNull(lector.GetOrdinal("IdConfiguracion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdConfiguracion"));
                    //objConfiguracion.Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado"));
                    objConfiguracion.TiempoColor = lector.IsDBNull(lector.GetOrdinal("TiempoColor")) ? default(byte) : lector.GetByte(lector.GetOrdinal("TiempoColor"));
                    objConfiguracion.Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre"));
                    objConfiguracion.TiempoEntreMarcaciones = lector.IsDBNull(lector.GetOrdinal("TiempoEntreMarcaciones")) ? default(string) : lector.GetString(lector.GetOrdinal("TiempoEntreMarcaciones"));
                    objConfiguracion.TiempoRELAY = lector.IsDBNull(lector.GetOrdinal("TiempoRELAY")) ? default(byte) : lector.GetByte(lector.GetOrdinal("TiempoRELAY"));
                    objConfiguracion.IdTipo = lector.IsDBNull(lector.GetOrdinal("IdTipo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdTipo"));
                    objConfiguracion.IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado"));
                    objConfiguracion.IdEmpresa = lector.IsDBNull(lector.GetOrdinal("IdEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresa"));
                }
            }
            return objConfiguracion;
        }

        public int MantenimientoConfiguracion(Configuracion objConfiguracionp)
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConfiguracion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objConfiguracionp.IdConfiguracion });
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objConfiguracionp.Nombre });
                listaParams.Add(new SqlParameter("@IdEstado", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objConfiguracionp.IdEstado });
                listaParams.Add(new SqlParameter("@IdTipo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objConfiguracionp.IdTipo });
                listaParams.Add(new SqlParameter("@TiempoEntreMarcaciones", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objConfiguracionp.TiempoEntreMarcaciones });
                listaParams.Add(new SqlParameter("@TiempoColor", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objConfiguracionp.TiempoColor });
                listaParams.Add(new SqlParameter("@TiempoRELAY", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objConfiguracionp.TiempoRELAY });
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objConfiguracionp.IdEmpresa });
                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_ConfiguracionMantenimiento", listaParams.ToArray());
            }
            return r;
        }

        public string EliminarConfiguracion(string IdConfiguracion,int Accion)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConfiguracion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdConfiguracion });
                listaParams.Add(new SqlParameter("@Accion", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = Accion });
                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_ConfiguracionEliminar", listaParams.ToArray()).ToString();
            }
            return r;
        }
    }
}

using Acces;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Datos
{
    public class EntidadDAL: IEntidad
    {
        public List<EntidadPaginationDto> PaginadoEntidad(PaginationParameter objPaginationParameter)
        {
            List<EntidadPaginationDto> retList = new List<EntidadPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_EntidadPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new EntidadPaginationDto
                    {
                        IdEntidad = lector.IsDBNull(lector.GetOrdinal("IdEntidad")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEntidad")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0: lector.GetInt32(lector.GetOrdinal("Cantidad")),
                    });
                }
            }
            return retList;
        }

        public List<EntidadConsultaDto> ListadoEntidad(string Entidad)
        {
            List<EntidadConsultaDto> retList = new List<EntidadConsultaDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Entidad });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_EntidadListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new EntidadConsultaDto
                    {
                        IdEntidad = lector.IsDBNull(lector.GetOrdinal("IdEntidad")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEntidad")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                    });
                }
            }
            return retList;
        }

        public EntidadConsultaDto ConsultaEntidad(EntidadConsultaDto objEntidad)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdEntidad", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objEntidad.IdEntidad });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_EntidadConsulta", listaParams.ToArray());
                while (lector.Read())
                {
                    objEntidad.IdEntidad = lector.IsDBNull(lector.GetOrdinal("IdEntidad")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEntidad"));
                    objEntidad.Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre"));
                    objEntidad.Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado"));
                    objEntidad.IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado"));
                    objEntidad.IdPrincipal = lector.IsDBNull(lector.GetOrdinal("IdPrincipal")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPrincipal"));

                }
            }
            return objEntidad;
        }

        public int MantenimientoEntidad(Entidades objEntidadp)
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdEntidad", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objEntidadp.IdEntidad });
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objEntidadp.Nombre });
                listaParams.Add(new SqlParameter("@IdEstado", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objEntidadp.IdEstado });
                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_EntidadMantenimiento", listaParams.ToArray());
            }
            return r;
        }

        public string EliminarEntidad(string IdEntidad,int Accion)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdEntidad", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdEntidad });
                listaParams.Add(new SqlParameter("@Accion", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = Accion });
                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_EntidadEliminar", listaParams.ToArray()).ToString();
            }
            return r;
        }
    }
}

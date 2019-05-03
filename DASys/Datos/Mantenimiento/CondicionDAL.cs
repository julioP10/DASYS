using Acces;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Datos
{
    public class CondicionDAL : ICondicion
    {
        public List<CondicionPaginationDto> PaginadoCondicion(PaginationParameter objPaginationParameter)
        {
            List<CondicionPaginationDto> retList = new List<CondicionPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_CondicionPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new CondicionPaginationDto
                    {
                        IdCondicion = lector.IsDBNull(lector.GetOrdinal("IdCondicion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdCondicion")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        Regimen = lector.IsDBNull(lector.GetOrdinal("Regimen")) ? default(string) : lector.GetString(lector.GetOrdinal("Regimen")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad")),

                    });
                }
            }
            return retList;
        }

        public List<CondicionConsultaDto> ListadoCondicion(string Condicion)
        {
            List<CondicionConsultaDto> retList = new List<CondicionConsultaDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Condicion });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_CondicionListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new CondicionConsultaDto
                    {
                        IdCondicion = lector.IsDBNull(lector.GetOrdinal("IdCondicion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdCondicion")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado")),
                        IdRegimen = lector.IsDBNull(lector.GetOrdinal("IdRegimen")) ? default(string) : lector.GetString(lector.GetOrdinal("IdRegimen")),

                    });
                }
            }
            return retList;
        }

        public CondicionConsultaDto ConsultaCondicion(CondicionConsultaDto objCondicion)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdCondicion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objCondicion.IdCondicion });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_CondicionConsulta", listaParams.ToArray());
                while (lector.Read())
                {
                    objCondicion.IdCondicion = lector.IsDBNull(lector.GetOrdinal("IdCondicion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdCondicion"));
                    objCondicion.Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre"));
                    objCondicion.IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado"));
                    objCondicion.IdRegimen = lector.IsDBNull(lector.GetOrdinal("IdRegimen")) ? default(string) : lector.GetString(lector.GetOrdinal("IdRegimen"));
                }
            }
            return objCondicion;
        }

        public int MantenimientoCondicion(Condicion objCondicionp)
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdCondicion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objCondicionp.IdCondicion });
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objCondicionp.Nombre });
                listaParams.Add(new SqlParameter("@IdEstado", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objCondicionp.IdEstado });
                listaParams.Add(new SqlParameter("@IdRegimen", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objCondicionp.IdRegimen });
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objCondicionp.IdEmpresa });
                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_CondicionMantenimiento", listaParams.ToArray());
            }
            return r;
        }

        public string EliminarCondicion(string IdCondicion, int Accion)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdCondicion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdCondicion });
                listaParams.Add(new SqlParameter("@Accion", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = Accion });
                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_CondicionEliminar", listaParams.ToArray()).ToString();
            }
            return r;
        }
    }
}

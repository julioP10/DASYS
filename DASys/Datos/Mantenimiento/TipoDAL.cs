using Acces;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Datos
{
    public class TipoDAL: ITipo
    {
        public List<TipoPaginationDto> PaginadoTipo(PaginationParameter objPaginationParameter)
        {
            List<TipoPaginationDto> retList = new List<TipoPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_TipoPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new TipoPaginationDto
                    {
                        IdTipo = lector.IsDBNull(lector.GetOrdinal("IdTipo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdTipo")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                    });
                }
            }
            return retList;
        }

        public List<TipoConsultaDto> ListadoTipo(string Tipo)
        {
            List<TipoConsultaDto> retList = new List<TipoConsultaDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Tipo });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_TipoListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new TipoConsultaDto
                    {
                        IdTipo = lector.IsDBNull(lector.GetOrdinal("IdTipo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdTipo")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado")),
                        IdPrincipal = lector.IsDBNull(lector.GetOrdinal("IdPrincipal")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPrincipal")),

                    });
                }
            }
            return retList;
        }

        public TipoConsultaDto ConsultaTipo(TipoConsultaDto objTipo)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdTipo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objTipo.IdTipo });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_TipoConsulta", listaParams.ToArray());
                while (lector.Read())
                {
                    objTipo.IdTipo = lector.IsDBNull(lector.GetOrdinal("IdTipo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdTipo"));
                    objTipo.Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre"));
                    objTipo.Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado"));
                    objTipo.IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado"));
                    objTipo.IdPrincipal = lector.IsDBNull(lector.GetOrdinal("IdPrincipal")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPrincipal"));
                }
            }
            return objTipo;
        }

        public int MantenimientoTipo(Tipo objTipop)
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdTipo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objTipop.IdTipo });
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objTipop.Nombre });
                listaParams.Add(new SqlParameter("@IdEstado", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objTipop.IdEstado });
                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_TipoMantenimiento", listaParams.ToArray());
            }
            return r;
        }

        public string EliminarTipo(string IdTipo)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdTipo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdTipo });
                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_TipoEliminar", listaParams.ToArray()).ToString();
            }
            return r;
        }
    }
}

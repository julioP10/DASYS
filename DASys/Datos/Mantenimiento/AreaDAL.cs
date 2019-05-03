using Acces;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class AreaDAL : IArea
    {
        public List<AreaPaginationDto> PaginadoArea(PaginationParameter objPaginationParameter)
        {
            List<AreaPaginationDto> retList = new List<AreaPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_AreaPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new AreaPaginationDto
                    {
                        IdArea = lector.IsDBNull(lector.GetOrdinal("IdArea")) ? default(string) : lector.GetString(lector.GetOrdinal("IdArea")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad")),

                    });
                }
            }
            return retList;
        }

        public List<Area> ListadoArea(string area)
        {
            List<Area> retList = new List<Area>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = area });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_AreaListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new Area
                    {
                      IdArea = lector.IsDBNull(lector.GetOrdinal("IdArea")) ? default(string) : lector.GetString(lector.GetOrdinal("IdArea")),
                      IdPrincipal = lector.IsDBNull(lector.GetOrdinal("IdPrincipal")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPrincipal")),
                      Nivel = lector.IsDBNull(lector.GetOrdinal("Nivel")) ? 0 : lector.GetInt32(lector.GetOrdinal("Nivel")),
                      Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),

                    });
                }
            }
            return retList;
        }

        public AreaConsultaDto ConsultaArea(AreaConsultaDto objArea)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdArea", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objArea.IdArea });
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objArea.IdEmpresa });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_AreaConsulta", listaParams.ToArray());
                while (lector.Read())
                {
                    objArea.IdArea = lector.IsDBNull(lector.GetOrdinal("IdArea")) ? default(string) : lector.GetString(lector.GetOrdinal("IdArea"));
                    objArea.IdEmpresa = lector.IsDBNull(lector.GetOrdinal("IdEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresa"));
                    objArea.Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre"));
                    objArea.IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado"));

                }
            }
            return objArea;
        }

        public int MantenimientoArea(Area objAreap)
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdArea", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objAreap.IdArea });
                //listaParams.Add(new SqlParameter("@IdPrincipal", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objAreap.IdPrincipal });
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objAreap.Nombre });
                listaParams.Add(new SqlParameter("@IdEstado", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objAreap.IdEstado });
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objAreap.IdEmpresa });
                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_AreaMantenimiento", listaParams.ToArray());
            }
            return r;
        }

        public string  EliminarArea(string IdArea,int Accion)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdArea", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdArea });
                listaParams.Add(new SqlParameter("@Accion", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = Accion });
                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_AreaEliminar", listaParams.ToArray()).ToString();
            }
            return r;
        }
    }
}

using Acces;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Datos
{
    public class CategoriaDAL:ICategoria
    {
        public List<CategoriaPaginationDto> PaginadoCategoria(PaginationParameter objPaginationParameter)
        {
            List<CategoriaPaginationDto> retList = new List<CategoriaPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_CategoriaPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new CategoriaPaginationDto
                    {
                        IdCategoria = lector.IsDBNull(lector.GetOrdinal("IdCategoria")) ? default(string) : lector.GetString(lector.GetOrdinal("IdCategoria")),
                        Entidad = lector.IsDBNull(lector.GetOrdinal("Entidad")) ? default(string) : lector.GetString(lector.GetOrdinal("Entidad")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad")),
                        

                    });
                }
            }
            return retList;
        }

        public List<Categoria> ListadoCategoria(string Categoria)
        {
            List<Categoria> retList = new List<Categoria>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Categoria });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_CategoriaListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new Categoria
                    {
                        IdCategoria = lector.IsDBNull(lector.GetOrdinal("IdCategoria")) ? default(string) : lector.GetString(lector.GetOrdinal("IdCategoria")),
                        IdEntidad = lector.IsDBNull(lector.GetOrdinal("IdEntidad")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEntidad")),
                        IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre"))

                    });
                }
            }
            return retList;
        }

        public CategoriaConsultaDto ConsultaCategoria(CategoriaConsultaDto objCategoria)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdCategoria", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objCategoria.IdCategoria });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_CategoriaConsulta", listaParams.ToArray());
                while (lector.Read())
                {
                    objCategoria.IdCategoria = lector.IsDBNull(lector.GetOrdinal("IdCategoria")) ? default(string) : lector.GetString(lector.GetOrdinal("IdCategoria"));
                    objCategoria.IdEntidad = lector.IsDBNull(lector.GetOrdinal("IdEntidad")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEntidad"));
                    objCategoria.IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado"));
                    objCategoria.Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre"));

                }
            }
            return objCategoria;
        }

        public int MantenimientoCategoria(Categoria objCategoriap)
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdCategoria", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objCategoriap.IdCategoria });
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objCategoriap.Nombre });
                listaParams.Add(new SqlParameter("@IdEntidad", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objCategoriap.IdEntidad });
                listaParams.Add(new SqlParameter("@IdEstado", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objCategoriap.IdEstado });
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objCategoriap.IdEmpresa });
                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_CategoriaMantenimiento", listaParams.ToArray());
            }
            return r;
        }

        public string EliminarCategoria(string IdCategoria, int Accion)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdCategoria", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdCategoria });
                listaParams.Add(new SqlParameter("@Accion", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = Accion });
                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_CategoriaEliminar", listaParams.ToArray()).ToString();
            }
            return r;
        }
    }
}

using Acces;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Datos
{
    public class MarcaDAL : IMarca
    {

        public List<MarcaPaginationDto> PaginadoMarca(PaginationParameter objPaginationParameter)
        {
            List<MarcaPaginationDto> retList = new List<MarcaPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_MarcaPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new MarcaPaginationDto
                    {
                        IdMarca = lector.IsDBNull(lector.GetOrdinal("IdMarca")) ? default(string) : lector.GetString(lector.GetOrdinal("IdMarca")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Entidad = lector.IsDBNull(lector.GetOrdinal("Entidad")) ? default(string) : lector.GetString(lector.GetOrdinal("Entidad")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad")),

                    });
                }
            }
            return retList;
        }

        public List<MarcaConsultaDto> ListadoMarca(string Marca)
        {
            List<MarcaConsultaDto> retList = new List<MarcaConsultaDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Marca });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_MarcaListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new MarcaConsultaDto
                    {
                        IdMarca = lector.IsDBNull(lector.GetOrdinal("IdMarca")) ? default(string) : lector.GetString(lector.GetOrdinal("IdMarca")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado")),

                    });
                }
            }
            return retList;
        }

        public MarcaConsultaDto ConsultaMarca(MarcaConsultaDto objMarca)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdMarca", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objMarca.IdMarca });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_MarcaConsulta", listaParams.ToArray());
                while (lector.Read())
                {
                    objMarca.IdMarca = lector.IsDBNull(lector.GetOrdinal("IdMarca")) ? default(string) : lector.GetString(lector.GetOrdinal("IdMarca"));
                    objMarca.Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre"));
                    objMarca.Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado"));
                    objMarca.IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado"));
                    objMarca.IdEmpresa = lector.IsDBNull(lector.GetOrdinal("IdEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresa"));
                }
            }
            return objMarca;
        }

        public string MantenimientoMarca(Marca objMarcap)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdMarca", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objMarcap.IdMarca });
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objMarcap.Nombre });
                listaParams.Add(new SqlParameter("@IdEstado", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objMarcap.IdEstado });
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objMarcap.IdEmpresa });
                listaParams.Add(new SqlParameter("@IdEntidad", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objMarcap.IdEntidad });
                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_MarcaMantenimiento", listaParams.ToArray()).ToString();
            }
            return r;
        }

        public string EliminarMarca(string IdMarca, int Accion)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdMarca", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdMarca });
                listaParams.Add(new SqlParameter("@Accion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Accion });

                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_MarcaEliminar", listaParams.ToArray()).ToString();
            }
            return r;
        }
    }
}

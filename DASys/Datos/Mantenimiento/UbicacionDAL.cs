using Acces;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Datos
{
    public class UbicacionDAL: IUbicacion
    {
        public List<UbicacionPaginationDto> PaginadoUbicacion(PaginationParameter objPaginationParameter)
        {
            List<UbicacionPaginationDto> retList = new List<UbicacionPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_UbicacionPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new UbicacionPaginationDto
                    {
                        IdUbicacion = lector.IsDBNull(lector.GetOrdinal("IdUbicacion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdUbicacion")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Departamento = lector.IsDBNull(lector.GetOrdinal("Departamento")) ? default(string) : lector.GetString(lector.GetOrdinal("Departamento")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        Nivel = lector.IsDBNull(lector.GetOrdinal("Nivel")) ? 0 : lector.GetInt32(lector.GetOrdinal("Nivel")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad")),

                    });
                }
            }
            return retList;
        }

        public List<UbicacionConsultaDto> ListadoUbicacion(string Ubicacion)
        {
            List<UbicacionConsultaDto> retList = new List<UbicacionConsultaDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Ubicacion });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_UbicacionListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new UbicacionConsultaDto
                    {
                        IdUbicacion = lector.IsDBNull(lector.GetOrdinal("IdUbicacion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdUbicacion")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Departamento = lector.IsDBNull(lector.GetOrdinal("Departamento")) ? default(string) : lector.GetString(lector.GetOrdinal("Departamento")),
                        IdDepartamento = lector.IsDBNull(lector.GetOrdinal("IdDepartamento")) ? default(string) : lector.GetString(lector.GetOrdinal("IdDepartamento")),
                        Nivel = lector.IsDBNull(lector.GetOrdinal("Nivel")) ? 0 : lector.GetInt32(lector.GetOrdinal("Nivel")),
                    });
                }
            }
            return retList;
        }

        public UbicacionConsultaDto ConsultaUbicacion(UbicacionConsultaDto objUbicacion)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdUbicacion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUbicacion.IdUbicacion });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_UbicacionConsulta", listaParams.ToArray());
                while (lector.Read())
                {
                    objUbicacion.IdUbicacion = lector.IsDBNull(lector.GetOrdinal("IdUbicacion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdUbicacion"));
                    objUbicacion.Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre"));
                    objUbicacion.Departamento = lector.IsDBNull(lector.GetOrdinal("Departamento")) ? default(string) : lector.GetString(lector.GetOrdinal("Departamento"));
                    objUbicacion.IdDepartamento = lector.IsDBNull(lector.GetOrdinal("IdDepartamento")) ? default(string) : lector.GetString(lector.GetOrdinal("IdDepartamento"));
                    objUbicacion.Nivel = lector.IsDBNull(lector.GetOrdinal("Nivel")) ? 0 : lector.GetInt32(lector.GetOrdinal("Nivel"));
                    objUbicacion.IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado"));
                    objUbicacion.IdEmpresa = lector.IsDBNull(lector.GetOrdinal("IdEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresa"));
                }
            }
            return objUbicacion;
        }

        public int MantenimientoUbicacion(Ubicacion objUbicacionp)
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdUbicacion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUbicacionp.IdUbicacion });
                listaParams.Add(new SqlParameter("@IdDepartamento", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUbicacionp.IdDepartamento });
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUbicacionp.Nombre });
                listaParams.Add(new SqlParameter("@IdEstado", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUbicacionp.IdEstado });
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUbicacionp.IdEmpresa });
                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_UbicacionMantenimiento", listaParams.ToArray());
            }
            return r;
        }

        public string EliminarUbicacion(string IdUbicacion, int Accion)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdUbicacion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdUbicacion });
                listaParams.Add(new SqlParameter("@Accion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Accion });
                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_UbicacionEliminar", listaParams.ToArray()).ToString();
            }
            return r;
        }
    }
}

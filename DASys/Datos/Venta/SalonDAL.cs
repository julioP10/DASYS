using Acces;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Datos
{
    public class SalonDAL : ISalon
    {

        public string MantenimientoSalon(Salon objSalon)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("IdSalon", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objSalon.IdSalon });
                listaParams.Add(new SqlParameter("Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objSalon.Nombre });
                listaParams.Add(new SqlParameter("Estado", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objSalon.Estado });
                listaParams.Add(new SqlParameter("IdSucursal", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objSalon.IdSucursal });
                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_SalonMantenimiento", listaParams.ToArray()).ToString();
            }
            return r;
        }
        public string EliminarSalon(int IdSalon, int Accion)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdSalon", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = IdSalon });
                listaParams.Add(new SqlParameter("@Accion", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = Accion });
                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_SalonEliminar", listaParams.ToArray()).ToString();
            }
            return r;
        }

        public List<SalonConsultaDto> ListadoSalon(SalonConsultaDto objSalon)
        {
            List<SalonConsultaDto> retList = new List<SalonConsultaDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objSalon.Nombre });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_SalonListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new SalonConsultaDto
                    {
                        IdSalon = lector.IsDBNull(lector.GetOrdinal("IdSalon")) ? 0 : lector.GetInt32(lector.GetOrdinal("IdSalon")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? 0 : lector.GetInt32(lector.GetOrdinal("Estado")),
                        IdSucursal = lector.IsDBNull(lector.GetOrdinal("IdSucursal")) ? 0 : lector.GetInt32(lector.GetOrdinal("IdSucursal")),
                        Sucursal = lector.IsDBNull(lector.GetOrdinal("Sucursal")) ? default(string) : lector.GetString(lector.GetOrdinal("Sucursal")),
                    });
                }
            }
            return retList;
        }
        public List<SalonPaginationDto> PaginadoSalon(PaginationParameter objPaginationParameter)
        {
            List<SalonPaginationDto> retList = new List<SalonPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_SalonPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new SalonPaginationDto
                    {
                        IdSalon = lector.IsDBNull(lector.GetOrdinal("IdSalon")) ? 0 : lector.GetInt32(lector.GetOrdinal("IdSalon")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? 0 : lector.GetInt32(lector.GetOrdinal("Estado")),
                        IdSucursal = lector.IsDBNull(lector.GetOrdinal("IdSucursal")) ? 0 : lector.GetInt32(lector.GetOrdinal("IdSucursal")),
                    });
                }
            }
            return retList;
        }
        public SalonConsultaDto ConsultaSalon(SalonConsultaDto objSalon)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdSalon", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objSalon.IdSalon });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_SalonConsulta", listaParams.ToArray());
                while (lector.Read())
                {
                    objSalon.IdSalon = lector.IsDBNull(lector.GetOrdinal("IdSalon")) ? 0 : lector.GetInt32(lector.GetOrdinal("IdSalon"));
                    objSalon.Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre"));
                    objSalon.Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? 0 : lector.GetInt32(lector.GetOrdinal("Estado"));
                    objSalon.IdSucursal = lector.IsDBNull(lector.GetOrdinal("IdSucursal")) ? 0 : lector.GetInt32(lector.GetOrdinal("IdSucursal"));
                }
            }
            return objSalon;
        }

    }
}
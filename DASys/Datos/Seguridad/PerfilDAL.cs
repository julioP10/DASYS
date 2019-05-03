using Acces;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Datos
{
    public class PerfilDAL: IPerfil
    {
        public List<PerfilPaginationDto> PaginadoPerfil(PaginationParameter objPaginationParameter)
        {
            List<PerfilPaginationDto> retList = new List<PerfilPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_PerfilPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new PerfilPaginationDto
                    {
                        IdPerfil = lector.IsDBNull(lector.GetOrdinal("IdPerfil")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPerfil")),
                        Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad")),

                    });
                }
            }
            return retList;
        }

        public List<PerfilConsultaDto> ListadoPerfil(string Perfil)
        {
            List<PerfilConsultaDto> retList = new List<PerfilConsultaDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Perfil });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_PerfilListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new PerfilConsultaDto
                    {
                        IdPerfil = lector.IsDBNull(lector.GetOrdinal("IdPerfil")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPerfil")),
                        Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),

                    });
                }
            }
            return retList;
        }

        public PerfilConsultaDto ConsultaPerfil(PerfilConsultaDto objPerfil)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdPerfil", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPerfil.IdPerfil });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_PerfilConsulta", listaParams.ToArray());
                while (lector.Read())
                {
                    objPerfil.IdPerfil = lector.IsDBNull(lector.GetOrdinal("IdPerfil")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPerfil"));
                    objPerfil.Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion"));
                    objPerfil.Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre"));
                    objPerfil.IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado"));
                }
            }
            return objPerfil;
        }

        public int MantenimientoPerfil(Perfil objPerfilp)
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdPerfil", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPerfilp.IdPerfil });
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPerfilp.Nombre });
                listaParams.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPerfilp.Descripcion });
                listaParams.Add(new SqlParameter("@IdEstado", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPerfilp.IdEstado });
                listaParams.Add(new SqlParameter("@IdModulo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPerfilp.IdModulo });

                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_PerfilMantenimiento", listaParams.ToArray());
            }
            return r;
        }

        public string EliminarPerfil(string IdPerfil,int Accion)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdPerfil", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdPerfil });
                listaParams.Add(new SqlParameter("@Accion", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = Accion });
                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_PerfilEliminar", listaParams.ToArray()).ToString();
            }
            return r;
        }
    }
}

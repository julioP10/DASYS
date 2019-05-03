using Acces;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Datos
{
    public class PerfilOpcionDAL: IPerfilOpcion
    {
        public List<PerfilOpcionPaginationDto> PaginadoPerfilOpcion(PaginationParameter objPaginationParameter)
        {
            List<PerfilOpcionPaginationDto> retList = new List<PerfilOpcionPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_PerfilOpcionPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new PerfilOpcionPaginationDto
                    {
                        IdPerfil = lector.IsDBNull(lector.GetOrdinal("IdPerfil")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPerfil")),
                        Actualizar = lector.IsDBNull(lector.GetOrdinal("Actualizar")) ? default(string) : lector.GetString(lector.GetOrdinal("Actualizar")),
                        Consultar = lector.IsDBNull(lector.GetOrdinal("Consultar")) ? default(string) : lector.GetString(lector.GetOrdinal("Consultar")),
                        Ejecutar = lector.IsDBNull(lector.GetOrdinal("Ejecutar")) ? default(string) : lector.GetString(lector.GetOrdinal("Ejecutar")),
                        Eliminar = lector.IsDBNull(lector.GetOrdinal("Eliminar")) ? default(string) : lector.GetString(lector.GetOrdinal("Eliminar")),
                        IdOpcion = lector.IsDBNull(lector.GetOrdinal("IdOpcion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdOpcion")),
                        Imprimir = lector.IsDBNull(lector.GetOrdinal("Imprimir")) ? default(string) : lector.GetString(lector.GetOrdinal("Imprimir")),
                        Registrar = lector.IsDBNull(lector.GetOrdinal("Registrar")) ? default(string) : lector.GetString(lector.GetOrdinal("Registrar")),

                    });
                }
            }
            return retList;
        }

        public List<PerfilOpcionConsultaDto> ListadoPerfilOpcion(string IdPerfil,string IdOpcion)
        {
            List<PerfilOpcionConsultaDto> retList = new List<PerfilOpcionConsultaDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdOpcion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdOpcion });
                listaParams.Add(new SqlParameter("@IdPerfil", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdPerfil });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_PerfilOpcionListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new PerfilOpcionConsultaDto
                    {
                        IdPerfil = lector.IsDBNull(lector.GetOrdinal("IdPerfil")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPerfil")),
                        Actualizar = lector.IsDBNull(lector.GetOrdinal("Actualizar")) ? default(string) : lector.GetString(lector.GetOrdinal("Actualizar")),
                        Consultar = lector.IsDBNull(lector.GetOrdinal("Consultar")) ? default(string) : lector.GetString(lector.GetOrdinal("Consultar")),
                        Ejecutar = lector.IsDBNull(lector.GetOrdinal("Ejecutar")) ? default(string) : lector.GetString(lector.GetOrdinal("Ejecutar")),
                        Eliminar = lector.IsDBNull(lector.GetOrdinal("Eliminar")) ? default(string) : lector.GetString(lector.GetOrdinal("Eliminar")),
                        IdOpcion = lector.IsDBNull(lector.GetOrdinal("IdOpcion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdOpcion")),
                        Imprimir = lector.IsDBNull(lector.GetOrdinal("Imprimir")) ? default(string) : lector.GetString(lector.GetOrdinal("Imprimir")),
                        Registrar = lector.IsDBNull(lector.GetOrdinal("Registrar")) ? default(string) : lector.GetString(lector.GetOrdinal("Registrar")),

                    });
                }
            }
            return retList;
        }

        public PerfilOpcionConsultaDto ConsultaPerfilOpcion(PerfilOpcionConsultaDto objPerfilOpcion)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdOpcion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPerfilOpcion.IdOpcion });
                listaParams.Add(new SqlParameter("@IdPerfil", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPerfilOpcion.IdPerfil });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_PerfilOpcionConsulta", listaParams.ToArray());
                while (lector.Read())
                {
                    objPerfilOpcion.IdPerfil = lector.IsDBNull(lector.GetOrdinal("IdPerfil")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPerfil"));
                    objPerfilOpcion.Actualizar = lector.IsDBNull(lector.GetOrdinal("Actualizar")) ? default(string) : lector.GetString(lector.GetOrdinal("Actualizar"));
                    objPerfilOpcion.Consultar = lector.IsDBNull(lector.GetOrdinal("Consultar")) ? default(string) : lector.GetString(lector.GetOrdinal("Consultar"));
                    objPerfilOpcion.Ejecutar = lector.IsDBNull(lector.GetOrdinal("Ejecutar")) ? default(string) : lector.GetString(lector.GetOrdinal("Ejecutar"));
                    objPerfilOpcion.Eliminar = lector.IsDBNull(lector.GetOrdinal("Eliminar")) ? default(string) : lector.GetString(lector.GetOrdinal("Eliminar"));
                    objPerfilOpcion.IdOpcion = lector.IsDBNull(lector.GetOrdinal("IdOpcion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdOpcion"));
                    objPerfilOpcion.Imprimir = lector.IsDBNull(lector.GetOrdinal("Imprimir")) ? default(string) : lector.GetString(lector.GetOrdinal("Imprimir"));
                    objPerfilOpcion.Registrar = lector.IsDBNull(lector.GetOrdinal("Registrar")) ? default(string) : lector.GetString(lector.GetOrdinal("Registrar"));
                }
            }
            return objPerfilOpcion;
        }

        public int MantenimientoPerfilOpcion(PerfilOpcion objPerfilOpcion)
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdOpcion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPerfilOpcion.IdOpcion });
                listaParams.Add(new SqlParameter("@IdPerfil", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPerfilOpcion.IdPerfil });
                listaParams.Add(new SqlParameter("@Imprimir", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPerfilOpcion.Imprimir });
                listaParams.Add(new SqlParameter("@Registrar", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPerfilOpcion.Registrar });
                listaParams.Add(new SqlParameter("@Actualizar", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPerfilOpcion.Actualizar });
                listaParams.Add(new SqlParameter("@Consultar", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPerfilOpcion.Consultar });
                listaParams.Add(new SqlParameter("@Ejecutar", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPerfilOpcion.Ejecutar });
                listaParams.Add(new SqlParameter("@Eliminar", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPerfilOpcion.Eliminar });
                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_PerfilOpcionMantenimiento", listaParams.ToArray());
            }
            return r;
        }

        public int EliminarPerfilOpcion(string IdPerfil, string IdOpcion)
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdPerfil", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdPerfil });
                listaParams.Add(new SqlParameter("@IdOpcion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdOpcion });
                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_PerfilOpcionEliminar", listaParams.ToArray());
            }
            return r;
        }
    }
}

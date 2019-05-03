using Entidad;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acces;
namespace Datos
{
   public  class UsuarioOpcionDAL: IUsuarioOpcion
    {
        public List<UsuarioOpcionPaginationDto> PaginadoUsuarioOpcion(PaginationParameter objPaginationParameter)
        {
            List<UsuarioOpcionPaginationDto> retList = new List<UsuarioOpcionPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_UsuarioOpcionPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new UsuarioOpcionPaginationDto
                    {
                        IdUsuario = lector.IsDBNull(lector.GetOrdinal("Actualizar")) ? default(string) : lector.GetString(lector.GetOrdinal("IdUsuario")),
                        Actualizar = lector.IsDBNull(lector.GetOrdinal("Actualizar")) ? default(string) : lector.GetString(lector.GetOrdinal("Actualizar")),
                        Consultar = lector.IsDBNull(lector.GetOrdinal("Consultar")) ? default(string) : lector.GetString(lector.GetOrdinal("Consultar")),
                        Ejecutar = lector.IsDBNull(lector.GetOrdinal("Ejecutar")) ? default(string) : lector.GetString(lector.GetOrdinal("Ejecutar")),
                        Eliminar = lector.IsDBNull(lector.GetOrdinal("Eliminar")) ? default(string) : lector.GetString(lector.GetOrdinal("Eliminar")),
                        IdOpcion = lector.IsDBNull(lector.GetOrdinal("IdOpcion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdOpcion")),
                        Imprimir = lector.IsDBNull(lector.GetOrdinal("Imprimir")) ? default(string) : lector.GetString(lector.GetOrdinal("Imprimir")),
                        Registrar = lector.IsDBNull(lector.GetOrdinal("Registrar")) ? default(string) : lector.GetString(lector.GetOrdinal("Registrar")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado ")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado ")),
                        Opcion = lector.IsDBNull(lector.GetOrdinal("Opcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Opcion"))

                    });
                }
            }
            return retList;
        }

        public List<UsuarioOpcionConsultaDto> ListadoUsuarioOpcion(string IdUsuario, string IdOpcion)
        {
            List<UsuarioOpcionConsultaDto> retList = new List<UsuarioOpcionConsultaDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdUsuario", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdUsuario });
                listaParams.Add(new SqlParameter("@IdOpcion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdOpcion });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_UsuarioOpcionListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new UsuarioOpcionConsultaDto
                    {
                        IdUsuario = lector.IsDBNull(lector.GetOrdinal("Actualizar")) ? default(string) : lector.GetString(lector.GetOrdinal("IdUsuario")),
                        Actualizar = lector.IsDBNull(lector.GetOrdinal("Actualizar")) ? default(string) : lector.GetString(lector.GetOrdinal("Actualizar")),
                        Consultar = lector.IsDBNull(lector.GetOrdinal("Consultar")) ? default(string) : lector.GetString(lector.GetOrdinal("Consultar")),
                        Ejecutar = lector.IsDBNull(lector.GetOrdinal("Ejecutar")) ? default(string) : lector.GetString(lector.GetOrdinal("Ejecutar")),
                        Eliminar = lector.IsDBNull(lector.GetOrdinal("Eliminar")) ? default(string) : lector.GetString(lector.GetOrdinal("Eliminar")),
                        IdOpcion = lector.IsDBNull(lector.GetOrdinal("IdOpcion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdOpcion")),
                        Imprimir = lector.IsDBNull(lector.GetOrdinal("Imprimir")) ? default(string) : lector.GetString(lector.GetOrdinal("Imprimir")),
                        Registrar = lector.IsDBNull(lector.GetOrdinal("Registrar")) ? default(string) : lector.GetString(lector.GetOrdinal("Registrar")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado")),
                        Opcion = lector.IsDBNull(lector.GetOrdinal("Opcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Opcion"))

                });
                }
            }
            return retList;
        }

        public UsuarioOpcionConsultaDto ConsultaUsuarioOpcion(UsuarioOpcionConsultaDto objUsuarioOpcion)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdUsuario", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuarioOpcion.IdUsuario });
                listaParams.Add(new SqlParameter("@IdOpcion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuarioOpcion.IdOpcion });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_UsuarioOpcionConsulta", listaParams.ToArray());
                while (lector.Read())
                {
                    objUsuarioOpcion.IdUsuario = lector.IsDBNull(lector.GetOrdinal("Actualizar")) ? default(string) : lector.GetString(lector.GetOrdinal("IdUsuario"));
                    objUsuarioOpcion.Actualizar = lector.IsDBNull(lector.GetOrdinal("Actualizar")) ? default(string) : lector.GetString(lector.GetOrdinal("Actualizar"));
                    objUsuarioOpcion.Consultar = lector.IsDBNull(lector.GetOrdinal("Consultar")) ? default(string) : lector.GetString(lector.GetOrdinal("Consultar"));
                    objUsuarioOpcion.Ejecutar = lector.IsDBNull(lector.GetOrdinal("Ejecutar")) ? default(string) : lector.GetString(lector.GetOrdinal("Ejecutar"));
                    objUsuarioOpcion.    Eliminar = lector.IsDBNull(lector.GetOrdinal("Eliminar")) ? default(string) : lector.GetString(lector.GetOrdinal("Eliminar"));
                    objUsuarioOpcion.    IdOpcion = lector.IsDBNull(lector.GetOrdinal("IdOpcion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdOpcion"));
                    objUsuarioOpcion.Imprimir = lector.IsDBNull(lector.GetOrdinal("Imprimir")) ? default(string) : lector.GetString(lector.GetOrdinal("Imprimir"));
                    objUsuarioOpcion.Registrar = lector.IsDBNull(lector.GetOrdinal("Registrar")) ? default(string) : lector.GetString(lector.GetOrdinal("Registrar"));
                    objUsuarioOpcion.Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado"));
                    objUsuarioOpcion.IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado"));
                    objUsuarioOpcion.Opcion = lector.IsDBNull(lector.GetOrdinal("Opcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Opcion"));
                }
            }
            return objUsuarioOpcion;
        }

        public int MantenimientoUsuarioOpcion(UsuarioOpcion objUsuarioOpcion)
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdOpcion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuarioOpcion.IdOpcion });
                listaParams.Add(new SqlParameter("@IdUsuario", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuarioOpcion.IdUsuario });
                listaParams.Add(new SqlParameter("@Imprimir", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuarioOpcion.Imprimir });
                listaParams.Add(new SqlParameter("@Registrar", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuarioOpcion.Registrar });
                listaParams.Add(new SqlParameter("@Actualizar", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuarioOpcion.Actualizar });
                listaParams.Add(new SqlParameter("@Consultar", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuarioOpcion.Consultar });
                listaParams.Add(new SqlParameter("@Ejecutar", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuarioOpcion.Ejecutar });
                listaParams.Add(new SqlParameter("@Eliminar", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuarioOpcion.Eliminar });

                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_UsuarioOpcionMantenimiento", listaParams.ToArray());
            }
            return r;
        }

        public int EliminarUsuarioOpcion(string IdUsuario, string IdOpcion)
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdUsuario", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdUsuario });
                listaParams.Add(new SqlParameter("@IdOpcion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdOpcion });
                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_UsuarioOpcionEliminar", listaParams.ToArray());
            }
            return r;
        }
    }
}

using Interfaces;
using System;
using Entidad;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Acces;

namespace Datos
{
    public class UsuarioPerfilDAL: IUsuarioPerfil
    {
        public List<UsuarioPerfilPaginationDto> PaginadoUsuarioPerfil(PaginationParameter objPaginationParameter)
        {
            List<UsuarioPerfilPaginationDto> retList = new List<UsuarioPerfilPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_UsuarioPerfilPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new UsuarioPerfilPaginationDto
                    {
                        IdUsuario = lector.IsDBNull(lector.GetOrdinal("IdUsuario")) ? default(string) : lector.GetString(lector.GetOrdinal("IdUsuario")),
                        IdPerfil = lector.IsDBNull(lector.GetOrdinal("IdPerfil")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPerfil")),
                        Perfil = lector.IsDBNull(lector.GetOrdinal("Perfil")) ? default(string) : lector.GetString(lector.GetOrdinal("Perfil")),
                        Usuario = lector.IsDBNull(lector.GetOrdinal("Usuario")) ? default(string) : lector.GetString(lector.GetOrdinal("Usuario")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),

                    });
                }
            }
            return retList;
        }

        public List<UsuarioPerfilConsultaDto> ListadoUsuarioPerfil(string IdUsuario, string IdPerfil)
        {
            List<UsuarioPerfilConsultaDto> retList = new List<UsuarioPerfilConsultaDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdUsuario", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdUsuario });
                listaParams.Add(new SqlParameter("@IdPerfil", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdPerfil });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_UsuarioPerfilListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new UsuarioPerfilConsultaDto
                    {
                        IdUsuario = lector.IsDBNull(lector.GetOrdinal("IdUsuario")) ? default(string) : lector.GetString(lector.GetOrdinal("IdUsuario")),
                        IdPerfil = lector.IsDBNull(lector.GetOrdinal("IdPerfil")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPerfil")),
                        Perfil = lector.IsDBNull(lector.GetOrdinal("Perfil")) ? default(string) : lector.GetString(lector.GetOrdinal("Perfil")),
                        Usuario = lector.IsDBNull(lector.GetOrdinal("Usuario")) ? default(string) : lector.GetString(lector.GetOrdinal("Usuario")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado")),
                    });
                }
            }
            return retList;
        }

        public UsuarioPerfilConsultaDto ConsultaUsuarioPerfil(UsuarioPerfilConsultaDto objUsuarioPerfil)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdUsuario", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuarioPerfil.IdUsuario });
                listaParams.Add(new SqlParameter("@IdPerfil", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuarioPerfil.IdPerfil });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_UsuarioPerfilConsulta", listaParams.ToArray());
                while (lector.Read())
                {
                    objUsuarioPerfil.IdUsuario = lector.IsDBNull(lector.GetOrdinal("IdUsuario")) ? default(string) : lector.GetString(lector.GetOrdinal("IdUsuario"));
                    objUsuarioPerfil.IdPerfil = lector.IsDBNull(lector.GetOrdinal("IdPerfil")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPerfil"));
                    objUsuarioPerfil.Perfil = lector.IsDBNull(lector.GetOrdinal("Perfil")) ? default(string) : lector.GetString(lector.GetOrdinal("Perfil"));
                    objUsuarioPerfil.Usuario = lector.IsDBNull(lector.GetOrdinal("Usuario")) ? default(string) : lector.GetString(lector.GetOrdinal("Usuario"));
                    objUsuarioPerfil.Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado"));
                    objUsuarioPerfil.IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado"));
                }
            }
            return objUsuarioPerfil;
        }

        public int MantenimientoUsuarioPerfil(UsuarioPerfil objUsuarioPerfilp)
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdUsuario", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuarioPerfilp.IdUsuario });
                listaParams.Add(new SqlParameter("@IdPerfil", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuarioPerfilp.IdPerfil });

                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_UsuarioPerfilMantenimiento", listaParams.ToArray());
            }
            return r;
        }

        public int EliminarUsuarioPerfil(string IdUsuario,string IdPerfil)
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdUsuario", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdUsuario });
                listaParams.Add(new SqlParameter("@IdPerfil", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdPerfil });
                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_UsuarioPerfilEliminar", listaParams.ToArray());
            }
            return r;
        }
    }
}

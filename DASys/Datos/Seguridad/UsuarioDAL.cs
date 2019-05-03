using Acces;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Datos
{
    public class UsuarioDAL: IUsuario
    {
        public List<UsuarioPaginationDto> PaginadoUsuario(PaginationParameter objPaginationParameter)
        {
            List<UsuarioPaginationDto> retList = new List<UsuarioPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_UsuarioPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new UsuarioPaginationDto
                    {
                        IdUsuario = lector.IsDBNull(lector.GetOrdinal("IdUsuario")) ? default(string) : lector.GetString(lector.GetOrdinal("IdUsuario")),
                        Perfil = lector.IsDBNull(lector.GetOrdinal("Perfil")) ? default(string) : lector.GetString(lector.GetOrdinal("Perfil")),
                        IdPerfil = lector.IsDBNull(lector.GetOrdinal("IdPerfil")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPerfil")),
                        Correo = lector.IsDBNull(lector.GetOrdinal("Correo")) ? default(string) : lector.GetString(lector.GetOrdinal("Correo")),
                        IdPersona = lector.IsDBNull(lector.GetOrdinal("IdPersona")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPersona")),
                        Usuarios = lector.IsDBNull(lector.GetOrdinal("Usuario")) ? default(string) : lector.GetString(lector.GetOrdinal("Usuario")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        Empresa = lector.IsDBNull(lector.GetOrdinal("Empresa")) ? default(string) : lector.GetString(lector.GetOrdinal("Empresa")),
                    });
                }
            }
            return retList;
        }

        public List<UsuarioConsultaDto> ListadoUsuario(UsuarioActualDto objUsuarioActualDto)
        {
            List<UsuarioConsultaDto> retList = new List<UsuarioConsultaDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuarioActualDto.DescripcionAdcional });
                listaParams.Add(new SqlParameter("@IdEmpresaPadre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuarioActualDto.IdEmpresa });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_UsuarioListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new UsuarioConsultaDto
                    {
                        IdUsuario = lector.IsDBNull(lector.GetOrdinal("IdUsuario")) ? default(string) : lector.GetString(lector.GetOrdinal("IdUsuario")),
                        IdEmpresaPadre = lector.IsDBNull(lector.GetOrdinal("IdEmpresaPadre")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresaPadre")),
                        Correo = lector.IsDBNull(lector.GetOrdinal("Correo")) ? default(string) : lector.GetString(lector.GetOrdinal("Correo")),
                        IdPersona = lector.IsDBNull(lector.GetOrdinal("IdPersona")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPersona")),
                        TipoEmpresa = lector.IsDBNull(lector.GetOrdinal("TipoEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("TipoEmpresa")),
                        IdEmpresa = lector.IsDBNull(lector.GetOrdinal("IdEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresa")),
                        Usuarios = lector.IsDBNull(lector.GetOrdinal("Usuario")) ? default(string) : lector.GetString(lector.GetOrdinal("Usuario")),
                        IdPerfil = lector.IsDBNull(lector.GetOrdinal("IdPerfil")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPerfil")),
                        Perfil = lector.IsDBNull(lector.GetOrdinal("Perfil")) ? default(string) : lector.GetString(lector.GetOrdinal("Perfil")),
                        IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado")),
                });
                }
            }
            return retList;
        }

        public UsuarioConsultaDto ConsultaUsuario(UsuarioConsultaDto objUsuario)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdUsuario", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuario.IdUsuario });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_UsuarioConsulta", listaParams.ToArray());
                while (lector.Read())
                {
                    objUsuario.IdUsuario = lector.IsDBNull(lector.GetOrdinal("IdUsuario")) ? default(string) : lector.GetString(lector.GetOrdinal("IdUsuario"));
                    objUsuario.Correo = lector.IsDBNull(lector.GetOrdinal("Correo")) ? default(string) : lector.GetString(lector.GetOrdinal("Correo"));
                    objUsuario.IdPersona = lector.IsDBNull(lector.GetOrdinal("IdPersona")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPersona"));
                    objUsuario.IdPerfil = lector.IsDBNull(lector.GetOrdinal("IdPerfil")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPerfil"));
                    objUsuario.Perfil = lector.IsDBNull(lector.GetOrdinal("Perfil")) ? default(string) : lector.GetString(lector.GetOrdinal("Perfil"));
                    objUsuario.Usuarios = lector.IsDBNull(lector.GetOrdinal("Usuario")) ? default(string) : lector.GetString(lector.GetOrdinal("Usuario"));
                    objUsuario.Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado"));
                    objUsuario.IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado"));
                    objUsuario.IdEmpresa = lector.IsDBNull(lector.GetOrdinal("IdEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresa"));
                    objUsuario.Tipo = lector.IsDBNull(lector.GetOrdinal("Tipo")) ? default(string) : lector.GetString(lector.GetOrdinal("Tipo"));
                }
            }
            return objUsuario;
        }
        public UsuarioConsultaDto UsuarioLogin(UsuarioConsultaDto objUsuario)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@Usuario", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuario.Usuarios });
                listaParams.Add(new SqlParameter("@Password", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuario.Password });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_UsuarioLogin", listaParams.ToArray());
                while (lector.Read())
                {
                    objUsuario.IdUsuario = lector.IsDBNull(lector.GetOrdinal("IdUsuario")) ? default(string) : lector.GetString(lector.GetOrdinal("IdUsuario"));
                    objUsuario.IdEmpresaPadre = lector.IsDBNull(lector.GetOrdinal("IdEmpresaPadre")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresaPadre"));
                    objUsuario.Correo = lector.IsDBNull(lector.GetOrdinal("Correo")) ? default(string) : lector.GetString(lector.GetOrdinal("Correo"));
                    objUsuario.IdPersona = lector.IsDBNull(lector.GetOrdinal("IdPersona")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPersona"));
                    objUsuario.TipoEmpresa = lector.IsDBNull(lector.GetOrdinal("TipoEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("TipoEmpresa"));
                    objUsuario.IdEmpresa = lector.IsDBNull(lector.GetOrdinal("IdEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresa"));
                    objUsuario.Usuarios = lector.IsDBNull(lector.GetOrdinal("Usuario")) ? default(string) : lector.GetString(lector.GetOrdinal("Usuario"));
                    objUsuario.IdPerfil = lector.IsDBNull(lector.GetOrdinal("IdPerfil")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPerfil"));
                    objUsuario.Perfil = lector.IsDBNull(lector.GetOrdinal("Perfil")) ? default(string) : lector.GetString(lector.GetOrdinal("Perfil"));
                    objUsuario.IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado"));
                    objUsuario.EmpresaNombre = lector.IsDBNull(lector.GetOrdinal("EmpresaNombre")) ? default(string) : lector.GetString(lector.GetOrdinal("EmpresaNombre"));

                }
            }
            return objUsuario;
        }
        public UsuarioConsultaDto UsuarioRecuperar(UsuarioConsultaDto objUsuario)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@Usuario", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuario.Usuarios });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_UsuarioRecuperar", listaParams.ToArray());
                while (lector.Read())
                {
                    objUsuario.IdUsuario = lector.IsDBNull(lector.GetOrdinal("IdUsuario")) ? default(string) : lector.GetString(lector.GetOrdinal("IdUsuario"));
                    objUsuario.IdEmpresaPadre = lector.IsDBNull(lector.GetOrdinal("IdEmpresaPadre")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresaPadre"));
                    objUsuario.Correo = lector.IsDBNull(lector.GetOrdinal("Correo")) ? default(string) : lector.GetString(lector.GetOrdinal("Correo"));
                    objUsuario.IdPersona = lector.IsDBNull(lector.GetOrdinal("IdPersona")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPersona"));
                    objUsuario.TipoEmpresa = lector.IsDBNull(lector.GetOrdinal("TipoEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("TipoEmpresa"));
                    objUsuario.IdEmpresa = lector.IsDBNull(lector.GetOrdinal("IdEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresa"));
                    objUsuario.Usuarios = lector.IsDBNull(lector.GetOrdinal("Usuario")) ? default(string) : lector.GetString(lector.GetOrdinal("Usuario"));
                    objUsuario.IdPerfil = lector.IsDBNull(lector.GetOrdinal("IdPerfil")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPerfil"));
                    objUsuario.Perfil = lector.IsDBNull(lector.GetOrdinal("Perfil")) ? default(string) : lector.GetString(lector.GetOrdinal("Perfil"));
                    objUsuario.IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado"));
                    objUsuario.EmpresaNombre = lector.IsDBNull(lector.GetOrdinal("EmpresaNombre")) ? default(string) : lector.GetString(lector.GetOrdinal("EmpresaNombre"));
                    objUsuario.Tipo = lector.IsDBNull(lector.GetOrdinal("Tipo")) ? default(string) : lector.GetString(lector.GetOrdinal("Tipo"));
                    objUsuario.Password = lector.IsDBNull(lector.GetOrdinal("Password")) ? default(string) : lector.GetString(lector.GetOrdinal("Password"));


                }
            }
            return objUsuario;
        }
        public string MantenimientoUsuario(Usuario objUsuariop)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdUsuario", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuariop.IdUsuario });
                listaParams.Add(new SqlParameter("@IdPerfil", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuariop.IdPerfil });
                listaParams.Add(new SqlParameter("@IdPersona", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuariop.IdPersona });
                listaParams.Add(new SqlParameter("@Password", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuariop.Password });
                listaParams.Add(new SqlParameter("@Usuario", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuariop.Usuarios});
                listaParams.Add(new SqlParameter("@Correo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuariop.Correo });
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuariop.IdEmpresa });
                listaParams.Add(new SqlParameter("@IdEstado", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuariop.IdEstado });
                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_UsuarioMantenimiento", listaParams.ToArray()).ToString();
            }
            return r;
        }
        public int PermisoUsuario(Usuario objUsuariop)
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdUsuario", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuariop.IdUsuario });
                listaParams.Add(new SqlParameter("@IdPerfil", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuariop.IdPerfil });
                listaParams.Add(new SqlParameter("@IdOpcion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuariop.IdOpcion });
                listaParams.Add(new SqlParameter("@Check", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objUsuariop.Check });
                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_UsuarioPermiso", listaParams.ToArray());
            }
            return r;
        }

        public string EliminarUsuario(string IdUsuario,int Accion)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdUsuario", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdUsuario });
                listaParams.Add(new SqlParameter("@Accion", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = Accion });

                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_UsuarioEliminar", listaParams.ToArray()).ToString();
            }
            return r;
        }
    }
}

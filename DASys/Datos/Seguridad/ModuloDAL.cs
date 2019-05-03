using Acces;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Datos
{
    public class ModuloDAL: IModulo
    {
        public List<ModuloPaginationDto> PaginadoModulo(PaginationParameter objPaginationParameter)
        {
            List<ModuloPaginationDto> retList = new List<ModuloPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ModuloPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new ModuloPaginationDto
                    {
                        IdModulo = lector.IsDBNull(lector.GetOrdinal("IdModulo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdModulo")),
                        Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        Posicion = lector.IsDBNull(lector.GetOrdinal("Posicion")) ? 0 : lector.GetInt32(lector.GetOrdinal("Posicion")),
                        Icono = lector.IsDBNull(lector.GetOrdinal("Icono")) ? default(string) : lector.GetString(lector.GetOrdinal("Icono")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad")),
                    });
                }
            }
            return retList;
        }

        public List<ModuloConsultaDto> ListadoModulo(UsuarioPermisoDto objUsuarioPermisoDto)
        {
            List<ModuloConsultaDto> retList = new List<ModuloConsultaDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuarioPermisoDto.IdPerfil });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ModuloListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new ModuloConsultaDto
                    {
                        IdModulo = lector.IsDBNull(lector.GetOrdinal("IdModulo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdModulo")),
                        Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado")),
                        Icono = lector.IsDBNull(lector.GetOrdinal("Icono")) ? default(string) : lector.GetString(lector.GetOrdinal("Icono")),

                    });
                }
            }
            return retList;
        }
        public List<ModuloConsultaDto> ListadoModuloXPerfil(UsuarioPermisoDto objUsuarioPermisoDto)
        {
            List<ModuloConsultaDto> retList = new List<ModuloConsultaDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdPerfil", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuarioPermisoDto.IdPerfil });
                listaParams.Add(new SqlParameter("@IdUsuario", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuarioPermisoDto.IdUsuario });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ModuloXPerfilListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new ModuloConsultaDto
                    {
                        IdModulo = lector.IsDBNull(lector.GetOrdinal("IdModulo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdModulo")),
                        Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado")),
                        Icono = lector.IsDBNull(lector.GetOrdinal("Icono")) ? default(string) : lector.GetString(lector.GetOrdinal("Icono")),

                    });
                }
            }
            return retList;
        }

        public ModuloConsultaDto ConsultaModulo(ModuloConsultaDto objModulo)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdModulo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objModulo.IdModulo });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ModuloConsulta", listaParams.ToArray());
                while (lector.Read())
                {
                    objModulo.IdModulo = lector.IsDBNull(lector.GetOrdinal("IdModulo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdModulo"));
                    objModulo.Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion"));
                    objModulo.Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre"));
                    objModulo.IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado"));
                    objModulo.Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado"));
                    objModulo.Posicion = lector.IsDBNull(lector.GetOrdinal("Posicion")) ? 0 : lector.GetInt32(lector.GetOrdinal("Posicion"));
                    objModulo.Icono = lector.IsDBNull(lector.GetOrdinal("Icono")) ? default(string) : lector.GetString(lector.GetOrdinal("Icono"));
                }
            }
            return objModulo;
        }

        public int MantenimientoModulo(Modulo objModulop)
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdModulo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objModulop.IdModulo });
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objModulop.Nombre });
                listaParams.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objModulop.Descripcion });
                listaParams.Add(new SqlParameter("@Posicion", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objModulop.Posicion });
                listaParams.Add(new SqlParameter("@Icono", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objModulop.Icono });
                listaParams.Add(new SqlParameter("@IdEstado", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objModulop.IdEstado });

                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_ModuloMantenimiento", listaParams.ToArray());
            }
            return r;
        }

        public string EliminarModulo(string IdModulo, int Accion)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdModulo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdModulo });
                listaParams.Add(new SqlParameter("@Accion", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = Accion });

                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_ModuloEliminar", listaParams.ToArray()).ToString();
            }
            return r;
        }
    }
}

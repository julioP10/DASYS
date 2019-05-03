using Acces;
using Entidad;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class OpcionDAL: IOpcion
    {
        public List<OpcionPaginationDto> PaginadoOpcion(PaginationParameter objPaginationParameter)
        {
            List<OpcionPaginationDto> retList = new List<OpcionPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_OpcionPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new OpcionPaginationDto
                    {
                        IdOpcion = lector.IsDBNull(lector.GetOrdinal("IdOpcion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdOpcion")),
                        Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Area = lector.IsDBNull(lector.GetOrdinal("Area")) ? default(string) : lector.GetString(lector.GetOrdinal("Area")),
                        Controlador = lector.IsDBNull(lector.GetOrdinal("Controlador")) ? default(string) : lector.GetString(lector.GetOrdinal("Controlador")),
                        Accion = lector.IsDBNull(lector.GetOrdinal("Accion")) ? default(string) : lector.GetString(lector.GetOrdinal("Accion")),
                        Posicion = lector.IsDBNull(lector.GetOrdinal("Posicion")) ? 0 : lector.GetInt32(lector.GetOrdinal("Posicion")),
                        Icono = lector.IsDBNull(lector.GetOrdinal("Icono")) ? default(string) : lector.GetString(lector.GetOrdinal("Icono")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad")),

                    });
                }
            }
            return retList;
        }

        public List<OpcionConsultaDto> ListadoOpcionXPerfil(UsuarioPermisoDto objUsuarioPermisoDto)
        {
            List<OpcionConsultaDto> retList = new List<OpcionConsultaDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdPerfil", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuarioPermisoDto.IdPerfil });
                listaParams.Add(new SqlParameter("@IdUsuario", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuarioPermisoDto.IdUsuario });
                listaParams.Add(new SqlParameter("@Ver", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objUsuarioPermisoDto.Ver });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_OpcionXPerfilListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new OpcionConsultaDto
                    {
                        IdOpcion = lector.IsDBNull(lector.GetOrdinal("IdOpcion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdOpcion")),
                        Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado")),
                        IdModulo = lector.IsDBNull(lector.GetOrdinal("IdModulo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdModulo")),
                        Controlador = lector.IsDBNull(lector.GetOrdinal("Controlador")) ? default(string) : lector.GetString(lector.GetOrdinal("Controlador")),
                        Accion = lector.IsDBNull(lector.GetOrdinal("Accion")) ? default(string) : lector.GetString(lector.GetOrdinal("Accion")),
                        Area = lector.IsDBNull(lector.GetOrdinal("Area")) ? default(string) : lector.GetString(lector.GetOrdinal("Area")),
                        EsInicio = lector.IsDBNull(lector.GetOrdinal("EsInicio")) ? false: lector.GetBoolean(lector.GetOrdinal("EsInicio")),
                        IdPadre = lector.IsDBNull(lector.GetOrdinal("IdPadre")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPadre")),
                        Icono = lector.IsDBNull(lector.GetOrdinal("Icono")) ? default(string) : lector.GetString(lector.GetOrdinal("Icono")),
                        SubMenu = lector.IsDBNull(lector.GetOrdinal("SubMenu")) ? 0 : lector.GetInt32(lector.GetOrdinal("SubMenu")),
                        Permitido = lector.IsDBNull(lector.GetOrdinal("Permitido")) ? 0 : lector.GetInt32(lector.GetOrdinal("Permitido")),
                    });
                }
            }
            return retList;
        }
        public List<OpcionConsultaDto> ListadoOpcion(UsuarioPermisoDto objUsuarioPermisoDto)
        {
            List<OpcionConsultaDto> retList = new List<OpcionConsultaDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuarioPermisoDto.IdPerfil });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_OpcionListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new OpcionConsultaDto
                    {
                        IdOpcion = lector.IsDBNull(lector.GetOrdinal("IdOpcion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdOpcion")),
                        Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado")),
                        IdModulo = lector.IsDBNull(lector.GetOrdinal("IdModulo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdModulo")),
                        Controlador = lector.IsDBNull(lector.GetOrdinal("Controlador")) ? default(string) : lector.GetString(lector.GetOrdinal("Controlador")),
                        Accion = lector.IsDBNull(lector.GetOrdinal("Accion")) ? default(string) : lector.GetString(lector.GetOrdinal("Accion")),
                        Area = lector.IsDBNull(lector.GetOrdinal("Area")) ? default(string) : lector.GetString(lector.GetOrdinal("Area")),
                        EsInicio = lector.IsDBNull(lector.GetOrdinal("EsInicio")) ? false : lector.GetBoolean(lector.GetOrdinal("EsInicio")),
                        IdPadre = lector.IsDBNull(lector.GetOrdinal("IdPadre")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPadre")),
                        Icono = lector.IsDBNull(lector.GetOrdinal("Icono")) ? default(string) : lector.GetString(lector.GetOrdinal("Icono")),
                        SubMenu = lector.IsDBNull(lector.GetOrdinal("SubMenu")) ? 0 : lector.GetInt32(lector.GetOrdinal("SubMenu")),

                    });
                }
            }
            return retList;
        }
        public List<OpcionConsultaDto> ListadoOpcionHijo(UsuarioPermisoDto objUsuarioPermisoDto)
        {
            List<OpcionConsultaDto> retList = new List<OpcionConsultaDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuarioPermisoDto.IdPerfil });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_OpcionListadoHijo", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new OpcionConsultaDto
                    {
                        IdOpcion = lector.IsDBNull(lector.GetOrdinal("IdOpcion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdOpcion")),
                        Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado")),
                        IdModulo = lector.IsDBNull(lector.GetOrdinal("IdModulo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdModulo")),
                        Controlador = lector.IsDBNull(lector.GetOrdinal("Controlador")) ? default(string) : lector.GetString(lector.GetOrdinal("Controlador")),
                        Accion = lector.IsDBNull(lector.GetOrdinal("Accion")) ? default(string) : lector.GetString(lector.GetOrdinal("Accion")),
                        Area = lector.IsDBNull(lector.GetOrdinal("Area")) ? default(string) : lector.GetString(lector.GetOrdinal("Area")),
                        EsInicio = lector.IsDBNull(lector.GetOrdinal("EsInicio")) ? false : lector.GetBoolean(lector.GetOrdinal("EsInicio")),
                        IdPadre = lector.IsDBNull(lector.GetOrdinal("IdPadre")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPadre")),
                        Icono = lector.IsDBNull(lector.GetOrdinal("Icono")) ? default(string) : lector.GetString(lector.GetOrdinal("Icono")),

                    });
                }
            }
            return retList;
        }

        public OpcionConsultaDto ConsultaOpcion(OpcionConsultaDto objOpcion)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdOpcion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objOpcion.IdOpcion });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_OpcionConsulta", listaParams.ToArray());
                while (lector.Read())
                {
                    objOpcion.IdOpcion = lector.IsDBNull(lector.GetOrdinal("IdOpcion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdOpcion"));
                    objOpcion.IdModulo = lector.IsDBNull(lector.GetOrdinal("IdModulo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdModulo"));
                    objOpcion.Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion"));
                    objOpcion.Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre"));
                    objOpcion.Area = lector.IsDBNull(lector.GetOrdinal("Area")) ? default(string) : lector.GetString(lector.GetOrdinal("Area"));
                    objOpcion.Controlador = lector.IsDBNull(lector.GetOrdinal("Controlador")) ? default(string) : lector.GetString(lector.GetOrdinal("Controlador"));
                    objOpcion.Accion = lector.IsDBNull(lector.GetOrdinal("Accion")) ? default(string) : lector.GetString(lector.GetOrdinal("Accion"));
                    objOpcion.Posicion = lector.IsDBNull(lector.GetOrdinal("Posicion")) ? 0 : lector.GetInt32(lector.GetOrdinal("Posicion"));
                    objOpcion.Icono = lector.IsDBNull(lector.GetOrdinal("Icono")) ? default(string) : lector.GetString(lector.GetOrdinal("Icono"));
                    objOpcion.IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado"));
                    objOpcion.Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado"));
                    objOpcion.EsInicio = lector.IsDBNull(lector.GetOrdinal("EsInicio")) ?false : lector.GetBoolean(lector.GetOrdinal("EsInicio"));

                }
            }
            return objOpcion;
        }

        public int MantenimientoOpcion(Opcion objOpcionp)
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdOpcion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objOpcionp.IdOpcion });
                listaParams.Add(new SqlParameter("@IdModulo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objOpcionp.IdModulo });
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objOpcionp.Nombre });
                listaParams.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objOpcionp.Descripcion });
                listaParams.Add(new SqlParameter("@Area", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objOpcionp.Area });
                listaParams.Add(new SqlParameter("@Controlador", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objOpcionp.Controlador });
                listaParams.Add(new SqlParameter("@Accion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objOpcionp.Accion });
                listaParams.Add(new SqlParameter("@Posicion", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objOpcionp.Posicion });
                listaParams.Add(new SqlParameter("@Icono", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objOpcionp.Icono });
                listaParams.Add(new SqlParameter("@IdEstado", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objOpcionp.IdEstado });
                listaParams.Add(new SqlParameter("@EsInicio", SqlDbType.Bit) { Direction = ParameterDirection.Input, Value = objOpcionp.EsInicio });
                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_OpcionMantenimiento", listaParams.ToArray());
            }
            return r;
        }

        public string EliminarOpcion(string IdOpcion,int Accion)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdOpcion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdOpcion });
                listaParams.Add(new SqlParameter("@Accion", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = Accion });
                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_OpcionEliminar", listaParams.ToArray()).ToString();
            }
            return r;
        }
    }
}

using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Acces;
namespace Datos
{
    public class TerminalDAL: ITerminal
    {
        public List<TerminalPaginationDto> PaginadoTerminal(PaginationParameter objPaginationParameter)
        {
            List<TerminalPaginationDto> retList = new List<TerminalPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_TerminalPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new TerminalPaginationDto
                    {
                        IdTerminal = lector.IsDBNull(lector.GetOrdinal("IdTerminal")) ? default(string) : lector.GetString(lector.GetOrdinal("IdTerminal")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        Configuracion = lector.IsDBNull(lector.GetOrdinal("Configuracion")) ? default(string) : lector.GetString(lector.GetOrdinal("Configuracion")),
                        Marca = lector.IsDBNull(lector.GetOrdinal("Marca")) ? default(string) : lector.GetString(lector.GetOrdinal("Marca")),
                        IP = lector.IsDBNull(lector.GetOrdinal("IP")) ? default(string) : lector.GetString(lector.GetOrdinal("IP")),
                        Puerto = lector.IsDBNull(lector.GetOrdinal("Puerto")) ? default(string) : lector.GetString(lector.GetOrdinal("Puerto")),
                        Area = lector.IsDBNull(lector.GetOrdinal("Area")) ? default(string) : lector.GetString(lector.GetOrdinal("Area")),
                        Modelo = lector.IsDBNull(lector.GetOrdinal("Modelo")) ? default(string) : lector.GetString(lector.GetOrdinal("Modelo")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad")),

                    });
                }
            }
            return retList;
        }

        public List<TerminalConsultaDto> ListadoTerminal(string Terminal)
        {
            List<TerminalConsultaDto> retList = new List<TerminalConsultaDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Terminal });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_TerminalListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new TerminalConsultaDto
                    {
                        IdTerminal = lector.IsDBNull(lector.GetOrdinal("IdTerminal")) ? default(string) : lector.GetString(lector.GetOrdinal("IdTerminal")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        IdEstado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        Configuracion = lector.IsDBNull(lector.GetOrdinal("Configuracion")) ? default(string) : lector.GetString(lector.GetOrdinal("Configuracion")),
                        IdConfiguracion = lector.IsDBNull(lector.GetOrdinal("IdConfiguracion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdConfiguracion")),
                        Marca = lector.IsDBNull(lector.GetOrdinal("Marca")) ? default(string) : lector.GetString(lector.GetOrdinal("Marca")),
                        IdMarca = lector.IsDBNull(lector.GetOrdinal("IdMarca")) ? default(string) : lector.GetString(lector.GetOrdinal("IdMarca")),
                        IP = lector.IsDBNull(lector.GetOrdinal("IP")) ? default(string) : lector.GetString(lector.GetOrdinal("IP")),
                        Puerto = lector.IsDBNull(lector.GetOrdinal("Puerto")) ? default(string) : lector.GetString(lector.GetOrdinal("Puerto")),
                        Area = lector.IsDBNull(lector.GetOrdinal("Area")) ? default(string) : lector.GetString(lector.GetOrdinal("Area")),
                        IdArea = lector.IsDBNull(lector.GetOrdinal("IdArea")) ? default(string) : lector.GetString(lector.GetOrdinal("IdArea")),

                    });
                }
            }
            return retList;
        }

        public TerminalConsultaDto ConsultaTerminal(TerminalConsultaDto objTerminal)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdTerminal", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objTerminal.IdTerminal });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_TerminalConsulta", listaParams.ToArray());
                while (lector.Read())
                {
                    objTerminal.IdTerminal = lector.IsDBNull(lector.GetOrdinal("IdTerminal")) ? default(string) : lector.GetString(lector.GetOrdinal("IdTerminal"));
                    objTerminal.Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre"));
                    objTerminal.IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado"));
                    objTerminal.Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado"));
                    objTerminal.Configuracion = lector.IsDBNull(lector.GetOrdinal("Configuracion")) ? default(string) : lector.GetString(lector.GetOrdinal("Configuracion"));
                    objTerminal.IdConfiguracion = lector.IsDBNull(lector.GetOrdinal("IdConfiguracion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdConfiguracion"));
                    objTerminal.Marca = lector.IsDBNull(lector.GetOrdinal("Marca")) ? default(string) : lector.GetString(lector.GetOrdinal("Marca"));
                    objTerminal.IdMarca = lector.IsDBNull(lector.GetOrdinal("IdMarca")) ? default(string) : lector.GetString(lector.GetOrdinal("IdMarca"));
                    objTerminal.IP = lector.IsDBNull(lector.GetOrdinal("IP")) ? default(string) : lector.GetString(lector.GetOrdinal("IP"));
                    objTerminal.Puerto = lector.IsDBNull(lector.GetOrdinal("Puerto")) ? default(string) : lector.GetString(lector.GetOrdinal("Puerto"));
                    objTerminal.Area = lector.IsDBNull(lector.GetOrdinal("Area")) ? default(string) : lector.GetString(lector.GetOrdinal("Area"));
                    objTerminal.IdArea = lector.IsDBNull(lector.GetOrdinal("IdArea")) ? default(string) : lector.GetString(lector.GetOrdinal("IdArea"));
                    objTerminal.Modelo = lector.IsDBNull(lector.GetOrdinal("Modelo")) ? default(string) : lector.GetString(lector.GetOrdinal("Modelo"));
                    objTerminal.IdModelo = lector.IsDBNull(lector.GetOrdinal("IdModelo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdModelo"));
                    objTerminal.IdEmpresa = lector.IsDBNull(lector.GetOrdinal("IdEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresa"));
                }
            }
            return objTerminal;
        }

        public int MantenimientoTerminal(Terminal objTerminalp)
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdTerminal", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objTerminalp.IdTerminal });
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objTerminalp.Nombre });
                listaParams.Add(new SqlParameter("@IdMarca", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objTerminalp.IdMarca });
                listaParams.Add(new SqlParameter("@IdModelo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objTerminalp.IdModelo });
                listaParams.Add(new SqlParameter("@IdConfiguracion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objTerminalp.IdConfiguracion });
                listaParams.Add(new SqlParameter("@IP", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objTerminalp.IP });
                listaParams.Add(new SqlParameter("@Puerto", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objTerminalp.Puerto });
                listaParams.Add(new SqlParameter("@IdArea", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objTerminalp.IdArea });
                listaParams.Add(new SqlParameter("@IdEstado", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objTerminalp.IdEstado });
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objTerminalp.IdEmpresa });
                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_TerminalMantenimiento", listaParams.ToArray());
            }
            return r;
        }

        public string EliminarTerminal(string IdTerminal, int Accion )
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdTerminal", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdTerminal });
                listaParams.Add(new SqlParameter("@Accion", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = Accion });
                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_TerminalEliminar", listaParams.ToArray()).ToString();
            }
            return r;
        }
    }
}

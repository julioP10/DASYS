using Acces;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Datos
{
    public class ModeloDAL: IModelo
    {
        public List<ModeloPaginationDto> PaginadoModelo(PaginationParameter objPaginationParameter)
        {
            List<ModeloPaginationDto> retList = new List<ModeloPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ModeloPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new ModeloPaginationDto
                    {
                        IdModelo = lector.IsDBNull(lector.GetOrdinal("IdModelo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdModelo")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        Configuracion= lector.IsDBNull(lector.GetOrdinal("Configuracion")) ? default(string) : lector.GetString(lector.GetOrdinal("Configuracion")),
                        Marca = lector.IsDBNull(lector.GetOrdinal("Marca")) ? default(string) : lector.GetString(lector.GetOrdinal("Marca")),
                        SDK = lector.IsDBNull(lector.GetOrdinal("SDK")) ?0 : lector.GetInt32(lector.GetOrdinal("SDK")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad")),

                    });
                }
            }
            return retList;
        }

        public List<ModeloConsultaDto> ListadoModelo(string Modelo)
        {
            List<ModeloConsultaDto> retList = new List<ModeloConsultaDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Modelo });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ModeloListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new ModeloConsultaDto
                    {
                        IdModelo = lector.IsDBNull(lector.GetOrdinal("IdModelo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdModelo")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado")),
                        Configuracion = lector.IsDBNull(lector.GetOrdinal("Configuracion")) ? default(string) : lector.GetString(lector.GetOrdinal("Configuracion")),
                        IdConfiguracion = lector.IsDBNull(lector.GetOrdinal("IdConfiguracion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdConfiguracion")),
                        Marca = lector.IsDBNull(lector.GetOrdinal("Marca")) ? default(string) : lector.GetString(lector.GetOrdinal("Marca")),
                        IdMarca = lector.IsDBNull(lector.GetOrdinal("IdMarca")) ? default(string) : lector.GetString(lector.GetOrdinal("IdMarca")),
                        SDK = lector.IsDBNull(lector.GetOrdinal("SDK")) ? 0 : lector.GetInt32(lector.GetOrdinal("SDK")),

                    });
                }
            }
            return retList;
        }

        public ModeloConsultaDto ConsultaModelo(ModeloConsultaDto objModelo)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdModelo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objModelo.IdModelo });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ModeloConsulta", listaParams.ToArray());
                while (lector.Read())
                {
                    objModelo.IdModelo = lector.IsDBNull(lector.GetOrdinal("IdModelo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdModelo"));
                    objModelo.Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre"));
                    objModelo.Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado"));
                    objModelo.IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado"));
                    objModelo.Configuracion = lector.IsDBNull(lector.GetOrdinal("Configuracion")) ? default(string) : lector.GetString(lector.GetOrdinal("Configuracion"));
                    objModelo.IdConfiguracion = lector.IsDBNull(lector.GetOrdinal("IdConfiguracion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdConfiguracion"));
                    objModelo.Marca = lector.IsDBNull(lector.GetOrdinal("Marca")) ? default(string) : lector.GetString(lector.GetOrdinal("Marca"));
                    objModelo.IdMarca = lector.IsDBNull(lector.GetOrdinal("IdMarca")) ? default(string) : lector.GetString(lector.GetOrdinal("IdMarca"));
                    objModelo.SDK = lector.IsDBNull(lector.GetOrdinal("SDK")) ? 0 : lector.GetInt32(lector.GetOrdinal("SDK"));
                    objModelo.IdEmpresa = lector.IsDBNull(lector.GetOrdinal("IdEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresa"));
                }
            }
            return objModelo;
        }

        public int MantenimientoModelo(Modelo objModelop)
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdModelo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objModelop.IdModelo });
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objModelop.Nombre });
                listaParams.Add(new SqlParameter("@IdMarca", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objModelop.IdMarca });
                listaParams.Add(new SqlParameter("@SDK", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objModelop.SDK });
                listaParams.Add(new SqlParameter("@IdConfiguracion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objModelop.IdConfiguracion });
                listaParams.Add(new SqlParameter("@IdEstado", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objModelop.IdEstado });
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objModelop.IdEmpresa });
                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_ModeloMantenimiento", listaParams.ToArray());
            }
            return r;
        }

        public string EliminarModelo(string IdModelo, int Accion)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdModelo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdModelo });
                listaParams.Add(new SqlParameter("@Accion", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = Accion });

                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_ModeloEliminar", listaParams.ToArray()).ToString();
            }
            return r;
        }
    }
}

using Acces;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class ProveedorDAL : IProveedor
    {
        public List<ProveedorPaginationDto> PaginadoProveedor(PaginationParameter objPaginationParameter)
        {
            List<ProveedorPaginationDto> retList = new List<ProveedorPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ProveedorPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new ProveedorPaginationDto
                    {
                        IdProveedor = lector.IsDBNull(lector.GetOrdinal("IdProveedor")) ? default(string) : lector.GetString(lector.GetOrdinal("IdProveedor")),
                        Codigo = lector.IsDBNull(lector.GetOrdinal("Codigo")) ? default(string) : lector.GetString(lector.GetOrdinal("Codigo")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Empresa = lector.IsDBNull(lector.GetOrdinal("Empresa")) ? default(string) : lector.GetString(lector.GetOrdinal("Empresa")),
                        Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad")),

                    });
                }
            }
            return retList;
        }

        public List<Proveedor> ListadoProveedor(string Proveedor)
        {
            List<Proveedor> retList = new List<Proveedor>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Proveedor });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ProveedorListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new Proveedor
                    {
                        IdProveedor = lector.IsDBNull(lector.GetOrdinal("IdProveedor")) ? default(string) : lector.GetString(lector.GetOrdinal("IdProveedor")),
                        IdEmpresa = lector.IsDBNull(lector.GetOrdinal("IdEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresa")),
                        Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion")),
                        Codigo = lector.IsDBNull(lector.GetOrdinal("Codigo")) ? default(string) : lector.GetString(lector.GetOrdinal("Codigo")),
                        IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),

                    });
                }
            }
            return retList;
        }

        public ProveedorConsultaDto ConsultaProveedor(ProveedorConsultaDto objProveedor)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdProveedor", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objProveedor.IdProveedor });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ProveedorConsulta", listaParams.ToArray());
                while (lector.Read())
                {
                    objProveedor.IdProveedor = lector.IsDBNull(lector.GetOrdinal("IdProveedor")) ? default(string) : lector.GetString(lector.GetOrdinal("IdProveedor"));
                    objProveedor.IdEmpresa = lector.IsDBNull(lector.GetOrdinal("IdEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresa"));
                    objProveedor.Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre"));
                    objProveedor.Codigo = lector.IsDBNull(lector.GetOrdinal("Codigo")) ? default(string) : lector.GetString(lector.GetOrdinal("Codigo"));
                    objProveedor.Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion"));
                    objProveedor.IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado"));

                }
            }
            return objProveedor;
        }

        public int MantenimientoProveedor(Proveedor objProveedorp)
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdProveedor", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objProveedorp.IdProveedor });
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objProveedorp.Nombre });
                listaParams.Add(new SqlParameter("@Codigo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objProveedorp.Codigo });
                listaParams.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objProveedorp.Descripcion });
                listaParams.Add(new SqlParameter("@IdEstado", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objProveedorp.IdEstado });
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objProveedorp.IdEmpresa });
                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_ProveedorMantenimiento", listaParams.ToArray());
            }
            return r;
        }

        public string EliminarProveedor(string IdProveedor, int Accion)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdProveedor", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdProveedor });
                listaParams.Add(new SqlParameter("@Accion", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = Accion });
                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_ProveedorEliminar", listaParams.ToArray()).ToString();
            }
            return r;
        }
    }
}

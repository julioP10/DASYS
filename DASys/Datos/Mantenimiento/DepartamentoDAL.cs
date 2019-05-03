using Acces;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public  class DepartamentoDAL: IDepartamento
    {
        public List<DepartamentoPaginationDto> PaginadoDepartamento(PaginationParameter objPaginationParameter)
        {
            List<DepartamentoPaginationDto> retList = new List<DepartamentoPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_DepartamentoPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DepartamentoPaginationDto
                    {
                        IdDepartamento = lector.IsDBNull(lector.GetOrdinal("IdDepartamento")) ? default(string) : lector.GetString(lector.GetOrdinal("IdDepartamento")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        Nivel = lector.IsDBNull(lector.GetOrdinal("Nivel")) ? 0 : lector.GetInt32(lector.GetOrdinal("Nivel")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad"))
                    });
                }
            }
            return retList;
        }

        public List<DepartamentoConsultaDto> ListadoDepartamento(string Departamento)
        {
            List<DepartamentoConsultaDto> retList = new List<DepartamentoConsultaDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Departamento });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_DepartamentoListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DepartamentoConsultaDto
                    {
                        IdDepartamento = lector.IsDBNull(lector.GetOrdinal("IdDepartamento")) ? default(string) : lector.GetString(lector.GetOrdinal("IdDepartamento")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Nivel = lector.IsDBNull(lector.GetOrdinal("Nivel")) ? 0 : lector.GetInt32(lector.GetOrdinal("Nivel")),

                    });
                }
            }
            return retList;
        }

        public DepartamentoConsultaDto ConsultaDepartamento(DepartamentoConsultaDto objDepartamento)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdDepartamento", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objDepartamento.IdDepartamento });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_DepartamentoConsulta", listaParams.ToArray());
                while (lector.Read())
                {
                    objDepartamento.IdDepartamento = lector.IsDBNull(lector.GetOrdinal("IdDepartamento")) ? default(string) : lector.GetString(lector.GetOrdinal("IdDepartamento"));
                    objDepartamento.Nivel = lector.IsDBNull(lector.GetOrdinal("Nivel")) ? 0 : lector.GetInt32(lector.GetOrdinal("Nivel"));
                    objDepartamento.Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre"));
                    objDepartamento.IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado"));
                }
            }
            return objDepartamento;
        }

        public int MantenimientoDepartamento(Departamento objDepartamentop)
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdDepartamento", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objDepartamentop.IdDepartamento });
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objDepartamentop.Nombre });
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objDepartamentop.IdEmpresa });
                listaParams.Add(new SqlParameter("@IdEstado", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objDepartamentop.IdEstado });
                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_DepartamentoMantenimiento", listaParams.ToArray());
            }
            return r;
        }

        public string EliminarDepartamento(string IdDepartamento,int Accion)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdDepartamento", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdDepartamento });
                listaParams.Add(new SqlParameter("@Accion", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = Accion });
                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_DepartamentoEliminar", listaParams.ToArray()).ToString();
            }
            return r;
        }
    }
}

using Acces;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class RegimenDAL : IRegimen
    {
        public List<RegimenPaginationDto> PaginadoRegimen(PaginationParameter objPaginationParameter)
        {
            List<RegimenPaginationDto> retList = new List<RegimenPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_RegimenPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new RegimenPaginationDto
                    {
                        IdRegimen = lector.IsDBNull(lector.GetOrdinal("IdRegimen")) ? default(string) : lector.GetString(lector.GetOrdinal("IdRegimen")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad")),

                    });
                }
            }
            return retList;
        }

        public List<RegimenConsultaDto> ListadoRegimen(string Regimen)
        {
            List<RegimenConsultaDto> retList = new List<RegimenConsultaDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Regimen });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_RegimenListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new RegimenConsultaDto
                    {
                        IdRegimen = lector.IsDBNull(lector.GetOrdinal("IdRegimen")) ? default(string) : lector.GetString(lector.GetOrdinal("IdRegimen")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado")),

                    });
                }
            }
            return retList;
        }

        public RegimenConsultaDto ConsultaRegimen(RegimenConsultaDto objRegimen)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdRegimen", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objRegimen.IdRegimen });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_RegimenConsulta", listaParams.ToArray());
                while (lector.Read())
                {
                    objRegimen.IdRegimen = lector.IsDBNull(lector.GetOrdinal("IdRegimen")) ? default(string) : lector.GetString(lector.GetOrdinal("IdRegimen"));
                    objRegimen.Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre"));
                    objRegimen.IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado"));
                }
            }
            return objRegimen;
        }

        public int MantenimientoRegimen(Regimen objRegimenp)
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdRegimen", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objRegimenp.IdRegimen });
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objRegimenp.Nombre });
                listaParams.Add(new SqlParameter("@IdEstado", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objRegimenp.IdEstado });
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objRegimenp.IdEmpresa });
                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_RegimenMantenimiento", listaParams.ToArray());
            }
            return r;
        }

        public string EliminarRegimen(string IdRegimen,int Accion)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdRegimen", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdRegimen });
                listaParams.Add(new SqlParameter("@Accion", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = Accion });
                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_RegimenEliminar", listaParams.ToArray()).ToString();
            }
            return r;
        }
    }
}

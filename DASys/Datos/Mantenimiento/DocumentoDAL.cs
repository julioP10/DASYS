using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Data;
using Acces;
using System.Data.SqlClient;
namespace Datos
{
   public class DocumentoDAL: IDocumento
    {
        public List<DocumentoPaginationDto> PaginadoDocumento(PaginationParameter objPaginationParameter)
        {
            List<DocumentoPaginationDto> retList = new List<DocumentoPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_DocumentoPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DocumentoPaginationDto
                    {
                        IdDocumento = lector.IsDBNull(lector.GetOrdinal("IdDocumento")) ? default(string) : lector.GetString(lector.GetOrdinal("IdDocumento")),
                        Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        FechaVencimiento = lector.IsDBNull(lector.GetOrdinal("FechaVencimiento")) ? false : lector.GetBoolean(lector.GetOrdinal("FechaVencimiento")),
                        Categoria = lector.IsDBNull(lector.GetOrdinal("Categoria")) ? default(string) : lector.GetString(lector.GetOrdinal("Categoria")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        Obligatorio = lector.IsDBNull(lector.GetOrdinal("Obligatorio")) ? false: lector.GetBoolean(lector.GetOrdinal("Obligatorio")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad")),

                    });
                }
            }
            return retList;
        }

        public List<DocumentoConsultaDto> ListadoDocumento(string Documento)
        {
            List<DocumentoConsultaDto> retList = new List<DocumentoConsultaDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Documento });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_DocumentoListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DocumentoConsultaDto
                    {
                        IdDocumento = lector.IsDBNull(lector.GetOrdinal("IdDocumento")) ? default(string) : lector.GetString(lector.GetOrdinal("IdDocumento")),
                        Descripcion = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        FechaVencimiento = lector.IsDBNull(lector.GetOrdinal("FechaVencimiento")) ? false : lector.GetBoolean(lector.GetOrdinal("FechaVencimiento")),
                        Categoria = lector.IsDBNull(lector.GetOrdinal("Entidad")) ? default(string) : lector.GetString(lector.GetOrdinal("Entidad")),
                        IdCategoria = lector.IsDBNull(lector.GetOrdinal("IdEntidad")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEntidad")),
                        IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado")),
                        Obligatorio = lector.IsDBNull(lector.GetOrdinal("Obligatorio")) ? false : lector.GetBoolean(lector.GetOrdinal("Obligatorio")),

                    });
                }
            }
            return retList;
        }

        public DocumentoConsultaDto ConsultaDocumento(DocumentoConsultaDto objDocumento)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdDocumento", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objDocumento.IdDocumento });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_DocumentoConsulta", listaParams.ToArray());
                while (lector.Read())
                {
                    objDocumento.IdDocumento = lector.IsDBNull(lector.GetOrdinal("IdDocumento")) ? default(string) : lector.GetString(lector.GetOrdinal("IdDocumento"));
                    objDocumento.Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion"));
                    objDocumento.Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre"));
                    objDocumento.FechaVencimiento = lector.IsDBNull(lector.GetOrdinal("FechaVencimiento")) ? false : lector.GetBoolean(lector.GetOrdinal("FechaVencimiento"));
                    objDocumento.IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado"));
                    objDocumento.Obligatorio = lector.IsDBNull(lector.GetOrdinal("Obligatorio")) ? false : lector.GetBoolean(lector.GetOrdinal("Obligatorio"));
                    objDocumento.IdEntidad = lector.IsDBNull(lector.GetOrdinal("IdEntidad")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEntidad"));
                }
            }
            return objDocumento;
        }

        public int MantenimientoDocumento(Documento objDocumentop)
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdDocumento", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objDocumentop.IdDocumento });
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objDocumentop.Nombre });
                listaParams.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objDocumentop.Descripcion });
                listaParams.Add(new SqlParameter("@FechaVencimiento", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objDocumentop.FechaVencimiento });
                listaParams.Add(new SqlParameter("@Obligatorio", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objDocumentop.Obligatorio });
                listaParams.Add(new SqlParameter("@IdEntidad", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objDocumentop.IdEntidad });
                listaParams.Add(new SqlParameter("@IdCategoria", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objDocumentop.IdCategoria });
                listaParams.Add(new SqlParameter("@IdEstado", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objDocumentop.IdEstado });
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objDocumentop.IdEmpresa });
                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_DocumentoMantenimiento", listaParams.ToArray());
            }
            return r;
        }

        public string EliminarDocumento(string IdDocumento,int Accion)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdDocumento", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdDocumento });
                listaParams.Add(new SqlParameter("@Accion", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = Accion });
                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_DocumentoEliminar", listaParams.ToArray()).ToString();
            }
            return r;
        }
    }
}

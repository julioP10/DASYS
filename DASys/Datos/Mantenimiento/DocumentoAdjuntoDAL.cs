using Acces;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Datos
{
    public class DocumentoAdjuntoDAL: IDocumentoAdjunto
    {
        public List<DocumentoAdjuntoPaginationDto> PaginadoDocumentoAdjunto(PaginationParameter objPaginationParameter)
        {
            List<DocumentoAdjuntoPaginationDto> retList = new List<DocumentoAdjuntoPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_DocumentoAdjuntoPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DocumentoAdjuntoPaginationDto
                    {
                        IdDocumentoAdjunto = lector.IsDBNull(lector.GetOrdinal("IdDocumentoAdjunto")) ? default(string) : lector.GetString(lector.GetOrdinal("IdDocumentoAdjunto")),
                        Descripcion = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Fecha = lector.IsDBNull(lector.GetOrdinal("Fecha")) ? default(string) : lector.GetString(lector.GetOrdinal("Fecha")),
                        Hora = lector.IsDBNull(lector.GetOrdinal("Hora")) ? default(string) : lector.GetString(lector.GetOrdinal("Hora")),
                        IdPersona = lector.IsDBNull(lector.GetOrdinal("IdPersona")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPersona"))


                    });
                }
            }
            return retList;
        }

        public List<DocumentoAdjuntoConsultaDto> ListadoDocumentoAdjunto(string IdDigitalizacion)
        {
            List<DocumentoAdjuntoConsultaDto> retList = new List<DocumentoAdjuntoConsultaDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdDigitalizacion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdDigitalizacion });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_DocumentoAdjuntoListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DocumentoAdjuntoConsultaDto
                    {

                        IdDocumentoAdjunto = lector.IsDBNull(lector.GetOrdinal("IdDocumentoAdjunto")) ? default(string) : lector.GetString(lector.GetOrdinal("IdDocumentoAdjunto")),
                        IdDigitalizacion = lector.IsDBNull(lector.GetOrdinal("IdDigitalizacion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdDigitalizacion")),
                        FechaVencimiento = lector.IsDBNull(lector.GetOrdinal("FechaVencimiento")) ? default(string) : lector.GetString(lector.GetOrdinal("FechaVencimiento")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Fecha = lector.IsDBNull(lector.GetOrdinal("Fecha")) ? default(string) : lector.GetString(lector.GetOrdinal("Fecha")),
                        Ruta = lector.IsDBNull(lector.GetOrdinal("Ruta")) ? default(string) : lector.GetString(lector.GetOrdinal("Ruta")),
                        Observacion = lector.IsDBNull(lector.GetOrdinal("Observacion")) ? default(string) : lector.GetString(lector.GetOrdinal("Observacion")),
                        Documento = lector.IsDBNull(lector.GetOrdinal("Documento")) ? default(string) : lector.GetString(lector.GetOrdinal("Documento")),
                        Vencimiento = lector.IsDBNull(lector.GetOrdinal("Vencimiento")) ? default(string) : lector.GetString(lector.GetOrdinal("Vencimiento")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        Obligatorio = lector.IsDBNull(lector.GetOrdinal("Obligatorio")) ? default(string) : lector.GetString(lector.GetOrdinal("Obligatorio"))

                    });
                }
            }
            return retList;
        }

        public DocumentoAdjuntoConsultaDto ConsultaDocumentoAdjunto(DocumentoAdjuntoConsultaDto objDocumentoAdjunto)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdDocumentoAdjunto", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objDocumentoAdjunto.IdDocumentoAdjunto });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_DocumentoAdjuntoConsulta", listaParams.ToArray());
                while (lector.Read())
                {
                    objDocumentoAdjunto.IdDocumentoAdjunto = lector.IsDBNull(lector.GetOrdinal("IdDocumentoAdjunto")) ? default(string) : lector.GetString(lector.GetOrdinal("IdDocumentoAdjunto"));
                    objDocumentoAdjunto.Descripcion = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion"));
                    objDocumentoAdjunto.Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre"));
                    objDocumentoAdjunto.Fecha = lector.IsDBNull(lector.GetOrdinal("Fecha")) ? default(string) : lector.GetString(lector.GetOrdinal("Fecha"));
                    objDocumentoAdjunto.Hora = lector.IsDBNull(lector.GetOrdinal("Hora")) ? default(string) : lector.GetString(lector.GetOrdinal("Hora"));
                    objDocumentoAdjunto.IdPersona = lector.IsDBNull(lector.GetOrdinal("IdPersona")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPersona"));
                    objDocumentoAdjunto.IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado"));

                }
            }
            return objDocumentoAdjunto;
        }

        public int MantenimientoDocumentoAdjunto(DocumentoAdjunto objDocumentoAdjuntop)
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdDocumentoAdjunto", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objDocumentoAdjuntop.IdDocumentoAdjunto });
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objDocumentoAdjuntop.Nombre });
                listaParams.Add(new SqlParameter("@Ruta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objDocumentoAdjuntop.Ruta });
                listaParams.Add(new SqlParameter("@Fecha", SqlDbType.DateTime) { Direction = ParameterDirection.Input, Value = objDocumentoAdjuntop.Fecha });
                listaParams.Add(new SqlParameter("@IdPersona", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objDocumentoAdjuntop.IdPersona });
                listaParams.Add(new SqlParameter("@IdDigitalizacion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objDocumentoAdjuntop.IdDigitalizacion });
                listaParams.Add(new SqlParameter("@IdDocumento", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objDocumentoAdjuntop.IdDocumento });
                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_DocumentoAdjuntoMantenimiento", listaParams.ToArray());
            }
            return r;
        }

        public int EliminarDocumentoAdjunto(string IdDocumentoAdjunto)
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdDocumentoAdjunto", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdDocumentoAdjunto });
                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_DocumentoAdjuntoEliminar", listaParams.ToArray());
            }
            return r;
        }
    }
}

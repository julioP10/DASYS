using Acces;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Datos
{
    public   class EmpresaDAL: IEmpresa
    {
        public List<EmpresaPaginationDto> PaginadoEmpresa(PaginationParameter objPaginationParameter)
        {
            List<EmpresaPaginationDto> retList = new List<EmpresaPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_EmpresaPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new EmpresaPaginationDto
                    {
                        IdEmpresa = lector.IsDBNull(lector.GetOrdinal("IdEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresa")),
                        DireccionFiscal = lector.IsDBNull(lector.GetOrdinal("DireccionFiscal")) ? default(string) : lector.GetString(lector.GetOrdinal("DireccionFiscal")),
                        RazonSocial = lector.IsDBNull(lector.GetOrdinal("RazonSocial")) ? default(string) : lector.GetString(lector.GetOrdinal("RazonSocial")),
                        RUC = lector.IsDBNull(lector.GetOrdinal("RUC")) ? default(string) : lector.GetString(lector.GetOrdinal("RUC")),
                        Entidad = lector.IsDBNull(lector.GetOrdinal("Entidad")) ? default(string) : lector.GetString(lector.GetOrdinal("Entidad")),
                        EsPrincipal = lector.IsDBNull(lector.GetOrdinal("EsPrincipal")) ? 0 : lector.GetInt32(lector.GetOrdinal("EsPrincipal")),
                        EsContratista = lector.IsDBNull(lector.GetOrdinal("EsContratista")) ? 0 : lector.GetInt32(lector.GetOrdinal("EsContratista")),
                        EsSubContratista = lector.IsDBNull(lector.GetOrdinal("EsSubContratista")) ? 0 : lector.GetInt32(lector.GetOrdinal("EsSubContratista")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        TipoEmpresa = lector.IsDBNull(lector.GetOrdinal("TipoEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("TipoEmpresa")),
                        IdPadre = lector.IsDBNull(lector.GetOrdinal("IdPadre")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPadre")),
                        Categoria = lector.IsDBNull(lector.GetOrdinal("Categoria")) ? default(string) : lector.GetString(lector.GetOrdinal("Categoria")),
                        IdPadreSubcontratista = lector.IsDBNull(lector.GetOrdinal("IdPadreSubcontratista")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPadreSubcontratista")),
                        PadreSubcontratista = lector.IsDBNull(lector.GetOrdinal("PadreSubcontratista")) ? default(string) : lector.GetString(lector.GetOrdinal("PadreSubcontratista")),
                        Digitalizacion = lector.IsDBNull(lector.GetOrdinal("Digitalizacion")) ? default(string) : lector.GetString(lector.GetOrdinal("Digitalizacion")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0: lector.GetInt32(lector.GetOrdinal("Cantidad")),
                    });
                }
            }
            return retList;
        }
        public List<EmpresaDigitalizacionPaginationDto> PaginadoEmpresaDigitalizacion(PaginationParameter objPaginationParameter)
        {
            List<EmpresaDigitalizacionPaginationDto> retList = new List<EmpresaDigitalizacionPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_EmpresaDigitalizacionPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new EmpresaDigitalizacionPaginationDto
                    {
                        IdEmpresa = lector.IsDBNull(lector.GetOrdinal("IdEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresa")),
                        IdPersona = lector.IsDBNull(lector.GetOrdinal("IdPersona")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPersona")),
                        Categoria = lector.IsDBNull(lector.GetOrdinal("Categoria")) ? default(string) : lector.GetString(lector.GetOrdinal("Categoria")),
                        Documento = lector.IsDBNull(lector.GetOrdinal("Documento")) ? default(string) : lector.GetString(lector.GetOrdinal("Documento")),
                        Alias = lector.IsDBNull(lector.GetOrdinal("RazonSocial")) ? default(string) : lector.GetString(lector.GetOrdinal("RazonSocial")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        EstadoAdjunto = lector.IsDBNull(lector.GetOrdinal("EstadoAdjunto")) ? default(string) : lector.GetString(lector.GetOrdinal("EstadoAdjunto")),
                        FechaVencimiento = lector.IsDBNull(lector.GetOrdinal("FechaVencimiento")) ? default(string) : lector.GetString(lector.GetOrdinal("FechaVencimiento")),
                        Obligatorio = lector.IsDBNull(lector.GetOrdinal("Obligatorio")) ? default(string) : lector.GetString(lector.GetOrdinal("Obligatorio")),
                        Adjuntado = lector.IsDBNull(lector.GetOrdinal("Adjuntado")) ? 0 : lector.GetInt32(lector.GetOrdinal("Adjuntado")),
                        IdCategoria = lector.IsDBNull(lector.GetOrdinal("IdCategoria")) ? default(string) : lector.GetString(lector.GetOrdinal("IdCategoria")),
                        IdDocumento = lector.IsDBNull(lector.GetOrdinal("IdDocumento")) ? default(string) : lector.GetString(lector.GetOrdinal("IdDocumento")),
                        Observacion = lector.IsDBNull(lector.GetOrdinal("Observacion")) ? default(string) : lector.GetString(lector.GetOrdinal("Observacion")),
                        ObservacionAdjunto = lector.IsDBNull(lector.GetOrdinal("ObservacionAdjunto")) ? default(string) : lector.GetString(lector.GetOrdinal("ObservacionAdjunto")),
                        IdDocumentoAdjunto = lector.IsDBNull(lector.GetOrdinal("IdDocumentoAdjunto")) ? default(string) : lector.GetString(lector.GetOrdinal("IdDocumentoAdjunto")),
                        IdDigitalizacion = lector.IsDBNull(lector.GetOrdinal("IdDigitalizacion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdDigitalizacion")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad")),
                    });
                }
            }
            return retList;
        }
        public List<DigitalizacionExcelDto> ListaEmpresaDigitalizacionExcel(string IdPersona)
        {
            List<DigitalizacionExcelDto> retList = new List<DigitalizacionExcelDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdPersona", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdPersona });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_EmpresaDigitalizacionExcel", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DigitalizacionExcelDto
                    {
                        Codigo = lector.IsDBNull(lector.GetOrdinal("Codigo")) ? default(string) : lector.GetString(lector.GetOrdinal("Codigo")),
                        Numero = lector.IsDBNull(lector.GetOrdinal("Numero")) ? default(string) : lector.GetString(lector.GetOrdinal("Numero")),
                        Empresa = lector.IsDBNull(lector.GetOrdinal("Empresa")) ? default(string) : lector.GetString(lector.GetOrdinal("Empresa")),
                        Categoria = lector.IsDBNull(lector.GetOrdinal("Categoria")) ? default(string) : lector.GetString(lector.GetOrdinal("Categoria")),
                        Documento = lector.IsDBNull(lector.GetOrdinal("Documento")) ? default(string) : lector.GetString(lector.GetOrdinal("Documento")),
                        Observacion = lector.IsDBNull(lector.GetOrdinal("Observacion")) ? default(string) : lector.GetString(lector.GetOrdinal("Observacion")),
                        Obligatorio = lector.IsDBNull(lector.GetOrdinal("Obligatorio")) ? default(string) : lector.GetString(lector.GetOrdinal("Obligatorio")),
                        Vencimiento = lector.IsDBNull(lector.GetOrdinal("Vencimiento")) ? default(string) : lector.GetString(lector.GetOrdinal("Vencimiento")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                    });
                }
            }
            return retList;
        }
        public List<EmpresaConsultaDto> ListadoEmpresa(string Empresa)
        {
            List<EmpresaConsultaDto> retList = new List<EmpresaConsultaDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Empresa });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_EmpresaListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new EmpresaConsultaDto
                    {
                        IdEmpresa = lector.IsDBNull(lector.GetOrdinal("IdEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresa")),
                        IdPersona = lector.IsDBNull(lector.GetOrdinal("IdPersona")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPersona")),
                        DireccionFiscal = lector.IsDBNull(lector.GetOrdinal("DireccionFiscal")) ? default(string) : lector.GetString(lector.GetOrdinal("DireccionFiscal")),
                        RazonSocial = lector.IsDBNull(lector.GetOrdinal("RazonSocial")) ? default(string) : lector.GetString(lector.GetOrdinal("RazonSocial")),
                        RUC = lector.IsDBNull(lector.GetOrdinal("RUC")) ? default(string) : lector.GetString(lector.GetOrdinal("RUC")),
                        IdEntidad = lector.IsDBNull(lector.GetOrdinal("IdEntidad")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEntidad")),
                        Entidad = lector.IsDBNull(lector.GetOrdinal("Entidad")) ? default(string) : lector.GetString(lector.GetOrdinal("Entidad")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado")),
                        Mensaje = lector.IsDBNull(lector.GetOrdinal("Mensaje")) ? default(string) : lector.GetString(lector.GetOrdinal("Mensaje")),
                        Digitalizado = lector.IsDBNull(lector.GetOrdinal("Digitalizado")) ? 0 : lector.GetInt32(lector.GetOrdinal("Digitalizado")),
                        IdUsuario = lector.IsDBNull(lector.GetOrdinal("IdUsuario")) ? default(string) : lector.GetString(lector.GetOrdinal("IdUsuario")),
                        Enviado = lector.IsDBNull(lector.GetOrdinal("Enviado")) ? 0 : lector.GetInt32(lector.GetOrdinal("Enviado")),
                    });
                }
            }
            return retList;
        }
        public List<ParametrosCorreoDto> ListadoParametrosCorreo(ParametrosCorreoDto objParametrosCorreoDto)
        {
            List<ParametrosCorreoDto> retList = new List<ParametrosCorreoDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objParametrosCorreoDto.IdEmpresa });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ParametrosCorreoListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new ParametrosCorreoDto
                    {
                        IdEmpresa = lector.IsDBNull(lector.GetOrdinal("IdEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresa")),
                        IdParametros = lector.IsDBNull(lector.GetOrdinal("IdParametros")) ? default(string) : lector.GetString(lector.GetOrdinal("IdParametros")),
                        Host = lector.IsDBNull(lector.GetOrdinal("Host")) ? default(string) : lector.GetString(lector.GetOrdinal("Host")),
                        Port = lector.IsDBNull(lector.GetOrdinal("Port")) ? 0 : lector.GetInt32(lector.GetOrdinal("Port")),
                        Correo = lector.IsDBNull(lector.GetOrdinal("Correo")) ? default(string) : lector.GetString(lector.GetOrdinal("Correo")),
                        Password = lector.IsDBNull(lector.GetOrdinal("Password")) ? default(string) : lector.GetString(lector.GetOrdinal("Password")),
                        Empresa = lector.IsDBNull(lector.GetOrdinal("Empresa")) ? default(string) : lector.GetString(lector.GetOrdinal("Empresa")),
                        Ruc = lector.IsDBNull(lector.GetOrdinal("Ruc")) ? default(string) : lector.GetString(lector.GetOrdinal("Ruc")),
                    });
                }
            }
            return retList;
        }
        public ParametrosCorreoDto ConsultaParametrosCorreo(ParametrosCorreoDto objParametrosCorreoDto)
        {
            ParametrosCorreoDto obj = new ParametrosCorreoDto();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objParametrosCorreoDto.IdEmpresa });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ParametrosCorreoConsulta", listaParams.ToArray());
                while (lector.Read())
                {

                    obj.IdEmpresa = lector.IsDBNull(lector.GetOrdinal("IdEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresa"));
                    obj.IdParametros = lector.IsDBNull(lector.GetOrdinal("IdParametros")) ? default(string) : lector.GetString(lector.GetOrdinal("IdParametros"));
                    obj.Host = lector.IsDBNull(lector.GetOrdinal("Host")) ? default(string) : lector.GetString(lector.GetOrdinal("Host"));
                    obj.Port = lector.IsDBNull(lector.GetOrdinal("Port")) ? 0 : lector.GetInt32(lector.GetOrdinal("Port"));
                    obj.Correo = lector.IsDBNull(lector.GetOrdinal("Correo")) ? default(string) : lector.GetString(lector.GetOrdinal("Correo"));
                    obj.Password = lector.IsDBNull(lector.GetOrdinal("Password")) ? default(string) : lector.GetString(lector.GetOrdinal("Password"));
                    obj.Empresa = lector.IsDBNull(lector.GetOrdinal("Empresa")) ? default(string) : lector.GetString(lector.GetOrdinal("Empresa"));
                }
            }
            return obj;
        }
        public EmpresaConsultaDto ConsultaEmpresa(EmpresaConsultaDto objEmpresa)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objEmpresa.IdEmpresa });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_EmpresaConsulta", listaParams.ToArray());
                while (lector.Read())
                {
                    objEmpresa.IdEmpresa = lector.IsDBNull(lector.GetOrdinal("IdEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresa"));
                    objEmpresa.DireccionFiscal = lector.IsDBNull(lector.GetOrdinal("DireccionFiscal")) ? default(string) : lector.GetString(lector.GetOrdinal("DireccionFiscal"));
                    objEmpresa.RazonSocial = lector.IsDBNull(lector.GetOrdinal("RazonSocial")) ? default(string) : lector.GetString(lector.GetOrdinal("RazonSocial"));
                    objEmpresa.RUC = lector.IsDBNull(lector.GetOrdinal("RUC")) ? default(string) : lector.GetString(lector.GetOrdinal("RUC"));
                    objEmpresa.IdEntidad = lector.IsDBNull(lector.GetOrdinal("IdEntidad")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEntidad"));
                    objEmpresa.Entidad = lector.IsDBNull(lector.GetOrdinal("Entidad")) ? default(string) : lector.GetString(lector.GetOrdinal("Entidad"));
                    objEmpresa.Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado"));
                    objEmpresa.IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado"));
                    objEmpresa.IdCategoria = lector.IsDBNull(lector.GetOrdinal("IdCategoria")) ? default(string) : lector.GetString(lector.GetOrdinal("IdCategoria"));
                    objEmpresa.IdPadre = lector.IsDBNull(lector.GetOrdinal("IdPadre")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPadre"));
                    objEmpresa.EsContratista = lector.IsDBNull(lector.GetOrdinal("EsContratista")) ? 0 : lector.GetInt32(lector.GetOrdinal("EsContratista"));
                    objEmpresa.EsSubContratista = lector.IsDBNull(lector.GetOrdinal("EsSubContratista")) ? 0 : lector.GetInt32(lector.GetOrdinal("EsSubContratista"));
                    objEmpresa.EsPrincipal = lector.IsDBNull(lector.GetOrdinal("EsPrincipal")) ? 0 : lector.GetInt32(lector.GetOrdinal("EsPrincipal"));
                    objEmpresa.TipoEmpresa = lector.IsDBNull(lector.GetOrdinal("TipoEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("TipoEmpresa"));
                }
            }
            return objEmpresa;
        }

        public string MantenimientoEmpresa(Empresa objEmpresap)
        {
            string r ="";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objEmpresap.IdEmpresa });
                listaParams.Add(new SqlParameter("@IdPadre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objEmpresap.IdPadre });
                listaParams.Add(new SqlParameter("@DireccionFiscal", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objEmpresap.DireccionFiscal });
                listaParams.Add(new SqlParameter("@RazonSocial", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objEmpresap.RazonSocial });
                listaParams.Add(new SqlParameter("@RUC", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objEmpresap.RUC });
                listaParams.Add(new SqlParameter("@IdEstado", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objEmpresap.IdEstado });
                listaParams.Add(new SqlParameter("@IdCategoria", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objEmpresap.IdCategoria });
                listaParams.Add(new SqlParameter("@EsPrincipal", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objEmpresap.EsPrincipal });
                listaParams.Add(new SqlParameter("@EsContratista", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objEmpresap.EsContratista });
                listaParams.Add(new SqlParameter("@EsSubContratista", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objEmpresap.EsSubContratista });

                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_EmpresaMantenimiento", listaParams.ToArray()).ToString();
            }
            return r;
        }

        public int MantenimientoParametrosCorreo(ParametrosCorreoDto objParametrosCorreoDto)
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objParametrosCorreoDto.IdEmpresa });
                listaParams.Add(new SqlParameter("@IdParametros", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objParametrosCorreoDto.IdParametros });
                listaParams.Add(new SqlParameter("@Password", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objParametrosCorreoDto.Password });
                listaParams.Add(new SqlParameter("@Port", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objParametrosCorreoDto.Port });
                listaParams.Add(new SqlParameter("@Correo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objParametrosCorreoDto.Correo });
                listaParams.Add(new SqlParameter("@Host", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objParametrosCorreoDto.Host });

                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_ParametrosCorreoMantenimiento", listaParams.ToArray());
            }
            return r;
        }

        public string EliminarEmpresa(string IdEmpresa, int Accion)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdEmpresa });
                listaParams.Add(new SqlParameter("@Accion", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = Accion });

                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_EmpresaEliminar", listaParams.ToArray()).ToString();
            }
            return r;
        }
    }
}

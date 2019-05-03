using Acces;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class ReportesDAL:IReportes
    {
        public List<ReporteColaboradorDto> ReporteColaborador(PaginationParameter objPaginationParameter,ReporteFilterDto objReporte)
        {
            List<ReporteColaboradorDto> retList = new List<ReporteColaboradorDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();


                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objReporte.NombreSearch });
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objReporte.IdEmpresaSearch });
                listaParams.Add(new SqlParameter("@IdEmpresaPrincipal", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objReporte.IdEmpresaPrincipal });
                listaParams.Add(new SqlParameter("@IdPersona", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objReporte.IdPersonaSearch });
                listaParams.Add(new SqlParameter("@IdTipoLugar", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objReporte.IdTipoLugarSearch });
                listaParams.Add(new SqlParameter("@IdDepartamento", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objReporte.IdDepartamentoSearch });
                listaParams.Add(new SqlParameter("@IdUbicacion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objReporte.IdUbicacionSearch });

                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ReporteColaborador", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new ReporteColaboradorDto
                    {
                        IdColaborador = lector.IsDBNull(lector.GetOrdinal("IdColaborador")) ? default(string) : lector.GetString(lector.GetOrdinal("IdColaborador")),
                        ApellidoMaterno = lector.IsDBNull(lector.GetOrdinal("ApellidoMaterno")) ? default(string) : lector.GetString(lector.GetOrdinal("ApellidoMaterno")),
                        ApellidoPaterno = lector.IsDBNull(lector.GetOrdinal("ApellidoPaterno")) ? default(string) : lector.GetString(lector.GetOrdinal("ApellidoPaterno")),
                        Departamento = lector.IsDBNull(lector.GetOrdinal("Departamento")) ? default(string) : lector.GetString(lector.GetOrdinal("Departamento")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        NumeroDocumento = lector.IsDBNull(lector.GetOrdinal("NumeroDocumento")) ? default(string) : lector.GetString(lector.GetOrdinal("NumeroDocumento")),
                        FechaNacimiento = lector.IsDBNull(lector.GetOrdinal("FechaNacimiento")) ? default(string) : lector.GetString(lector.GetOrdinal("FechaNacimiento")),
                        Sexo = lector.IsDBNull(lector.GetOrdinal("Genero")) ? default(string) : lector.GetString(lector.GetOrdinal("Genero")),
                        Ubicacion = lector.IsDBNull(lector.GetOrdinal("Ubicacion")) ? default(string) : lector.GetString(lector.GetOrdinal("Ubicacion")),
                        Entidad = lector.IsDBNull(lector.GetOrdinal("Entidad")) ? default(string) : lector.GetString(lector.GetOrdinal("Entidad")),
                        Digitalizacion = lector.IsDBNull(lector.GetOrdinal("Digitalizacion")) ? default(string) : lector.GetString(lector.GetOrdinal("Digitalizacion")),
                        Empresa = lector.IsDBNull(lector.GetOrdinal("Empresa")) ? default(string) : lector.GetString(lector.GetOrdinal("Empresa")),
                        PadreSubcontratista = lector.IsDBNull(lector.GetOrdinal("PadreSubcontratista")) ? default(string) : lector.GetString(lector.GetOrdinal("PadreSubcontratista")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad")),
                    });
                }
            }
            return retList;
        }
        public List<ReporteVehiculoDto> ReporteVehiculo(PaginationParameter objPaginationParameter, ReporteFilterDto objReporte)
        {
            List<ReporteVehiculoDto> retList = new List<ReporteVehiculoDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();


                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objReporte.NombreSearch });
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objReporte.IdEmpresaSearch });
                listaParams.Add(new SqlParameter("@IdEmpresaPrincipal", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objReporte.IdEmpresaPrincipal });
                listaParams.Add(new SqlParameter("@IdPersona", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objReporte.IdPersonaSearch });
                listaParams.Add(new SqlParameter("@IdTipo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objReporte.IdTipoSearch });
                listaParams.Add(new SqlParameter("@IdMarca", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objReporte.IdMarcaSearch });
                listaParams.Add(new SqlParameter("@IdModelo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objReporte.IdModeloSearch });
                listaParams.Add(new SqlParameter("@IdCategoria", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objReporte.IdCategoriaSearch });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ReporteVehiculo", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new ReporteVehiculoDto
                    {
                        IdVehiculo = lector.IsDBNull(lector.GetOrdinal("IdVehiculo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdVehiculo")),
                        Codigo = lector.IsDBNull(lector.GetOrdinal("Codigo")) ? default(string) : lector.GetString(lector.GetOrdinal("Codigo")),
                        Categoria = lector.IsDBNull(lector.GetOrdinal("Categoria")) ? default(string) : lector.GetString(lector.GetOrdinal("Categoria")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        NumeroDocumento = lector.IsDBNull(lector.GetOrdinal("Documento")) ? default(string) : lector.GetString(lector.GetOrdinal("Documento")),
                        InicioContrato = lector.IsDBNull(lector.GetOrdinal("InicioContrato")) ? default(string) : lector.GetString(lector.GetOrdinal("InicioContrato")),
                        FinContrato = lector.IsDBNull(lector.GetOrdinal("FinContrato")) ? default(string) : lector.GetString(lector.GetOrdinal("FinContrato")),
                        Marca = lector.IsDBNull(lector.GetOrdinal("Marca")) ? default(string) : lector.GetString(lector.GetOrdinal("Marca")),
                        Modelo = lector.IsDBNull(lector.GetOrdinal("Modelo")) ? default(string) : lector.GetString(lector.GetOrdinal("Modelo")),
                        Entidad = lector.IsDBNull(lector.GetOrdinal("Entidad")) ? default(string) : lector.GetString(lector.GetOrdinal("Entidad")),
                        Digitalizacion = lector.IsDBNull(lector.GetOrdinal("Digitalizacion")) ? default(string) : lector.GetString(lector.GetOrdinal("Digitalizacion")),
                        Empresa = lector.IsDBNull(lector.GetOrdinal("Empresa")) ? default(string) : lector.GetString(lector.GetOrdinal("Empresa")),
                        PadreSubcontratista = lector.IsDBNull(lector.GetOrdinal("PadreSubcontratista")) ? default(string) : lector.GetString(lector.GetOrdinal("PadreSubcontratista")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad")),
                    });
                }
            }
            return retList;
        }

        public List<ReporteMaquinariaDto> ReporteMaquinaria(PaginationParameter objPaginationParameter, ReporteFilterDto objReporte)
        {
            List<ReporteMaquinariaDto> retList = new List<ReporteMaquinariaDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();


                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objReporte.NombreSearch });
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objReporte.IdEmpresaSearch });
                listaParams.Add(new SqlParameter("@IdEmpresaPrincipal", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objReporte.IdEmpresaPrincipal });
                listaParams.Add(new SqlParameter("@IdPersona", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objReporte.IdPersonaSearch });
                listaParams.Add(new SqlParameter("@IdTipo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objReporte.IdTipoSearch });
                listaParams.Add(new SqlParameter("@IdMarca", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objReporte.IdMarcaSearch });
                listaParams.Add(new SqlParameter("@IdModelo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objReporte.IdModeloSearch });
                listaParams.Add(new SqlParameter("@IdCategoria", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objReporte.IdCategoriaSearch });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ReporteMaquinaria", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new ReporteMaquinariaDto
                    {
                        IdMaquinaria = lector.IsDBNull(lector.GetOrdinal("IdMaquinaria")) ? default(string) : lector.GetString(lector.GetOrdinal("IdMaquinaria")),
                        Codigo = lector.IsDBNull(lector.GetOrdinal("Codigo")) ? default(string) : lector.GetString(lector.GetOrdinal("Codigo")),
                        Categoria = lector.IsDBNull(lector.GetOrdinal("Categoria")) ? default(string) : lector.GetString(lector.GetOrdinal("Categoria")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        NumeroDocumento = lector.IsDBNull(lector.GetOrdinal("Documento")) ? default(string) : lector.GetString(lector.GetOrdinal("Documento")),
                        InicioContrato = lector.IsDBNull(lector.GetOrdinal("InicioContrato")) ? default(string) : lector.GetString(lector.GetOrdinal("InicioContrato")),
                        FinContrato = lector.IsDBNull(lector.GetOrdinal("FinContrato")) ? default(string) : lector.GetString(lector.GetOrdinal("FinContrato")),
                        Marca = lector.IsDBNull(lector.GetOrdinal("Marca")) ? default(string) : lector.GetString(lector.GetOrdinal("Marca")),
                        Modelo = lector.IsDBNull(lector.GetOrdinal("Modelo")) ? default(string) : lector.GetString(lector.GetOrdinal("Modelo")),
                        Entidad = lector.IsDBNull(lector.GetOrdinal("Entidad")) ? default(string) : lector.GetString(lector.GetOrdinal("Entidad")),
                        Digitalizacion = lector.IsDBNull(lector.GetOrdinal("Digitalizacion")) ? default(string) : lector.GetString(lector.GetOrdinal("Digitalizacion")),
                        Empresa = lector.IsDBNull(lector.GetOrdinal("Empresa")) ? default(string) : lector.GetString(lector.GetOrdinal("Empresa")),
                        PadreSubcontratista = lector.IsDBNull(lector.GetOrdinal("PadreSubcontratista")) ? default(string) : lector.GetString(lector.GetOrdinal("PadreSubcontratista")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad")),
                    });
                }
            }
            return retList;
        }
        public List<ReporteEmpresaDto> ReporteEmpresa(PaginationParameter objPaginationParameter)
        {
            List<ReporteEmpresaDto> retList = new List<ReporteEmpresaDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ReporteEmpresaPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new ReporteEmpresaDto
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
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad")),
                    });
                }
            }
            return retList;
        }
        public List<ReporteDigitalizacionDto> ReporteDigitalizacion(PaginationParameter objPaginationParameter)
        {
            List<ReporteDigitalizacionDto> retList = new List<ReporteDigitalizacionDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ReporteDigitalizacionPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new ReporteDigitalizacionDto
                    {
                        Categoria = lector.IsDBNull(lector.GetOrdinal("Categoria")) ? default(string) : lector.GetString(lector.GetOrdinal("Categoria")),
                        Documento = lector.IsDBNull(lector.GetOrdinal("Documento")) ? default(string) : lector.GetString(lector.GetOrdinal("Documento")),
                        FechaVencimiento = lector.IsDBNull(lector.GetOrdinal("FechaVencimiento")) ? default(string) : lector.GetString(lector.GetOrdinal("FechaVencimiento")),
                        Obligatorio = lector.IsDBNull(lector.GetOrdinal("Obligatorio")) ? default(string) : lector.GetString(lector.GetOrdinal("Obligatorio")),
                        Entidad = lector.IsDBNull(lector.GetOrdinal("Entidad")) ? default(string) : lector.GetString(lector.GetOrdinal("Entidad")),
                        Id = lector.IsDBNull(lector.GetOrdinal("Id")) ? default(string) : lector.GetString(lector.GetOrdinal("Id")),

                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad")),
                    });
                }
            }
            return retList;
        }
        public List<ReporteDocumentosDto> ReporteDocumentos(PaginationParameter objPaginationParameter)
        {
            List<ReporteDocumentosDto> retList = new List<ReporteDocumentosDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                listaParams.Add(new SqlParameter("@Parameters", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Parameters });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ReporteDocumentoPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new ReporteDocumentosDto
                    {
                        IdPersona = lector.IsDBNull(lector.GetOrdinal("IdPersona")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPersona")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Categoria = lector.IsDBNull(lector.GetOrdinal("Categoria")) ? default(string) : lector.GetString(lector.GetOrdinal("Categoria")),
                        Documento = lector.IsDBNull(lector.GetOrdinal("Documento")) ? default(string) : lector.GetString(lector.GetOrdinal("Documento")),
                        EstadoAdjunto = lector.IsDBNull(lector.GetOrdinal("EstadoAdjunto")) ? default(string) : lector.GetString(lector.GetOrdinal("EstadoAdjunto")),
                        FechaVencimiento = lector.IsDBNull(lector.GetOrdinal("FechaVencimiento")) ? default(string) : lector.GetString(lector.GetOrdinal("FechaVencimiento")),
                        Obligatorio = lector.IsDBNull(lector.GetOrdinal("Obligatorio")) ? default(string) : lector.GetString(lector.GetOrdinal("Obligatorio")),
                        Adjuntado = lector.IsDBNull(lector.GetOrdinal("Adjuntado")) ? default(string): lector.GetString(lector.GetOrdinal("Adjuntado")),
                        ObservacionAdjunto = lector.IsDBNull(lector.GetOrdinal("ObservacionAdjunto")) ? default(string) : lector.GetString(lector.GetOrdinal("ObservacionAdjunto")),
                        Fecha = lector.IsDBNull(lector.GetOrdinal("Fecha")) ? default(string) : lector.GetString(lector.GetOrdinal("Fecha")),
                        Id = lector.IsDBNull(lector.GetOrdinal("Id")) ? default(string) : lector.GetString(lector.GetOrdinal("Id")),
                        DiasRestante = lector.IsDBNull(lector.GetOrdinal("DiasRestante")) ? default(string) : lector.GetString(lector.GetOrdinal("DiasRestante")),
                        Genero = lector.IsDBNull(lector.GetOrdinal("Genero")) ? default(string) : lector.GetString(lector.GetOrdinal("Genero")),
                        Empresa = lector.IsDBNull(lector.GetOrdinal("Empresa")) ? default(string) : lector.GetString(lector.GetOrdinal("Empresa")),
                        Departamento = lector.IsDBNull(lector.GetOrdinal("Departamento")) ? default(string) : lector.GetString(lector.GetOrdinal("Departamento")),
                        Ubicacion = lector.IsDBNull(lector.GetOrdinal("Ubicacion")) ? default(string) : lector.GetString(lector.GetOrdinal("Ubicacion")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad")),
                    });
                }
            }
            return retList;
        }
        public List<ReporteOchoMarcacionesDto> ReporteOchoMarcaciones(PaginationParameter objPaginationParameter)
        {
            List<ReporteOchoMarcacionesDto> retList = new List<ReporteOchoMarcacionesDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                listaParams.Add(new SqlParameter("@Parameters", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Parameters });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ReporteOchoMarcacionesPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new ReporteOchoMarcacionesDto
                    {
                        Id = lector.IsDBNull(lector.GetOrdinal("Id")) ? default(string) : lector.GetString(lector.GetOrdinal("Id")),
                        Fecha = lector.IsDBNull(lector.GetOrdinal("Fecha")) ? default(string) : lector.GetString(lector.GetOrdinal("Fecha")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Marcaciones = lector.IsDBNull(lector.GetOrdinal("Marcaciones")) ? 0 : lector.GetInt32(lector.GetOrdinal("Marcaciones")),
                        Numero = lector.IsDBNull(lector.GetOrdinal("Numero")) ? default(string) : lector.GetString(lector.GetOrdinal("Numero")),
                        Empresa = lector.IsDBNull(lector.GetOrdinal("Empresa")) ? default(string) : lector.GetString(lector.GetOrdinal("Empresa")),
                        Terminal = lector.IsDBNull(lector.GetOrdinal("Terminal")) ? default(string) : lector.GetString(lector.GetOrdinal("Terminal")),
                        PrimeraMarcacion = lector.IsDBNull(lector.GetOrdinal("PrimeraMarcacion")) ? default(string) : lector.GetString(lector.GetOrdinal("PrimeraMarcacion")),
                        SegundaMarcacion = lector.IsDBNull(lector.GetOrdinal("SegundaMarcacion")) ? default(string) : lector.GetString(lector.GetOrdinal("SegundaMarcacion")),
                        TerceraMarcacion = lector.IsDBNull(lector.GetOrdinal("TerceraMarcacion")) ? default(string) : lector.GetString(lector.GetOrdinal("TerceraMarcacion")),
                        CuartaMarcacion = lector.IsDBNull(lector.GetOrdinal("CuartaMarcacion")) ? default(string) : lector.GetString(lector.GetOrdinal("CuartaMarcacion")),
                        QuintaMarcacion = lector.IsDBNull(lector.GetOrdinal("QuintaMarcacion")) ? default(string) : lector.GetString(lector.GetOrdinal("QuintaMarcacion")),
                        SextaMarcacion = lector.IsDBNull(lector.GetOrdinal("SextaMarcacion")) ? default(string) : lector.GetString(lector.GetOrdinal("SextaMarcacion")),
                        SeptimaMarcacion = lector.IsDBNull(lector.GetOrdinal("SeptimaMarcacion")) ? default(string) : lector.GetString(lector.GetOrdinal("SeptimaMarcacion")),
                        OctavaMarcacion = lector.IsDBNull(lector.GetOrdinal("OctavaMarcacion")) ? default(string) : lector.GetString(lector.GetOrdinal("OctavaMarcacion")),
                        Departamento = lector.IsDBNull(lector.GetOrdinal("Departamento")) ? default(string) : lector.GetString(lector.GetOrdinal("Departamento")),
                        Ubicacion = lector.IsDBNull(lector.GetOrdinal("Ubicacion")) ? default(string) : lector.GetString(lector.GetOrdinal("Ubicacion")),
                        Genero = lector.IsDBNull(lector.GetOrdinal("Genero")) ? default(string) : lector.GetString(lector.GetOrdinal("Genero")),
                        Categoria = lector.IsDBNull(lector.GetOrdinal("Categoria")) ? default(string) : lector.GetString(lector.GetOrdinal("Categoria")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad")),
                    });
                }
            }
            return retList;
        }
        public List<ReporteMarcacionesDto> ReporteMarcaciones(PaginationParameter objPaginationParameter)
        {
            List<ReporteMarcacionesDto> retList = new List<ReporteMarcacionesDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                listaParams.Add(new SqlParameter("@Parameters", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Parameters });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ReporteMarcacionesPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new ReporteMarcacionesDto
                    {
                        Id = lector.IsDBNull(lector.GetOrdinal("Id")) ? default(string) : lector.GetString(lector.GetOrdinal("Id")),
                        Fecha = lector.IsDBNull(lector.GetOrdinal("Fecha")) ? default(string) : lector.GetString(lector.GetOrdinal("Fecha")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Numero = lector.IsDBNull(lector.GetOrdinal("Numero")) ? default(string) : lector.GetString(lector.GetOrdinal("Numero")),
                        Empresa = lector.IsDBNull(lector.GetOrdinal("Empresa")) ? default(string) : lector.GetString(lector.GetOrdinal("Empresa")),
                        Terminal = lector.IsDBNull(lector.GetOrdinal("Terminal")) ? default(string) : lector.GetString(lector.GetOrdinal("Terminal")),
                        IdEmpresa = lector.IsDBNull(lector.GetOrdinal("IdEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresa")),
                        IdTerminal = lector.IsDBNull(lector.GetOrdinal("IdTerminal")) ? default(string) : lector.GetString(lector.GetOrdinal("IdTerminal")),
                        Marcacion = lector.IsDBNull(lector.GetOrdinal("Marcacion")) ? default(string) : lector.GetString(lector.GetOrdinal("Marcacion")),
                        Condicion = lector.IsDBNull(lector.GetOrdinal("Condicion")) ? default(string) : lector.GetString(lector.GetOrdinal("Condicion")),
                        Categoria = lector.IsDBNull(lector.GetOrdinal("Categoria")) ? default(string) : lector.GetString(lector.GetOrdinal("Categoria")),
                        TipoMarcacion = lector.IsDBNull(lector.GetOrdinal("TipoMarcacion")) ? default(string) : lector.GetString(lector.GetOrdinal("TipoMarcacion")),
                        Genero = lector.IsDBNull(lector.GetOrdinal("Genero")) ? default(string) : lector.GetString(lector.GetOrdinal("Genero")),
                        Departamento = lector.IsDBNull(lector.GetOrdinal("Departamento")) ? default(string) : lector.GetString(lector.GetOrdinal("Departamento")),
                        Ubicacion = lector.IsDBNull(lector.GetOrdinal("Ubicacion")) ? default(string) : lector.GetString(lector.GetOrdinal("Ubicacion")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad")),
                    });
                }
            }
            return retList;
        }
        public List<ReporteAsistenciaDto> ReporteAsistencia(PaginationParameter objPaginationParameter)
        {
            List<ReporteAsistenciaDto> retList = new List<ReporteAsistenciaDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                listaParams.Add(new SqlParameter("@Parameters", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Parameters });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ReporteAsistenciaPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new ReporteAsistenciaDto
                    {
                        Id = lector.IsDBNull(lector.GetOrdinal("Id")) ? default(string) : lector.GetString(lector.GetOrdinal("Id")),
                        Fecha = lector.IsDBNull(lector.GetOrdinal("Fecha")) ? default(string) : lector.GetString(lector.GetOrdinal("Fecha")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Numero = lector.IsDBNull(lector.GetOrdinal("Numero")) ? default(string) : lector.GetString(lector.GetOrdinal("Numero")),
                        Empresa = lector.IsDBNull(lector.GetOrdinal("Empresa")) ? default(string) : lector.GetString(lector.GetOrdinal("Empresa")),
                        Terminal = lector.IsDBNull(lector.GetOrdinal("Terminal")) ? default(string) : lector.GetString(lector.GetOrdinal("Terminal")),
                        IdEmpresa = lector.IsDBNull(lector.GetOrdinal("IdEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresa")),
                        Acumulado = lector.IsDBNull(lector.GetOrdinal("Acumulado")) ? default(string) : lector.GetString(lector.GetOrdinal("Acumulado")),
                        Salida = lector.IsDBNull(lector.GetOrdinal("Salida")) ? default(string) : lector.GetString(lector.GetOrdinal("Salida")),
                        Condicion = lector.IsDBNull(lector.GetOrdinal("Condicion")) ? default(string) : lector.GetString(lector.GetOrdinal("Condicion")),
                        Entrada = lector.IsDBNull(lector.GetOrdinal("Entrada")) ? default(string) : lector.GetString(lector.GetOrdinal("Entrada")),
                        Genero = lector.IsDBNull(lector.GetOrdinal("Genero")) ? default(string) : lector.GetString(lector.GetOrdinal("Genero")),
                        Departamento = lector.IsDBNull(lector.GetOrdinal("Departamento")) ? default(string) : lector.GetString(lector.GetOrdinal("Departamento")),
                        Ubicacion = lector.IsDBNull(lector.GetOrdinal("Ubicacion")) ? default(string) : lector.GetString(lector.GetOrdinal("Ubicacion")),
                        Marcaciones = lector.IsDBNull(lector.GetOrdinal("Marcaciones")) ? 0 : lector.GetInt32(lector.GetOrdinal("Marcaciones")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad")),
                    });
                }
            }
            return retList;
        }
    }
}

using Acces;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Datos
{
    public class MaquinariaDAL : IMaquinaria
    {
        public List<MaquinariaPaginationDto> PaginadoMaquinaria(PaginationParameter objPaginationParameter, MaquinariaFilterDto MaquinariaFilterDto)
        {
            List<MaquinariaPaginationDto> retList = new List<MaquinariaPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = MaquinariaFilterDto.Nombre });
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = MaquinariaFilterDto.IdEmpresa });
                listaParams.Add(new SqlParameter("@IdPadre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = MaquinariaFilterDto.IdPadre });
                listaParams.Add(new SqlParameter("@IdPadreSubcontratista", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = MaquinariaFilterDto.IdPadreSubcontratista });
                listaParams.Add(new SqlParameter("@Digitalizacion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = MaquinariaFilterDto.Digitalizacion });

                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_MaquinariaPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new MaquinariaPaginationDto
                    {
                        IdMaquinaria = lector.IsDBNull(lector.GetOrdinal("IdMaquinaria")) ? default(string) : lector.GetString(lector.GetOrdinal("IdMaquinaria")),
                        Categoria = lector.IsDBNull(lector.GetOrdinal("Categoria")) ? default(string) : lector.GetString(lector.GetOrdinal("Categoria")),
                        Codigo = lector.IsDBNull(lector.GetOrdinal("Codigo")) ? default(string) : lector.GetString(lector.GetOrdinal("Codigo")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion")),
                        Documento = lector.IsDBNull(lector.GetOrdinal("Documento")) ? default(string) : lector.GetString(lector.GetOrdinal("Documento")),
                        FinContrato = lector.IsDBNull(lector.GetOrdinal("FinContrato")) ? default(string) : lector.GetString(lector.GetOrdinal("FinContrato")),
                        InicioContrato = lector.IsDBNull(lector.GetOrdinal("InicioContrato")) ? default(string) : lector.GetString(lector.GetOrdinal("InicioContrato")),
                        IdPersona = lector.IsDBNull(lector.GetOrdinal("IdPersona")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPersona")),
                        Marca = lector.IsDBNull(lector.GetOrdinal("Marca")) ? default(string) : lector.GetString(lector.GetOrdinal("Marca")),
                        Modelo = lector.IsDBNull(lector.GetOrdinal("Modelo")) ? default(string) : lector.GetString(lector.GetOrdinal("Modelo")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        Observacion = lector.IsDBNull(lector.GetOrdinal("Observacion")) ? default(string) : lector.GetString(lector.GetOrdinal("Observacion")),
                        Proveedor = lector.IsDBNull(lector.GetOrdinal("Proveedor")) ? default(string) : lector.GetString(lector.GetOrdinal("Proveedor")),
                        Tipo = lector.IsDBNull(lector.GetOrdinal("Tipo")) ? default(string) : lector.GetString(lector.GetOrdinal("Tipo")),
                        Empresa = lector.IsDBNull(lector.GetOrdinal("Empresa")) ? default(string) : lector.GetString(lector.GetOrdinal("Empresa")),
                        Digitalizacion = lector.IsDBNull(lector.GetOrdinal("Digitalizacion")) ? default(string) : lector.GetString(lector.GetOrdinal("Digitalizacion")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad")),
                    });
                }
            }
            return retList;
        }

        public List<MaquinariaDigitalizacionPaginationDto> PaginadoMaquinariaDigitalizacion(PaginationParameter objPaginationParameter)
        {
            List<MaquinariaDigitalizacionPaginationDto> retList = new List<MaquinariaDigitalizacionPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_MaquinariaDigitalizacionPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new MaquinariaDigitalizacionPaginationDto
                    {
                        IdMaquinaria = lector.IsDBNull(lector.GetOrdinal("IdMaquinaria")) ? default(string) : lector.GetString(lector.GetOrdinal("IdMaquinaria")),
                        IdPersona = lector.IsDBNull(lector.GetOrdinal("IdPersona")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPersona")),
                        Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion")),
                        Codigo = lector.IsDBNull(lector.GetOrdinal("Codigo")) ? default(string) : lector.GetString(lector.GetOrdinal("Codigo")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Categoria = lector.IsDBNull(lector.GetOrdinal("Categoria")) ? default(string) : lector.GetString(lector.GetOrdinal("Categoria")),
                        Documento = lector.IsDBNull(lector.GetOrdinal("Documento")) ? default(string) : lector.GetString(lector.GetOrdinal("Documento")),
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
        public List<MaquinariaDigitalizacionPaginationDto> ListaMaquinariaDigitalizacion(string IdPersona)
        {
            List<MaquinariaDigitalizacionPaginationDto> retList = new List<MaquinariaDigitalizacionPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdPersona", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdPersona });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_MaquinariaDigitalizacionListar", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new MaquinariaDigitalizacionPaginationDto
                    {
                        IdMaquinaria = lector.IsDBNull(lector.GetOrdinal("IdMaquinaria")) ? default(string) : lector.GetString(lector.GetOrdinal("IdMaquinaria")),
                        Categoria = lector.IsDBNull(lector.GetOrdinal("Categoria")) ? default(string) : lector.GetString(lector.GetOrdinal("Categoria")),
                        Codigo = lector.IsDBNull(lector.GetOrdinal("Codigo")) ? default(string) : lector.GetString(lector.GetOrdinal("Codigo")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion")),
                        Documento = lector.IsDBNull(lector.GetOrdinal("Documento")) ? default(string) : lector.GetString(lector.GetOrdinal("Documento")),
                        FinContrato = lector.IsDBNull(lector.GetOrdinal("FinContrato")) ? default(string) : lector.GetString(lector.GetOrdinal("FinContrato")),
                        InicioContrato = lector.IsDBNull(lector.GetOrdinal("InicioContrato")) ? default(string) : lector.GetString(lector.GetOrdinal("InicioContrato")),
                        IdPersona = lector.IsDBNull(lector.GetOrdinal("IdPersona")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPersona")),
                        Marca = lector.IsDBNull(lector.GetOrdinal("Marca")) ? default(string) : lector.GetString(lector.GetOrdinal("Marca")),
                        Modelo = lector.IsDBNull(lector.GetOrdinal("Modelo")) ? default(string) : lector.GetString(lector.GetOrdinal("Modelo")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        Observacion = lector.IsDBNull(lector.GetOrdinal("Observacion")) ? default(string) : lector.GetString(lector.GetOrdinal("Observacion")),
                        Proveedor = lector.IsDBNull(lector.GetOrdinal("Proveedor")) ? default(string) : lector.GetString(lector.GetOrdinal("Proveedor")),
                        Tipo = lector.IsDBNull(lector.GetOrdinal("Tipo")) ? default(string) : lector.GetString(lector.GetOrdinal("Tipo")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad")),
                    });
                }
            }
            return retList;
        }

        public List<DigitalizacionExcelDto> ListaMaquinariaDigitalizacionExcel(string IdUsuario)
        {
            List<DigitalizacionExcelDto> retList = new List<DigitalizacionExcelDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdPersona", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdUsuario });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_MaquinariaDigitalizacionExcel", listaParams.ToArray());
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

        public List<Maquinaria> ListadoMaquinaria(string Maquinaria)
        {
            List<Maquinaria> retList = new List<Maquinaria>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Maquinaria });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_MaquinariaListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new Maquinaria
                    {
                        IdMaquinaria = lector.IsDBNull(lector.GetOrdinal("IdMaquinaria")) ? default(string) : lector.GetString(lector.GetOrdinal("IdMaquinaria")),
                        Categoria = lector.IsDBNull(lector.GetOrdinal("Categoria")) ? default(string) : lector.GetString(lector.GetOrdinal("Categoria")),
                        IdCategoria = lector.IsDBNull(lector.GetOrdinal("IdCategoria")) ? default(string) : lector.GetString(lector.GetOrdinal("IdCategoria")),
                        Codigo = lector.IsDBNull(lector.GetOrdinal("Codigo")) ? default(string) : lector.GetString(lector.GetOrdinal("Codigo")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion")),
                        Documento = lector.IsDBNull(lector.GetOrdinal("Documento")) ? default(string) : lector.GetString(lector.GetOrdinal("Documento")),
                        FinContrato = lector.IsDBNull(lector.GetOrdinal("FinContrato")) ? default(string) : lector.GetString(lector.GetOrdinal("FinContrato")),
                        InicioContrato = lector.IsDBNull(lector.GetOrdinal("InicioContrato")) ? default(string) : lector.GetString(lector.GetOrdinal("InicioContrato")),
                        IdPersona = lector.IsDBNull(lector.GetOrdinal("IdPersona")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPersona")),
                        Marca = lector.IsDBNull(lector.GetOrdinal("Marca")) ? default(string) : lector.GetString(lector.GetOrdinal("Marca")),
                        IdMarca = lector.IsDBNull(lector.GetOrdinal("IdMarca")) ? default(string) : lector.GetString(lector.GetOrdinal("IdMarca")),
                        Modelo = lector.IsDBNull(lector.GetOrdinal("Modelo")) ? default(string) : lector.GetString(lector.GetOrdinal("Modelo")),
                        IdModelo = lector.IsDBNull(lector.GetOrdinal("IdModelo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdModelo")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado")),
                        Observacion = lector.IsDBNull(lector.GetOrdinal("Observacion")) ? default(string) : lector.GetString(lector.GetOrdinal("Observacion")),
                        Proveedor = lector.IsDBNull(lector.GetOrdinal("Proveedor")) ? default(string) : lector.GetString(lector.GetOrdinal("Proveedor")),
                        IdProveedor = lector.IsDBNull(lector.GetOrdinal("IdProveedor")) ? default(string) : lector.GetString(lector.GetOrdinal("IdProveedor")),
                        Tipo = lector.IsDBNull(lector.GetOrdinal("Tipo")) ? default(string) : lector.GetString(lector.GetOrdinal("Tipo")),
                        IdTipo = lector.IsDBNull(lector.GetOrdinal("IdTipo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdTipo")),

                    });
                }
            }
            return retList;
        }


        public MaquinariaConsultaDto ConsultaMaquinaria(MaquinariaConsultaDto objMaquinaria)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdMaquinaria", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objMaquinaria.IdMaquinaria });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_MaquinariaConsulta", listaParams.ToArray());
                while (lector.Read())
                {
                    objMaquinaria.IdMaquinaria = lector.IsDBNull(lector.GetOrdinal("IdMaquinaria")) ? default(string) : lector.GetString(lector.GetOrdinal("IdMaquinaria"));
                    objMaquinaria.Categoria = lector.IsDBNull(lector.GetOrdinal("Categoria")) ? default(string) : lector.GetString(lector.GetOrdinal("Categoria"));
                    objMaquinaria.IdCategoria = lector.IsDBNull(lector.GetOrdinal("IdCategoria")) ? default(string) : lector.GetString(lector.GetOrdinal("IdCategoria"));
                    objMaquinaria.Codigo = lector.IsDBNull(lector.GetOrdinal("Codigo")) ? default(string) : lector.GetString(lector.GetOrdinal("Codigo"));
                    objMaquinaria.Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre"));
                    objMaquinaria.Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion"));
                    objMaquinaria.Documento = lector.IsDBNull(lector.GetOrdinal("Documento")) ? default(string) : lector.GetString(lector.GetOrdinal("Documento"));
                    objMaquinaria.FinContrato = lector.IsDBNull(lector.GetOrdinal("FinContrato")) ? default(string) : lector.GetString(lector.GetOrdinal("FinContrato"));
                    objMaquinaria.InicioContrato = lector.IsDBNull(lector.GetOrdinal("InicioContrato")) ? default(string) : lector.GetString(lector.GetOrdinal("InicioContrato"));
                    objMaquinaria.IdPersona = lector.IsDBNull(lector.GetOrdinal("IdPersona")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPersona"));
                    objMaquinaria.Marca = lector.IsDBNull(lector.GetOrdinal("Marca")) ? default(string) : lector.GetString(lector.GetOrdinal("Marca"));
                    objMaquinaria.IdMarca = lector.IsDBNull(lector.GetOrdinal("IdMarca")) ? default(string) : lector.GetString(lector.GetOrdinal("IdMarca"));
                    objMaquinaria.Modelo = lector.IsDBNull(lector.GetOrdinal("Modelo")) ? default(string) : lector.GetString(lector.GetOrdinal("Modelo"));
                    objMaquinaria.IdModelo = lector.IsDBNull(lector.GetOrdinal("IdModelo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdModelo"));
                    objMaquinaria.Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado"));
                    objMaquinaria.IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado"));
                    objMaquinaria.Observacion = lector.IsDBNull(lector.GetOrdinal("Observacion")) ? default(string) : lector.GetString(lector.GetOrdinal("Observacion"));
                    objMaquinaria.Proveedor = lector.IsDBNull(lector.GetOrdinal("Proveedor")) ? default(string) : lector.GetString(lector.GetOrdinal("Proveedor"));
                    objMaquinaria.IdProveedor = lector.IsDBNull(lector.GetOrdinal("IdProveedor")) ? default(string) : lector.GetString(lector.GetOrdinal("IdProveedor"));
                    objMaquinaria.IdEmpresa = lector.IsDBNull(lector.GetOrdinal("IdEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresa"));
                    objMaquinaria.Tipo = lector.IsDBNull(lector.GetOrdinal("Tipo")) ? default(string) : lector.GetString(lector.GetOrdinal("Tipo"));
                    objMaquinaria.IdTipo = lector.IsDBNull(lector.GetOrdinal("IdTipo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdTipo"));
                }
            }
            return objMaquinaria;
        }

        public string MantenimientoMaquinaria(Maquinaria objMaquinariap)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdMaquinaria", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objMaquinariap.IdMaquinaria });
                listaParams.Add(new SqlParameter("@IdPersona", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objMaquinariap.IdPersona });
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objMaquinariap.Nombre });
                listaParams.Add(new SqlParameter("@IdCategoria", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objMaquinariap.IdCategoria });
                listaParams.Add(new SqlParameter("@Codigo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objMaquinariap.Codigo });
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objMaquinariap.IdEmpresa });
                listaParams.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objMaquinariap.Descripcion });
                listaParams.Add(new SqlParameter("@Documento", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objMaquinariap.Documento });
                listaParams.Add(new SqlParameter("@FinContrato", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objMaquinariap.FinContrato });
                listaParams.Add(new SqlParameter("@InicioContrato", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objMaquinariap.InicioContrato });
                listaParams.Add(new SqlParameter("@IdMarca", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objMaquinariap.IdMarca });
                listaParams.Add(new SqlParameter("@IdModelo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objMaquinariap.IdModelo });
                listaParams.Add(new SqlParameter("@IdEstado", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objMaquinariap.IdEstado });
                listaParams.Add(new SqlParameter("@Observacion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objMaquinariap.Observacion });
                listaParams.Add(new SqlParameter("@IdProveedor", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objMaquinariap.IdProveedor });
                listaParams.Add(new SqlParameter("@IdTipo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objMaquinariap.IdTipo });

                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_MaquinariaMantenimiento", listaParams.ToArray()).ToString();
            }
            return r;
        }

        public string EliminarMaquinaria(string IdMaquinaria, int Accion)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdMaquinaria", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdMaquinaria });
                listaParams.Add(new SqlParameter("@Accion", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = Accion });
                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_MaquinariaEliminar", listaParams.ToArray()).ToString();
            }
            return r;
        }

    }
}
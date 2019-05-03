using Acces;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Datos
{
    public class VehiculoDAL : IVehiculo
    {
        public List<VehiculoPaginationDto> PaginadoVehiculo(PaginationParameter objPaginationParameter, VehiculoFilterDto VehiculoFilterDto)
        {
            List<VehiculoPaginationDto> retList = new List<VehiculoPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = VehiculoFilterDto.Nombre });
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = VehiculoFilterDto.IdEmpresa });
                listaParams.Add(new SqlParameter("@IdPadre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = VehiculoFilterDto.IdPadre });
                listaParams.Add(new SqlParameter("@IdPadreSubcontratista", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = VehiculoFilterDto.IdPadreSubcontratista });
                listaParams.Add(new SqlParameter("@Digitalizacion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = VehiculoFilterDto.Digitalizacion });

                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_VehiculoPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new VehiculoPaginationDto
                    {
                        IdVehiculo = lector.IsDBNull(lector.GetOrdinal("IdVehiculo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdVehiculo")),
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

        public List<VehiculoDigitalizacionPaginationDto> PaginadoVehiculoDigitalizacion(PaginationParameter objPaginationParameter)
        {
            List<VehiculoDigitalizacionPaginationDto> retList = new List<VehiculoDigitalizacionPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_VehiculoDigitalizacionPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new VehiculoDigitalizacionPaginationDto
                    {
                        IdVehiculo = lector.IsDBNull(lector.GetOrdinal("IdVehiculo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdVehiculo")),
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
        public List<VehiculoDigitalizacionPaginationDto> ListaVehiculoDigitalizacion(string IdPersona)
        {
            List<VehiculoDigitalizacionPaginationDto> retList = new List<VehiculoDigitalizacionPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdPersona", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdPersona });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_VehiculoDigitalizacionListar", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new VehiculoDigitalizacionPaginationDto
                    {
                        IdVehiculo = lector.IsDBNull(lector.GetOrdinal("IdVehiculo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdVehiculo")),
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

        public List<DigitalizacionExcelDto> ListaVehiculoDigitalizacionExcel(string IdUsuario)
        {
            List<DigitalizacionExcelDto> retList = new List<DigitalizacionExcelDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdPersona", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdUsuario });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_VehiculoDigitalizacionExcel", listaParams.ToArray());
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

        public List<Vehiculo> ListadoVehiculo(string Vehiculo)
        {
            List<Vehiculo> retList = new List<Vehiculo>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Vehiculo });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_VehiculoListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new Vehiculo
                    {
                        IdVehiculo = lector.IsDBNull(lector.GetOrdinal("IdVehiculo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdVehiculo")),
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


        public VehiculoConsultaDto ConsultaVehiculo(VehiculoConsultaDto objVehiculo)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdVehiculo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objVehiculo.IdVehiculo });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_VehiculoConsulta", listaParams.ToArray());
                while (lector.Read())
                {
                    objVehiculo.IdVehiculo = lector.IsDBNull(lector.GetOrdinal("IdVehiculo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdVehiculo"));
                    objVehiculo.Categoria = lector.IsDBNull(lector.GetOrdinal("Categoria")) ? default(string) : lector.GetString(lector.GetOrdinal("Categoria"));
                    objVehiculo.IdCategoria = lector.IsDBNull(lector.GetOrdinal("IdCategoria")) ? default(string) : lector.GetString(lector.GetOrdinal("IdCategoria"));
                    objVehiculo.Codigo = lector.IsDBNull(lector.GetOrdinal("Codigo")) ? default(string) : lector.GetString(lector.GetOrdinal("Codigo"));
                    objVehiculo.Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre"));
                    objVehiculo.Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion"));
                    objVehiculo.Documento = lector.IsDBNull(lector.GetOrdinal("Documento")) ? default(string) : lector.GetString(lector.GetOrdinal("Documento"));
                    objVehiculo.FinContrato = lector.IsDBNull(lector.GetOrdinal("FinContrato")) ? default(string) : lector.GetString(lector.GetOrdinal("FinContrato"));
                    objVehiculo.InicioContrato = lector.IsDBNull(lector.GetOrdinal("InicioContrato")) ? default(string) : lector.GetString(lector.GetOrdinal("InicioContrato"));
                    objVehiculo.IdPersona = lector.IsDBNull(lector.GetOrdinal("IdPersona")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPersona"));
                    objVehiculo.Marca = lector.IsDBNull(lector.GetOrdinal("Marca")) ? default(string) : lector.GetString(lector.GetOrdinal("Marca"));
                    objVehiculo.IdMarca = lector.IsDBNull(lector.GetOrdinal("IdMarca")) ? default(string) : lector.GetString(lector.GetOrdinal("IdMarca"));
                    objVehiculo.Modelo = lector.IsDBNull(lector.GetOrdinal("Modelo")) ? default(string) : lector.GetString(lector.GetOrdinal("Modelo"));
                    objVehiculo.IdModelo = lector.IsDBNull(lector.GetOrdinal("IdModelo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdModelo"));
                    objVehiculo.Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado"));
                    objVehiculo.IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado"));
                    objVehiculo.Observacion = lector.IsDBNull(lector.GetOrdinal("Observacion")) ? default(string) : lector.GetString(lector.GetOrdinal("Observacion"));
                    objVehiculo.Proveedor = lector.IsDBNull(lector.GetOrdinal("Proveedor")) ? default(string) : lector.GetString(lector.GetOrdinal("Proveedor"));
                    objVehiculo.IdProveedor = lector.IsDBNull(lector.GetOrdinal("IdProveedor")) ? default(string) : lector.GetString(lector.GetOrdinal("IdProveedor"));
                    objVehiculo.IdEmpresa = lector.IsDBNull(lector.GetOrdinal("IdEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresa"));
                    objVehiculo.Tipo = lector.IsDBNull(lector.GetOrdinal("Tipo")) ? default(string) : lector.GetString(lector.GetOrdinal("Tipo"));
                    objVehiculo.IdTipo = lector.IsDBNull(lector.GetOrdinal("IdTipo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdTipo"));
                }
            }
            return objVehiculo;
        }

        public string MantenimientoVehiculo(Vehiculo objVehiculop)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdVehiculo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objVehiculop.IdVehiculo });
                listaParams.Add(new SqlParameter("@IdPersona", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objVehiculop.IdPersona });
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objVehiculop.Nombre });
                listaParams.Add(new SqlParameter("@IdCategoria", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objVehiculop.IdCategoria });
                listaParams.Add(new SqlParameter("@Codigo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objVehiculop.Codigo });
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objVehiculop.IdEmpresa });
                listaParams.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objVehiculop.Descripcion });
                listaParams.Add(new SqlParameter("@Documento", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objVehiculop.Documento });
                listaParams.Add(new SqlParameter("@FinContrato", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objVehiculop.FinContrato });
                listaParams.Add(new SqlParameter("@InicioContrato", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objVehiculop.InicioContrato });
                listaParams.Add(new SqlParameter("@IdMarca", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objVehiculop.IdMarca });
                listaParams.Add(new SqlParameter("@IdModelo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objVehiculop.IdModelo });
                listaParams.Add(new SqlParameter("@IdEstado", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objVehiculop.IdEstado });
                listaParams.Add(new SqlParameter("@Observacion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objVehiculop.Observacion });
                listaParams.Add(new SqlParameter("@IdProveedor", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objVehiculop.IdProveedor });
                listaParams.Add(new SqlParameter("@IdTipo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objVehiculop.IdTipo });

                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_VehiculoMantenimiento", listaParams.ToArray()).ToString();
            }
            return r;
        }

        public string EliminarVehiculo(string IdVehiculo, int Accion)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdVehiculo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdVehiculo });
                listaParams.Add(new SqlParameter("@Accion", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = Accion });
                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_VehiculoEliminar", listaParams.ToArray()).ToString();
            }
            return r;
        }

    }
}
using Acces;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Datos
{
    public class ColaboradorDAL: IColaborador
    {
        public List<ColaboradorPaginationDto> PaginadoColaborador(PaginationParameter objPaginationParameter, ColaboradorFilterDto colaboradorFilterDto)
        {
            List<ColaboradorPaginationDto> retList = new List<ColaboradorPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = colaboradorFilterDto.Nombre});
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = colaboradorFilterDto.IdEmpresa});
                listaParams.Add(new SqlParameter("@IdPadre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = colaboradorFilterDto.IdPadre});
                listaParams.Add(new SqlParameter("@IdPadreSubcontratista", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = colaboradorFilterDto.IdPadreSubcontratista});
                listaParams.Add(new SqlParameter("@Digitalizacion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = colaboradorFilterDto.Digitalizacion });

                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ColaboradorPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new ColaboradorPaginationDto
                    {
                        IdColaborador = lector.IsDBNull(lector.GetOrdinal("IdColaborador")) ? default(string) : lector.GetString(lector.GetOrdinal("IdColaborador")),
                        ApellidoMaterno = lector.IsDBNull(lector.GetOrdinal("ApellidoMaterno")) ? default(string) : lector.GetString(lector.GetOrdinal("ApellidoMaterno")),
                        ApellidoPaterno = lector.IsDBNull(lector.GetOrdinal("ApellidoPaterno")) ? default(string) : lector.GetString(lector.GetOrdinal("ApellidoPaterno")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Departamento = lector.IsDBNull(lector.GetOrdinal("Departamento")) ? default(string) : lector.GetString(lector.GetOrdinal("Departamento")),
                        FechaNacimiento = lector.IsDBNull(lector.GetOrdinal("FechaNacimiento")) ? default(string) : lector.GetString(lector.GetOrdinal("FechaNacimiento")),
                        Sexo = lector.IsDBNull(lector.GetOrdinal("Genero")) ? default(string) : lector.GetString(lector.GetOrdinal("Genero")),
                        Ubicacion = lector.IsDBNull(lector.GetOrdinal("Ubicacion")) ? default(string) : lector.GetString(lector.GetOrdinal("Ubicacion")),
                        Entidad = lector.IsDBNull(lector.GetOrdinal("Entidad")) ? default(string) : lector.GetString(lector.GetOrdinal("Entidad")),
                        Digitalizacion = lector.IsDBNull(lector.GetOrdinal("Digitalizacion")) ? default(string) : lector.GetString(lector.GetOrdinal("Digitalizacion")),
                        Empresa = lector.IsDBNull(lector.GetOrdinal("Empresa")) ? default(string) : lector.GetString(lector.GetOrdinal("Empresa")),
                        Estado = lector.IsDBNull(lector.GetOrdinal("Estado")) ? default(string) : lector.GetString(lector.GetOrdinal("Estado")),
                        PadreSubcontratista = lector.IsDBNull(lector.GetOrdinal("PadreSubcontratista")) ? default(string) : lector.GetString(lector.GetOrdinal("PadreSubcontratista")),
                        Cantidad = lector.IsDBNull(lector.GetOrdinal("Cantidad")) ? 0 : lector.GetInt32(lector.GetOrdinal("Cantidad")),
                    });
                }
            }
            return retList;
        }

        public List<ColaboradorDigitalizacionPaginationDto> PaginadoColaboradorDigitalizacion(PaginationParameter objPaginationParameter)
        {
            List<ColaboradorDigitalizacionPaginationDto> retList = new List<ColaboradorDigitalizacionPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@WhereFilter", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.WhereFilter });
                listaParams.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objPaginationParameter.OrderBy });
                listaParams.Add(new SqlParameter("@Start", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.Start });
                listaParams.Add(new SqlParameter("@AmountRows", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = objPaginationParameter.AmountRows });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ColaboradorDigitalizacionPaginado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new ColaboradorDigitalizacionPaginationDto
                    {
                        IdColaborador = lector.IsDBNull(lector.GetOrdinal("IdColaborador")) ? default(string) : lector.GetString(lector.GetOrdinal("IdColaborador")),
                        IdPersona = lector.IsDBNull(lector.GetOrdinal("IdPersona")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPersona")),
                        ApellidoMaterno = lector.IsDBNull(lector.GetOrdinal("ApellidoMaterno")) ? default(string) : lector.GetString(lector.GetOrdinal("ApellidoMaterno")),
                        ApellidoPaterno = lector.IsDBNull(lector.GetOrdinal("ApellidoPaterno")) ? default(string) : lector.GetString(lector.GetOrdinal("ApellidoPaterno")),
                        Nombres = lector.IsDBNull(lector.GetOrdinal("Nombres")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombres")),
                        Categoria = lector.IsDBNull(lector.GetOrdinal("Categoria")) ? default(string) : lector.GetString(lector.GetOrdinal("Categoria")),
                        Documento = lector.IsDBNull(lector.GetOrdinal("Documento")) ? default(string) : lector.GetString(lector.GetOrdinal("Documento")),
                        Alias = lector.IsDBNull(lector.GetOrdinal("Alias")) ? default(string) : lector.GetString(lector.GetOrdinal("Alias")),
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
        public List<ColaboradorDigitalizacionPaginationDto> ListaColaboradorDigitalizacion(string IdPersona)
        {
            List<ColaboradorDigitalizacionPaginationDto> retList = new List<ColaboradorDigitalizacionPaginationDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdPersona", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdPersona });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ColaboradorDigitalizacionListar", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new ColaboradorDigitalizacionPaginationDto
                    {
                        IdColaborador = lector.IsDBNull(lector.GetOrdinal("IdColaborador")) ? default(string) : lector.GetString(lector.GetOrdinal("IdColaborador")),
                        IdPersona = lector.IsDBNull(lector.GetOrdinal("IdPersona")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPersona")),
                        ApellidoMaterno = lector.IsDBNull(lector.GetOrdinal("ApellidoMaterno")) ? default(string) : lector.GetString(lector.GetOrdinal("ApellidoMaterno")),
                        ApellidoPaterno = lector.IsDBNull(lector.GetOrdinal("ApellidoPaterno")) ? default(string) : lector.GetString(lector.GetOrdinal("ApellidoPaterno")),
                        Nombres = lector.IsDBNull(lector.GetOrdinal("Nombres")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombres")),
                        Categoria = lector.IsDBNull(lector.GetOrdinal("Categoria")) ? default(string) : lector.GetString(lector.GetOrdinal("Categoria")),
                        Documento = lector.IsDBNull(lector.GetOrdinal("Documento")) ? default(string) : lector.GetString(lector.GetOrdinal("Documento")),
                        Alias = lector.IsDBNull(lector.GetOrdinal("Alias")) ? default(string) : lector.GetString(lector.GetOrdinal("Alias")),
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
                    });
                }
            }
            return retList;
        }

        public List<DigitalizacionExcelDto> ListaColaboradorDigitalizacionExcel(string IdUsuario)
        {
            List<DigitalizacionExcelDto> retList = new List<DigitalizacionExcelDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdPersona", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdUsuario });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ColaboradorDigitalizacionExcel", listaParams.ToArray());
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

        public List<Colaborador> ListadoColaborador(string Colaborador)
        {
            List<Colaborador> retList = new List<Colaborador>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Colaborador });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ColaboradorListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new Colaborador
                    {
                    IdColaborador = lector.IsDBNull(lector.GetOrdinal("IdColaborador")) ? default(string) : lector.GetString(lector.GetOrdinal("IdColaborador")),
                    ApellidoMaterno = lector.IsDBNull(lector.GetOrdinal("ApellidoMaterno")) ? default(string) : lector.GetString(lector.GetOrdinal("ApellidoMaterno")),
                    ApellidoPaterno = lector.IsDBNull(lector.GetOrdinal("ApellidoPaterno")) ? default(string) : lector.GetString(lector.GetOrdinal("ApellidoPaterno")),
                    Nombre = lector.IsDBNull(lector.GetOrdinal("Nombres")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombres")),
                    Alias = lector.IsDBNull(lector.GetOrdinal("Alias")) ? default(string) : lector.GetString(lector.GetOrdinal("Alias")),
                    FechaNacimiento = lector.IsDBNull(lector.GetOrdinal("FechaNacimiento")) ? default(string) : lector.GetString(lector.GetOrdinal("FechaNacimiento")),
                    Sexo = lector.IsDBNull(lector.GetOrdinal("Genero")) ? default(string) : lector.GetString(lector.GetOrdinal("Genero")),
                    IdUbicacion = lector.IsDBNull(lector.GetOrdinal("IdUbicacion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdUbicacion")),
                    IdDepartamento = lector.IsDBNull(lector.GetOrdinal("IdDepartamento")) ? default(string) : lector.GetString(lector.GetOrdinal("IdDepartamento")),


                    IdDistrito = lector.IsDBNull(lector.GetOrdinal("IdDistrito")) ? default(string) : lector.GetString(lector.GetOrdinal("IdDistrito")),
                    IdUDepartamento = lector.IsDBNull(lector.GetOrdinal("IdUDepartamento")) ? default(string) : lector.GetString(lector.GetOrdinal("IdUDepartamento")),
                    IdProvincia = lector.IsDBNull(lector.GetOrdinal("IdProvincia")) ? default(string) : lector.GetString(lector.GetOrdinal("IdProvincia")),

                    IdCategoria = lector.IsDBNull(lector.GetOrdinal("IdCategoria")) ? default(string) : lector.GetString(lector.GetOrdinal("IdCategoria")),
                    IdCondicion = lector.IsDBNull(lector.GetOrdinal("IdCondicion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdCondicion")),
                    Direccion = lector.IsDBNull(lector.GetOrdinal("Direccion")) ? default(string) : lector.GetString(lector.GetOrdinal("Direccion")),
                    NumeroDocumento = lector.IsDBNull(lector.GetOrdinal("NumeroDocumento")) ? default(string) : lector.GetString(lector.GetOrdinal("NumeroDocumento")),
                    IdEmpresa = lector.IsDBNull(lector.GetOrdinal("IdEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresa")),
                    IdPersona = lector.IsDBNull(lector.GetOrdinal("IdPersona")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPersona")),

                    Telefono = lector.IsDBNull(lector.GetOrdinal("Telefono")) ? default(string) : lector.GetString(lector.GetOrdinal("Telefono")),
                    Correo = lector.IsDBNull(lector.GetOrdinal("Correo")) ? default(string) : lector.GetString(lector.GetOrdinal("Correo")),
                    Tarjeta = lector.IsDBNull(lector.GetOrdinal("Tarjeta")) ? default(string) : lector.GetString(lector.GetOrdinal("Tarjeta")),
                    IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado")),

                    IdTipoLugar = lector.IsDBNull(lector.GetOrdinal("IdTipoLugar")) ? default(string) : lector.GetString(lector.GetOrdinal("IdTipoLugar")),
                    Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion")),
                    Mensaje = lector.IsDBNull(lector.GetOrdinal("Mensaje")) ? default(string) : lector.GetString(lector.GetOrdinal("Mensaje")),
                    Digitalizado = lector.IsDBNull(lector.GetOrdinal("Digitalizado")) ?0 : lector.GetInt32(lector.GetOrdinal("Digitalizado")),
                    IdUsuario = lector.IsDBNull(lector.GetOrdinal("IdUsuario")) ? default(string) : lector.GetString(lector.GetOrdinal("IdUsuario")),
                    Enviado = lector.IsDBNull(lector.GetOrdinal("Enviado")) ? 0 : lector.GetInt32(lector.GetOrdinal("Enviado")),

                    });
                }
            }
            return retList;
        }

        public List<ColaboradorTiposDto> ListadoColaboradorTelefono(string Colaborador)
        {
            List<ColaboradorTiposDto> retList = new List<ColaboradorTiposDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdPersona", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Colaborador });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ColaboradorTelefonoListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new ColaboradorTiposDto
                    {
                        IdPersona = lector.IsDBNull(lector.GetOrdinal("IdPersona")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPersona")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Tipo = lector.IsDBNull(lector.GetOrdinal("Tipo")) ? default(string) : lector.GetString(lector.GetOrdinal("Tipo")),
                        IdTipo = lector.IsDBNull(lector.GetOrdinal("IdTipo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdTipo")),
                    });
                }
            }
            return retList;
        }

        public List<ColaboradorTiposDto> ListadoColaboradorCorreo(string Colaborador)
        {
            List<ColaboradorTiposDto> retList = new List<ColaboradorTiposDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdPersona", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Colaborador });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ColaboradorCorreoListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new ColaboradorTiposDto
                    {
                        IdPersona = lector.IsDBNull(lector.GetOrdinal("IdPersona")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPersona")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Tipo = lector.IsDBNull(lector.GetOrdinal("Tipo")) ? default(string) : lector.GetString(lector.GetOrdinal("Tipo")),
                        IdTipo = lector.IsDBNull(lector.GetOrdinal("IdTipo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdTipo")),
                    });
                }
            }
            return retList;
        }

        public List<ColaboradorTiposDto> ListadoColaboradorTarjeta(string Colaborador)
        {
            List<ColaboradorTiposDto> retList = new List<ColaboradorTiposDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdPersona", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Colaborador });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ColaboradorTarjetaListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new ColaboradorTiposDto
                    {
                        IdPersona = lector.IsDBNull(lector.GetOrdinal("IdPersona")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPersona")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Tipo = lector.IsDBNull(lector.GetOrdinal("Tipo")) ? default(string) : lector.GetString(lector.GetOrdinal("Tipo")),
                        IdTipo = lector.IsDBNull(lector.GetOrdinal("IdTipo")) ? default(string) : lector.GetString(lector.GetOrdinal("IdTipo")),
                    });
                }
            }
            return retList;
        }

        public ColaboradorConsultaDto ConsultaColaborador(ColaboradorConsultaDto objColaborador)
        {
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdColaborador", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaborador.IdColaborador });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ColaboradorConsulta", listaParams.ToArray());
                while (lector.Read())
                {
                    objColaborador.IdColaborador = lector.IsDBNull(lector.GetOrdinal("IdColaborador")) ? default(string) : lector.GetString(lector.GetOrdinal("IdColaborador"));
                    objColaborador.ApellidoMaterno = lector.IsDBNull(lector.GetOrdinal("ApellidoMaterno")) ? default(string) : lector.GetString(lector.GetOrdinal("ApellidoMaterno"));
                    objColaborador.ApellidoPaterno = lector.IsDBNull(lector.GetOrdinal("ApellidoPaterno")) ? default(string) : lector.GetString(lector.GetOrdinal("ApellidoPaterno"));
                    objColaborador.Nombre = lector.IsDBNull(lector.GetOrdinal("Nombres")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombres"));
                    objColaborador.Alias = lector.IsDBNull(lector.GetOrdinal("Alias")) ? default(string) : lector.GetString(lector.GetOrdinal("Alias"));
                    objColaborador.FechaNacimiento = lector.IsDBNull(lector.GetOrdinal("FechaNacimiento")) ? default(string) : lector.GetString(lector.GetOrdinal("FechaNacimiento"));
                    objColaborador.Sexo = lector.IsDBNull(lector.GetOrdinal("Genero")) ? default(string) : lector.GetString(lector.GetOrdinal("Genero"));
                    objColaborador.IdUbicacion = lector.IsDBNull(lector.GetOrdinal("IdUbicacion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdUbicacion"));
                    objColaborador.IdDepartamento = lector.IsDBNull(lector.GetOrdinal("IdDepartamento")) ? default(string) : lector.GetString(lector.GetOrdinal("IdDepartamento"));


                    objColaborador.IdDistrito = lector.IsDBNull(lector.GetOrdinal("IdDistrito")) ? default(string) : lector.GetString(lector.GetOrdinal("IdDistrito"));
                    objColaborador.IdUDepartamento = lector.IsDBNull(lector.GetOrdinal("IdUDepartamento")) ? default(string) : lector.GetString(lector.GetOrdinal("IdUDepartamento"));
                    objColaborador.IdProvincia = lector.IsDBNull(lector.GetOrdinal("IdProvincia")) ? default(string) : lector.GetString(lector.GetOrdinal("IdProvincia"));

                    objColaborador.IdCategoria = lector.IsDBNull(lector.GetOrdinal("IdCategoria")) ? default(string) : lector.GetString(lector.GetOrdinal("IdCategoria"));
                    objColaborador.IdCondicion = lector.IsDBNull(lector.GetOrdinal("IdCondicion")) ? default(string) : lector.GetString(lector.GetOrdinal("IdCondicion"));
                    objColaborador.Direccion = lector.IsDBNull(lector.GetOrdinal("Direccion")) ? default(string) : lector.GetString(lector.GetOrdinal("Direccion"));
                    objColaborador.NumeroDocumento = lector.IsDBNull(lector.GetOrdinal("NumeroDocumento")) ? default(string) : lector.GetString(lector.GetOrdinal("NumeroDocumento"));
                    objColaborador.IdEmpresaPrincipal = lector.IsDBNull(lector.GetOrdinal("IdEmpresaPrincipal")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresaPrincipal"));
                    objColaborador.IdEmpresa = lector.IsDBNull(lector.GetOrdinal("IdEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresa"));
                    objColaborador.IdPersona = lector.IsDBNull(lector.GetOrdinal("IdPersona")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPersona"));

                    objColaborador.Telefono = lector.IsDBNull(lector.GetOrdinal("Telefono")) ? default(string) : lector.GetString(lector.GetOrdinal("Telefono"));
                    objColaborador.Correo = lector.IsDBNull(lector.GetOrdinal("Correo")) ? default(string) : lector.GetString(lector.GetOrdinal("Correo"));
                    objColaborador.Tarjeta = lector.IsDBNull(lector.GetOrdinal("Tarjeta")) ? default(string) : lector.GetString(lector.GetOrdinal("Tarjeta"));
                    objColaborador.IdEstado = lector.IsDBNull(lector.GetOrdinal("IdEstado")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEstado"));

                    objColaborador.IdTipoLugar = lector.IsDBNull(lector.GetOrdinal("IdTipoLugar")) ? default(string) : lector.GetString(lector.GetOrdinal("IdTipoLugar"));
                    objColaborador.Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? default(string) : lector.GetString(lector.GetOrdinal("Descripcion"));
                    objColaborador.Foto = lector.IsDBNull(lector.GetOrdinal("Foto")) ? default(string) : lector.GetString(lector.GetOrdinal("Foto"));
                }
            }
            return objColaborador;
        }

        public string MantenimientoColaborador(Colaborador objColaboradorp)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdColaborador", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorp.IdColaborador });
                listaParams.Add(new SqlParameter("@IdPersona", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorp.IdPersona });
                listaParams.Add(new SqlParameter("@Nombres", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorp.Nombre });
                listaParams.Add(new SqlParameter("@ApellidoMaterno", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorp.ApellidoMaterno });
                listaParams.Add(new SqlParameter("@ApellidoPaterno", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorp.ApellidoPaterno });
                listaParams.Add(new SqlParameter("@IdDepartamento", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorp.IdDepartamento});
                listaParams.Add(new SqlParameter("@IdUbicacion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorp.IdUbicacion });
                listaParams.Add(new SqlParameter("@FechaNacimiento", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorp.FechaNacimiento });
                listaParams.Add(new SqlParameter("@Genero", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorp.Sexo });
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorp.IdEmpresa });
                listaParams.Add(new SqlParameter("@Foto", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorp.Foto });
                //listaParams.Add(new SqlParameter("@Tarjeta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorp.Tarjeta });
                listaParams.Add(new SqlParameter("@IdUDepartamento", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorp.IdUDepartamento });
                listaParams.Add(new SqlParameter("@IdProvincia", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorp.IdProvincia });
                listaParams.Add(new SqlParameter("@IdDistrito", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorp.IdDistrito });
                listaParams.Add(new SqlParameter("@IdCondicion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorp.IdCondicion });
                listaParams.Add(new SqlParameter("@Direccion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorp.Direccion });
                listaParams.Add(new SqlParameter("@NumeroDocumento", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorp.NumeroDocumento });
                listaParams.Add(new SqlParameter("@IdCategoria", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorp.IdCategoria });
                listaParams.Add(new SqlParameter("@Alias", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorp.Alias });
                listaParams.Add(new SqlParameter("@IdEstado", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorp.IdEstado });
                listaParams.Add(new SqlParameter("@IdArea", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorp.IdArea });
                listaParams.Add(new SqlParameter("@IdTipoLugar", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorp.IdTipoLugar });
                listaParams.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorp.Descripcion });

                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_ColaboradorMantenimiento", listaParams.ToArray()).ToString();
            }
            return r;
        }

        public string MantenimientoTelefonoColaborador(ColaboradorTiposDto objColaboradorTiposDto)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdTipo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorTiposDto.IdTipo });
                listaParams.Add(new SqlParameter("@IdPersona", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorTiposDto.IdPersona });
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorTiposDto.Nombre });
                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_ColaboradorTelefonoMantenimiento", listaParams.ToArray()).ToString();
            }
            return r;
        }
        public string EliminarTelefonoColaborador(ColaboradorTiposDto objColaboradorTiposDto)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdTipo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorTiposDto.IdTipo });
                listaParams.Add(new SqlParameter("@IdPersona", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorTiposDto.IdPersona });
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorTiposDto.Nombre });
                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_ColaboradorTelefonoEliminar", listaParams.ToArray()).ToString();
            }
            return r;
        }

        public string MantenimientoCorreoColaborador(ColaboradorTiposDto objColaboradorTiposDto)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdTipo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorTiposDto.IdTipo });
                listaParams.Add(new SqlParameter("@IdPersona", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorTiposDto.IdPersona });
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorTiposDto.Nombre });
                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_ColaboradorCorreoMantenimiento", listaParams.ToArray()).ToString();
            }
            return r;
        }
        public string EliminarCorreoColaborador(ColaboradorTiposDto objColaboradorTiposDto)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdTipo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorTiposDto.IdTipo });
                listaParams.Add(new SqlParameter("@IdPersona", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorTiposDto.IdPersona });
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorTiposDto.Nombre });
                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_ColaboradorCorreoEliminar", listaParams.ToArray()).ToString();
            }
            return r;
        }


        public string MantenimientoTarjetaColaborador(ColaboradorTiposDto objColaboradorTiposDto)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdTipo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorTiposDto.IdTipo });
                listaParams.Add(new SqlParameter("@IdPersona", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorTiposDto.IdPersona });
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorTiposDto.Nombre });
                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_ColaboradorTarjetaMantenimiento", listaParams.ToArray()).ToString();
            }
            return r;
        }
        public string EliminarTarjetaColaborador(ColaboradorTiposDto objColaboradorTiposDto)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdTipo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorTiposDto.IdTipo });
                listaParams.Add(new SqlParameter("@IdPersona", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorTiposDto.IdPersona });
                listaParams.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objColaboradorTiposDto.Nombre });
                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_ColaboradorTarjetaEliminar", listaParams.ToArray()).ToString();
            }
            return r;
        }
        public string EliminarColaborador(string IdColaborador, int Accion)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdColaborador", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdColaborador });
                listaParams.Add(new SqlParameter("@Accion", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = Accion });
                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_ColaboradorEliminar", listaParams.ToArray()).ToString();
            }
            return r;
        }
    }
}

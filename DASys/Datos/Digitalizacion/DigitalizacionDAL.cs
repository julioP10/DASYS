using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Acces;
namespace Datos
{
    public class DigitalizacionDAL: IDigitalizacion
    {
        public string MantenimientoDigitalizacion(Digitalizacion objDigitalizacion)
        {
            string r = "";
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdPersona", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objDigitalizacion.IdPersona });
                listaParams.Add(new SqlParameter("@FechaVencimiento", SqlDbType.DateTime) { Direction = ParameterDirection.Input, Value = objDigitalizacion.FechaVencimiento });
                listaParams.Add(new SqlParameter("@IdDigitalizacion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objDigitalizacion.IdDigitalizacion });
                listaParams.Add(new SqlParameter("@IdDocumento", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objDigitalizacion.IdDocumento });
                listaParams.Add(new SqlParameter("@Observacion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objDigitalizacion.Observacion });
                listaParams.Add(new SqlParameter("@IdEstado", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objDigitalizacion.IdEstado });
                listaParams.Add(new SqlParameter("@Opcion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objDigitalizacion.Opcion });
                listaParams.Add(new SqlParameter("@IdUsuario", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objDigitalizacion.IdUsuario });
                r = SQLHelper.ExecuteScalar(conn, System.Data.CommandType.StoredProcedure, @"Sp_DigitalizacionMantenimiento", listaParams.ToArray()).ToString();
            }
            return r;
        }
        public int EliminarDigitalizacion(string IdDigitalizacion)
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdDigitalizacion", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value =IdDigitalizacion });
                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_DigitalizacionEliminar", listaParams.ToArray());
            }
            return r;
        }
        public List<DigitalizacionCorreoDto> ListaDigtalizados(UsuarioActualDto objUsuarioActualDto)
        {
            List<DigitalizacionCorreoDto> retList = new List<DigitalizacionCorreoDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuarioActualDto.IdEmpresa });
                listaParams.Add(new SqlParameter("@Tipo", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = objUsuarioActualDto.DescripcionAdcional });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_CorreDigitalizacionListado", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DigitalizacionCorreoDto
                    {
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Numero = lector.IsDBNull(lector.GetOrdinal("Numero")) ? default(string) : lector.GetString(lector.GetOrdinal("Numero")),
                        Empresa = lector.IsDBNull(lector.GetOrdinal("Empresa")) ? default(string) : lector.GetString(lector.GetOrdinal("Empresa")),
                        Categoria = lector.IsDBNull(lector.GetOrdinal("Categoria")) ? default(string) : lector.GetString(lector.GetOrdinal("Categoria")),
                        IdEmpresa = lector.IsDBNull(lector.GetOrdinal("IdEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("IdEmpresa")),
                        IdPersona = lector.IsDBNull(lector.GetOrdinal("IdPersona")) ? default(string) : lector.GetString(lector.GetOrdinal("IdPersona")),
                        TipoEmpresa = lector.IsDBNull(lector.GetOrdinal("TipoEmpresa")) ? default(string) : lector.GetString(lector.GetOrdinal("TipoEmpresa")),
                        Enviado = lector.IsDBNull(lector.GetOrdinal("Enviado")) ? 0 : lector.GetInt32(lector.GetOrdinal("Enviado")),
                        EstadoDigitalizacion = lector.IsDBNull(lector.GetOrdinal("EstadoDigitalizacion")) ? default(string) : lector.GetString(lector.GetOrdinal("EstadoDigitalizacion")),
                    });
                }
            }
            return retList;
        }
      
    }
}

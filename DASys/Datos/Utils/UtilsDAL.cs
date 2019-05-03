using Entidad;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acces;

namespace Datos
{
    public class UtilsDAL
    {
        public static int IniciarSistema()
        {
            int r = 0;
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                r = SQLHelper.ExecuteNonQuery(conn, System.Data.CommandType.StoredProcedure, @"Sp_Iniciar_Sistema", listaParams.ToArray());
            }
            return r;
        }
        public static List<DropDownDto> ListaArea(string Consulta,string Empresa)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Empresa });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_AreaDropDown", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Valor1 = lector.IsDBNull(lector.GetOrdinal("Valor1")) ? default(string) : lector.GetString(lector.GetOrdinal("Valor1")),
                    });
                }
            }
            return retList;
        }
        public static  List<DropDownDto> ListaCategoria(string Consulta, string empresa)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = empresa });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_CategoriaDropDown", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Valor1 = lector.IsDBNull(lector.GetOrdinal("Valor1")) ? default(string) : lector.GetString(lector.GetOrdinal("Valor1")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),

                    });
                }
            }
            return retList;
        }
        public static  List<DropDownDto> ListaConfiguracion(string Consulta)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ConfiguracionDropDown", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),

                    });
                }
            }
            return retList;
        }
        public static  List<DropDownDto> ListaEntidad(string Consulta)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_EntidadDropDown", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Valor1 = lector.IsDBNull(lector.GetOrdinal("Valor1")) ? default(string) : lector.GetString(lector.GetOrdinal("Valor1")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),

                    });
                }
            }
            return retList;
        }
        public static List<DropDownDto> ListaMarcaEntidad(string Consulta)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_EntidadMarcaDropDown", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Valor1 = lector.IsDBNull(lector.GetOrdinal("Valor1")) ? default(string) : lector.GetString(lector.GetOrdinal("Valor1")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),

                    });
                }
            }
            return retList;
        }
        

        public static  List<DropDownDto> ListaEmpresa(string Consulta)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_EmpresaDropDown", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Valor1 = lector.IsDBNull(lector.GetOrdinal("Valor1")) ? default(string) : lector.GetString(lector.GetOrdinal("Valor1")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),

                    });
                }
            }
            return retList;
        }

        public static List<DropDownDto> ListaEmpresaColaborador(string Consulta,string empresa)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = empresa });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_EmpresaColaboradorDropDown", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Valor1 = lector.IsDBNull(lector.GetOrdinal("Valor1")) ? default(string) : lector.GetString(lector.GetOrdinal("Valor1")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),

                    });
                }
            }
            return retList;
        }
        
        public static List<DropDownDto> ListaEmpresaSuperUsuario(string Consulta)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_EmpresaXSuperUsuarioDropDown", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Valor1 = lector.IsDBNull(lector.GetOrdinal("Valor1")) ? default(string) : lector.GetString(lector.GetOrdinal("Valor1")),
                        Valor5 = lector.IsDBNull(lector.GetOrdinal("Valor5")) ? default(string) : lector.GetString(lector.GetOrdinal("Valor5")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),

                    });
                }
            }
            return retList;
        }
        public static List<DropDownDto> ListaEmpresaUsuario(string Consulta,string empresa)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = empresa });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_EmpresaUsuarioDropDown_X_Empresa", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Valor1 = lector.IsDBNull(lector.GetOrdinal("Valor1")) ? default(string) : lector.GetString(lector.GetOrdinal("Valor1")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Valor2 = lector.IsDBNull(lector.GetOrdinal("Valor2")) ? default(string) : lector.GetString(lector.GetOrdinal("Valor2")),
                        Valor3 = lector.IsDBNull(lector.GetOrdinal("Valor3")) ? default(string) : lector.GetString(lector.GetOrdinal("Valor3")),
                    });
                }
            }
            return retList;
        }
        public static List<DropDownDto> ListaEmpresaXempresa(string Consulta)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_EmpresaDropDown_X_Empresa", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Valor1 = lector.IsDBNull(lector.GetOrdinal("Valor1")) ? default(string) : lector.GetString(lector.GetOrdinal("Valor1")),
                        Valor5 = lector.IsDBNull(lector.GetOrdinal("Valor5")) ? default(string) : lector.GetString(lector.GetOrdinal("Valor5")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),

                    });
                }
            }
            return retList;
        }
        public static  List<DropDownDto> ListaEstado(string Consulta)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_EstadoDropDown", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),

                    });
                }
            }
            return retList;
        }
        public static  List<DropDownDto> ListaMarca(string Consulta, string entidad)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                listaParams.Add(new SqlParameter("@IdEntidad", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = entidad });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_MarcaDropDown", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),

                    });
                }
            }
            return retList;
        }

        public static List<DropDownDto> ListaConfiguiracion(string Consulta, string entidad)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                listaParams.Add(new SqlParameter("@IdEntidad", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = entidad });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ConfiguracionDropDown", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),

                    });
                }
            }
            return retList;
        }
        
        public static  List<DropDownDto> ListaModelo(string Consulta,string IdEmpresa)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                listaParams.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = IdEmpresa });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ModeloDropDown", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),

                    });
                }
            }
            return retList;
        }
        public static  List<DropDownDto> ListaOperador(string Consulta)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_OperadorDropDown", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),

                    });
                }
            }
            return retList;
        }
        public static  List<DropDownDto> ListaTipo(string Consulta)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_TipoDropDown", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),

                    });
                }
            }
            return retList;
        }
        public static  List<DropDownDto> ListaTerminal(string Consulta)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_TerminalDropDown", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),

                    });
                }
            }
            return retList;
        }
        public static  List<DropDownDto> ListaUbicacion(string Consulta)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_UbicacionDropDown", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Valor1 = lector.IsDBNull(lector.GetOrdinal("Valor1")) ? default(string) : lector.GetString(lector.GetOrdinal("Valor1")),
                    });
                }
            }
            return retList;
        }
        public static  List<DropDownDto> ListaUDepartamento(string Consulta)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_UDepartamentoDropDown", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Valor1 = lector.IsDBNull(lector.GetOrdinal("Valor1")) ? default(string) : lector.GetString(lector.GetOrdinal("Valor1")),

                    });
                }
            }
            return retList;
        }
        public static  List<DropDownDto> ListaDepartamento(string Consulta)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_DepartamentoDropDown", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),

                    });
                }
            }
            return retList;
        }
        public static  List<DropDownDto> ListaProvincia(string Consulta)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ProvinciaDropDown", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Valor1 = lector.IsDBNull(lector.GetOrdinal("Valor1")) ? default(string) : lector.GetString(lector.GetOrdinal("Valor1")),
                    });
                }
            }
            return retList;
        }
        public static  List<DropDownDto> ListaDistrito(string Consulta,string Consulta2)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                listaParams.Add(new SqlParameter("@IdConsulta2", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta2 });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_DistritoDropDown", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Valor1 = lector.IsDBNull(lector.GetOrdinal("Valor1")) ? default(string) : lector.GetString(lector.GetOrdinal("Valor1")),
                    });
                }
            }
            return retList;
        }
        public static  List<DropDownDto> ListaEmail(string Consulta)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_EmailDropDown", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),

                    });
                }
            }
            return retList;
        }
        public static List<DropDownDto> ListaRegimen(string Consulta)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_RegimenDropDown", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),

                    });
                }
            }
            return retList;
        }
        public static List<DropDownDto> ListaCondicion(string Consulta)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_CondicionDropDown", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),

                    });
                }
            }
            return retList;
        }

        public static List<DropDownDto> ListaModulo(string Consulta)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ModuloDropDown", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Valor1 = lector.IsDBNull(lector.GetOrdinal("Valor1")) ? default(string) : lector.GetString(lector.GetOrdinal("Valor1")),

                    });
                }
            }
            return retList;
        }

        public static List<DropDownDto> ListaPerfil(string Consulta)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_PerfilDropDown", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),

                    });
                }
            }
            return retList;
        }

        public static List<DropDownDto> ListaTipoLugar(string Consulta)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_TipoLugarDropDown", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Valor1 = lector.IsDBNull(lector.GetOrdinal("Valor1")) ? default(string) : lector.GetString(lector.GetOrdinal("Valor1")),
                    });
                }
            }
            return retList;
        }
        public static List<DropDownDto> ListaProveedor(string Consulta)
        {
            List<DropDownDto> retList = new List<DropDownDto>();
            using (SqlConnection conn = DataContext.GetConnection())
            {
                var listaParams = new List<SqlParameter>();
                listaParams.Add(new SqlParameter("@IdConsulta", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = Consulta });
                SqlDataReader lector = SQLHelper.ExecuteReader(conn, System.Data.CommandType.StoredProcedure, @"Sp_ProveedorDropDown", listaParams.ToArray());
                while (lector.Read())
                {
                    retList.Add(new DropDownDto
                    {
                        Value = lector.IsDBNull(lector.GetOrdinal("Value")) ? default(string) : lector.GetString(lector.GetOrdinal("Value")),
                        Nombre = lector.IsDBNull(lector.GetOrdinal("Nombre")) ? default(string) : lector.GetString(lector.GetOrdinal("Nombre")),
                        Valor1 = lector.IsDBNull(lector.GetOrdinal("Valor1")) ? default(string) : lector.GetString(lector.GetOrdinal("Valor1")),
                    });
                }
            }
            return retList;
        }
        
    }
}


using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class SQLHelper
    {

        private static string CONNECTION_STRING = @"Data Source=DESKTOP-EVJE5FP;Initial Catalog=DASys4;User Id=sa;Password=sql;";
        //Data Source=104.238.93.77;Initial Catalog=JOBSMM;User Id=LEKTOR_ADMIN;Password=ZX5000HK**1#.LEKTOR;

        public static class DatabaseConnection
        {
            public static string ConnectionString { get; set; }

            public static SqlConnection ConnectionFactory()
            {
                return new SqlConnection(ConnectionString);
            }
        }
        public static string ConnectionString { get { return CONNECTION_STRING; } }

        public static int ExecuteNonQuery(SqlConnection conn, string cmdText, SqlParameter[] cmdParms)
        {
            SqlCommand cmd = conn.CreateCommand();
            PrepareCommand(cmd, conn, null, CommandType.StoredProcedure, cmdText, cmdParms);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        public static int ExecuteNonQuery(SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            SqlCommand cmd = conn.CreateCommand();
            using (conn)
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static SqlDataReader ExecuteReader(SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            SqlCommand cmd = conn.CreateCommand();
            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
            var rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return rdr;
        }

        // DataTable only available in .NET CORE 2.0 preview or later
        // 2017-05-13: sqldataadapter just put into builds YESTERDAY, not in any releases - https://github.com/dotnet/corefx/pull/19682/files/422ee5fcd9aa6f97b348fe278af634f1ff2c694e
        //  have to use datatables the hard way
        public static DataTable ExecuteDataTable(SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            DataTable dt = new DataTable();
            // just doing this cause dr.load fails
            dt.Columns.Add("CustomerID");
            dt.Columns.Add("CustomerName");
            SqlDataReader dr = ExecuteReader(conn, cmdType, cmdText, cmdParms);
            // as of now dr.Load throws a big nasty exception saying its not supported. wip.
            // dt.Load(dr);
            while (dr.Read())
            {
                dt.Rows.Add(dr[0], dr[1]);
            }
            return dt;
        }

        public static DataTable ExecuteDataTableSqlDA(SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            System.Data.DataTable dt = new DataTable();
            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmdText, conn);
            da.Fill(dt);
            return dt;
        }

        public static object ExecuteScalar(SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            SqlCommand cmd = conn.CreateCommand();
            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] commandParameters)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = cmdType;
            //attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(cmd, commandParameters);
            }
        }
        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            foreach (SqlParameter p in commandParameters)
            {
                //check for derived output value with no value assigned
                if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
                {
                    p.Value = DBNull.Value;
                }
                command.Parameters.Add(p);
            }
        }
    }
}

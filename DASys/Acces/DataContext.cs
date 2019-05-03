using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Acces
{
    public static class DataContext
    {
        public static string  ConnectionString { get; set; }


        public  static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }


    }
}

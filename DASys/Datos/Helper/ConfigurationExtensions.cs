using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Helper
{
    public static class ConfigurationExtensions
    {
        public static string GetConection(this IConfiguration configuration)
        {
            string result = configuration["ConnectionStrings:DefaultConnection"];
            return result;
        }
    }
}

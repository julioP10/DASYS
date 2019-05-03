using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entidad
{
    public class PaginationParameter
    {
        public string OrderBy { get; set; }
        public int Start { get; set; }
        public string Parameters { get; set; }
        public int AmountRows { get; set; }
        public string WhereFilter { get; set; }
    }
}

using System.Collections.Generic;

namespace Entidad
{
    public class DataTableModel<T>
    {
        public IList<ColumnDataTableModel> columns { get; set; }
        public object data { get; set; }
        public int draw { get; set; }
        public T filter { get; set; }
        public int length { get; set; }
        public IList<OrderDataTableModel> order { get; set; }
        public string orderBy { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }                
        public int start { get; set; }
        public string whereFilter { get; set; }
        public string parameters { get; set; }
    }
}
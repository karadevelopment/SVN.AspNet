using System.Collections.Generic;

namespace SVN.AspNet.DataTransferObjects
{
    public class DataTableParameters
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<DataTableColumn> columns { get; set; }
        public List<DataTableOrder> order { get; set; }
        public DataTableSearch search { get; set; }

        public int index
        {
            get => default(int) < this.length ? this.start / this.length : default(int);
        }

        public int amount
        {
            get => this.length;
        }
    }
}
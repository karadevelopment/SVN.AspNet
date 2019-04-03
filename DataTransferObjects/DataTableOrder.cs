namespace SVN.AspNet.DataTransferObjects
{
    public class DataTableOrder
    {
        public int column { get; set; }
        public string dir { get; set; }

        public bool asc
        {
            get => this.dir == "asc";
        }

        public bool desc
        {
            get => this.dir == "desc";
        }
    }
}
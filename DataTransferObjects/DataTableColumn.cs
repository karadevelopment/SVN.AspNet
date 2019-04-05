﻿namespace SVN.AspNet.DataTransferObjects
{
    public class DataTableColumn
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool orderable { get; set; }
        public DataTableSearch search { get; set; }
        public bool searchable { get; set; }
    }
}
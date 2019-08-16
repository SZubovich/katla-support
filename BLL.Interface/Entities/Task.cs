using System;
using System.IO;

namespace BLL.Interface.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
        public DateTime CreatingDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public EmployeeInfo Creator { get; set; }
        public EmployeeInfo Engineer { get; set; }
        public string BlobPath { get; set; }
        public Stream File { get; set; }
        public string Comment { get; set; }
        public bool IsClosed { get; set; }
    }
}

using System;

namespace DAL.Interface.DTO
{
    public class TaskDTO
    {
        public int Id { get; set; }

        public int TaskCreator { get; set; }

        public string Text { get; set; }

        public string Category { get; set; }

        public DateTime CreatingDate { get; set; }

        public DateTime ClosingDate { get; set; }

        public int Engineer { get; set; }

        public bool IsClosed { get; set; }

        public string Comment { get; set; }
    }
}

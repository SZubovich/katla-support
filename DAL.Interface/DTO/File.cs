using System;

namespace DAL.Interface.DTO
{
    public class File
    {
        public int Id { get; set; }

        public Folder Folder { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public DateTime CreatingDate { get; set; }
    }
}

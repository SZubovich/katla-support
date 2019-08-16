using System.Collections.Generic;

namespace DAL.Interface.DTO
{
    public class Folder
    {
        public int Id { get; set; }

        public Folder Parrent { get; set; }

        public string Name { get; set; }
    }
}

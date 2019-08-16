using System;

namespace WebAPI.Models
{
    public class ShortTaskInfo
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Text { get; set; }
        public DateTime CreatingDate { get; set; }
        public bool IsClosed { get; set; }
    }
}
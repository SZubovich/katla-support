using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class FullTaskInfo : ShortTaskInfo
    {
        public string EngineerName { get; set; }
        public string CreatorName { get; set; }
        public string Comment { get; set; }
        public DateTime ClosingDate { get; set; }
        public string BlobPath { get; set; }
        public int CreatorId { get; set; }
        public int EngineerId { get; set; }
        public Stream File { get; set; }
    }
}
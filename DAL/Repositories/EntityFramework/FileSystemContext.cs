using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Interface.DTO;

namespace DAL.Repositories.EntityFramework
{
    public class FileSystemContext : DbContext
    {
        public FileSystemContext() : base("SupportServiceDb") { }

        public DbSet<File> Files { get; set; }
        public DbSet<Folder> Folders { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    Database.SetInitializer<FileSystemContext>(null);
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Interface.Interfaces;
using DAL.Interface.DTO;
using System.Text;

namespace DAL.Repositories.EntityFramework
{
    public class FileSystemRepository : IFileSystemRepository
    {
        private const string blobPath = @"https://storage365.blob.core.windows.net/testcontainer";

        public void CreateFilePathIfNotExists(int userId, int taskId, string fileName)
        {
            using (FileSystemContext db = new FileSystemContext())
            {
                Folder users = db.Folders.First(f => f.Name == "users" && f.Parrent == null);
                Folder userFolder = db.Folders.FirstOrDefault(f => f.Name == userId.ToString() && f.Parrent.Name == users.Name);

                if (userFolder is null)
                {
                    userFolder = new Folder() {
                        Name = userId.ToString(),
                        Parrent = users
                    };

                    db.Folders.Add(userFolder);
                }

                Folder tasks = db.Folders.FirstOrDefault(f => f.Name == "tasks" && f.Parrent.Id == userFolder.Id);

                if (tasks is null)
                {
                    tasks = new Folder()
                    {
                        Name = "tasks",
                        Parrent = userFolder
                    };

                    db.Folders.Add(tasks);
                }

                Folder taskFolder = db.Folders.FirstOrDefault(f => f.Name == taskId.ToString() && f.Parrent.Name == tasks.Name);

                if (taskFolder is null)
                {
                    taskFolder = new Folder() {
                        Name = taskId.ToString(),
                        Parrent = tasks
                    };
                    db.Folders.Add(taskFolder);

                    File file = new File() {
                        Name = fileName,
                        Folder = taskFolder,
                        CreatingDate = DateTime.Now.Date
                    };
                    db.Files.Add(file);
                }

                db.SaveChanges();
            }
        }

        public string GetFilePathByTaskId(int taskId)
        {
            StringBuilder filePath = new StringBuilder();

            using (FileSystemContext db = new FileSystemContext())
            {
                File file = db.Files.Include("Folder.Parrent.Parrent.Parrent")
                    .FirstOrDefault(f => f.Folder.Name == taskId.ToString());

                if (file != null)
                {
                    filePath.Append('/');
                    filePath.Append(file.Name);
                    GetParrent(file.Folder);
                    
                    void GetParrent(Folder folder)
                    {
                        filePath.Insert(0, folder.Name);
                        filePath.Insert(0, '/');
                        if (folder.Parrent != null)
                        {
                            GetParrent(folder.Parrent);
                        }
                    }

                    filePath.Insert(0, blobPath);
                }
            }

            return filePath.ToString();
        }
    }
}

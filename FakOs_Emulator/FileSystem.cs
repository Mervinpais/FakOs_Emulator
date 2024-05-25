using System;
using System.Collections.Generic;
using System.Linq;

namespace FakOs_Emulator
{
    public class File
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }

        public File(string name, string path, string type, string data)
        {
            Name = name;
            Path = path;
            Type = type;
            Data = data;
        }
    }

    public class Folder
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public List<File> Files { get; set; }
        public List<Folder> SubFolders { get; set; }

        public Folder(string name, string path)
        {
            Name = name;
            Path = path;
            Files = new List<File>();
            SubFolders = new List<Folder>();
        }

        public void AddFile(File file)
        {
            Files.Add(file);
        }

        public void WriteToFile(File file)
        {
            var existingFile = Files.FirstOrDefault(f => f.Name == file.Name);
            if (existingFile != null)
            {
                Files.Remove(existingFile);
            }
            Files.Add(file);
        }

        public void AddSubFolder(Folder folder)
        {
            SubFolders.Add(folder);
        }

        public void RemoveFile(string fileName)
        {
            Files.RemoveAll(f => f.Name == fileName);
        }

        public void RemoveSubFolder(string folderName)
        {
            SubFolders.RemoveAll(f => f.Name == folderName);
        }
    }

    public class FileSystem
    {
        public Folder RootFolder { get; private set; }

        public FileSystem()
        {
            RootFolder = new Folder("root", "");
        }

        public Folder GetFolderByPath(string path)
        {
            var segments = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            Folder currentFolder = RootFolder;

            foreach (var segment in segments)
            {
                currentFolder = currentFolder?.SubFolders.FirstOrDefault(f => f.Name == segment);
                if (currentFolder == null) break;
            }

            return currentFolder;
        }

        public void AddFile(string path, File file)
        {
            var folder = GetFolderByPath(path);
            folder?.AddFile(file);
        }

        public void WriteToFile(string path, File file)
        {
            var folder = GetFolderByPath(path);
            folder?.WriteToFile(file);
        }

        public void AddFolder(string path, Folder folder)
        {
            var parentFolder = GetFolderByPath(path);
            parentFolder?.AddSubFolder(folder);
        }

        public void RemoveFile(string path, string fileName)
        {
            var folder = GetFolderByPath(path);
            folder?.RemoveFile(fileName);
        }

        public void RemoveFolder(string path, string folderName)
        {
            var folder = GetFolderByPath(path);
            folder?.RemoveSubFolder(folderName);
        }
    }
}

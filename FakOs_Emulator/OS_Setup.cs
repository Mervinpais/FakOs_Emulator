using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FakOs_Emulator
{
    public static class OS_Setup
    {
        public static void SetupOS(string[] args, out Memory_4bit mem4b, out PartitionTable ptable)
        {
            mem4b = new Memory_4bit();
            ptable = new PartitionTable();

            Memory_4bit mem4b_ = new Memory_4bit();
            PartitionTable ptable_ = new PartitionTable();

            string FilePath = string.Join(" ", args);
            string[] FileLines = System.IO.File.ReadAllLines(FilePath);

            bool storageSection = false;
            bool processSection = false;

            for (int i = 0; i < FileLines.Length; i++)
            {
                string line = FileLines[i];

                if (line.StartsWith("storage:"))
                {
                    int start = i;
                    int end = 0;

                    do
                    {
                        i++;
                        line = FileLines[i];
                        end = i;
                    } while (line.StartsWith("\t"));

                    string currentFile = null;
                    for (int j = start + 1; j < end; j++)
                    {
                        line = FileLines[j];

                        if (line.EndsWith(":"))
                        {
                            // If currentFile is not null, write the existing file to the system
                            if (!string.IsNullOrEmpty(currentFile))
                            {
                                // End of previous file, so now save it (optional if data is stored per line)
                                ptable_.Partitions[0].FileSystem.AddFile("root/", new File(currentFile, "root/", "com", null));
                            }

                            currentFile = line.TrimEnd(':'); // Remove the ":" to get the file name
                        }
                        else
                        {
                            if (currentFile == null)
                            {
                                throw new InvalidOperationException("Data line found before any file declaration.");
                            }

                            // Create a new file or update the existing file content
                            var existingFile = ptable_.Partitions[0].FileSystem.GetFolderByPath("root/").Files
                                               .FirstOrDefault(f => f.Name == currentFile);

                            if (existingFile == null)
                            {
                                ptable_.Partitions[0].FileSystem.AddFile("root/", new File(currentFile, "root/", "com", FileLines[j]));
                            }
                            else
                            {
                                ptable_.Partitions[0].FileSystem.WriteToFile("root/", new File(currentFile, "root/", "com", FileLines[j]));
                            }
                        }
                    }

                    storageSection = false;
                }
                else if (line.StartsWith("processes:"))
                {
                    int start = i;
                    int end = 0;

                    do
                    {
                        i++;
                        line = FileLines[i];
                        end = i;
                    } while (line.StartsWith("\t"));

                    for (int j = start + 1; j < end; j++)
                    {
                        mem4b_.processStack.Push(FileLines[j]);
                    }

                    processSection = false;
                }
                else if (line.StartsWith("end:"))
                {
                    return;
                }
            }
        }
    }
}

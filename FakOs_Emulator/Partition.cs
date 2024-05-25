using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakOs_Emulator
{
    public class PartitionTable
    {
        public List<Partition> Partitions = new List<Partition>();

        public PartitionTable()
        {
            Partitions.Add(new Partition());
        }

        public void AddPartition(Partition partition)
        {
            Partitions.Add(partition);
        }

        public void RemovePartition(int index)
        {
            if (index >= 0 && index < Partitions.Count)
            {
                Partitions.RemoveAt(index);
            }
        }
    }

    public class Partition
    {
        public int MemoryLength { get; set; }
        public FileSystem FileSystem { get; private set; }

        public Partition(int memoryLength = 65536)
        {
            MemoryLength = memoryLength;
            FileSystem = new FileSystem();
        }

        public void AddData(string path, File file)
        {
            FileSystem.AddFile(path, file);
        }

        public void AddFolder(string path, Folder folder)
        {
            FileSystem.AddFolder(path, folder);
        }

        public void Wipe()
        {
            FileSystem = new FileSystem();
        }
    }
}

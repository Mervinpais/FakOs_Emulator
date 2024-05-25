namespace FakOs_Emulator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BasicInstructionSet basicInstructionSet = new BasicInstructionSet();
            Memory_4bit memory_4Bit = new Memory_4bit();
            PartitionTable partitionTable = new PartitionTable();
            OS_Setup.SetupOS(args, out memory_4Bit, out partitionTable);
            basicInstructionSet.Run_4Bit(memory_4Bit);
        }
    }
}

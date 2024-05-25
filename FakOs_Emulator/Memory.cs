using System;
using System.Collections.Generic;

namespace FakOs_Emulator
{
    public class MemoryStack_4bit
    {
        protected const int MemoryLength = 16;
        public List<string> instructions;

        public MemoryStack_4bit()
        {
            instructions = new List<string>(MemoryLength);
            for (int i = 0; i < MemoryLength; i++)
            {
                instructions.Add("");
            }
        }

        public void Push(string instruction)
        {
            instructions.Add(instruction);
        }

        public string Pop()
        {
            if (instructions.Count == 0)
            {
                throw new InvalidOperationException("Stack is empty");
            }

            string poppedInstruction = instructions[instructions.Count - 1];
            instructions.RemoveAt(instructions.Count - 1);
            return poppedInstruction;
        }

        public object Read(int address)
        {
            if (address < 0 || address >= instructions.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(address), "Invalid memory address");
            }

            return instructions[address];
        }

        public void Write(int address, string instruction)
        {
            if (address < 0 || address >= instructions.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(address), "Invalid memory address");
            }

            instructions[address] = instruction;
        }

        public void Wipe()
        {
            instructions = new List<string>(MemoryLength);
            for (int i = 0; i < MemoryLength; i++)
            {
                instructions.Add("");
            }
        }
    }

    public class VariableStack_Memory_4bit : MemoryStack_4bit
    {
        // Additional methods or properties specific to variable stack
    }

    public class FunctionsStack_Memory_4bit : MemoryStack_4bit
    {
        // Additional methods or properties specific to functions stack
    }

    public class ProcessesStack_Memory_4bit : MemoryStack_4bit
    {
        // Additional methods or properties specific to processes stack
    }

    public class DevicesStack_Memory_4bit : MemoryStack_4bit
    {
        // Additional methods or properties specific to devices stack
    }

    public class Output_Memory_4bit : MemoryStack_4bit
    {
        // Additional methods or properties specific to variable stack
    }

    public class Input_Memory_4bit : MemoryStack_4bit
    {
        // Additional methods or properties specific to variable stack
    }

    public class Memory_4bit
    {
        public VariableStack_Memory_4bit variableStack;
        public FunctionsStack_Memory_4bit functionStack;
        public ProcessesStack_Memory_4bit processStack;
        public DevicesStack_Memory_4bit deviceStack;
        public Input_Memory_4bit inputStack;
        public Output_Memory_4bit outputStack;

        public Memory_4bit()
        {
            variableStack = new VariableStack_Memory_4bit();
            functionStack = new FunctionsStack_Memory_4bit();
            processStack = new ProcessesStack_Memory_4bit();
            deviceStack = new DevicesStack_Memory_4bit();
            inputStack = new Input_Memory_4bit();
            outputStack = new Output_Memory_4bit();
        }

        public void Wipe(MemoryStack_4bit memory)
        {
            memory.Wipe();
        }
    }
}

using System;
using System.Collections.Generic;

namespace FakOs_Emulator
{
    public class BasicInstructionSet
    {
        Memory_4bit mem4b = new Memory_4bit();
        enum activeStack
        {
            processStack,
            variableStack,
            functionStack,
            deviceStack
        }
        public void Run_4Bit(Memory_4bit mem4b_)
        {
            mem4b = mem4b_;

            foreach (string instruction in mem4b.processStack.instructions)
            {
                List<string> parts = instruction.Trim().Split(' ').ToList();
                if (parts.Count > 0)
                {
                    string operationCode = parts[0];
                    if (operationCode == "mov")
                    {
                        ExecuteMovInstruction(parts, activeStack.processStack);
                    }
                    else if (operationCode == "add")
                    {
                        ExecuteAddInstruction(parts, activeStack.processStack);
                    }
                    else if (parts[parts.Count-1].EndsWith(":"))
                    {
                        continue;
                    }
                    else if (operationCode == "")
                    {
                        continue;
                    }
                    else
                    {
                        throw new InvalidOperationException($"Unsupported operation: {operationCode}");
                    }
                }
            }
        }

        private void ExecuteAddInstruction(List<string> parts, activeStack aS)
        {
            if (parts.Count < 3)
            {
                throw new ArgumentException("add instruction requires at least two operands");
            }

            // Remove the operation code
            parts.RemoveAt(0);

            if (int.TryParse(parts[1], out int destinationIndex) && int.TryParse(parts[2], out int sourceIndex))
            {
                int destinationValue = 0;
                int sourceValue = 0;

                switch (aS)
                {
                    case activeStack.processStack:
                        destinationValue = int.Parse(mem4b.processStack.instructions[destinationIndex]);
                        sourceValue = int.Parse(mem4b.processStack.instructions[sourceIndex]);
                        mem4b.processStack.instructions[destinationIndex] = (destinationValue + sourceValue).ToString();
                        break;
                    case activeStack.functionStack:
                        destinationValue = int.Parse(mem4b.functionStack.instructions[destinationIndex]);
                        sourceValue = int.Parse(mem4b.functionStack.instructions[sourceIndex]);
                        mem4b.functionStack.instructions[destinationIndex] = (destinationValue + sourceValue).ToString();
                        break;
                    case activeStack.deviceStack:
                        destinationValue = int.Parse(mem4b.deviceStack.instructions[destinationIndex]);
                        sourceValue = int.Parse(mem4b.deviceStack.instructions[sourceIndex]);
                        mem4b.deviceStack.instructions[destinationIndex] = (destinationValue + sourceValue).ToString();
                        break;
                    case activeStack.variableStack:
                        destinationValue = int.Parse(mem4b.variableStack.instructions[destinationIndex]);
                        sourceValue = int.Parse(mem4b.variableStack.instructions[sourceIndex]);
                        mem4b.variableStack.instructions[destinationIndex] = (destinationValue + sourceValue).ToString();
                        break;
                    default:
                        throw new InvalidOperationException($"Unsupported stack: {aS}");
                }
            }
            else
            {
                throw new ArgumentException("Invalid operand format");
            }
        }

        private void ExecuteMovInstruction(List<string> parts, activeStack aS)
        {
            if (parts.Count < 3)
            {
                throw new ArgumentException("mov instruction requires at least two operands");
            }

            parts.RemoveAt(0);

            if (aS == activeStack.processStack)
            {
                mem4b.processStack.instructions[Convert.ToInt32(parts[1])] =
                    mem4b.processStack.instructions[Convert.ToInt32(parts[2])];
                mem4b.processStack.instructions[Convert.ToInt32(parts[2])] = "";
            }
            else if (aS == activeStack.functionStack)
            {
                mem4b.functionStack.instructions[Convert.ToInt32(parts[1])] = 
                    mem4b.functionStack.instructions[Convert.ToInt32(parts[2])];
                mem4b.functionStack.instructions[Convert.ToInt32(parts[2])] = "";
            }
            else if (aS == activeStack.deviceStack)
            {
                mem4b.deviceStack.instructions[Convert.ToInt32(parts[1])] = 
                    mem4b.deviceStack.instructions[Convert.ToInt32(parts[2])];
                mem4b.deviceStack.instructions[Convert.ToInt32(parts[2])] = "";
            }
            else if (aS == activeStack.variableStack)
            {
                mem4b.variableStack.instructions[Convert.ToInt32(parts[1])] = 
                    mem4b.variableStack.instructions[Convert.ToInt32(parts[2])];
                mem4b.variableStack.instructions[Convert.ToInt32(parts[2])] = "";
            }
        }
    }
}

using CPUEmulator;
using CPUEmulator.External;
using CPUEmulator.Internal;
using System.ComponentModel.Design;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;

namespace CPUEmulatorTester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            if (Path.Exists(filePath))
            {
                RAM ram = new RAM((uint)Math.Pow(2,20));
                ram.SetAddress(0);
                ram.Clear();
                ram.LoadFromFile(filePath);
                for(int i = 0; i<20; i++)
                {
                    ram.SetAddress((uint)i);
                    Console.WriteLine(ram.Get());
                }
            }
            */

            string filePath = @"..\..\..\program.txt";

            CPUEmulator.Custom.EPC epc = new CPUEmulator.Custom.EPC(8, 8);

            if (Path.Exists(filePath))
            {
                epc.RAM.SetAddress(0);
                epc.RAM.Clear();
                epc.RAM.LoadFromFile(filePath);
            }

            epc.ProgramCache.TransferDirectlyFullData(ref epc.RAM.GetMemoryReference());
            Console.WriteLine("Program Loaded into cache\n");
            epc.PC.SetCurrentLine(0);
            while (epc.CU.ExecuteInstruction(epc.CU.DecodeInstruction(epc.CU.InstructionSplit(epc.CU.InstructionFetch()))))
            {
                Console.WriteLine($"Instruction #{epc.PC.GetCurrentLine()}: {epc.CU.InstructionFetch().Substring(0, 4)} {epc.CU.InstructionFetch().Substring(4, 8)}  |  {epc.CU.InstructionFetch()}");
                epc.PC.Increment();
            }
        }
    }
}

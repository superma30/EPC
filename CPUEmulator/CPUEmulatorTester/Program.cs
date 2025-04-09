using CPUEmulator;
using CPUEmulator.External;
using CPUEmulator.Internal;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;

namespace CPUEmulatorTester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            string filePath = @"..\..\..\program.txt";
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

            CPUEmulator.Custom.EPC epc = new CPUEmulator.Custom.EPC(8);

            
        }
    }
}

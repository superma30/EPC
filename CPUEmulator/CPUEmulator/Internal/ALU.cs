using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUEmulator.Internal
{
    public class ALU
    {
        public readonly Register ALUInputA;
        public readonly Register ALUInputB;
        public readonly Register ALUOutput1;
        public readonly Register ALUOutput2;
        public readonly Register FR;

        public CPU _CPU;
        public ExecuteDelegate Execute;

        public delegate void ExecuteDelegate(ALU alu, int OpCode);

        public ALU(CPU cpu, ExecuteDelegate customExecuteFunction, ref Register FlagRegister, ushort bits = 0)
        {
            _CPU = cpu;
            Execute = customExecuteFunction;
            FR = FlagRegister;
            ALUInputA = new Register(bits);
            ALUInputB = new Register(bits);
            ALUOutput1 = new Register(bits);
            ALUOutput2 = new Register(bits);
        }
    }
}

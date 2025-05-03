using CPUEmulator.Internal;
using System.Reflection.Emit;
using System;
using CPUEmulator.External;
using System.Diagnostics.CodeAnalysis;

namespace CPUEmulator.Custom
{
    public class EPC : CPU
    {
        const int _instructionInst = 4;
        const int _instructionOperator = 8;

        public ALU ALU;
        public HardDriveDisk HDD;
        public RAM RAM;
        public MMU MMU;
        public Register FR;
        public Register TR;
        public Register AR;
        public ProgramCache ProgramCache;
        public EPCCU CU;
        public PC PC;

        public Register R00;
        public Register R01;
        public Register R02;
        public Register R03;
        public Register R04;
        public Register R05;
        public Register R06;
        public Register R07;
        public Register R08;
        public Register R09;
        public Register R10;
        public Register R11;
        public Register R12;
        public Register R13;
        public Register R14;
        public Register R15;
        public Register R16;
        public Register R17;
        public Register R18;
        public Register R19;
        public Register R20;
        public Register R21;
        public Register R22;
        public Register R23;
        public Register R24;
        public Register R25;
        public Register R26;
        public Register R27;
        public Register R28;
        public Register R29;
        public Register R30;
        public Register R31;

        public EPC(ushort operatingBits, ushort dataBits)
        {
            R00 = new Register(operatingBits);
            R01 = new Register(operatingBits);
            R02 = new Register(operatingBits);
            R03 = new Register(operatingBits);
            R04 = new Register(operatingBits);
            R05 = new Register(operatingBits);
            R06 = new Register(operatingBits);
            R07 = new Register(operatingBits);
            R08 = new Register(operatingBits);
            R09 = new Register(operatingBits);
            R10 = new Register(operatingBits);
            R11 = new Register(operatingBits);
            R12 = new Register(operatingBits);
            R13 = new Register(operatingBits);
            R14 = new Register(operatingBits);
            R15 = new Register(operatingBits);
            R16 = new Register(operatingBits);
            R17 = new Register(operatingBits);
            R18 = new Register(operatingBits);
            R19 = new Register(operatingBits);
            R20 = new Register(operatingBits);
            R21 = new Register(operatingBits);
            R22 = new Register(operatingBits);
            R23 = new Register(operatingBits);
            R24 = new Register(operatingBits);
            R25 = new Register(operatingBits);
            R26 = new Register(operatingBits);
            R27 = new Register(operatingBits);
            R28 = new Register(operatingBits);
            R29 = new Register(operatingBits);
            R30 = new Register(operatingBits);
            R31 = new Register(operatingBits);
            FR = new Register(operatingBits);
            TR = new Register(operatingBits);
            AR = new Register(operatingBits);

            ProgramCache = new ProgramCache(operatingBits, _instructionInst+_instructionOperator);
            ALU = new ALU(this, EPCALUExecute, ref FR);
            HDD = new HardDriveDisk(1073741824, operatingBits);
            RAM = new RAM(1048576, operatingBits);
            MMU = new MMU(ref RAM, ref HDD);
            PC = new PC(operatingBits);
            CU = new EPCCU(this, _instructionInst, _instructionOperator);
        }

        //
        // HERE THERE ARE HARDCODED 8 BITS
        //
        public static void EPCALUExecute(ALU alu, int OpCode)
        {
            switch (OpCode)
            {
                case 0:
                    try
                    {
                        alu.ALUOutput1.Write(alu.ALUInputA.Get() + alu.ALUInputB.Get());
                        if(alu.ALUOutput1.Get() == 0)
                        {
                            alu.FR.Write(Convert.ToInt32("1".PadLeft(/**/8/**/, '0'), 2));
                        }
                        else if(alu.ALUOutput1.Get() < 0)
                        {
                            alu.FR.Write(Convert.ToInt32("10".PadLeft(/**/8/**/, '0'), 2));
                        }
                    }
                    catch
                    {
                        alu.ALUOutput1.Write(Convert.ToInt32(Convert.ToString(alu.ALUInputA.Get() + alu.ALUInputB.Get(), 2).Substring(Convert.ToString(alu.ALUInputA.Get() + alu.ALUInputB.Get(), 2).Length - /**/8/**/, /**/8/**/), 2));
                        if (alu.ALUOutput1.Get() == 0)
                        {
                            alu.FR.Write(Convert.ToInt32("101".PadLeft(/**/8/**/, '0'), 2));
                        }
                        else if (alu.ALUOutput1.Get() < 0)
                        {
                            alu.FR.Write(Convert.ToInt32("110".PadLeft(/**/8/**/, '0'), 2));
                        }
                    }
                    break;
                case 1:
                    try
                    {
                        alu.ALUOutput1.Write(alu.ALUInputA.Get() - alu.ALUInputB.Get());
                        if (alu.ALUOutput1.Get() == 0)
                        {
                            alu.FR.Write(Convert.ToInt32("1".PadLeft(/**/8/**/, '0'), 2));
                        }
                        else if (alu.ALUOutput1.Get() < 0)
                        {
                            alu.FR.Write(Convert.ToInt32("10".PadLeft(/**/8/**/, '0'), 2));
                        }
                    }
                    catch
                    {
                        alu.ALUOutput1.Write(Convert.ToInt32(Convert.ToString(alu.ALUInputA.Get() - alu.ALUInputB.Get(), 2).Substring(Convert.ToString(alu.ALUInputA.Get() - alu.ALUInputB.Get(), 2).Length - /**/8/**/, /**/8/**/), 2));
                        if (alu.ALUOutput1.Get() == 0)
                        {
                            alu.FR.Write(Convert.ToInt32("101".PadLeft(/**/8/**/, '0'), 2));
                        }
                        else if (alu.ALUOutput1.Get() < 0)
                        {
                            alu.FR.Write(Convert.ToInt32("110".PadLeft(/**/8/**/, '0'), 2));
                        }
                    }
                    break;
                case 2:
                    try
                    {
                        alu.ALUOutput1.Write(alu.ALUInputA.Get() * alu.ALUInputB.Get());
                        if (alu.ALUOutput1.Get() == 0)
                        {
                            alu.FR.Write(Convert.ToInt32("1".PadLeft(/**/8/**/, '0'), 2));
                        }
                        else if (alu.ALUOutput1.Get() < 0)
                        {
                            alu.FR.Write(Convert.ToInt32("10".PadLeft(/**/8/**/, '0'), 2));
                        }
                    }
                    catch
                    {
                        alu.ALUOutput1.Write(Convert.ToInt32(Convert.ToString(alu.ALUInputA.Get() * alu.ALUInputB.Get(), 2).Substring(Convert.ToString(alu.ALUInputA.Get() * alu.ALUInputB.Get(), 2).Length - /**/8/**/, /**/8/**/), 2));
                        if (alu.ALUOutput1.Get() == 0)
                        {
                            alu.FR.Write(Convert.ToInt32("101".PadLeft(/**/8/**/, '0'), 2));
                        }
                        else if (alu.ALUOutput1.Get() < 0)
                        {
                            alu.FR.Write(Convert.ToInt32("110".PadLeft(/**/8/**/, '0'), 2));
                        }
                    }
                    break;
                case 3:
                    try
                    {
                        alu.ALUOutput1.Write(alu.ALUInputA.Get() / alu.ALUInputB.Get());
                        alu.ALUOutput2.Write(alu.ALUInputA.Get() % alu.ALUInputB.Get());
                        if (alu.ALUOutput1.Get() == 0)
                        {
                            alu.FR.Write(Convert.ToInt32("1".PadLeft(/**/8/**/, '0'), 2));
                        }
                        else if (alu.ALUOutput1.Get() < 0)
                        {
                            alu.FR.Write(Convert.ToInt32("10".PadLeft(/**/8/**/, '0'), 2));
                        }
                    }
                    catch
                    {
                        alu.ALUOutput1.Write(Convert.ToInt32(Convert.ToString(alu.ALUInputA.Get() / alu.ALUInputB.Get(), 2).Substring(Convert.ToString(alu.ALUInputA.Get() / alu.ALUInputB.Get(), 2).Length - /**/8/**/, /**/8/**/), 2));
                        alu.ALUOutput2.Write(Convert.ToInt32(Convert.ToString(alu.ALUInputA.Get() % alu.ALUInputB.Get(), 2).Substring(Convert.ToString(alu.ALUInputA.Get() % alu.ALUInputB.Get(), 2).Length - /**/8/**/, /**/8/**/), 2));
                        if (alu.ALUOutput1.Get() == 0)
                        {
                            alu.FR.Write(Convert.ToInt32("101".PadLeft(/**/8/**/, '0'), 2));
                        }
                        else if (alu.ALUOutput1.Get() < 0)
                        {
                            alu.FR.Write(Convert.ToInt32("110".PadLeft(/**/8/**/, '0'), 2));
                        }
                    }
                    break;
                case 4:
                    alu.ALUOutput1.Write(GlobalData.Negate(alu.ALUInputA.Get(), 8));
                    if (alu.ALUOutput1.Get() == 0)
                    {
                        alu.FR.Write(Convert.ToInt32("1".PadLeft(/**/8/**/, '0'), 2));
                    }
                    else if (alu.ALUOutput1.Get() < 0)
                    {
                        alu.FR.Write(Convert.ToInt32("10".PadLeft(/**/8/**/, '0'), 2));
                    }
                    break;
                case 5:
                    alu.ALUOutput1.Write(alu.ALUInputA.Get() & alu.ALUInputB.Get());
                    if (alu.ALUOutput1.Get() == 0)
                    {
                        alu.FR.Write(Convert.ToInt32("1".PadLeft(/**/8/**/, '0'), 2));
                    }
                    else if (alu.ALUOutput1.Get() < 0)
                    {
                        alu.FR.Write(Convert.ToInt32("10".PadLeft(/**/8/**/, '0'), 2));
                    }
                    break;
                case 6:
                    alu.ALUOutput1.Write(alu.ALUInputA.Get() | alu.ALUInputB.Get());
                    if (alu.ALUOutput1.Get() == 0)
                    {
                        alu.FR.Write(Convert.ToInt32("1".PadLeft(/**/8/**/, '0'), 2));
                    }
                    else if (alu.ALUOutput1.Get() < 0)
                    {
                        alu.FR.Write(Convert.ToInt32("10".PadLeft(/**/8/**/, '0'), 2));
                    }
                    break;
                case 7:
                    alu.ALUOutput1.Write(alu.ALUInputA.Get() ^ alu.ALUInputB.Get());
                    if (alu.ALUOutput1.Get() == 0)
                    {
                        alu.FR.Write(Convert.ToInt32("1".PadLeft(/**/8/**/, '0'), 2));
                    }
                    else if (alu.ALUOutput1.Get() < 0)
                    {
                        alu.FR.Write(Convert.ToInt32("10".PadLeft(/**/8/**/, '0'), 2));
                    }
                    break;
                case 8:
                    alu.ALUOutput1.Write(GlobalData.Negate(alu.ALUInputA.Get() & alu.ALUInputB.Get(), 8));
                    if (alu.ALUOutput1.Get() == 0)
                    {
                        alu.FR.Write(Convert.ToInt32("1".PadLeft(/**/8/**/, '0'), 2));
                    }
                    else if (alu.ALUOutput1.Get() < 0)
                    {
                        alu.FR.Write(Convert.ToInt32("10".PadLeft(/**/8/**/, '0'), 2));
                    }
                    break;
                case 9:
                    alu.ALUOutput1.Write(GlobalData.Negate(alu.ALUInputA.Get() | alu.ALUInputB.Get(), 8));
                    if (alu.ALUOutput1.Get() == 0)
                    {
                        alu.FR.Write(Convert.ToInt32("1".PadLeft(/**/8/**/, '0'), 2));
                    }
                    else if (alu.ALUOutput1.Get() < 0)
                    {
                        alu.FR.Write(Convert.ToInt32("10".PadLeft(/**/8/**/, '0'), 2));
                    }
                    break;
                case 10:
                    alu.ALUOutput1.Write(GlobalData.Negate(alu.ALUInputA.Get() ^ alu.ALUInputB.Get(), 8));
                    if (alu.ALUOutput1.Get() == 0)
                    {
                        alu.FR.Write(Convert.ToInt32("1".PadLeft(/**/8/**/, '0'), 2));
                    }
                    else if (alu.ALUOutput1.Get() < 0)
                    {
                        alu.FR.Write(Convert.ToInt32("10".PadLeft(/**/8/**/, '0'), 2));
                    }
                    break;
                case 11:
                    alu.ALUOutput1.Write(alu.ALUInputA.Get() >>> 1);
                    if (alu.ALUOutput1.Get() == 0)
                    {
                        alu.FR.Write(Convert.ToInt32("1".PadLeft(/**/8/**/, '0'), 2));
                    }
                    else if (alu.ALUOutput1.Get() < 0)
                    {
                        alu.FR.Write(Convert.ToInt32("10".PadLeft(/**/8/**/, '0'), 2));
                    }
                    break;
                case 12:
                    alu.ALUOutput1.Write(alu.ALUInputA.Get() << 1);
                    if (alu.ALUOutput1.Get() == 0)
                    {
                        alu.FR.Write(Convert.ToInt32("1".PadLeft(/**/8/**/, '0'), 2));
                    }
                    else if (alu.ALUOutput1.Get() < 0)
                    {
                        alu.FR.Write(Convert.ToInt32("10".PadLeft(/**/8/**/, '0'), 2));
                    }
                    break;
            }
        }

        public Dictionary<string, int> GetRegisters()
        {
            return new Dictionary<string, int>()
                {
                    {"PC", (int)PC.GetCurrentLine()},
                    {"TR", TR.Get()},
                    {"FR", FR.Get()},
                    {"AR", AR.Get()},
                    {"R00", R00.Get()},
                    {"R01", R01.Get()},
                    {"R02", R02.Get()},
                    {"R03", R03.Get()},
                    {"R04", R04.Get()},
                    {"R05", R05.Get()},
                    {"R06", R06.Get()},
                    {"R07", R07.Get()},
                    {"R08", R08.Get()},
                    {"R09", R09.Get()},
                    {"R10", R10.Get()},
                    {"R11", R11.Get()},
                    {"R12", R12.Get()},
                    {"R13", R13.Get()},
                    {"R14", R14.Get()},
                    {"R15", R15.Get()},
                    {"R16", R16.Get()},
                    {"R17", R17.Get()},
                    {"R18", R18.Get()},
                    {"R19", R19.Get()},
                    {"R20", R20.Get()},
                    {"R21", R21.Get()},
                    {"R22", R22.Get()},
                    {"R23", R23.Get()},
                    {"R24", R24.Get()},
                    {"R25", R25.Get()},
                    {"R26", R26.Get()},
                    {"R27", R27.Get()},
                    {"R28", R28.Get()},
                    {"R29", R29.Get()},
                    {"R30", R30.Get()},
                    {"R31", R31.Get()},
                    {"ALUIA", ALU.ALUInputA.Get()},
                    {"ALUIB", ALU.ALUInputB.Get()},
                    {"ALUO1", ALU.ALUOutput1.Get()},
                    {"ALUO2", ALU.ALUOutput2.Get()}
                };
        }

        public class EPCCU : CU
        {
            private EPC _EPC;
            private int _instructionCodeBits;
            private int _operatorCodeBits;

            public EPCCU(in EPC epc, int instructionCodeBits, int operatorCodeBits)
            {
                _EPC = epc;

                _instructionCodeBits = instructionCodeBits;
                _operatorCodeBits = operatorCodeBits;
            }

            public string InstructionFetch()
            {
                _EPC.ProgramCache.SetAddress((uint)_EPC.PC.GetCurrentLine());
                return _EPC.ProgramCache.Get();
            }

            public Tuple<int, int> InstructionSplit(string fullCode)
            {
                return new Tuple<int, int>(Convert.ToInt32(fullCode.Substring(0, _instructionCodeBits), 2), Convert.ToInt32(fullCode.Substring(_instructionCodeBits, _operatorCodeBits), 2));
            }

            public Tuple<EPCInstructions, int> DecodeInstruction(Tuple<int, int> OpCode)
            {
                EPCInstructions instr = CodeToInstruction(OpCode.Item1);
                int operand = OpCode.Item2;
                return new Tuple<EPCInstructions, int>(instr, operand);
            }

            public bool ExecuteInstruction(Tuple<EPCInstructions, int> instruction)
            {
                Register? reg;
                switch (instruction.Item1)
                {
                    case EPCInstructions.NUL:
                        // Do nothing
                        return true;

                    case EPCInstructions.STP:
                        return false;

                    case EPCInstructions.GET:
                        reg = CodeToRegister(instruction.Item2);
                        if(instruction.Item2 == 0)  // Means it selected PC
                        {
                            _EPC.TR.Write((int)_EPC.PC.GetCurrentLine());
                        }
                        else if(reg != null)  // Means it selected any Register
                        {
                            _EPC.TR.Write(reg.Get());
                        }
                        return true;

                    case EPCInstructions.SET:
                        reg = CodeToRegister(instruction.Item2);
                        if (instruction.Item2 == 0)  // Means it selected PC
                        {
                            _EPC.PC.SetCurrentLine(_EPC.TR.Get());
                        }
                        else if (reg != null)  // Means it selected any Register
                        {
                            reg.Write(_EPC.TR.Get());
                        }
                        return true;

                    case EPCInstructions.LDD:
                        _EPC.RAM.SetAddress(
                            //(uint)instruction.Item2 //WRONG, MR FERRARIN - WRT USES AR, NOT OPERAND
                            (uint)(_EPC.AR).Get()
                        );
                        _EPC.TR.Write(_EPC.RAM.Get());
                        return true;

                    case EPCInstructions.WRT:
                        //throw new Exception($"{}");
                        _EPC.RAM.SetAddress(
                            //(uint)instruction.Item2 //WRONG, MR FERRARIN - WRT USES AR, NOT OPERAND
                            (uint)(_EPC.AR).Get()
                        );
                        _EPC.RAM.Write(_EPC.TR.Get());
                        return true;

                    case EPCInstructions.EXE:
                        _EPC.ALU.Execute(_EPC.ALU, instruction.Item2);
                        return true;

                    case EPCInstructions.JMP:
                        _EPC.PC.SetCurrentLine(_EPC.TR.Get()-1);
                        return true;

                    case EPCInstructions.IFD:
                        bool check;
                        var FRBinary = Convert.ToString(_EPC.FR.Get(), 2).PadLeft(8, '0');
                        switch (instruction.Item2)
                        {
                            case 0:
                                check = FRBinary[FRBinary.Length - 1 - (int)FRBits.ZeroFlag] == '1';
                                break;
                            case 1:
                                check = FRBinary[FRBinary.Length - 1 - (int)FRBits.ZeroFlag] == '0';
                                break;
                            case 2:
                                check = FRBinary[FRBinary.Length - 1 - (int)FRBits.NegativeFlag] == '0' && FRBinary[FRBinary.Length-1 - (int)FRBits.ZeroFlag] == '0';
                                break;
                            case 3:
                                check = FRBinary[FRBinary.Length - 1 - (int)FRBits.NegativeFlag] == '0' || FRBinary[FRBinary.Length - 1 - (int)FRBits.ZeroFlag] == '1';
                                break;
                            case 4:
                                check = FRBinary[FRBinary.Length - 1 - (int)FRBits.NegativeFlag] == '1' && FRBinary[FRBinary.Length - 1 - (int)FRBits.ZeroFlag] == '0';
                                break;
                            case 5:
                                check = FRBinary[FRBinary.Length - 1 - (int)FRBits.NegativeFlag] == '1' || FRBinary[FRBinary.Length - 1 - (int)FRBits.ZeroFlag] == '1';
                                break;
                            default:
                                check = false;
                                break;
                        }
                        if (!check)
                        {
                            _EPC.PC.Increment();
                        }
                        return true;

                    case EPCInstructions.LDI:
                        _EPC.TR.Write(instruction.Item2);
                        return true;
                }
                return false;
            }

            private EPCInstructions CodeToInstruction(int code)
            {
                switch (code)
                {
                    case 1:
                        return EPCInstructions.STP;
                    case 2:
                        return EPCInstructions.GET;
                    case 3:
                        return EPCInstructions.SET;
                    case 4:
                        return EPCInstructions.LDD;
                    case 5:
                        return EPCInstructions.WRT;
                    case 6:
                        return EPCInstructions.EXE;
                    case 7:
                        return EPCInstructions.JMP;
                    case 8:
                        return EPCInstructions.IFD;
                    case 9:
                        return EPCInstructions.LDI;
                    case 0:
                    default:
                        return EPCInstructions.NUL;
                }
            }

            private Register? CodeToRegister(int code)
            {
                switch (code)
                {
                    // case 0: EPC.PC;
                    case 1:
                        return _EPC.FR;
                    case 2:
                        return _EPC.ALU.ALUInputA;
                    case 3:
                        return _EPC.ALU.ALUInputB;
                    case 4:
                        return _EPC.ALU.ALUOutput1;
                    case 5:
                        return _EPC.ALU.ALUOutput2;
                    case 6:
                        return _EPC.AR;
                    case 10:
                        return _EPC.R00;
                    case 11:
                        return _EPC.R01;
                    case 12:
                        return _EPC.R02;
                    case 13:
                        return _EPC.R03;
                    case 14:
                        return _EPC.R04;
                    case 15:
                        return _EPC.R05;
                    case 16:
                        return _EPC.R06;
                    case 17:
                        return _EPC.R07;
                    case 18:
                        return _EPC.R08;
                    case 19:
                        return _EPC.R09;
                    case 20:
                        return _EPC.R10;
                    case 21:
                        return _EPC.R11;
                    case 22:
                        return _EPC.R12;
                    case 23:
                        return _EPC.R13;
                    case 24:
                        return _EPC.R14;
                    case 25:
                        return _EPC.R15;
                    case 26:
                        return _EPC.R16;
                    case 27:
                        return _EPC.R17;
                    case 28:
                        return _EPC.R18;
                    case 29:
                        return _EPC.R19;
                    case 30:
                        return _EPC.R20;
                    case 31:
                        return _EPC.R21;
                    case 32:
                        return _EPC.R22;
                    case 33:
                        return _EPC.R23;
                    case 34:
                        return _EPC.R24;
                    case 35:
                        return _EPC.R25;
                    case 36:
                        return _EPC.R26;
                    case 37:
                        return _EPC.R27;
                    case 38:
                        return _EPC.R28;
                    case 39:
                        return _EPC.R29;
                    case 40:
                        return _EPC.R30;
                    case 41:
                        return _EPC.R31;
                    default:
                        return null;
                }
            }


            // Eventual Pipeline Emulator

            /*
            public int PipelineInstructionFetch()
            {
                _programCache.SetAddress(_PC.GetCurrentLine());
                return _programCache.Get();
            }

            public Tuple<int, int> PipelineInstructionDecode(int fullCode)
            {
                return new Tuple<int, int>(int.Parse(fullCode.ToString().Substring(0, _instructionCodeBits)), int.Parse(fullCode.ToString().Substring(_instructionCodeBits, _operatorCodeBits)));
            }

            public void PipelineExecute(Tuple<int, int> opCode)
            {
                switch (opCode.Item1)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    // Populate later
                }
            }

            public void PipelineMemory()
            {

            }

            public void PipelineWriteBack()
            {

            }
            */
        }

        public enum FRBits
        {
            ZeroFlag = 0,
            NegativeFlag = 1,
            OverflowFlag = 2,
            CarryFlag = 3
        }

        public enum EPCInstructions
        {
            NUL,
            STP,
            GET,
            SET,
            LDD,
            WRT,
            EXE,
            JMP,
            IFD,
            LDI
        }
    }
}

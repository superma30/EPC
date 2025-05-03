using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EPCCompiler
{
    public class Compiler
    {
        public struct VarInfo
        {
            public int Address;
            public List<int> Size;

            public VarInfo(int a, List<int> s)
            {
                Address = a;
                Size = s;
            }
        }

        private Dictionary<string, VarInfo> variables = new();
        private Dictionary<string, string> opcodes = new()
        {
            { "+", "0" }, { "-", "1" }, { "*", "2" },
            { "/", "3" }, { "!", "4" }, { "&", "5" },
            { "|", "6" }, { "^", "7" }, { "!&", "8" },
            { "!|", "9" }, { "!^", "10" }, { ">>", "11" },
            { "<<", "12" }, { "%", "-1" }
        };
        private Dictionary<string, int> labels = new();
        private List<int> replaceStack = new();

        private int memPtr = 0x00;


        public void Compile(ProgramNode program, string outputPath)
        {
            File.WriteAllText(outputPath, Compile(program));
        }

        private void findAddress(string varname, List<PrimaryMemoryUnit> indexes, StringBuilder sb)
        {
            int baseAddress = (variables[varname].Address);

            List<int> weights = new();

            int res = 1;
            for (int i = 0; i<indexes.Count; i++)
            {
                weights.Insert(0, res);
                res *= variables[varname].Size[indexes.Count-1-i];
            }

            bool isDynamic = false;

            for (int i = 0; i < indexes.Count; i++)
                if (indexes[i] is Constant c)
                {
                    baseAddress += c.value * weights[i];
                }
                else if (indexes[i] is Register r)
                    isDynamic = true;
                else
                    throw new Exception("unexpected index");

            if (!isDynamic)
                sb.AppendLine($"LDI {baseAddress}");
            else
            {
                int j = 0;
                for (int i = 0; i < indexes.Count; i++)
                {
                    if (indexes[i] is not Register r)
                        continue;
                    else
                    {
                        if (j == 0)
                        {
                            sb.AppendLine($"LDI {baseAddress}");
                            sb.AppendLine($"SET AR");
                        }
                        else
                        {
                            sb.AppendLine($"GET ALUOutput1");
                            sb.AppendLine($"SET AR");
                        }
                        
                        sb.AppendLine($"GET R{r.NumberReg}");
                        sb.AppendLine($"SET ALUInput1");

                        sb.AppendLine($"LDI {weights[i]}");
                        sb.AppendLine($"SET ALUInput2");
                        sb.AppendLine($"EXE 2");

                        sb.AppendLine($"GET ALUOutput1");
                        sb.AppendLine($"SET ALUInput1");
                        sb.AppendLine($"GET AR");
                        sb.AppendLine($"SET ALUInput2");
                        sb.AppendLine($"EXE 0");

                        j++;
                    }

                }
                sb.AppendLine($"GET ALUOutput1");
            }
        }

        public string Compile(ProgramNode program)
        {
            var sb = new StringBuilder();

            int NodeCounter = 0;
            foreach (var stmt in program.Statements)
            {
                sb.AppendLine($"#{NodeCounter} : {stmt.ToString()}");
                switch (stmt)
                {
                    case VarDeclaration vd:
                        int startMemPtr = memPtr;
                        List<int> size = new();

                        for (int i = 0; i < vd.Size.Count; i++)
                        {
                            memPtr += vd.Size[i];
                            size.Add(vd.Size[i]);
                        }

                        variables[vd.Name.name] = new VarInfo(startMemPtr, size);
                        break;

                    case RegisterImmediateAssignment ria:
                        sb.AppendLine($"LDI {ria.Value}");
                        sb.AppendLine($"SET R{ria.RegisterDestination.NumberReg}");
                        break;

                    case RegisterSwapAssignment rsa:
                        sb.AppendLine($"GET R{rsa.Origin.NumberReg}");
                        sb.AppendLine($"SET R{rsa.RegisterDestination.NumberReg}");
                        break;

                    case RegisterMemoryAssignment rma:
                        findAddress(rma.Var.name, rma.Index, sb);
                        sb.AppendLine($"SET AR");
                        sb.AppendLine($"LDD");
                        sb.AppendLine($"SET R{rma.RegisterDestination.NumberReg}");
                        break;

                    case RegisterOperationAssignment roa:
                        if (roa.Input1 is Constant)
                        {
                            sb.AppendLine($"LDI {((Constant)(roa.Input1)).value}");
                            sb.AppendLine($"SET ALUInput1");
                        }
                        else
                        {
                            sb.AppendLine($"GET R{((Register)(roa.Input1)).NumberReg}");
                            sb.AppendLine($"SET ALUInput1");
                        }
                        if (roa.Operation != "!" && roa.Operation != "<<" && roa.Operation != ">>")
                        {
                            if (roa.Input2 is Constant)
                            {
                                sb.AppendLine($"LDI {((Constant)(roa.Input2)).value}");
                                sb.AppendLine($"SET ALUInput2");
                            }
                            else
                            {
                                sb.AppendLine($"GET R{((Register)(roa.Input2)).NumberReg}");
                                sb.AppendLine($"SET ALUInput2");
                            }
                        }
                        if (roa.Operation != "%")
                        {
                            sb.AppendLine($"EXE {opcodes[roa.Operation]}");
                            sb.AppendLine($"GET ALUOutput1");
                        }
                        else
                        {
                            sb.AppendLine($"EXE {opcodes["/"]}");
                            sb.AppendLine($"GET ALUOutput2");
                        }
                        sb.AppendLine($"SET R{roa.RegisterDestination.NumberReg}");
                        break;

                    case Statement s:
                        //Console.WriteLine("> " + s.Name);
                        if (s.Name.ToUpper() == "JMP")
                        {
                            compileJump(s.Data, 0, sb, replaceStack);

                            sb.AppendLine($"JMP");
                        }
                        else if (s.Name.ToUpper() == "JG")
                        {
                            moveValueOrRegValToTR(s.Data[0], sb);


                            sb.AppendLine($"SET ALUInput1");
                            if (s.Data.Count >= 3) { moveValueOrRegValToTR(s.Data[1], sb); } else { sb.AppendLine($"LDI 0"); }
                            sb.AppendLine($"SET ALUInput2");

                            compileJump(s.Data, (s.Data.Count >= 3 ? 2 : 1), sb, replaceStack);

                            jumpIstructions("2", sb);
                        }
                        else if (s.Name.ToUpper() == "JGE")
                        {
                            moveValueOrRegValToTR(s.Data[0], sb);

                            sb.AppendLine($"SET ALUInput1");
                            if (s.Data.Count >= 3) { moveValueOrRegValToTR(s.Data[1], sb); } else { sb.AppendLine($"LDI 0"); }
                            sb.AppendLine($"SET ALUInput2");

                            compileJump(s.Data, (s.Data.Count >= 3 ? 2 : 1), sb, replaceStack);

                            jumpIstructions("3", sb);
                        }
                        else if (s.Name.ToUpper() == "JZ")
                        {
                            moveValueOrRegValToTR(s.Data[0], sb);

                            sb.AppendLine($"SET ALUInput1");
                            if (s.Data.Count >= 3) { moveValueOrRegValToTR(s.Data[1], sb); } else { sb.AppendLine($"LDI 0"); }
                            sb.AppendLine($"SET ALUInput2");

                            compileJump(s.Data, (s.Data.Count >= 3 ? 2 : 1), sb, replaceStack);

                            jumpIstructions("0", sb);
                        }
                        else if (s.Name.ToUpper() == "JNZ")
                        {
                            moveValueOrRegValToTR(s.Data[0], sb);

                            sb.AppendLine($"SET ALUInput1");
                            if (s.Data.Count >= 3) { moveValueOrRegValToTR(s.Data[1], sb); } else { sb.AppendLine($"LDI 0"); }
                            sb.AppendLine($"SET ALUInput2");

                            compileJump(s.Data, (s.Data.Count >= 3 ? 2 : 1), sb, replaceStack);

                            jumpIstructions("1", sb);
                        }
                        else if (s.Name.ToUpper() == "JL")
                        {
                            moveValueOrRegValToTR(s.Data[0], sb);

                            sb.AppendLine($"SET ALUInput1");
                            if (s.Data.Count >= 3) { moveValueOrRegValToTR(s.Data[1], sb); } else { sb.AppendLine($"LDI 0"); }
                            sb.AppendLine($"SET ALUInput2");

                            compileJump(s.Data, (s.Data.Count >= 3 ? 2 : 1), sb, replaceStack);

                            jumpIstructions("4", sb);
                        }
                        else if (s.Name.ToUpper() == "JLE")
                        {
                            moveValueOrRegValToTR(s.Data[0], sb);

                            sb.AppendLine($"SET ALUInput1");
                            if (s.Data.Count >= 3) { moveValueOrRegValToTR(s.Data[1], sb); } else { sb.AppendLine($"LDI 0"); }
                            sb.AppendLine($"SET ALUInput2");

                            compileJump(s.Data, (s.Data.Count >= 3 ? 2 : 1), sb, replaceStack);

                            jumpIstructions("5", sb);
                        }
                        break;

                    case AssignmentStatement ass:
                        findAddress(ass.Destination.name, ass.Index, sb);
                        sb.AppendLine($"SET AR");

                        if (ass.Input is Register)
                        {
                            sb.AppendLine($"GET R{((Register)(ass.Input)).NumberReg}");
                        }
                        else if (ass.Input is Constant)
                        {
                            sb.AppendLine($"LDI {((Constant)(ass.Input)).value}");
                        }
                        sb.AppendLine("WRT");
                        break;
                    case JumpTarget jt:
                        labels[jt.Name] = ContaRighe(sb) - NodeCounter - 1;
                        break;

                    default:
                        throw new Exception("Non recognized node");
                        break;

                }
                NodeCounter++;
            }

            foreach (var r in replaceStack)
            {
                string riga = LeggiRiga(sb, r);
                string target = riga.Split(" ")[1];
                int targetN = labels[target];
                //Console.WriteLine(targetN);
                ModificaRiga(sb, r, "LDI " + targetN);
            }

            sb.AppendLine("STP");
            return sb.ToString();
        }

        private string OperandToRegister(AstNode node)
        {
            if (node is VariableName n)
            {
                if (variables.ContainsKey(n.name))
                    return $"@{variables[n.name]}";
                return n.name;
            }
            if (node is Constant c)
            {
                return c.value.ToString();
            }
            return "TR";
        }

        public static string LeggiRiga(StringBuilder sb, int indiceRiga)
        {
            string[] righe = sb.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            if (indiceRiga < 0 || indiceRiga >= righe.Length)
                throw new ArgumentOutOfRangeException(nameof(indiceRiga), "Indice riga fuori dal range." + indiceRiga);

            return righe[indiceRiga];
        }

        public static void ModificaRiga(StringBuilder sb, int indiceRiga, string nuovaRiga)
        {
            string[] righe = sb.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            if (indiceRiga < 0 || indiceRiga >= righe.Length)
                throw new ArgumentOutOfRangeException(nameof(indiceRiga), "Indice riga fuori dal range.");

            righe[indiceRiga] = nuovaRiga;

            sb.Clear();
            foreach (var riga in righe)
            {
                sb.AppendLine(riga);
            }
        }

        public static int ContaRighe(StringBuilder sb)
        {
            if (sb.Length == 0)
                return 0;

            int count = 0;
            for (int i = 0; i < sb.Length; i++)
            {
                if (sb[i] == '\n')
                    count++;
            }

            // Se il contenuto non finisce con newline, vuol dire che l’ultima riga non è ancora chiusa
            if (sb[sb.Length - 1] != '\n')
                count++;

            return count;
        }

        private void compileJump(List<PrimaryMemoryUnit> args, int preArgs, StringBuilder sb, List<int> replaceStack)
        {
            int len = args.Count - preArgs;
            PrimaryMemoryUnit jumpTarget = args[args.Count - 1]; //last arg
            if (len < 1)
                throw new Exception("incorrect number of args in jump");
            if (jumpTarget is Constant)
            {
                sb.AppendLine($"LDI {((Constant)(jumpTarget)).value}");
            }
            else if (jumpTarget is Register)
            {
                sb.AppendLine($"GET R{((Register)(jumpTarget)).NumberReg}");
            }
            else if (jumpTarget is GenericName)
            {
                sb.AppendLine($"LDI {((GenericName)(jumpTarget)).name}");
                replaceStack.Add(ContaRighe(sb) - 1);
            }
            else
            {
                throw new Exception("cannot compile jump");
            }
        }

        private void moveValueOrRegValToTR(PrimaryMemoryUnit v, StringBuilder sb)
        {
            if (v is Constant)
            {
                sb.AppendLine($"LDI {((Constant)(v)).value}");
            }
            else if (v is Register)
            {
                sb.AppendLine($"GET R{((Register)(v)).NumberReg}");
            }
            else
            {
                throw new Exception("attempeted to compile something that was not register or const " + v.ToString());
            }
        }

        private void jumpIstructions(string ifdo, StringBuilder sb)
        {
            sb.AppendLine($"EXE 1");
            sb.AppendLine($"IFD {ifdo}");
            sb.AppendLine($"JMP");
        }
    }
}
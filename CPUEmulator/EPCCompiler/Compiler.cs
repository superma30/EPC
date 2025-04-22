using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EPCCompiler
{
    public class Compiler
    {
        private Dictionary<string, int> variables = new();
        private Dictionary<string, string> opcodes = new()
        {
            { "+", "0" }, { "-", "1" }, { "*", "2" },
            { "/", "3" }, { "!", "4" }, { "&&", "5" },
            { "||", "6" }, { "^", "7" }, { "!&&", "8" },
            { "!||", "9" }, { "!^", "10" }, { ">>", "11" },
            { "<<", "12" }, { "%", "-1" }
        };
        private Dictionary<string, int> labels = new();
        private List<int> replaceStack = new();

        private int memPtr = 0x00;


        public void Compile(ProgramNode program, string outputPath)
        {
            var sb = new StringBuilder();

            int i = 0;
            foreach (var stmt in program.Statements)
            {
                sb.AppendLine($"#{i} : {stmt.ToString()}");
                switch (stmt)
                {
                    case VarDeclaration vd:
                        variables[vd.Name.name] = memPtr++;
                        break;

                    case RegisterImmediateAssignment ria:
                        sb.AppendLine($"LDI {ria.Value}");
                        sb.AppendLine($"SET R{ria.RegisterDestination.NumberReg}");
                        break;

                    case RegisterSwapAssignmente rsa:
                        sb.AppendLine($"GET {rsa.Origin.NumberReg}");
                        sb.AppendLine($"SET R{rsa.RegisterDestination.NumberReg}");
                        break;

                    case RegisterMemoryAssignmente rma:
                        int address = (variables[rma.Var.name]);
                        sb.AppendLine($"LDI {address}");
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

                            jumpIstructions("010", sb);
                        }
                        else if (s.Name.ToUpper() == "JGE")
                        {
                            moveValueOrRegValToTR(s.Data[0], sb);

                            sb.AppendLine($"SET ALUInput1");
                            if (s.Data.Count >= 3) { moveValueOrRegValToTR(s.Data[1], sb); } else { sb.AppendLine($"LDI 0"); }
                            sb.AppendLine($"SET ALUInput2");

                            compileJump(s.Data, (s.Data.Count >= 3 ? 2 : 1), sb, replaceStack);

                            jumpIstructions("011", sb);
                        }
                        else if (s.Name.ToUpper() == "JZ")
                        {
                            moveValueOrRegValToTR(s.Data[0], sb);
                            
                            sb.AppendLine($"SET ALUInput1");
                            if (s.Data.Count >= 3) { moveValueOrRegValToTR(s.Data[1], sb); } else { sb.AppendLine($"LDI 0"); }
                            sb.AppendLine($"SET ALUInput2");

                            compileJump(s.Data, (s.Data.Count >= 3 ? 2 : 1), sb, replaceStack);

                            jumpIstructions("000", sb);
                        }
                        else if (s.Name.ToUpper() == "JNZ")
                        {
                            moveValueOrRegValToTR(s.Data[0], sb);

                            sb.AppendLine($"SET ALUInput1");
                            if (s.Data.Count >= 3) { moveValueOrRegValToTR(s.Data[1], sb); } else { sb.AppendLine($"LDI 0"); }
                            sb.AppendLine($"SET ALUInput2");

                            compileJump(s.Data, (s.Data.Count >= 3 ? 2 : 1), sb, replaceStack);

                            jumpIstructions("001", sb);
                        }
                        else if (s.Name.ToUpper() == "JL")
                        {
                            moveValueOrRegValToTR(s.Data[0], sb);

                            sb.AppendLine($"SET ALUInput1");
                            if (s.Data.Count >= 3) { moveValueOrRegValToTR(s.Data[1], sb); } else { sb.AppendLine($"LDI 0"); }
                            sb.AppendLine($"SET ALUInput2");

                            compileJump(s.Data, (s.Data.Count >= 3 ? 2 : 1), sb, replaceStack);

                            jumpIstructions("100", sb);
                        }
                        else if (s.Name.ToUpper() == "JLE")
                        {
                            moveValueOrRegValToTR(s.Data[0], sb);

                            sb.AppendLine($"SET ALUInput1");
                            if (s.Data.Count >= 3) { moveValueOrRegValToTR(s.Data[1], sb); } else { sb.AppendLine($"LDI 0"); }
                            sb.AppendLine($"SET ALUInput2");

                            compileJump(s.Data, (s.Data.Count >= 3 ? 2 : 1), sb, replaceStack);

                            jumpIstructions("101", sb);
                        }
                        break;

                    case AssignmentStatement ass:
                        sb.AppendLine($"LDI {variables[ass.Destination.name]}");
                        sb.AppendLine($"SET AR");
                        sb.AppendLine($"GET R{ass.RegisterInput.NumberReg}");
                        sb.AppendLine("WRT");
                        break;
                    case JumpTarget jt:
                        labels[jt.Name] = ContaRighe(sb);
                        break;

                    default:
                        throw new Exception("Non recognized node");
                        break;

                }
                i++;
            }

            foreach (var r in replaceStack)
            {
                string riga = LeggiRiga(sb, r);
                string target = riga.Split(" ")[1];
                int targetN = labels[target];
                Console.WriteLine(targetN);
                ModificaRiga(sb, r, "LDI " + targetN);
            }

            sb.AppendLine("STP");
            File.WriteAllText(outputPath, sb.ToString());
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

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
            { "!||", "9" }, { "!^", "10" }, { "RSHIFT", "11" },
            { "LSHIFT", "12" }
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
                        variables[vd.Name.name] = memPtr;
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
                        if (roa.Operation != "!")
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
                            if (s.Data[0] is Constant)
                            {
                                sb.AppendLine($"LDI {((Constant)(s.Data[0])).value}");
                            }
                            else if (s.Data[0] is Register)
                            {
                                sb.AppendLine($"GET R{((Register)(s.Data[0])).NumberReg}");
                            }
                            else if (s.Data[0] is GenericName && (((GenericName)(s.Data[0])).name == "@"))
                            {
                                sb.AppendLine($"LDI {((GenericName)(s.Data[1])).name}");
                                replaceStack.Add(ContaRighe(sb) - 1);

                            }
                            sb.AppendLine($"JMP");
                        }
                        if (s.Name.ToUpper() == "JG")
                        {
                            if (s.Data[0] is Constant) //valore valutato
                            {
                                sb.AppendLine($"LDI {((Constant)(s.Data[0])).value}");
                            }
                            else if (s.Data[0] is Register)
                            {
                                sb.AppendLine($"GET R{((Register)(s.Data[0])).NumberReg}");
                            }
                            else
                            {
                                throw new Exception();
                            }
                            sb.AppendLine($"SET ALUInput1");
                            sb.AppendLine($"LDI 0");
                            sb.AppendLine($"SET ALUInput2");
                            if (s.Data[1] is Constant) //indirizzo del salto
                            {
                                sb.AppendLine($"LDI {((Constant)(s.Data[1])).value}");
                            }
                            else if (s.Data[1] is Register)
                            {
                                sb.AppendLine($"GET R{((Register)(s.Data[1])).NumberReg}");
                            }
                            else if (s.Data[1] is GenericName && (((GenericName)(s.Data[1])).name == "@"))
                            {
                                sb.AppendLine($"LDI {((GenericName)(s.Data[2])).name}");
                                replaceStack.Add(ContaRighe(sb) - 1);
                            }

                            sb.AppendLine($"EXE 1");
                            sb.AppendLine($"IFD 010");
                            sb.AppendLine($"JMP");
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
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using static EPCCompiler.Compiler;

namespace EPCCompiler
{
    public class Parser
    {
        private List<List<Token>> tokens;
        private int pos = 0;

        private Token Peek(int offset = 0) => (pos + offset < tokens[0].Count) ? tokens[0][pos + offset] : null;
        private Token Advance()
        {
            //Console.WriteLine(tokens[0][pos]);
            return pos < tokens[0].Count ? tokens[0][pos++] : null;
        }

        private bool Advance(List<Token> outtoken)
        {
            //Console.WriteLine(tokens[0][pos]);
            if (pos < tokens[0]?.Count)
            {
                outtoken.Add(tokens[0][pos++]);
                return true;
            }
            return false;
        }

        private Token Expect(string type)
        {
            if (Peek()?.Type != type)
                throw new Exception($"Expected token {type}, got {Peek()?.Type}");
            return Advance();
        }

        private bool isEOL ()
        {
            return tokens[0].Count-1 <= pos;
        }

        private bool eatLine (bool force)
        {
            if (isEOL() || force)
            {
                tokens.RemoveAt(0);
                pos = 0;
                return true;
            }
            //Console.WriteLine(" - " + tokens[0].Count + " - " + pos);
            return false;
        }

        public ProgramNode Parse(List<List<Token>> tokens)
        {
            this.tokens = tokens;
            var program = new ProgramNode();

            while (tokens.Count > 0)
            {
                pos = 0;
                program.Statements.AddRange(ParseLine());
            }

            fixIndexAccess(program);

            return program;
        }

        private void fixIndexAccess(ProgramNode program)
        {
            foreach (var stmt in program.Statements)
            {
                string[] splitted;
                switch (stmt)
                {
                    case AssignmentStatement ass:
                        splitted = ass.Destination.name.Split(@"\");
                        ass.Destination.name = splitted[0];
                        ass.Index = namesToAstNodes(GetElementsExceptFirst(splitted), true);
                        //ass.Print();
                        break;
                    case RegisterMemoryAssignment rma:
                        splitted = rma.Var.name.Split(@"\");
                        rma.Var.name = splitted[0];
                        rma.Index = namesToAstNodes(GetElementsExceptFirst(splitted), true);
                        //rma.Print();
                        break;
                }
            }
        }

        private List<string> GetElementsExceptFirst(string[] array)
        {
            List<string> result = new List<string>();
            for (int i = 1; i < array.Length; i++)
            {
                result.Add(array[i]);
            }
            return result;
        }

        private List<AstNode> ParseLine()
        {
            LogParseFunction("ParseLine");

            List<AstNode> output = new();

            if (!isEOL())
            {
                var token = Peek();

                if (token.Type == "Ident" && token.Value == "var")
                {
                    output = (ParseVarDeclaration());
                }
                else if (token.Type == "Call")
                {
                    output = new List<AstNode> { new JumpTarget { Name = tokens[0][1].Value } };
                }
                else if (tokens[0].Any(t => t.Type == "Equals"))
                { //assign
                    output = parseAssign();
                }
                else
                {
                    output = parseStatement();
                }
            }
            eatLine(true);
            return output;
        }

        private List<AstNode> ParseVarDeclaration()
        {
            LogParseFunction("ParseVarDeclaration");
            Advance(); // var
            string name = Advance().Value;
            List<int> size = new List<int> { };

            if (name.ToUpper()[0] == 'R')
                throw new Exception();

            while (Peek() != null && Peek().Type == "Number")
                size.Add(int.Parse(Advance().Value));

            if (size.Count == 0)
                size.Add(1);

            return new List<AstNode> {
                new VarDeclaration { Name = new VariableName (name), Size=size }
            };
        }

        private (string name, List<PrimaryMemoryUnit> indexes) parseVarIndex (int sIndex)
        {
            if (tokens[0][sIndex].Type == "Ident")
            {
                string name = tokens[0][sIndex].Value;

                List<PrimaryMemoryUnit> indexes = new();
                int i = 1;
                while (tokens[0][i + sIndex].Type == "IndexAccess")
                {
                    tokens.RemoveAt(i + sIndex);
                    var token = tokens[0][i + sIndex];
                    if (token.Type != "Number" && token.Type != "Ident")
                    {
                        if (token.Type == "Number")
                            indexes.Add(new Constant(int.Parse(token.Value)));
                        else if (token.Type == "Ident")
                            indexes.Add(new Register(int.Parse(token.Value.Substring(1))));
                    }
                    else
                    {
                        throw new Exception("index incorrect");
                    }
                    tokens.RemoveAt(i + sIndex);
                }
                return (name, indexes);
            }
            else
            {
                throw new Exception("attemped to consider as var something that was not ident");
            }
        }

        private List<AstNode> parseAssign()
        {
            LogParseFunction("parseAssign");
            if (tokens[0][0].Value.ToUpper()[0] == 'R')
            {
                if (tokens[0].Count == 3)
                {
                    LogParseFunction("parseAssign w no calculation");
                    if (tokens[0][2].Type == "Number")
                    {
                        return new List<AstNode> { new RegisterImmediateAssignment {
                            RegisterDestination = new Register (int.Parse(tokens[0][0].Value.Substring(1))),
                            Value = int.Parse(tokens[0][2].Value)
                            }
                        };
                    }
                    else if (tokens[0][2].Type == "Ident" && tokens[0][2].Value.ToUpper()[0] == 'R')
                    {
                        return new List<AstNode> { new RegisterSwapAssignment {
                            RegisterDestination = new Register (int.Parse(tokens[0][0].Value.Substring(1))),
                            Origin = new Register (int.Parse(tokens[0][2].Value.Substring(1)))
                            }
                        };
                    }
                    else if (tokens[0][2].Type == "Ident")
                    {
                        return new List<AstNode> { new RegisterMemoryAssignment {
                            RegisterDestination = new Register (int.Parse(tokens[0][0].Value.Substring(1))),
                            Var = new VariableName(tokens[0][2].Value),
                            }
                        };
                    }
                }
                else if (tokens[0].Count == 5)
                {
                    LogParseFunction("parseAssign w calculation 2 -> 1");
                    if ((tokens[0][2].Type != "Number" && tokens[0][2].Type != "Ident") || (tokens[0][4].Type != "Number" && tokens[0][4].Type != "Ident"))
                        throw new Exception();
                    if ((tokens[0][2].Type == "Ident" && tokens[0][2].Value.ToUpper()[0] != 'R') || (tokens[0][4].Type == "Ident" && tokens[0][4].Value.ToUpper()[0] != 'R'))
                        throw new Exception();

                    PrimaryMemoryUnit in1 = (tokens[0][2].Type == "Ident" ? new Register(int.Parse(tokens[0][2].Value.Substring(1))) : new Constant(int.Parse(tokens[0][2].Value)));
                    PrimaryMemoryUnit in2 = (tokens[0][4].Type == "Ident" ? new Register(int.Parse(tokens[0][4].Value.Substring(1))) : new Constant(int.Parse(tokens[0][4].Value)));
                    return new List<AstNode> { new RegisterOperationAssignment {
                        RegisterDestination = new Register (int.Parse(tokens[0][0].Value.Substring(1))),
                        Operation = tokens[0][3].Value,
                        Input1 = in1,
                        Input2 = in2
                        }
                    };
                }
                else if (tokens[0].Count == 4)
                {
                    LogParseFunction("parseAssign w calculation 1 -> 1");
                    if ((tokens[0][2].Type != "Number" && tokens[0][3].Type != "Ident"))
                        throw new Exception();
                    if ((tokens[0][2].Type == "Ident" && tokens[0][3].Value.ToUpper()[0] != 'R'))
                        throw new Exception();

                    PrimaryMemoryUnit in1 = (tokens[0][3].Type == "Ident" ? new Register(int.Parse(tokens[0][3].Value.Substring(1))) : new Constant(int.Parse(tokens[0][3].Value)));
                    return new List<AstNode> { new RegisterOperationAssignment {
                        RegisterDestination = new Register (int.Parse(tokens[0][0].Value.Substring(1))),
                        Operation = tokens[0][2].Value,
                        Input1 = in1
                        }
                    };
                }
            }
            else if (tokens[0].Count == 3)
            {
                if (tokens[0][2].Type == "Ident" && tokens[0][2].Value.ToUpper()[0] == 'R')
                {
                    return new List<AstNode> { new AssignmentStatement {
                            Destination = new VariableName (tokens[0][0].Value),
                            Input = new Register (int.Parse(tokens[0][2].Value.Substring(1))),
                            }
                    };
                }
                else if (tokens[0][2].Type == "Number")
                {
                    return new List<AstNode> { new AssignmentStatement {
                            Destination = new VariableName (tokens[0][0].Value),
                            Input = new Constant (int.Parse(tokens[0][2].Value))
                            }
                    };
                }

            }
            throw new Exception();
        }

        private List<AstNode> parseStatement()
        {
            LogParseFunction("parseStatement");
            List<PrimaryMemoryUnit> args = new();
            for (int i = 1; i < tokens[0].Count; i++)
            {
                if (tokens[0][i].Type == "Number")
                {
                    args.Add( new Constant (int.Parse(tokens[0][i].Value)));
                }
                else if (tokens[0][i].Type == "Ident" && tokens[0][i].Value.ToUpper()[0] == 'R')
                {
                    args.Add(new Register(int.Parse(tokens[0][i].Value.Substring(1))));
                }
                else
                {
                    args.Add(new GenericName(tokens[0][i].Value));
                }
            }
            //Console.WriteLine("asdasd");
            return new List<AstNode> { new Statement { Name = tokens[0][0].Value, Data = args } };
        }


        public static string TokenSpaceAsm(List<Token> tokens)
        {
            return string.Join(" ", tokens.Select(t => t.Value));
        }

        
        private PrimaryMemoryUnit nameToAstNode (string name, bool onlyPrimary)
        {
            if (name.ToUpper()[0] == 'R')
                return new Register(int.Parse(name.Substring(1)));
            else
                return new Constant(int.Parse(name));
        }

        private List<PrimaryMemoryUnit> namesToAstNodes(List<string> names, bool onlyPrimary)
        {
            var output = new List<PrimaryMemoryUnit>();
            foreach (var name in names)
            {
                output.Add(nameToAstNode(name, onlyPrimary));
            }
            return output;
        }

        /*private List<VariableName> tokensToAstNames(List<Token> tokens)
        {
            var output = new List<VariableName>();
            foreach (var token in tokens)
            {
                if (token.Type == "Ident")
                    output.Add((VariableName)tokenToAstNode(token));
            }
            return output;
        }*/

        private void LogParseFunction (string s)
        {
            if (false)
            {
                Console.WriteLine(s);
            }
        }
    }
}

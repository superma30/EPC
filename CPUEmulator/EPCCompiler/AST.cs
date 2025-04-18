using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace EPCCompiler
{
    public abstract class AstNode
    {
        public abstract void Print(string indent = "");
    }

    public class ProgramNode : AstNode
    {
        public List<AstNode> Statements = new List<AstNode>();

        public override void Print(string indent = "")
        {
            Console.WriteLine(indent + "ProgramNode");
            foreach (var stmt in Statements)
                stmt.Print(indent + "  ");
        }
    }

    public class Statement : AstNode
    {
        public string Name;
        public List<PrimaryMemoryUnit> Data;
        public override void Print(string indent = "")
        {
            Console.WriteLine(indent + "statement name: " + Name);
            foreach (var d in Data)
            {
                d.Print();
            }
        }
    }

    public class JumpTarget : AstNode
    {
        public string Name;
        public override void Print(string indent = "")
        {
            Console.WriteLine(indent + "jump label: " + Name);
        }
    }

    public class VarDeclaration : AstNode
    {
        public VariableName Name;

        public override void Print(string indent = "")
        {
            Console.WriteLine(indent + "VarDeclaration");
            Name?.Print(indent + "  ");
        }
    }

    public abstract class RegisterAssignment : AstNode
    {
        public Register RegisterDestination;
    }

    public class RegisterOperationAssignment : RegisterAssignment
    {
        public PrimaryMemoryUnit Input1;
        public PrimaryMemoryUnit Input2;
        public string Operation;

        public override void Print(string indent = "")
        {
            Console.WriteLine(indent + "RegisterAssignment");
            Console.WriteLine(indent + "  RegisterDestination: " + RegisterDestination);
            Console.WriteLine(indent + "  Input1: " + Input1);
            Console.WriteLine(indent + "  Input2: " + Input2);
            Console.WriteLine(indent + "  Operation: " + Operation);
        }
    }

    public class RegisterImmediateAssignment : RegisterAssignment
    {
        public int Value;

        public override void Print(string indent = "")
        {
            Console.WriteLine(indent + "RegisterAssignment");
            Console.WriteLine(indent + "  RegisterDestination: " + RegisterDestination);
            Console.WriteLine(indent + "  Value: " + Value);
        }
    }

    public class RegisterMemoryAssignmente : RegisterAssignment
    {
        public VariableName Var;

        public override void Print(string indent = "")
        {
            Console.WriteLine(indent + "RegisterAssignment");
            Console.WriteLine(indent + "  RegisterDestination: " + RegisterDestination);
            Console.WriteLine(indent + "  Var: " + Var);
        }
    }

    public class RegisterSwapAssignmente : RegisterAssignment
    {
        public Register Origin;

        public override void Print(string indent = "")
        {
            Console.WriteLine(indent + "RegisterAssignment");
            Console.WriteLine(indent + "  RegisterDestination: " + RegisterDestination);
            Console.WriteLine(indent + "  Reg: " + Origin);
        }
    }

    public class AssignmentStatement : AstNode
    {
        public VariableName Destination;
        public Register RegisterInput;

        public override void Print(string indent = "")
        {
            Console.WriteLine(indent + "AssignmentStatement");
            Console.WriteLine(indent + "  Destination:");
            Destination?.Print();
            Console.WriteLine(indent + "  Register:" + RegisterInput);
        }
    }

    public abstract class MemoryUnit : AstNode
    {

    }

    public abstract class PrimaryMemoryUnit : MemoryUnit
    {

    }

    public class Register : PrimaryMemoryUnit
    {
        public int NumberReg;

        public override void Print(string indent = "")
        {
            Console.WriteLine(indent + "Reg num: " + NumberReg);
        }

        public Register(int n)
        {
            NumberReg = n;
        }
    }

    public class Constant : PrimaryMemoryUnit
    {
        public int value = 0;

        public override void Print(string indent = "")
        {
            Console.WriteLine(indent + "Constant: " + value);
        }

        public Constant(int c)
        {
            value = c;
        }
    }

    public class VariableName : MemoryUnit
    {
        public string name = "";

        public override void Print(string indent = "")
        {
            Console.WriteLine(indent + "Name: " + name);
        }

        public VariableName (string n)
        {
            name = n;
        }
    }
    public class GenericName : PrimaryMemoryUnit
    {
        public string name = "";

        public override void Print(string indent = "")
        {
            Console.WriteLine(indent + "Name: " + name);
        }

        public GenericName(string n)
        {
            name = n;
        }
    }
}
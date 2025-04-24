using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPCCompiler
{
    // Classe per l'assembler Machine Code
    // Dalle KeyWord low level
    // A 0 e 1
    //
    // Tabella completa \/
    // https://docs.google.com/spreadsheets/d/15FkXNwweOnnaq57blI_y7s8rc9Au6iMYyGPP2iMU9yU/edit?usp=sharing
    //
    // Per esempi di file in input guardare lowlang.txt
    public class MachineCodeAssembler
    {
        private readonly Dictionary<string, string> opcodeMap = new()
        {
            { "NUL", "000000000000" },
            { "STP", "000100000000" },
            { "GET", "0010" },
            { "SET", "0011" },
            { "LDD", "010000000000" },
            { "WRT", "010100000000" },
            { "EXE", "0110" },
            { "JMP", "011100000000" },
            { "IFD", "1000" },
            { "LDI", "1001" }
        };
        private readonly Dictionary<string, string> registerMap = new()
        {
        { "PC", "00000000" },
        { "FR", "00000001" },
        { "ALUINPUT1", "00000010" },
        { "ALUINPUT2", "00000011" },
        { "ALUOUTPUT1", "00000100" },
        { "ALUOUTPUT2", "00000101" },
        { "AR", "00000110" },
        { "R0", "00001010" },
        { "R1", "00001011" },
        { "R2", "00001100" },
        { "R3", "00001101" },
        { "R4", "00001110" },
        { "R5", "00001111" },
        { "R6", "00010000" },
        { "R7", "00010001" },
        { "R8", "00010010" },
        { "R9", "00010011" },
        { "R10", "00010100" },
        { "R11", "00010101" },
        { "R12", "00010110" },
        { "R13", "00010111" },
        { "R14", "00011000" },
        { "R15", "00011001" },
        { "R16", "00011010" },
        { "R17", "00011011" },
        { "R18", "00011100" },
        { "R19", "00011101" },
        { "R20", "00011110" },
        { "R21", "00011111" },
        { "R22", "00100000" },
        { "R23", "00100001" },
        { "R24", "00100010" },
        { "R25", "00100011" },
        { "R26", "00100100" },
        { "R27", "00100101" },
        { "R28", "00100110" },
        { "R29", "00100111" },
        { "R30", "00101000" },
        { "R31", "00101001" }
        };
        private readonly Dictionary<string, string> ExecuteMap= new()
        {
        { "0",  "00000000" },
        { "1",  "00000001" },
        { "2",  "00000010" },
        { "3",  "00000011" },
        { "4",  "00000100" },
        { "5",  "00000101" },
        { "6",  "00000110" },
        { "7",  "00000111" },
        { "8",  "00001000" },
        { "9",  "00001001" },
        { "10", "00001010" },
        { "11", "00001011" },
        { "12", "00001100" }
};
        private readonly Dictionary<string, string> IfdMap = new()
        {
        { "0",  "00000000" },
        { "1",  "00000001" },
        { "2",  "00000010" },
        { "3",  "00000011" },
        { "4",  "00000100" },
        { "5",  "00000101" }
        };

        public string GetFromBin(int type, string bin)
        {
            switch (type)
            {
                case 0:
                    foreach(var item in opcodeMap)
                    {
                        if (item.Value.Substring(0, 4) == bin.PadLeft(4, '0').Substring(0, 4))
                        {
                            return item.Key;
                        }
                    }
                    break;
                case 1:
                    foreach (var item in registerMap)
                    {
                        if (item.Value == bin.PadLeft(8, '0').Substring(0, 8))
                        {
                            return item.Key;
                        }
                    }
                    break;
                case 2:
                    foreach (var item in ExecuteMap)
                    {
                        if (item.Value == bin.PadLeft(8, '0').Substring(0, 8))
                        {
                            return item.Key;
                        }
                    }
                    break;
            }
            return "";
        }

        public string From_low_To_Bin(string Path_input)
        {
            string[] all_lines_of_code = File.ReadAllLines(Path_input);
            string bin_code = "";
            List<string> output_bin = new();
            foreach (string line_of_code in all_lines_of_code)
            {
                string line = line_of_code.Trim().ToUpper();
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                    continue;
                string[] structure_line = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string istruction = structure_line[0];
                if (!opcodeMap.ContainsKey(istruction))
                {
                    Console.WriteLine($"Istruzione non riconosciuta: {istruction}");
                    return bin_code + $"Istruzione non riconosciuta: {istruction}";
                }
                string bin_istruction = opcodeMap[istruction];
                string parameter = "";
                string bin_parameter = "";
                if (structure_line.Length > 1)
                {
                    parameter = structure_line[1];

                }
                if (istruction == "GET" || istruction == "SET")
                {
                    bin_parameter = registerMap[parameter];
                }
                else if (istruction == "EXE")
                {
                    bin_parameter = ExecuteMap[parameter];
                }
                else if (istruction == "IFD")
                {
                    bin_parameter = IfdMap[parameter];

                }
                else if (istruction == "LDI")
                {
                    var arg = Convert.ToInt16(parameter);
                    bin_parameter = Convert.ToString(arg, 2).PadLeft(8, '0').Substring(0, 8);
                }

                bin_code += $"{bin_istruction}{bin_parameter}\n";
            }
            return bin_code;
        }

        public string From_low_To_Bin_String(string code)
        {
            string[] all_lines_of_code = code.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string bin_code = "";
            List<string> output_bin = new();
            foreach (string line_of_code in all_lines_of_code)
            {
                string line = line_of_code.Trim().ToUpper();
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                    continue;
                string[] structure_line = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string istruction = structure_line[0];
                if (!opcodeMap.ContainsKey(istruction))
                {
                    Console.WriteLine($"Istruzione non riconosciuta: {istruction}");
                    return bin_code + $"Istruzione non riconosciuta: {istruction}";
                }
                string bin_istruction = opcodeMap[istruction];
                string parameter = "";
                string bin_parameter = "";
                if (structure_line.Length > 1)
                {
                    parameter = structure_line[1];

                }
                if (istruction == "GET" || istruction == "SET")
                {
                    bin_parameter = registerMap[parameter];
                }
                else if (istruction == "EXE")
                {
                    bin_parameter = ExecuteMap[parameter];
                }
                else if (istruction == "IFD")
                {
                    bin_parameter = IfdMap[parameter];

                }
                else if (istruction == "LDI")
                {
                    var arg = Convert.ToInt16(parameter);
                    bin_parameter = Convert.ToString(arg, 2).PadLeft(8, '0').Substring(0, 8);
                }

                bin_code += $"{bin_istruction}{bin_parameter}\n";
            }
            return bin_code;
        }
    }
}

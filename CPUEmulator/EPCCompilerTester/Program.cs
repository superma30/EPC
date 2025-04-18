using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace EPCCompilerTester
{
    class FileCompiler
    {
        static void Main(string[] args)
        {
            string inputPath = @"..\..\..\midlang.txt";
            string outputPath = @"..\..\..\lowlang.txt";

            var code = File.ReadAllText(inputPath);


            var lexer = new EPCCompiler.Lexer();
            var tokens = lexer.Tokenize(code);

            var parser = new EPCCompiler.Parser();
            var ast = parser.Parse(tokens);

            var json = JsonSerializer.Serialize(ast, new JsonSerializerOptions
            {
                WriteIndented = true,
                IncludeFields = true
            });

            var compiler = new EPCCompiler.Compiler();
            compiler.Compile(ast, outputPath);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Compilazione completata. File salvato in lowlang.txt");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

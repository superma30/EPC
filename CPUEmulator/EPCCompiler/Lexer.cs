using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EPCCompiler
{
    public class Token
    {
        public string Type;
        public string Value;

        public Token(string type, string value)
        {
            Type = type;
            Value = value;
        }

        public override string ToString() => $"{Type}: {Value}";
    }

    public class Lexer
    {
        private static readonly Regex TokenRegex = new Regex(@"
            (?<Operation>\!\&\&|\!\|\||\!\^|\&\&|\|\||\^|\!|\+|\-|\*|\%|\/) |
            (?<Comment>\#.*) |
            (?<Call>@) |
            (?<Number>\d+) |
            (?<Ident>[A-Za-z_][A-Za-z0-9_]*) |
            (?<Equals>=) |
            (?<Comma>,) |
            (?<LBracket>\[) |
            (?<RBracket>\]) |
            (?<LBrace>\{) |
            (?<RBrace>\}) |
            (?<LParen>\() |
            (?<RParen>\)) |
            (?<NewLine>\n) |
            (?<Whitespace>\s+)
        ", RegexOptions.IgnorePatternWhitespace);

        public List<List<Token>> Tokenize(string input)
        {
            var lines = input.Split('\n');
            var allTokens = new List<List<Token>>();

            foreach (var line in lines)
            {
                var lineTokens = new List<Token>();
                var matches = TokenRegex.Matches(line);

                foreach (Match match in matches)
                {
                    foreach (string name in TokenRegex.GetGroupNames())
                    {
                        if (name == "0" || !match.Groups[name].Success) continue;
                        if (name == "Whitespace" || name == "Comment") break;

                        lineTokens.Add(new Token(name, match.Value));
                        break;
                    }
                }

                allTokens.Add(lineTokens);
            }

            return allTokens;
        }

    }
}

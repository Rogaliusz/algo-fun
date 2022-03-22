using System.Text.RegularExpressions;

namespace AlgoFun.StringCalculator;

public class StringCalculator
{
     private readonly IDictionary<char, Func<int, int, int>> _operators =
        new Dictionary<char, Func<int, int, int>>
        {
            { '*', (a, b) => a * b },
            { '/', (a, b) => a / b },
            { '+', (a, b) => a + b },
            { '-', (a, b) => a - b },
        };

     public decimal Evaluate(string expression)
     {
         expression = expression.Replace(" ", "");
         var chars = _operators.Keys;

         while (expression.Contains('('))
         {
             expression = CalculateNestedExpressions(expression, chars);
         }
         
         var result = Calculate(expression, chars);

         return result;
     }

     private string CalculateNestedExpressions(string expression, ICollection<char> chars)
     {
         var lastEndBracketIdx = expression.IndexOf(')');
         var pairedIdx = GetPairedBracket(expression, lastEndBracketIdx);
         var nestedExpression = expression.Substring(pairedIdx + 1, lastEndBracketIdx - pairedIdx - 1);
         var nestedResult = Calculate(nestedExpression, chars);

         expression = expression.Replace($"({nestedExpression})", nestedResult.ToString());
         return expression;
     }

     private static int GetPairedBracket(string expression, int lastEndBracketIdx)
     {
         for (var i = lastEndBracketIdx; i >= 0; i--)
         {
             if (expression[i] == '(')
             {
                 return i;
             }
         }

         return 0;
     }

     private int Calculate(string expression, ICollection<char> chars)
    {
        foreach (var c in chars)
        {
            var regex = new Regex($@"-?\d+\{c}-?\d+");
            while (regex.IsMatch(expression))
            {
                var match = regex.Match(expression);
                var splitted = match.Value
                    .Split(c, StringSplitOptions.RemoveEmptyEntries);
                
                var a = int.Parse(splitted[0]);
                var b = int.Parse(splitted[1]);
                
                var result = _operators[c].Invoke(a, b);
                expression = expression.Replace(match.Value, result.ToString());
            }
        }

        return int.Parse(expression);
    }
}
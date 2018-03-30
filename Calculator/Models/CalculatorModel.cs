using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using org.mariuszgromada.math.mxparser;

namespace Calculator.Models
{
    public interface IOperations
    {
        void Insert(string digit);
        void InsertOperation(Operations operation);
    }

    public enum Operations
    {
        Clear,
        Equal,
        Backspace,
        Bracket,
        Percentage
    }

    public class CalculatorModel : IOperations
    {
        public string Expression { get; private set; } = string.Empty;
        public string Result { get; private set; } = string.Empty;


        private const string OpenBracketSign = "(";
        private const string EncloseBracketSign = ")";

        private readonly Stack<string> _encloseBracketStack;

        public CalculatorModel()
        {
            _encloseBracketStack = new Stack<string>();
        }

        private void Clear()
        {
            Expression = string.Empty;
            Result = string.Empty;
        }

        private void Equal()
        {
            Expression = Result;
        }

        private void Backspace()
        {
            if (string.IsNullOrEmpty(Expression)) return;

            if (Regex.IsMatch(Expression, $@"\{EncloseBracketSign}$"))
                _encloseBracketStack.Push(EncloseBracketSign);
            else if (Regex.IsMatch(Expression, $@"\{OpenBracketSign}$"))
                _encloseBracketStack.Pop();


            Expression = Expression.Remove(Expression.Length - 1, 1);
            Result = CalculateExpression();
        }

        private void AddBracket()
        {
            if (!_encloseBracketStack.Any())
            {
                Expression += Regex.IsMatch(Expression.Last().ToString(), @"\d") ? $"*{OpenBracketSign}" : OpenBracketSign;
                _encloseBracketStack.Push(EncloseBracketSign);
                Result = string.Empty;
            }
            else
            {
                Expression += _encloseBracketStack.Pop();
                Result = CalculateExpression();
            }
        }

        public void Insert(string element)
        {
            if (Regex.IsMatch(element, @"[+\-*/%,]"))
                Expression += string.IsNullOrEmpty(Expression) || Regex.IsMatch(Expression, @"[+\-*/%]$") ? string.Empty : element;
            else
                Expression += element;

            Result = CalculateExpression();
        }

        public void InsertOperation(Operations operation)
        {
            switch (operation)
            {
                case Operations.Backspace:
                    Backspace();
                    break;
                case Operations.Clear:
                    Clear();
                    break;
                case Operations.Bracket:
                    AddBracket();
                    break;
                case Operations.Equal:
                    Equal();
                    break;
            }
        }

        string CalculateExpression()
        {
            if (string.IsNullOrEmpty(Expression) || !Regex.IsMatch(Expression.Last().ToString(), @"(\d|\))") || _encloseBracketStack.Any())
                return string.Empty;

            Expression expression = new Expression(Expression);

            #region MyRegion

            //var listOfDigits = Regex.Split(Expression, @"\D").Select(Convert.ToDouble).ToList();
            //var listOfOperations = Regex.Split(Expression, @"((\d*\,\d+)|\d+)").Select(sign =>
            //{
            //    switch (sign)
            //    {
            //        case "+":
            //            return Operations.Add;
            //        case "-":
            //            return Operations.Substruct;
            //        case "*":
            //            return Operations.Multiply;
            //        default:
            //            return Operations.Divide;
            //    }
            //}).ToList();


            //if (listOfDigits.Count > listOfOperations.Count)
            //{
            //    CurrentOperation = Operations.Add;
            //    var result = 0.0;
            //    for (int index = 0; index < listOfDigits.Count; index++)
            //    {
            //        var digit = listOfDigits[index];
            //        switch (CurrentOperation)
            //        {
            //            case Operations.Add:
            //                result += digit;
            //                break;
            //            case Operations.Substruct:
            //                result -= digit;
            //                break;
            //            case Operations.Multiply:
            //                result *= digit;
            //                break;
            //            case Operations.Divide:
            //                result /= digit;
            //                break;
            //            default:
            //                throw new ArgumentOutOfRangeException();
            //        }

            //        if (index > listOfOperations.Count)
            //        {

            //        }
            //        else
            //        {
            //            CurrentOperation = listOfOperations[index];
            //        }
            //    }
            //    return result.ToString(CultureInfo.InvariantCulture);
            //}
            //return string.Empty;


            #endregion
            return expression.calculate().ToString(CultureInfo.InvariantCulture);
        }
    }
}

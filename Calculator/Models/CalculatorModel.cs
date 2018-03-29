using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using org.mariuszgromada.math.mxparser;

namespace Calculator.Models
{
    public interface IOperations
    {
        void Add();
        void Substruct();
        void Divide();
        void Multiply();
        void Clear();
        void Equal();
        void Backspace();
        void AddBracket();
        void InsertDigit(string digit);
    }

    public class CalculatorModel : IOperations
    {
        public string Expression;
        public string Result;

        private const string AddSign = "+";
        private const string SubstructSign = "-";
        private const string MultiplySign = "*";
        private const string DivideSign = "/";
        private const string OpenBracketSign = "(";
        private const string EncloseBracketSign = ")";

        private Stack<char> _openParenthesisStack;
        private Stack<char> _closeParenthesisStack;

        public void Add()
        {
            Expression += AddSign;
        }

        public void Substruct()
        {
            Expression += SubstructSign;
        }

        public void Divide()
        {
            Expression += DivideSign;
        }

        public void Multiply()
        {
            Expression += MultiplySign;
        }

        public void Clear()
        {
            Expression = string.Empty;
            Result = string.Empty;
        }

        public void Equal()
        {
            Expression = string.Empty;
        }

        public void Backspace()
        {
            if (string.IsNullOrEmpty(Expression)) return;

            Expression = Expression.Remove(Expression.Length - 1, 1);
            Result = CalculateExpression();
        }

        public void AddBracket()
        {
            
        }

        public void InsertDigit(string digit)
        {
            Expression += digit;
            Result = CalculateExpression();
        }

        string CalculateExpression()
        {
            if (string.IsNullOrEmpty(Expression) || !Regex.IsMatch(Expression.Last().ToString(), @"\d"))
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

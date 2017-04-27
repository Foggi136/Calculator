using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Kalkul;

namespace Kalcul
{
   public class MathExprIntepreter: ParserBase
    {
        public static readonly NumberFormatInfo NFI =
            new NumberFormatInfo();
        public MathExprIntepreter(string source) : base(source)
        {

        }
        public double NUMBER()
        {
            string number = "";
            while (Current == '.' || char.IsDigit(Current))
            {
                number += Current;
                Next();
            }
          /*  if (number.Length == 0)
                Skip();*/

            return double.Parse(number, NFI);
        }
        public double Group()
        {
            if (IsMatch("("))
            {
                Match("(");
                double result = Add();
                Match(")");
                return result;
            }
            else
                return NUMBER();
        }
        public double Mult()
        {
            double result = Group();
            while (IsMatch("*","/"))
            {
                string oper = Match("*", "/");
                double temp = Group();
                result = oper == "*" ? result * temp
                                     : result / temp;
            }
            return result;                        
        }
        public double Add()
        {
            double result = Mult();
            while (IsMatch("+", "-"))
            {
                string oper = Match("+", "-");
                double temp = Mult();
                result = oper == "+" ? result + temp
                                     : result - temp;
            }
            return result;
        }
        public double Result()
        {
            return Add();
        }
        public double Execute()
        {
            Skip();
            double result = Result();
            if (End)
                return result;
            else return 0; // переделать костыль
        }
        public static double Execute(string source)
        {
            MathExprIntepreter mei = new MathExprIntepreter(source);
            return mei.Execute();
        }
    }
}

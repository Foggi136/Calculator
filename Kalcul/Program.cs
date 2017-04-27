using Kalcul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalkul
{
   public class ParserBase
    {
        public const string DEFAULT_WHITESPACES = " \n\r\t";
        private string source = null;
        private int pos = 0;

        public ParserBase(string source)
        {
            this.source = source;
        }

        public string Source
        {
            get { return source; }
        }
        public int Pos
        {
            get { return pos; }
        }

        protected char this [int index]
        {
            get
            {
                return index < source.Length ? source[index] : (char) 0;
            }
        }

        public char Current
        {
            get { return this[Pos]; }
        }
        public bool End
        {
            get { return Current == 0; }
        }
        public void Next()
        {
            if (!End)
                pos++;
        }
        public virtual void Skip ()
        {
            while (DEFAULT_WHITESPACES.IndexOf(this[pos]) >= 0)
                Next();
        }
        protected string MatchNoExcept(params string[] terms)
        {
            int pos = Pos;
            foreach(string s in terms)
            {
                bool match = true;
                foreach (char c in s)
                    if (Current == c)
                        Next();
                else
                    {
                        this.pos = pos;
                        match = false;
                        break;
                    }
                if (match)
                {
                    Skip();
                    return s;
                }
            }
            return null;
        }
        public bool IsMatch(params string[] terms)
        {
            int pos = Pos;
            string result = MatchNoExcept(terms);
            this.pos = pos;
            return result != null;
        }
        public string Match(params string[] termes)
        {
            int pos = Pos;
            string result = MatchNoExcept(termes);
            if (result == null)
            {
                string message = "Ожидалась одна из строк: ";
                bool first = true;
                foreach (string s in termes)
                {
                    if (!first)
                        message += ",";
                    message += string.Format("\"{0}\"", s);
                    first = false;
                }
                /*throw new ParserBaseException(
                    string.Format("{0} (pos = {1})", message, pos));*/
            }
            return result;
        }
        public string Match(string s)
        {
            int pos = Pos;
           
            
                return Match(new string[] { s });
            
            
               /* throw new ParserBaseException(
                string.Format(
                "{0}: '{1}' (pos={2})",
                s.Length == 1 ? "Ожидался символ"
                : "Ожидалась строка",
                s, pos
                )
                );*/
            
        }
        static void Main(string[] args)
        {
            var k = ConsoleKey.Escape;
            var cki = new ConsoleKeyInfo();
            Console.WriteLine("Alfa version 0.1.0\tДля выходы из программы нажмите Escape\nВведите ваш пример: ");
            do
            {
                double result = MathExprIntepreter.Execute(Console.ReadLine());
                Console.WriteLine("Ответ = " + result);    
                cki = Console.ReadKey(true);
            } while (cki.Key != k);
        }
    }
}

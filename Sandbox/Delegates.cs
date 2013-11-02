using System;
using System.Linq;

namespace Sandbox
{
    public class DelegateDemo
    {
        public delegate string Sayer(string name);

        public string SayHello(string name)
        {
            return string.Format("Hello, {0}!", name);
        }

        public string SayGoodbye(string name)
        {
            return string.Format("Goodbye, {0}.", name);
        }

        public void DoStuff()
        {
            Sayer sayer = SayHello;

            Console.WriteLine(sayer("James"));
        }

        public delegate int StringFunction(string str);

        public void MethodOne()
        {
            StringFunction myDelegate = delegate (string str)
            {
                if (string.IsNullOrWhiteSpace(str))
                    return 0;

                return str.Count(char.IsLetterOrDigit);
            };

            int result = myDelegate("Hello there!");
            Console.WriteLine("The result is: " + result);
        }

        public void MethodTwo()
        {
            var result = "Hello World".Apply(RemoveWhitespace);
        }

        public string RemoveWhitespace(string str)
        {
            return "";
        }
    }

    public static class StringExtensions
    {
        public static string Apply(this string s, Func<string, string> function)
        {
            return function(s);
        }
    }
}

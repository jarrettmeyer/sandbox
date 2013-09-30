using System;

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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox
{
    class Structs
    {
        //public void DoStuff()
        //{
        //    //Point<int> intPoints = null;
        //    char* chars = "Hello";
        //    String a1 = new String("Hello");
        //}
    }

    public struct Point<T>
    {
        private readonly T x;
        private readonly T y;

        public Point(T x, T y)
        {
            this.x = x;
            this.y = y;
        }

        public T X
        {
            get { return x; }
        }

        public T Y
        {
            get { return y; }
        }
    }
}

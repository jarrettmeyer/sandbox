using System;
using NUnit.Framework;

namespace Sandbox
{
    [TestFixture]
    public class Closures
    {
        Action<int> myAction;

        public int MyMethod(int value)
        {
            int i = 5;
            myAction = (x) =>
            {
                int result = x + value;
                i += result;
            };
            
            myAction(3);

            i += 2;
            myAction(4);

            i += 2;
            myAction(5);

            return i;
        }

        public Func<int, int> InitializeFunction(int value)
        {
            int i = 5;
            return (x) =>
            {
                int result = x + value;
                i += result;
                return i;
            };
        }

        public int MyOtherMethod()
        {
            var function = InitializeFunction(2);

            int y = 1;
            y += function(3);
            y += function(4);
            y += function(5);

            return y;
        }

        [Test]
        public void test_value_of_MyMethod()
        {
            Assert.AreEqual(27, MyMethod(2));
        }

        [Test]
        public void test_value_of_MyOtherMethod()
        {
            Assert.AreEqual(50, MyOtherMethod());
        }
    }
}

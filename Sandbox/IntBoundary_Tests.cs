using System;
using FluentAssertions;
using NUnit.Framework;

namespace Sandbox
{
    [TestFixture]
    public class IntBoundary_Tests
    {
        [Test]
        public void shorts_promote_to_ints()
        {
            // -1 is an int. Int * Short => Int
            var result = -1 * Int16.MinValue;
            result.GetType().Should().Be<Int32>();
        }

        [Test]
        public void sbytes_will_loop_around()
        {
            SByte.MinValue.Should().Be(-128);

            sbyte minSByte = sbyte.MinValue;
            sbyte result = (sbyte)(-1 * minSByte);
            Assert.AreEqual(-128, result);

            Assert.AreEqual(127, sbyte.MaxValue);
        }

        [Test]
        public void do_ints_promote_to_longs()
        {
            var minInt = Int32.MinValue;
            var intResult = -1 * minInt;
            Assert.AreEqual(-2147483648, intResult);

            var longResult = -1L * minInt;
            Assert.AreEqual(2147483648, longResult);

            // Just for a sanity check, what is max?
            Assert.AreEqual(2147483647, Int32.MaxValue);
        }
    }
}

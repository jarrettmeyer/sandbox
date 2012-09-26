using System;
using NUnit.Framework;

namespace Sandbox
{
    [TestFixture]
    public class DateTime_Tests
    {
        [Test]
        public void datetime_kind_is_unspecified_by_default()
        {
            var date = new DateTime(2012, 9, 26, 9, 25, 0);
            Assert.AreEqual(DateTimeKind.Unspecified, date.Kind);
        }

        [Test]
        public void datetime_kind_can_change_based_on_usage()
        {
            var date = new DateTime(2012, 9, 26, 10, 3, 0);

            // An unspecified time can be change to a local time. .NET
            // will assume that the original value was universal.
            var local = date.ToLocalTime();
            Assert.AreEqual(DateTimeKind.Local, local.Kind);            
            Assert.AreEqual(date.AddHours(-4), local);
            Assert.AreEqual(DateTimeKind.Unspecified, date.Kind);

            // An unspecified time can be change to a universal time. .NET
            // will assume that the original value was local.
            var utc = date.ToUniversalTime();
            Assert.AreEqual(DateTimeKind.Utc, utc.Kind);

            Assert.AreEqual(date.AddHours(4), utc);
            Assert.AreEqual(DateTimeKind.Unspecified, date.Kind);

            // So, according to .NET, 2 AM GMT and 2 AM in the current time
            // zone are the same thing. Pay no attention that they're different
            // by 4 hours (or 5 in the winter)!
            var utc2am = new DateTime(2012, 9, 26, 2, 0, 0, DateTimeKind.Utc);
            var local2am = new DateTime(2012, 9, 26, 2, 0, 0, DateTimeKind.Local);
            Assert.AreEqual(utc2am, local2am);
        }

        [Test]
        public void dates_are_ugly()
        {
            // These are the correct times for DST changeover in America/Indiana/Indianapolis
            var utc1 = new DateTime(2012, 11, 4, 5, 30, 0, DateTimeKind.Utc);
            var utc2 = new DateTime(2012, 11, 4, 6, 30, 0, DateTimeKind.Utc);
            Assert.AreNotEqual(utc1, utc2);

            // This test will only work in Eastern Time Zone. It will work
            // in Indianapolis or New York, but it will fail in Chicago.
            var local1 = utc1.ToLocalTime();
            var local2 = utc2.ToLocalTime();
            Assert.AreEqual(local1, local2);

            // Any test that relies on DateTime is a fragile test!
        }
    }
}

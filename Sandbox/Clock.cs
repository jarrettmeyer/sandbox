using System;
using FluentAssertions;
using NUnit.Framework;

namespace Sandbox
{
    public interface IClock
    {
        DateTime Now { get; }
    }

    public class SystemClock : IClock
    {
        private readonly static SystemClock systemClock = new SystemClock();

        private SystemClock() { }

        public static IClock Instance
        {
            get { return systemClock; }
        }

        public DateTime Now
        {
            get { return DateTime.UtcNow; }
        }
    }

    public class StubClock : IClock
    {
        private readonly DateTime stub;

        private StubClock(DateTime stub)
        {
            this.stub = stub;
        }

        public DateTime Now
        {
            get { return this.stub; }
        }

        public static IClock FromDate(int year, int month, int day)
        {
            var dateTime = new DateTime(year, month, day);
            return new StubClock(dateTime);
        }

        public static IClock FromDateTime(DateTime dateTime)
        {
            return new StubClock(dateTime);
        }
    }

    [TestFixture]
    public class StubClock_Tests
    {
        [Test]
        public void can_stub_time()
        {
            var stubTime = new DateTime(2012, 9, 26, 0, 0, 0);
            var clock = StubClock.FromDateTime(stubTime);
            clock.Now.Should().Be(stubTime);
        }

        [Test]
        public void can_create_stub_from_date()
        {
            var clock = StubClock.FromDate(2012, 7, 4);
            clock.Now.Should().Be(new DateTime(2012, 7, 4));
        }
    }

    [TestFixture]
    public class SystemClock_Tests
    {
        [Test]
        public void can_get_instance()
        {
            SystemClock.Instance.Should().BeAssignableTo<IClock>();
        }

        [Test]
        public void can_get_current_system_time()
        {
            SystemClock.Instance.Now.Should().BeWithin(TimeSpan.FromMilliseconds(1));
        }
    }
}

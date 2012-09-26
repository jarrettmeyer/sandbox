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
            get { return DateTime.Now; }
        }
    }

    public class StubClock : IClock
    {
        private readonly DateTime now;

        public StubClock(DateTime now)
        {
            this.now = now;
        }

        public DateTime Now
        {
            get { return this.now; }
        }
    }

    [TestFixture]
    public class StubClock_Tests
    {
        [Test]
        public void can_stub_time()
        {
            var stubTime = new DateTime(2012, 9, 26, 0, 0, 0);
            var clock = new StubClock(stubTime);
            clock.Now.Should().Be(stubTime);
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

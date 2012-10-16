using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Diagnostics;
using FluentAssertions;
using NUnit.Framework;

namespace Sandbox.MVC.Lib.Validators
{
    [TestFixture]
    public class SqlServerDateAttribute_Tests
    {
        private SqlServerDateAttribute attribute;
        private static readonly DateTime tooBig = SqlDateTime.MaxValue.Value.AddMilliseconds(1);
        private static readonly DateTime tooSmall = SqlDateTime.MinValue.Value.AddMilliseconds(-1);

        [SetUp]
        public void before_each_test()
        {
            attribute = new SqlServerDateAttribute();
        }

        [Test]
        public void does_not_error_when_value_is_null()
        {
            Action action = () => attribute.Validate(null, "Date");
            action.ShouldNotThrow<ValidationException>();
        }

        [Test]
        public void errors_when_value_is_low()
        {
            Debug.WriteLine("Testing value: " + tooSmall);
            Action action = () => attribute.Validate(tooSmall, "Date");
            action.ShouldThrow<ValidationException>();
        }

        [Test]
        public void errors_when_value_is_high()
        {
            Debug.WriteLine("Testing value: " + tooBig);
            Action action = () => attribute.Validate(tooBig, "Date");
            action.ShouldThrow<ValidationException>();
        }
    }
}

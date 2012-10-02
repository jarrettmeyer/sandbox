using System;
using FluentAssertions;
using NUnit.Framework;

namespace Sandbox
{
    public interface IRegistrySettings
    {
        object GetValue(string key);

        bool IsSet(string key);

        void SetValue(string key, object value);
    }

    public sealed class SystemRegistrySettings : IRegistrySettings
    {
        private readonly static SystemRegistrySettings instance = new SystemRegistrySettings();

        public static IRegistrySettings Instance
        {
            get { return instance; }
        }

        public object GetValue(string key)
        {
            return Microsoft.Win32.Registry.LocalMachine.GetValue(key);
        }

        public bool IsSet(string key)
        {
            return GetValue(key) != null;
        }

        public void SetValue(string key, object value)
        {
            Microsoft.Win32.Registry.LocalMachine.SetValue(key, value);
        }
    }

    [TestFixture]
    public class SystemRegistrySettings_Tests
    {
        private IRegistrySettings registrySettings;
        private string stringKey;
        private object objectValue;

        [SetUp]
        public void before_each_test()
        {
            registrySettings = SystemRegistrySettings.Instance;

            stringKey = Guid.NewGuid().ToString();
            CreateRegistrySetting(stringKey, "Hello, World!");
        }

        private void CreateRegistrySetting(string key, object value)
        {            
            Microsoft.Win32.Registry.LocalMachine.SetValue(key, value);
        }

        [TearDown]
        public void after_each_test()
        {
            DeleteRegistryKey(stringKey);
        }

        private void DeleteRegistryKey(string key)
        {
            if (Microsoft.Win32.Registry.LocalMachine.GetValue(key) != null)
                Microsoft.Win32.Registry.LocalMachine.DeleteValue(key);
        }

        [Test]
        public void can_get_value_from_registry()
        {
            objectValue = registrySettings.GetValue(stringKey);
            ((string)objectValue).Should().Be("Hello, World!");
        }

        [Test]
        public void can_set_value_in_registry()
        {
            var key = Guid.NewGuid().ToString();
            registrySettings.SetValue(key, "ABC");
            Microsoft.Win32.Registry.LocalMachine.GetValue(key).Should().Be("ABC");
            DeleteRegistryKey(key);
        }

        [Test]
        public void returns_true_when_setting_exists()
        {
            registrySettings.IsSet(stringKey).Should().BeTrue();
        }

        [Test]
        public void returns_false_when_setting_does_not_exist()
        {
            registrySettings.IsSet("Some complete junk key").Should().BeFalse();
        }
    }
}

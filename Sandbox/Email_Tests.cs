using System.IO;
using System.Net.Mail;
using FluentAssertions;
using NUnit.Framework;

namespace Sandbox
{
    [TestFixture]
    public class Email_Tests
    {
        private const string EMAIL_PATH = @"C:\temp\jarrettmeyer\sandbox\email_tests";
        private MailMessage mailMessage;
        private SmtpClient smtpClient;

        [SetUp]
        public void before_each_test()
        {
            DeleteDirectory();

            Directory.CreateDirectory(EMAIL_PATH);

            smtpClient = new SmtpClient();
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            smtpClient.PickupDirectoryLocation = EMAIL_PATH;

            mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("jarrettmeyer@gmail.com");
            mailMessage.Subject = "This is only a test";
        }

        [TearDown]
        public void after_each_test()
        {
            DeleteDirectory();
        }

        private static void DeleteDirectory()
        {
            if (Directory.Exists(EMAIL_PATH))
            {
                foreach (var file in Directory.GetFiles(EMAIL_PATH))
                {
                    File.Delete(file);
                }
                Directory.Delete(EMAIL_PATH);
            }
        }

        [Test]
        public void can_send_email_using_just_bcc()
        {
            mailMessage.Bcc.Add("jarrettmeyer@gmail.com");
            smtpClient.Send(mailMessage);

            Directory.GetFiles(EMAIL_PATH).Length.Should().Be(1);
        }
    }
}

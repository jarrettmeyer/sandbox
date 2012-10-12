using System.IO;
using System.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Sandbox
{
    [TestFixture]
    public class WebRequest_Tests
    {
        [Test]
        public void can_create_web_request_object()
        {
            // At this point, I'm assuming that if Google is down, then
            // the internet doesn't exist.
            var request = WebRequest.Create("http://www.google.com");
            request.Should().BeAssignableTo<WebRequest>();            
        }

        [Test]
        public void can_get_web_response_from_request()
        {
            var request = WebRequest.Create("http://www.google.com");
            var response = request.GetResponse();
            response.Should().BeAssignableTo<WebResponse>();
        }

        [Test]
        public void web_response_has_expected_status_code()
        {
            var request = WebRequest.Create("http://www.google.com");
            var response = request.GetResponse();
            var status = ((HttpWebResponse) response).StatusCode;
            status.Should().Be(HttpStatusCode.OK);            
        }

        [Test, Ignore("this test does not work when behind a proxy")]
        public void web_response_has_content()
        {
            var request = WebRequest.Create("http://www.google.com");
            var response = request.GetResponse();
            string responseContent;
            using (var stream = response.GetResponseStream())
            {
                if (stream == null)
                    return;

                using (var streamReader = new StreamReader(stream))
                {                    
                    responseContent = streamReader.ReadToEnd();                    
                }
            }            
            responseContent.Should().StartWith("<!doctype html>");
            responseContent.Should().EndWith("</html>");
        }
    }
}

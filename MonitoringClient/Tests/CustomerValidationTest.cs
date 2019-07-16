using MonitoringClient.RegExp;
using NUnit.Framework;

namespace MonitoringClient.Tests
{
    [TestFixture]
    class CustomerValidationTest
    {
        [Test]
        public void CorrectFirstname_FirstnameValidation_ReturnTrue()
        {
            string testFirstname = "Marco";
            var customerValidation = new CustomerValidation();

            Assert.AreEqual(true, customerValidation.FirstnameValidation(testFirstname));

        }
    }
}

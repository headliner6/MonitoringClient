using MonitoringClient.RegExp;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringClient.Tests
{
    [TestFixture]
    public class LogMessageValidationTest
    {
        [Test]
        public void PodValidation_checkDiffrentCombinations_allFalse()
        {
            //arrange
            string pod1 = "";
            string pod2 = null;

            //act

            var logMessageValidator = new LogMessageValidation();

            var resultat1 = logMessageValidator.PodValidation(pod1);
            var resultat2 = logMessageValidator.PodValidation(pod2);

            //assert

            Assert.That(resultat1.Equals("Pod darf nicht leer sein!"));
            Assert.That(resultat2.Equals("Pod darf nicht leer sein!"));
        }

        [Test]
        public void PodValidation_checkDiffrentCombinations_allTrue()
        {
            //arrange
            string pod1 = "Zbw";
            string pod2 = "TestPod@1";

            //act

            var logMessageValidator = new LogMessageValidation();

            var resultat1 = logMessageValidator.PodValidation(pod1);
            var resultat2 = logMessageValidator.PodValidation(pod2);

            //assert

            Assert.That(resultat1 == null);
            Assert.That(resultat2 == null);
        }

        [Test]
        public void SeverityValidation_checkDiffrentCombinations_allFalse()
        {
            //arrange
            string severity1 = "";
            string severity2 = null;
            string severity3 = "aasf5";
            string severity4 = "as";

            //act

            var logMessageValidator = new LogMessageValidation();

            var resultat1 = logMessageValidator.SeverityValidation(severity1);
            var resultat2 = logMessageValidator.SeverityValidation(severity2);
            var resultat3 = logMessageValidator.SeverityValidation(severity3);
            var resultat4 = logMessageValidator.SeverityValidation(severity4);

            //assert

            Assert.That(resultat1.Equals("Severity darf nur Zahlen enthalten!"));
            Assert.That(resultat2.Equals("Severity darf nur Zahlen enthalten!"));
            Assert.That(resultat3.Equals("Severity darf nur Zahlen enthalten!"));
            Assert.That(resultat4.Equals("Severity darf nur Zahlen enthalten!"));
        }

        [Test]
        public void SeverityValidation_checkDiffrentCombinations_allTrue()
        {
            //arrange
            string severity1 = "1";
            string severity2 = "2345";

            //act

            var logMessageValidator = new LogMessageValidation();

            var resultat1 = logMessageValidator.SeverityValidation(severity1);
            var resultat2 = logMessageValidator.SeverityValidation(severity2);

            //assert

            Assert.That(resultat1 == null);
            Assert.That(resultat2 == null);
        }

        [Test]
        public void HostnameValidation_checkDiffrentCombinations_allFalse()
        {
            //arrange
            string host1 = "";
            string host2 = null;

            //act

            var logMessageValidator = new LogMessageValidation();

            var resultat1 = logMessageValidator.HostnameValidation(host1);
            var resultat2 = logMessageValidator.HostnameValidation(host2);

            //assert

            Assert.That(resultat1.Equals("Hostname darf nicht leer sein!"));
            Assert.That(resultat2.Equals("Hostname darf nicht leer sein!"));
        }

        [Test]
        public void HostnameValidation_checkDiffrentCombinations_allTrue()
        {
            //arrange
            string host1 = "Zbw";
            string host2 = "Test125";

            //act

            var logMessageValidator = new LogMessageValidation();

            var resultat1 = logMessageValidator.HostnameValidation(host1);
            var resultat2 = logMessageValidator.HostnameValidation(host2);

            //assert

            Assert.That(resultat1 == null);
            Assert.That(resultat2 == null);
        }

        [Test]
        public void MessageValidation_checkDiffrentCombinations_allFalse()
        {
            //arrange
            string message1 = "";
            string message2 = null;

            //act

            var logMessageValidator = new LogMessageValidation();

            var resultat1 = logMessageValidator.MessageValidation(message1);
            var resultat2 = logMessageValidator.MessageValidation(message2);

            //assert

            Assert.That(resultat1.Equals("Message darf nicht leer sein!"));
            Assert.That(resultat2.Equals("Message darf nicht leer sein!"));
        }

        [Test]
        public void MessageValidation_checkDiffrentCombinations_allTrue()
        {
            //arrange
            string message1 = "Hallo Welt";
            string message2 = "Hallo Entwickler";

            //act

            var logMessageValidator = new LogMessageValidation();

            var resultat1 = logMessageValidator.MessageValidation(message1);
            var resultat2 = logMessageValidator.MessageValidation(message2);

            //assert

            Assert.That(resultat1 == null);
            Assert.That(resultat2 == null);
        }
    }
}
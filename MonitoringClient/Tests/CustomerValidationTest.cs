using MonitoringClient.RegExp;
using NUnit.Framework;

namespace MonitoringClient.Tests
{
    [TestFixture]
    public class CustomerValidationTest
    {
        [Test]
        public void FirstnameValidation_checkDiffrentCombinations_allFalse()
        {
            //arrange
            string firstname1 = "";
            string firstname2 = null;
            string firstname3 = "M.";
            string firstname4 = "TestMuster1";

            //act

            var customerValidator = new CustomerValidation();

            var resultat1 = customerValidator.FirstnameValidation(firstname1);
            var resultat2 = customerValidator.FirstnameValidation(firstname2);
            var resultat3 = customerValidator.FirstnameValidation(firstname3);
            var resultat4 = customerValidator.FirstnameValidation(firstname4);

            //assert

            Assert.That(resultat1.Equals("Firstname darf nicht leer sein!"));
            Assert.That(resultat2.Equals("Firstname darf nicht leer sein!"));
            Assert.That(resultat3.Equals("Firstname darf nicht leer sein!"));
            Assert.That(resultat4.Equals("Firstname darf nicht leer sein!"));


        }

        [Test]
        public void FirstnameValidation_checkDiffrentCombinations_allTrue()
        {
            //arrange
            string firstname1 = "M";
            string firstname2 = "TestMuster";

            //act

            var customerValidator = new CustomerValidation();

            var resultat1 = customerValidator.FirstnameValidation(firstname1);
            var resultat2 = customerValidator.FirstnameValidation(firstname2);

            //assert

            Assert.That(resultat1 == null);
            Assert.That(resultat2 == null);


        }

        [Test]
        public void LastnameValidation_checkDiffrentCombinations_allFalse()
        {
            //arrange
            string lastname1 = "";
            string lastname2 = null;

            //act

            var customerValidator = new CustomerValidation();

            var resultat1 = customerValidator.LastnameValidation(lastname1);
            var resultat2 = customerValidator.LastnameValidation(lastname2);

            //assert

            Assert.That(resultat1.Equals("Lastname darf nicht leer sein!"));
            Assert.That(resultat2.Equals("Lastname darf nicht leer sein!"));
        }

        [Test]
        public void LastnameValidation_checkDiffrentCombinations_allTrue()
        {
            //arrange
            string lastname1 = "M";
            string lastname2 = "TestMuster";

            //act

            var customerValidator = new CustomerValidation();

            var resultat1 = customerValidator.LastnameValidation(lastname1);
            var resultat2 = customerValidator.LastnameValidation(lastname2);

            //assert

            Assert.That(resultat1 == null);
            Assert.That(resultat2 == null);

        }

        [Test]
        public void PhoneNumberValidation_checkDiffrentCombinationsForSwitzerland_allFalse()
        {
            //arrange
            string phone1 = "+4155asdf897";
            string phone2 = "00 041 546 78 79-15";
            string phone3 = "41 558 88 99-15";
            string phone4 = "+43 55 44 213 12";
            string phone5 = "+0049 55 44 213 12-15";
            string phone6 = "55 44 213 12-15";

            //act
            var customerValidator = new CustomerValidation();

            var resultat1 = customerValidator.PhoneNumberValidation(phone1, "Schweiz");
            var resultat2 = customerValidator.PhoneNumberValidation(phone2, "Schweiz");
            var resultat3 = customerValidator.PhoneNumberValidation(phone3, "Schweiz");
            var resultat4 = customerValidator.PhoneNumberValidation(phone4, "Schweiz");
            var resultat5 = customerValidator.PhoneNumberValidation(phone5, "Schweiz");
            var resultat6 = customerValidator.PhoneNumberValidation(phone6, "Schweiz");

            //assert

            Assert.That(resultat1.Equals("Phone number muss ein gültiges Format haben!! Bsp.: +41 75 409 00 00-56"));
            Assert.That(resultat2.Equals("Phone number muss ein gültiges Format haben!! Bsp.: +41 75 409 00 00-56"));
            Assert.That(resultat3.Equals("Phone number muss ein gültiges Format haben!! Bsp.: +41 75 409 00 00-56"));
            Assert.That(resultat4.Equals("Phone number muss ein gültiges Format haben!! Bsp.: +41 75 409 00 00-56"));
            Assert.That(resultat5.Equals("Phone number muss ein gültiges Format haben!! Bsp.: +41 75 409 00 00-56"));
            Assert.That(resultat6.Equals("Phone number muss ein gültiges Format haben!! Bsp.: +41 75 409 00 00-56"));

        }

        [Test]
        public void PhoneNumberValidation_checkDiffrentCombinationsForSwitzerland_allTrue()
        {
            //arrange
            string phone1 = "+41 75 409 00 00";
            string phone2 = "+41 (0)75 409 00 00";
            string phone3 = "0041 75 409 00 00";
            string phone4 = "0041 (0)75 409 00 00";
            string phone5 = "075 409 00 00";
            string phone6 = "075 / 409 00 00";
            string phone7 = "075 / 409 00 00-15";

            //act
            var customerValidator = new CustomerValidation();

            var resultat1 = customerValidator.PhoneNumberValidation(phone1, "Schweiz");
            var resultat2 = customerValidator.PhoneNumberValidation(phone2, "Schweiz");
            var resultat3 = customerValidator.PhoneNumberValidation(phone3, "Schweiz");
            var resultat4 = customerValidator.PhoneNumberValidation(phone4, "Schweiz");
            var resultat5 = customerValidator.PhoneNumberValidation(phone5, "Schweiz");
            var resultat6 = customerValidator.PhoneNumberValidation(phone6, "Schweiz");
            var resultat7 = customerValidator.PhoneNumberValidation(phone7, "Schweiz");

            //assert

            Assert.That(resultat1 == null);
            Assert.That(resultat2 == null);
            Assert.That(resultat3 == null);
            Assert.That(resultat4 == null);
            Assert.That(resultat5 == null);
            Assert.That(resultat6 == null);
            Assert.That(resultat7 == null);

        }

        [Test]
        public void PhoneNumberValidation_checkDiffrentCombinationsForGermany_allFalse()
        {
            //arrange
            string phone1 = "+4955asdf897";
            string phone2 = "00 041 546 78 79-15";
            string phone3 = "49 558 88 99-15";
            string phone4 = "+49 55 44 213 12a";
            string phone5 = "+049 55 44 213 12-15";
            string phone6 = "55 44 213 12-15";

            //act
            var customerValidator = new CustomerValidation();

            var resultat1 = customerValidator.PhoneNumberValidation(phone1, "Deutschland");
            var resultat2 = customerValidator.PhoneNumberValidation(phone2, "Deutschland");
            var resultat3 = customerValidator.PhoneNumberValidation(phone3, "Deutschland");
            var resultat4 = customerValidator.PhoneNumberValidation(phone4, "Deutschland");
            var resultat5 = customerValidator.PhoneNumberValidation(phone5, "Deutschland");
            var resultat6 = customerValidator.PhoneNumberValidation(phone6, "Deutschland");

            //assert

            Assert.That(resultat1.Equals("Phone number muss ein gültiges Format haben!! Bsp.: +49 75 409 00 00-56"));
            Assert.That(resultat2.Equals("Phone number muss ein gültiges Format haben!! Bsp.: +49 75 409 00 00-56"));
            Assert.That(resultat3.Equals("Phone number muss ein gültiges Format haben!! Bsp.: +49 75 409 00 00-56"));
            Assert.That(resultat4.Equals("Phone number muss ein gültiges Format haben!! Bsp.: +49 75 409 00 00-56"));
            Assert.That(resultat5.Equals("Phone number muss ein gültiges Format haben!! Bsp.: +49 75 409 00 00-56"));
            Assert.That(resultat6.Equals("Phone number muss ein gültiges Format haben!! Bsp.: +49 75 409 00 00-56"));

        }

        [Test]
        public void PhoneNumberValidation_checkDiffrentCombinationsForGermany_allTrue()
        {
            //arrange
            string phone1 = "0049301234567";
            string phone2 = "+49 30 12345-67";
            string phone3 = "+49 (30) 12345-67";
            string phone4 = "0049 30 1234567";
            string phone5 = "+49 (30) 12345";
            string phone6 = "0049 (0)30 12345-67";

            //act
            var customerValidator = new CustomerValidation();

            var resultat1 = customerValidator.PhoneNumberValidation(phone1, "Deutschland");
            var resultat2 = customerValidator.PhoneNumberValidation(phone2, "Deutschland");
            var resultat3 = customerValidator.PhoneNumberValidation(phone3, "Deutschland");
            var resultat4 = customerValidator.PhoneNumberValidation(phone4, "Deutschland");
            var resultat5 = customerValidator.PhoneNumberValidation(phone5, "Deutschland");
            var resultat6 = customerValidator.PhoneNumberValidation(phone6, "Deutschland");

            //assert

            Assert.That(resultat1 == null);
            Assert.That(resultat2 == null);
            Assert.That(resultat3 == null);
            Assert.That(resultat4 == null);
            Assert.That(resultat5 == null);
            Assert.That(resultat6 == null);
        }

        [Test]
        public void PhoneNumberValidation_checkDiffrentCombinationsForLichtenstein_allFalse()
        {
            //arrange
            string phone1 = "+42355asdf897";
            string phone2 = "00423 041 546 78 79-155";
            string phone3 = "49 558 88 99-15";
            string phone4 = "+423 55 44 213 12a";
            string phone5 = "+41 55 44 213 12-15";
            string phone6 = "0 44 213 12-15";

            //act
            var customerValidator = new CustomerValidation();

            var resultat1 = customerValidator.PhoneNumberValidation(phone1, "Liechtenstein");
            var resultat2 = customerValidator.PhoneNumberValidation(phone2, "Liechtenstein");
            var resultat3 = customerValidator.PhoneNumberValidation(phone3, "Liechtenstein");
            var resultat4 = customerValidator.PhoneNumberValidation(phone4, "Liechtenstein");
            var resultat5 = customerValidator.PhoneNumberValidation(phone5, "Liechtenstein");
            var resultat6 = customerValidator.PhoneNumberValidation(phone6, "Liechtenstein");

            //assert

            Assert.That(resultat1.Equals("Phone number muss ein gültiges Format haben!! Bsp.: +423 75 409 00 00-56"));
            Assert.That(resultat2.Equals("Phone number muss ein gültiges Format haben!! Bsp.: +423 75 409 00 00-56"));
            Assert.That(resultat3.Equals("Phone number muss ein gültiges Format haben!! Bsp.: +423 75 409 00 00-56"));
            Assert.That(resultat4.Equals("Phone number muss ein gültiges Format haben!! Bsp.: +423 75 409 00 00-56"));
            Assert.That(resultat5.Equals("Phone number muss ein gültiges Format haben!! Bsp.: +423 75 409 00 00-56"));
            Assert.That(resultat6.Equals("Phone number muss ein gültiges Format haben!! Bsp.: +423 75 409 00 00-56"));

        }

        [Test]
        public void PhoneNumberValidation_checkDiffrentCombinationsForLichtenstein_allTrue()
        {
            //arrange
            string phone1 = "00423 236 88 11-15";
            string phone2 = "+423 236 06 03";
            string phone3 = "00423 237 74 00";
            string phone4 = "+423 237 74 00-15";

            //act
            var customerValidator = new CustomerValidation();

            var resultat1 = customerValidator.PhoneNumberValidation(phone1, "Lichtenstein");
            var resultat2 = customerValidator.PhoneNumberValidation(phone2, "Lichtenstein");
            var resultat3 = customerValidator.PhoneNumberValidation(phone3, "Lichtenstein");
            var resultat4 = customerValidator.PhoneNumberValidation(phone4, "Lichtenstein");

            //assert

            Assert.That(resultat1 == null);
            Assert.That(resultat2 == null);
            Assert.That(resultat3 == null);
            Assert.That(resultat4 == null);

        }

        [Test]
        public void AddressnumberValidation_checkDiffrentCombinations_allFalse()
        {
            //arrange
            string addressnumber1 = "CU123455466";
            string addressnumber2 = "15CU561";
            string addressnumber3 = "18879";

            //act

            var customerValidator = new CustomerValidation();

            var resultat1 = customerValidator.AddressnumberValidation(addressnumber1);
            var resultat2 = customerValidator.AddressnumberValidation(addressnumber2);
            var resultat3 = customerValidator.AddressnumberValidation(addressnumber3);

            //assert

            Assert.That(resultat1.Equals("Addressnumber muss zwingend mit Präfix 'CU' beginnen und anschliessend 5 Ziffern beinhalten."));
            Assert.That(resultat2.Equals("Addressnumber muss zwingend mit Präfix 'CU' beginnen und anschliessend 5 Ziffern beinhalten."));
            Assert.That(resultat3.Equals("Addressnumber muss zwingend mit Präfix 'CU' beginnen und anschliessend 5 Ziffern beinhalten."));
        }

        [Test]
        public void AddressnumberValidation_checkDiffrentCombinations_allTrue()
        {
            //arrange
            string addressnumber1 = "CU12345";
            string addressnumber2 = "CU54321";

            //act

            var customerValidator = new CustomerValidation();

            var resultat1 = customerValidator.AddressnumberValidation(addressnumber1);
            var resultat2 = customerValidator.AddressnumberValidation(addressnumber2);

            //assert

            Assert.That(resultat1 == null);
            Assert.That(resultat2 == null);
        }

        [Test]
        public void CustomerAccountNumberValidation_checkAccountNumber0And10To16_allFalse()
        {

            //act
            var customerValidator = new CustomerValidation();

            var resultat1 = customerValidator.CustomerAccountNumberValidation("0");
            var resultat2 = customerValidator.CustomerAccountNumberValidation("10");
            var resultat3 = customerValidator.CustomerAccountNumberValidation("11");
            var resultat4 = customerValidator.CustomerAccountNumberValidation("12");
            var resultat5 = customerValidator.CustomerAccountNumberValidation("13");
            var resultat6 = customerValidator.CustomerAccountNumberValidation("14");
            var resultat7 = customerValidator.CustomerAccountNumberValidation("15");
            var resultat8 = customerValidator.CustomerAccountNumberValidation("16");

            //assert

            Assert.That(resultat1.Equals("CustomerAccountNumber muss 1-8 sein!"));
            Assert.That(resultat2.Equals("CustomerAccountNumber muss 1-8 sein!"));
            Assert.That(resultat3.Equals("CustomerAccountNumber muss 1-8 sein!"));
            Assert.That(resultat4.Equals("CustomerAccountNumber muss 1-8 sein!"));
            Assert.That(resultat5.Equals("CustomerAccountNumber muss 1-8 sein!"));
            Assert.That(resultat6.Equals("CustomerAccountNumber muss 1-8 sein!"));
            Assert.That(resultat7.Equals("CustomerAccountNumber muss 1-8 sein!"));
            Assert.That(resultat8.Equals("CustomerAccountNumber muss 1-8 sein!"));

        }

        [Test]
        public void CustomerAccountNumberValidation_checkAccountNumber1To8_allTrue()
        {
            //act
            var customerValidator = new CustomerValidation();

            var resultat1 = customerValidator.CustomerAccountNumberValidation("1");
            var resultat2 = customerValidator.CustomerAccountNumberValidation("2");
            var resultat3 = customerValidator.CustomerAccountNumberValidation("3");
            var resultat4 = customerValidator.CustomerAccountNumberValidation("4");
            var resultat5 = customerValidator.CustomerAccountNumberValidation("5");
            var resultat6 = customerValidator.CustomerAccountNumberValidation("6");
            var resultat7 = customerValidator.CustomerAccountNumberValidation("7");
            var resultat8 = customerValidator.CustomerAccountNumberValidation("8");

            //assert

            Assert.That(resultat1 == null);
            Assert.That(resultat2 == null);
            Assert.That(resultat3 == null);
            Assert.That(resultat4 == null);
            Assert.That(resultat5 == null);
            Assert.That(resultat6 == null);
            Assert.That(resultat7 == null);
            Assert.That(resultat8 == null);
        }

        [Test]
        public void EmailValidation_checkDiffrentCombinations_allFalse()
        {
            //act
            var customerValidator = new CustomerValidation();

            var resultat1 = customerValidator.EmailValidation("test@test.");
            var resultat2 = customerValidator.EmailValidation("mw.com");
            var resultat3 = customerValidator.EmailValidation("@123S.ch");

            //assert

            Assert.That(resultat1.Equals("Email address muss ein gültiges Format haben! Bsp.: test@provider.ch"));
            Assert.That(resultat2.Equals("Email address muss ein gültiges Format haben! Bsp.: test@provider.ch"));
            Assert.That(resultat3.Equals("Email address muss ein gültiges Format haben! Bsp.: test@provider.ch"));
        }

        [Test]
        public void EmailValidation_checkDiffrentCombinations_allTrue()
        {
            //act
            var customerValidator = new CustomerValidation();

            var resultat1 = customerValidator.EmailValidation("test@test.ch");
            var resultat2 = customerValidator.EmailValidation("m@w.com");
            var resultat3 = customerValidator.EmailValidation("hallo@123S.ch");


            //assert

            Assert.That(resultat1 == null);
            Assert.That(resultat2 == null);
            Assert.That(resultat3 == null);

        }

        [Test]
        public void WebsiteValidation_checkDiffrentCombinations_allFalse()
        {
            //act
            var customerValidator = new CustomerValidation();

            var resultat1 = customerValidator.WebsiteValidation("https//adsfasfd.ch.sa/asf.");
            var resultat2 = customerValidator.WebsiteValidation("asdfsadf.ww.");
            var resultat3 = customerValidator.WebsiteValidation("www.sdafsdf.");

            //assert

            Assert.That(resultat1.Equals("Website muss ein gültiges Format haben! Bsp.: www.google.ch"));
            Assert.That(resultat2.Equals("Website muss ein gültiges Format haben! Bsp.: www.google.ch"));
            Assert.That(resultat3.Equals("Website muss ein gültiges Format haben! Bsp.: www.google.ch"));
        }

        [Test]
        public void WebsiteValidationn_checkDiffrentCombinations_allTrue()
        {
            //act
            var customerValidator = new CustomerValidation();

            var resultat1 = customerValidator.WebsiteValidation("mw.com");
            var resultat2 = customerValidator.WebsiteValidation("123S.ch");
            var resultat3 = customerValidator.WebsiteValidation("www.google.ch");


            //assert

            Assert.That(resultat1 == null);
            Assert.That(resultat2 == null);
            Assert.That(resultat3 == null);
        }

        [Test]
        public void PasswordValidation_checkDiffrentCombinations_allFalse()
        {

            //act
            var customerValidator = new CustomerValidation();

            var resultat1 = customerValidator.PasswordValidation("Password1");
            var resultat2 = customerValidator.PasswordValidation("33esWasfd65648");
            var resultat3 = customerValidator.PasswordValidation("/7123+44dqwee*");
            var resultat4 = customerValidator.PasswordValidation("asdfjhweadasfw32");
            var resultat5 = customerValidator.PasswordValidation("*+*ç9");
            var resultat6 = customerValidator.PasswordValidation("*+çdasfwae42");
            var resultat7 = customerValidator.PasswordValidation("ASDF&ass");
            var resultat8 = customerValidator.PasswordValidation("A**");

            //assert

            Assert.That(resultat1, Is.False);
            Assert.That(resultat2, Is.False);
            Assert.That(resultat3, Is.False);
            Assert.That(resultat4, Is.False);
            Assert.That(resultat5, Is.False);
            Assert.That(resultat6, Is.False);
            Assert.That(resultat7, Is.False);
            Assert.That(resultat8, Is.False);
        }

        [Test]
        public void PasswordValidation_checkDiffrentCombinations_allTrue()
        {
            //act
            var customerValidator = new CustomerValidation();

            var resultat1 = customerValidator.PasswordValidation("P@ssword1");
            var resultat2 = customerValidator.PasswordValidation("!233esWasfd65648");
            var resultat3 = customerValidator.PasswordValidation("/7123+44Sdqwee*");
            var resultat4 = customerValidator.PasswordValidation("asdfjhwea*çdasfIw32");
            var resultat5 = customerValidator.PasswordValidation("sdfahoishfWwe*+*ç9");
            var resultat6 = customerValidator.PasswordValidation("*+çdasfwaerWEDA2342");
            var resultat7 = customerValidator.PasswordValidation("ASDF23424ç%&ass");
            var resultat8 = customerValidator.PasswordValidation("ASsadf234**");

            //assert

            Assert.That(resultat1, Is.True);
            Assert.That(resultat2, Is.True);
            Assert.That(resultat3, Is.True);
            Assert.That(resultat4, Is.True);
            Assert.That(resultat5, Is.True);
            Assert.That(resultat6, Is.True);
            Assert.That(resultat7, Is.True);
            Assert.That(resultat8, Is.True);
        }

    }
}

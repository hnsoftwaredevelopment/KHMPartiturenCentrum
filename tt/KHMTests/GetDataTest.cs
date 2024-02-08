using KHM.Converters;
using NUnit.Framework;
using System.Data;

namespace KHMTests
{
    public class GetDataTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("Begeleiding")]
        [TestCase("Bibliotheek")]
        public void GetData_NotNullTest(string tableName)
        {

            // Arrange
            //string tableName = "Begeleiding"; // Replace with an actual table name

            // Act
            var result = DBCommands.GetData(tableName);


            // Assert
            if(result != null){Assert.Pass(tableName + " exists");}
           
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void HashPassword_ShouldGenerateValidHash()
        {
            // Arrange
            var password = "test123";

            // Act
            var hashedPassword = Helper.HashPassword(password);

            // Assert
            Assert.That(hashedPassword, Is.Not.Null);
            Assert.That(hashedPassword, Is.Not.Empty);
            Assert.That(hashedPassword, Is.Not.EqualTo(password));
        }

        [Test]
        public void HashPepperPassword_ShouldGenerateValidHash()
        {
            // Arrange
            string password = "test123";
            string username = "user123";

            // Act
            string hashedPassword = Helper.HashPepperPassword(password, username);

            // Assert
            Assert.That(hashedPassword, Is.Not.Null);
            Assert.That(hashedPassword, Is.Not.Empty);
            Assert.That(hashedPassword, Is.Not.EqualTo(password));
            Assert.That(hashedPassword, Is.Not.EqualTo(username));
        }

        [Test]
        public void HashPepperPassword_ShouldIncludeUsernameInHash()
        {
            // Arrange
            string password = "test123";
            string username = "user123";

            // Act
            string hashedPassword = Helper.HashPepperPassword(password, username);

            // Assert
            Assert.That(hashedPassword.Contains(username, StringComparison.OrdinalIgnoreCase), Is.True);


        }
    }
}
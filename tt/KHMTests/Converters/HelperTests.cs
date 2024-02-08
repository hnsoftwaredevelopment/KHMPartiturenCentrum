using KHM.Converters;
using NUnit.Framework;
using System;

namespace KHMTests.Converters
{
    [TestFixture]
    public class HelperTests
    {
        [Test]
        public void HashPassword_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            string password = null;

            // Act
            var result = Helper.HashPassword(password);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void HashPepperPassword_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            string password = null;
            string username = null;

            // Act
            var result = Helper.HashPepperPassword(password,
                username);

            // Assert
            Assert.Fail();
        }
    }
}

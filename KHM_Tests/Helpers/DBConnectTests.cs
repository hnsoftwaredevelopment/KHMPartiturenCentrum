namespace KHM_Tests.Helpers
{
    [TestFixture]
    public class DBConnectTests
    {
        [Test]
        public void ConnectionStringExcists()
        {
            // Arrange
            var connection = DBConnect.ConnectionString;

            // Act


            // Assert
            Assert.That(connection, Is.Not.Empty);
        }
    }
}

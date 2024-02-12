namespace KHM_Tests.Helpers
{
    [TestFixture]
    public class DBCommandsGetDataTests
    {
        [Test]
        [TestCase("view_Users", "Fullname")]
        [TestCase("view_Users", "nosort")]
        public void GetData_ExpectedBehaviorExistingDataBaseOrdering(string table, string orderByFieldName)
        {
            // Arrange
            var result = false;


            // Act
            var dataTable = DBCommands.GetData(table, orderByFieldName);

            if (dataTable != null)
            {
                result = true;

            }

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [TestCase("view_Users", "Fullname", "Id", "1")]
        [TestCase("view_Users", "nosort", "Id", "1")]
        public void GetData_ExpectedBehaviorExistingDataBaseOrderingSimpleFilter(string table, string orderByFieldName, string whereFieldName, string whereFieldValue)
        {
            // Arrange
            var result = false;


            // Act
            var dataTable = DBCommands.GetData(table, orderByFieldName, whereFieldName, whereFieldValue);

            if (dataTable != null)
            {
                result = true;

            }

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [TestCase("view_Users", "Fullname", "Id", "1", "Fullname", "herbertnijkamp")]
        [TestCase("view_Users", "nosort", "Id", "1", "Fullname", "herbertnijkamp")]
        public void GetData_ExpectedBehaviorExistingDataBaseOrderingExtendedFilter(string table, string orderByFieldName, string whereFieldName, string whereFieldValue, string andWhereFieldName, string andWhereFieldValue)
        {
            // Arrange
            var result = false;


            // Act
            var dataTable = DBCommands.GetData(table, orderByFieldName, whereFieldName, whereFieldValue, andWhereFieldName, andWhereFieldValue);

            if (dataTable != null)
            {
                result = true;

            }

            // Assert
            Assert.That(result, Is.True);
        }

    }
}

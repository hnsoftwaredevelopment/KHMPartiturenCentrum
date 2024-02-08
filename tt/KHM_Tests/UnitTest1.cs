using KHM.Helpers;
using NUnit.Framework;
using System.Data;

namespace KHM_Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void GetData_ReturnsDataTable()
        {
            // Arrange
            string tableName = "Begeleiding"; // Replace with an actual table name

            // Act
            DataTable result = DBCommands.GetData(tableName);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
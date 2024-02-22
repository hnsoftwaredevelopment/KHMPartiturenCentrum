using KHM.Helpers;

using NUnit.Framework;

namespace KHM_Tests.Helpers
	{
	[TestFixture]
	public class FileTypesTests
		{
		[Test]
		public void Enum_Contains_All_File_Types()
        {
            // Arrange
            var enumValues = Enum.GetValues(typeof(FileTypes.FileType));

            // Assert
            Assert.That(enumValues.GetValue(0), Is.EqualTo(FileTypes.FileType.ork));
            Assert.That(enumValues.GetValue(1), Is.EqualTo(FileTypes.FileType.orp));
            Assert.That(enumValues.GetValue(2), Is.EqualTo(FileTypes.FileType.tok));
            Assert.That(enumValues.GetValue(3), Is.EqualTo(FileTypes.FileType.top));
            Assert.That(enumValues.GetValue(4), Is.EqualTo(FileTypes.FileType.pia));
            Assert.That(enumValues.GetValue(5), Is.EqualTo(FileTypes.FileType.piano));
            Assert.That(enumValues.GetValue(6), Is.EqualTo(FileTypes.FileType.t1));
            Assert.That(enumValues.GetValue(7), Is.EqualTo(FileTypes.FileType.tenor1));
            Assert.That(enumValues.GetValue(8), Is.EqualTo(FileTypes.FileType.t2));
            Assert.That(enumValues.GetValue(9), Is.EqualTo(FileTypes.FileType.tenor2));
            Assert.That(enumValues.GetValue(10), Is.EqualTo(FileTypes.FileType.b1));
            Assert.That(enumValues.GetValue(11), Is.EqualTo(FileTypes.FileType.bariton));
            Assert.That(enumValues.GetValue(12), Is.EqualTo(FileTypes.FileType.b2));
            Assert.That(enumValues.GetValue(13), Is.EqualTo(FileTypes.FileType.bas));
            Assert.That(enumValues.GetValue(14), Is.EqualTo(FileTypes.FileType.sol));
            Assert.That(enumValues.GetValue(15), Is.EqualTo(FileTypes.FileType.solo));
            Assert.That(enumValues.GetValue(16), Is.EqualTo(FileTypes.FileType.sol1));
            Assert.That(enumValues.GetValue(17), Is.EqualTo(FileTypes.FileType.solo1));
            Assert.That(enumValues.GetValue(18), Is.EqualTo(FileTypes.FileType.sol2));
            Assert.That(enumValues.GetValue(19), Is.EqualTo(FileTypes.FileType.solo2));
            Assert.That(enumValues.GetValue(20), Is.EqualTo(FileTypes.FileType.uitv));
            Assert.That(enumValues.GetValue(21), Is.EqualTo(FileTypes.FileType.uitvoering));
            Assert.That(enumValues.GetValue(22), Is.EqualTo(FileTypes.FileType.tot));
            Assert.That(enumValues.GetValue(23), Is.EqualTo(FileTypes.FileType.totaal));

            // You can also check the total number of enum values
            Assert.That(enumValues.Length, Is.EqualTo(24));
        }
	}
}

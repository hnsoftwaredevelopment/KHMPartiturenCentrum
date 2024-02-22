using KHM.Views;

using Moq;

using NUnit.Framework;

using System;
using System.Collections.ObjectModel;

namespace KHM_Tests.Views
	{
	[TestFixture]
	public class ScoresTests
		{
		private MockRepository mockRepository;



		[SetUp]
		public void SetUp ()
			{
			this.mockRepository = new MockRepository ( MockBehavior.Strict );


			}

		private Scores CreateScores ()
			{
			return new Scores ();
			}


		[Test]
		public void ResetFields_StateUnderTest_ExpectedBehavior ()
			{
			// Arrange
			var scores = this.CreateScores();

			// Act
			scores.ResetFields ();

			// Assert
			Assert.Fail ();
			this.mockRepository.VerifyAll ();
			}

		[Test]
		public void ResetChanged_StateUnderTest_ExpectedBehavior ()
			{
			// Arrange
			var scores = this.CreateScores();

			// Act
			scores.ResetChanged ();

			// Assert
			Assert.Fail ();
			this.mockRepository.VerifyAll ();
			}
		}
	}

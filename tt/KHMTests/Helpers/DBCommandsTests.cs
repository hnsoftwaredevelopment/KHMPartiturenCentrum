using KHM.Helpers;
using NUnit.Framework;
using System;
using System.Collections.ObjectModel;
using KHM.Models;
using System.Data;

namespace KHMTests.Helpers
{
    [TestFixture]
    public class DBCommandsTests
    {
        [Test]
        public void GetData_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            string _table = null;

            // Act
            var result = DBCommands.GetData(_table);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetData_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            string _table = null;
            string OrderByFieldName = null;

            // Act
            var result = DBCommands.GetData(
                _table,
                OrderByFieldName);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetData_StateUnderTest_ExpectedBehavior2()
        {
            // Arrange
            string _table = null;
            string OrderByFieldName = null;
            string WhereFieldName = null;
            string WhereFieldValue = null;

            // Act
            var result = DBCommands.GetData(_table, OrderByFieldName, WhereFieldName, WhereFieldValue);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetData_StateUnderTest_ExpectedBehavior3()
        {
            // Arrange
            string _table = null;
            string OrderByFieldName = null;
            string WhereFieldName = null;
            string WhereFieldValue = null;
            string AndWhereFieldName = null;
            string AndWhereFieldValue = null;

            // Act
            var result = DBCommands.GetData(
                _table,
                OrderByFieldName,
                WhereFieldName,
                WhereFieldValue,
                AndWhereFieldName,
                AndWhereFieldValue);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetAvailableScores_StateUnderTest_ExpectedBehavior()
        {
            // Act
            var result = DBCommands.GetAvailableScores();

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetScores_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            string _table = null;
            string _orderByFieldName = null;
            string _whereFieldName = null;
            string _whereFieldValue = null;

            // Act
            var result = DBCommands.GetScores(
                _table,
                _orderByFieldName,
                _whereFieldName,
                _whereFieldValue);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void DeleteScore_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            
            string ScoreNumber = null;
            string ScoreSubNumber = null;

            // Act
            DBCommands.DeleteScore(
                ScoreNumber,
                ScoreSubNumber);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void DeleteUser_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            string _userId = null;

            // Act
            DBCommands.DeleteUser(
                _userId);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void CheckForSubScores_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            string _scoreNumber = null;

            // Act
            var result = DBCommands.CheckForSubScores(
                _scoreNumber);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void getHighestSubNumber_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            
            string _scoreNumber = null;

            // Act
            var result = DBCommands.getHighestSubNumber(
                _scoreNumber);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void RemoveSubScore_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            
            string _scoreNumber = null;

            // Act
            DBCommands.RemoveSubScore(
                _scoreNumber);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void AddSubScore_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            string _scoreNumber = null;
            string _subScoreNumber = null;

            // Act
            DBCommands.AddSubScore(
                _scoreNumber,
                _subScoreNumber);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void ReAddScore_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            string _scoreNumber = null;

            // Act
            DBCommands.ReAddScore(
                _scoreNumber);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void AddNewScore_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            string _scoreNumber = null;

            // Act
            DBCommands.AddNewScore(
                _scoreNumber);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void AddNewScoreAsSubscore_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            ObservableCollection<ScoreModel> _score = null;

            // Act
            DBCommands.AddNewScoreAsSubscore(
                _score);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void AddNewUser_StateUnderTest_ExpectedBehavior()
        {
            // Arrange

            // Act
            DBCommands.AddNewUser();

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetAddedUserId_StateUnderTest_ExpectedBehavior()
        {
            // Arrange

            // Act
            var result = DBCommands.GetAddedUserId();

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetEmptyScores_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            string _table = null;
            string _orderByFieldName = null;

            // Act
            var result = DBCommands.GetEmptyScores(
                _table,
                _orderByFieldName);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetAccompaniments_StateUnderTest_ExpectedBehavior()
        {

            // Act
            var result = DBCommands.GetAccompaniments();

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetArchives_StateUnderTest_ExpectedBehavior()
        {
            // Act
            var result = DBCommands.GetArchives();

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetGenres_StateUnderTest_ExpectedBehavior()
        {
            // Act
            var result = DBCommands.GetGenres();

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetLanguages_StateUnderTest_ExpectedBehavior()
        {
            // Act
            var result = DBCommands.GetLanguages();

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetPublishers_StateUnderTest_ExpectedBehavior()
        {
            // Act
            var result = DBCommands.GetPublishers();

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetRepertoires_StateUnderTest_ExpectedBehavior()
        {
            // Act
            var result = DBCommands.GetRepertoires();

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetUserRoles_StateUnderTest_ExpectedBehavior()
        {
            // Act
            var result = DBCommands.GetUserRoles();

            // Assert
            Assert.Fail();
        }

        [Test]
        public void SaveScore_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            ObservableCollection<SaveScoreModel> scoreList = null;

            // Act
            DBCommands.SaveScore(
                scoreList);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void UpdateUser_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            ObservableCollection<UserModel> modifiedUser = null;

            // Act
            DBCommands.UpdateUser(
                modifiedUser);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetTargetId_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            string _scoreNumber = null;

            // Act
            var result = DBCommands.GetTargetId(
                _scoreNumber);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void RenumberScoreList_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            DataTable scoreList = null;
            string newScoreNumber = null;

            // Act
            DBCommands.RenumberScoreList(
                scoreList,
                newScoreNumber);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void CheckUserPassword_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            string login = null;
            string password = null;

            // Act
            var result = DBCommands.CheckUserPassword(
                login,
                password);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void CheckUserName_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            
            string userName = null;
            int userId = 0;

            // Act
            var result = DBCommands.CheckUserName(
                userName,
                userId);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void CheckEMail_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            
            string email = null;
            int userId = 0;

            // Act
            var result = DBCommands.CheckEMail(
                email,
                userId);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void IsValidEmail_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            
            string email = null;

            // Act
            var result = DBCommands.IsValidEmail(
                email);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetUsers_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            
            int _userId = 0;

            // Act
            var result = DBCommands.GetUsers(
                _userId);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetUsers_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            

            // Act
            var result = DBCommands.GetUsers();

            // Assert
            Assert.Fail();
        }

        [Test]
        public void WriteLog_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            
            int loggedInUser = 0;
            string action = null;
            string description = null;

            // Act
            DBCommands.WriteLog(
                loggedInUser,
                action,
                description);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void WriteDetailLog_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            
            int logId = 0;
            string field = null;
            string oldValue = null;
            string newValue = null;

            // Act
            DBCommands.WriteDetailLog(
                logId,
                field,
                oldValue,
                newValue);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetAddedHistoryId_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            

            // Act
            var result = DBCommands.GetAddedHistoryId();

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetHistoryLog_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            

            // Act
            var result = DBCommands.GetHistoryLog();

            // Assert
            Assert.Fail();
        }
    }
}

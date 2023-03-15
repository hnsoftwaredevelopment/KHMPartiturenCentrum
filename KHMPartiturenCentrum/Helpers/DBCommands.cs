using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Xml;
using Google.Protobuf.WellKnownTypes;
using K4os.Compression.LZ4.Internal;
using KHMPartiturenCentrum.Converters;
using KHMPartiturenCentrum.Models;
using KHMPartiturenCentrum.Views;
using Microsoft.VisualBasic.ApplicationServices;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Crypto;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static KHMPartiturenCentrum.App;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604

namespace KHMPartiturenCentrum.Helpers;

public class DBCommands
{
    #region GetData
    #region GetData unsorted
    public static DataTable GetData(string _table)
    {
        string selectQuery = DBNames.SqlSelectAll + DBNames.SqlFrom + DBNames.Database + "." + _table;
        MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open();
        DataTable table = new();
        MySqlDataAdapter adapter = new(selectQuery, connection);
        adapter.Fill(table);
        connection.Close();
        return table;
    }
    #endregion

    #region GetData Sorted
    public static DataTable GetData(string _table, string OrderByFieldName)
    {
        string selectQuery = "";
        if (OrderByFieldName.ToLower() == "nosort")
        {
            selectQuery = DBNames.SqlSelectAll + DBNames.SqlFrom + DBNames.Database + "." + _table;
        }
        else
        {
            selectQuery = DBNames.SqlSelectAll + DBNames.SqlFrom + DBNames.Database + "." + _table + DBNames.SqlOrder + OrderByFieldName;
        }

        MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open();
        DataTable table = new();
        MySqlDataAdapter adapter = new(selectQuery, connection);
        adapter.Fill(table);
        connection.Close();
        return table;
    }
    #endregion

    #region GetData Sorted and filtered
    public static DataTable GetData(string _table, string OrderByFieldName, string WhereFieldName, string WhereFieldValue)
    {
        string selectQuery = "";
        if (OrderByFieldName.ToLower() == "nosort")
        {
            selectQuery = DBNames.SqlSelectAll + DBNames.SqlFrom + DBNames.Database + "." + _table + DBNames.SqlWhere + WhereFieldName + " = '" + WhereFieldValue + "';";
        }
        else
        {
            selectQuery = DBNames.SqlSelectAll + DBNames.SqlFrom + DBNames.Database + "." + _table + DBNames.SqlWhere + WhereFieldName + " = '" + WhereFieldValue + "'" + DBNames.SqlOrder + OrderByFieldName + ";";
        }

        MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open();
        DataTable table = new();
        MySqlDataAdapter adapter = new(selectQuery, connection);
        adapter.Fill(table);
        connection.Close();
        return table;
    }


    /// <summary>
    /// Get Score info for a score with subnumber
    /// </summary>
    /// <param name="_table"></param>
    /// <param name="OrderByFieldName"></param>
    /// <param name="WhereFieldName"></param>
    /// <param name="WhereFieldValue"></param>
    /// <param name="AndWhereFieldName"></param>
    /// <param name="AndWhereFieldValue"></param>
    /// <returns></returns>
    public static DataTable GetData ( string _table, string OrderByFieldName, string WhereFieldName, string WhereFieldValue, string AndWhereFieldName, string AndWhereFieldValue )
    {
        string selectQuery = "";
        if ( OrderByFieldName.ToLower () == "nosort" )
        {
            selectQuery = DBNames.SqlSelectAll + DBNames.SqlFrom + DBNames.Database + "." + _table + DBNames.SqlWhere + WhereFieldName + " = '" + WhereFieldValue + "'" + DBNames.SqlAnd + AndWhereFieldName + " = '" + AndWhereFieldValue + "';";
        }
        else
        {
            selectQuery = DBNames.SqlSelectAll + DBNames.SqlFrom + DBNames.Database + "." + _table + DBNames.SqlWhere + WhereFieldName + " = '" + WhereFieldValue + "'" + DBNames.SqlAnd + AndWhereFieldName + " = '" + AndWhereFieldValue + "'" + DBNames.SqlOrder + OrderByFieldName + ";";
        }

        MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ();
        DataTable table = new();
        MySqlDataAdapter adapter = new(selectQuery, connection);
        adapter.Fill ( table );
        connection.Close ();
        return table;
    }
    #endregion
    #endregion

    #region Get Available Scores
    public static ObservableCollection<ScoreModel> GetAvailableScores()
    {
        ObservableCollection<ScoreModel> Scores = new();

        DataTable dataTable = DBCommands.GetData(DBNames.AvailableScoresView, DBNames.AvailableScoresFieldNameNumber);

        if (dataTable.Rows.Count > 0)
        {
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Scores.Add(new ScoreModel
                {
                    ScoreId = int.Parse(dataTable.Rows[i].ItemArray[0].ToString()),
                    ScoreNumber = dataTable.Rows[i].ItemArray[1].ToString()
                });
            }
        }

        return Scores;
    }
    #endregion

    #region Get Scores
    public static ObservableCollection<ScoreModel> GetScores(string _table, string _orderByFieldName, string _whereFieldName, string _whereFieldValue)
    {
        ObservableCollection<ScoreModel> Scores = new();
        DataTable dataTable = new();

        if (_whereFieldName != null)
        {
            dataTable = GetData(_table, _orderByFieldName, _whereFieldName, _whereFieldValue);
        }
        else
        {
            dataTable = GetData(_table, _orderByFieldName);
        }


        if (dataTable.Rows.Count > 0)
        {
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                // Set the bools
                bool check = false, byHeart = false;
                bool pdfORP = false, pdfORK = false, pdfTOP = false, pdfTOK = false;
                bool mscORP = false, mscORK = false, mscTOP = false, mscTOK = false, mscOnline = false;
                bool mp3B1 = false, mp3B2 = false, mp3T1 = false, mp3T2 = false, mp3SOL = false, mp3TOT = false, mp3PIA = false;

                if (int.Parse(dataTable.Rows[i].ItemArray[18].ToString()) == 0) { check = false; } else { check = true; }
                if (int.Parse(dataTable.Rows[i].ItemArray[23].ToString()) == 0) { pdfORP = false; } else { pdfORP = true; }
                if (int.Parse(dataTable.Rows[i].ItemArray[24].ToString()) == 0) { pdfORK = false; } else { pdfORK = true; }
                if (int.Parse(dataTable.Rows[i].ItemArray[25].ToString()) == 0) { pdfTOP = false; } else { pdfTOP = true; }
                if (int.Parse(dataTable.Rows[i].ItemArray[26].ToString()) == 0) { pdfTOK = false; } else { pdfTOK = true; }
                if (int.Parse(dataTable.Rows[i].ItemArray[27].ToString()) == 0) { mscORP = false; } else { mscORP = true; }
                if (int.Parse(dataTable.Rows[i].ItemArray[28].ToString()) == 0) { mscORK = false; } else { mscORK = true; }
                if (int.Parse(dataTable.Rows[i].ItemArray[29].ToString()) == 0) { mscTOP = false; } else { mscTOP = true; }
                if (int.Parse(dataTable.Rows[i].ItemArray[30].ToString()) == 0) { mscTOK = false; } else { mscTOK = true; }
                if (int.Parse(dataTable.Rows[i].ItemArray[31].ToString()) == 0) { mp3TOT = false; } else { mp3TOT = true; }
                if (int.Parse(dataTable.Rows[i].ItemArray[32].ToString()) == 0) { mp3T1 = false; } else { mp3T1 = true; }
                if (int.Parse(dataTable.Rows[i].ItemArray[33].ToString()) == 0) { mp3T2 = false; } else { mp3T2 = true; }
                if (int.Parse(dataTable.Rows[i].ItemArray[34].ToString()) == 0) { mp3B1 = false; } else { mp3B1 = true; }
                if (int.Parse(dataTable.Rows[i].ItemArray[35].ToString()) == 0) { mp3B2 = false; } else { mp3B2 = true; }
                if (int.Parse(dataTable.Rows[i].ItemArray[36].ToString()) == 0) { mp3SOL = false; } else { mp3SOL = true; }
                if (int.Parse(dataTable.Rows[i].ItemArray[37].ToString()) == 0) { mp3PIA = false; } else { mp3PIA = true; }
                if (int.Parse(dataTable.Rows[i].ItemArray[38].ToString()) == 0) { mscOnline = false; } else { mscOnline = true; }
                if (int.Parse(dataTable.Rows[i].ItemArray[39].ToString()) == 0) { byHeart = false; } else { byHeart = true; }

                // Set total
                var total = 0;

                total = int.Parse(dataTable.Rows[i].ItemArray[42].ToString()) +
                        int.Parse(dataTable.Rows[i].ItemArray[43].ToString()) +
                        int.Parse(dataTable.Rows[i].ItemArray[44].ToString()) +
                        int.Parse(dataTable.Rows[i].ItemArray[45].ToString());

                // Set the datestrings
                string dateCreated = "";
                if (dataTable.Rows[i].ItemArray[19].ToString() != "")
                {
                    string[] _tempCreated = dataTable.Rows[i].ItemArray[19].ToString().Split(" ");
                    dateCreated = _tempCreated[0];
                }

                string dateModified = "";
                if (dataTable.Rows[i].ItemArray[20].ToString() != "")
                {
                    string[] _tempModified = dataTable.Rows[i].ItemArray[20].ToString().Split(" ");
                    dateModified = _tempModified[0];
                }

                // When Title is empty don't add that row to the list
                if (dataTable.Rows[i].ItemArray[4].ToString() != string.Empty)
                {
                    Scores.Add(new ScoreModel
                    {
                        ScoreId = int.Parse(dataTable.Rows[i].ItemArray[0].ToString()),
                        Score = dataTable.Rows[i].ItemArray[1].ToString(),
                        ScoreNumber = dataTable.Rows[i].ItemArray[2].ToString(),
                        ScoreSubNumber = dataTable.Rows[i].ItemArray[3].ToString(),
                        ScoreTitle = dataTable.Rows[i].ItemArray[4].ToString(),
                        ScoreSubTitle = dataTable.Rows[i].ItemArray[5].ToString(),
                        Composer = dataTable.Rows[i].ItemArray[6].ToString(),
                        Textwriter = dataTable.Rows[i].ItemArray[7].ToString(),
                        Arranger = dataTable.Rows[i].ItemArray[8].ToString(),
                        ArchiveId = int.Parse(dataTable.Rows[i].ItemArray[9].ToString()),
                        ArchiveName = dataTable.Rows[i].ItemArray[10].ToString(),
                        RepertoireId = int.Parse(dataTable.Rows[i].ItemArray[11].ToString()),
                        RepertoireName = dataTable.Rows[i].ItemArray[12].ToString(),
                        LanguageId = int.Parse(dataTable.Rows[i].ItemArray[13].ToString()),
                        LanguageName = dataTable.Rows[i].ItemArray[14].ToString(),
                        GenreId = int.Parse(dataTable.Rows[i].ItemArray[15].ToString()),
                        GenreName = dataTable.Rows[i].ItemArray[16].ToString(),
                        Lyrics = dataTable.Rows[i].ItemArray[17].ToString(),
                        CheckInt = int.Parse(dataTable.Rows[i].ItemArray[18].ToString()),
                        DateCreatedString = dateCreated,
                        DateModifiedString = dateModified,
                        Checked = check,
                        AccompanimentId = int.Parse(dataTable.Rows[i].ItemArray[21].ToString()),
                        AccompanimentName = dataTable.Rows[i].ItemArray[22].ToString(),
                        PDFORPInt = int.Parse(dataTable.Rows[i].ItemArray[23].ToString()),
                        PDFORP = pdfORP,
                        PDFORKInt = int.Parse(dataTable.Rows[i].ItemArray[24].ToString()),
                        PDFORK = pdfORK,
                        PDFTOPInt = int.Parse(dataTable.Rows[i].ItemArray[25].ToString()),
                        PDFTOP = pdfTOP,
                        PDFTOKInt = int.Parse(dataTable.Rows[i].ItemArray[26].ToString()),
                        PDFTOK = pdfTOK,
                        MuseScoreORPInt = int.Parse(dataTable.Rows[i].ItemArray[27].ToString()),
                        MuseScoreORP = mscORP,
                        MuseScoreORKInt = int.Parse(dataTable.Rows[i].ItemArray[28].ToString()),
                        MuseScoreORK = mscORK,
                        MuseScoreTOPInt = int.Parse(dataTable.Rows[i].ItemArray[29].ToString()),
                        MuseScoreTOP = mscTOP,
                        MuseScoreTOKInt = int.Parse(dataTable.Rows[i].ItemArray[30].ToString()),
                        MuseScoreTOK = mscTOK,
                        MP3TOTInt = int.Parse(dataTable.Rows[i].ItemArray[31].ToString()),
                        MP3TOT = mp3TOT,
                        MP3T1Int = int.Parse(dataTable.Rows[i].ItemArray[32].ToString()),
                        MP3T1 = mp3T1,
                        MP3T2Int = int.Parse(dataTable.Rows[i].ItemArray[33].ToString()),
                        MP3T2 = mp3T2,
                        MP3B1Int = int.Parse(dataTable.Rows[i].ItemArray[34].ToString()),
                        MP3B1 = mp3B1,
                        MP3B2Int = int.Parse(dataTable.Rows[i].ItemArray[35].ToString()),
                        MP3B2 = mp3B2,
                        MP3SOLInt = int.Parse(dataTable.Rows[i].ItemArray[36].ToString()),
                        MP3SOL = mp3SOL,
                        MP3PIAInt = int.Parse(dataTable.Rows[i].ItemArray[37].ToString()),
                        MP3PIA = mp3PIA,
                        MuseScoreOnlineInt = int.Parse(dataTable.Rows[i].ItemArray[38].ToString()),
                        MuseScoreOnline = mscOnline,
                        ByHeartInt = int.Parse(dataTable.Rows[i].ItemArray[39].ToString()),
                        ByHeart = byHeart,
                        MusicPiece = dataTable.Rows[i].ItemArray[40].ToString(),
                        Notes = dataTable.Rows[i].ItemArray[41].ToString(),
                        AmountPublisher1 = int.Parse(dataTable.Rows[i].ItemArray[42].ToString()),
                        AmountPublisher2 = int.Parse(dataTable.Rows[i].ItemArray[43].ToString()),
                        AmountPublisher3 = int.Parse(dataTable.Rows[i].ItemArray[44].ToString()),
                        AmountPublisher4 = int.Parse(dataTable.Rows[i].ItemArray[45].ToString()),
                        AmountTotal = total,
                        Publisher1Id = int.Parse(dataTable.Rows[i].ItemArray[46].ToString()),
                        Publisher1Name = dataTable.Rows[i].ItemArray[47].ToString(),
                        Publisher2Id = int.Parse(dataTable.Rows[i].ItemArray[48].ToString()),
                        Publisher2Name = dataTable.Rows[i].ItemArray[49].ToString(),
                        Publisher3Id = int.Parse(dataTable.Rows[i].ItemArray[50].ToString()),
                        Publisher3Name = dataTable.Rows[i].ItemArray[51].ToString(),
                        Publisher4Id = int.Parse(dataTable.Rows[i].ItemArray[52].ToString()),
                        Publisher4Name = dataTable.Rows[i].ItemArray[53].ToString()
                    });
                }
            }
        }
        return Scores;
    }
    #endregion

    #region Delete Score
    public static void DeleteScore(string ScoreNumber, string ScoreSubNumber)
    {
        // Check if the selected Score is used in a set of Scores
        var sqlQuery = DBNames.SqlSelect + DBNames.SqlCountAll + DBNames.SqlFrom + DBNames.Database + "." + DBNames.ScoresTable + DBNames.SqlWhere + DBNames.ScoresFieldNameScoreNumber + " = '" + ScoreNumber + "';";

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open();

        using MySqlCommand cmd = new(sqlQuery, connection);


        var NumberOfScores = int.Parse(cmd.ExecuteScalar().ToString());


        //var NumberOfScores = CheckForSubScores(DBNames.ScoresTable, ScoreNumber);

        if (NumberOfScores == 1 || ScoreSubNumber == "")
        {
            ExecuteDeleteScore ( DBNames.ScoresTable, ScoreNumber );
        }
        else
        {
            // There are Subscores avaiable
            // If Selected Score is 01 then delete it, no need to add the ScoreNumber again, because it already exists
            if ( ScoreSubNumber == "01" )
            {
                ExecuteDeleteScore ( DBNames.ScoresTable, ScoreNumber, ScoreSubNumber );
            }
            else
            {
                // If there are two Scores and the second one is deleted SubNumber should be removed from First Score
                if ( NumberOfScores == 2 )
                {
                    ExecuteDeleteScore ( DBNames.ScoresTable, ScoreNumber, ScoreSubNumber );
                    RemoveSubScore ( ScoreNumber );

                    // Write Renumber Action to the database
                    DBCommands.WriteLog ( int.Parse ( ScoreUsers.SelectedUserName ), DBNames.LogScoreRenumbered, $"Partituur: {ScoreNumber}" );

                    // Get Added History Id
                    int _historyId = DBCommands.GetAddedHistoryId();

                    // Write detail info to the log
                    DBCommands.WriteDetailLog ( _historyId, DBNames.LogScoreNumber, $"{ScoreNumber}-{ScoreSubNumber}", ScoreNumber );
                }
                else
                {
                    // Subscore can be deleted without effecting the other scores in the set
                    ExecuteDeleteScore ( DBNames.ScoresTable, ScoreNumber, ScoreSubNumber );
                }
            }
        }
    }
    #endregion

    #region Delete User
    public static void DeleteUser(string _userId)
    {
        var sqlQuery = DBNames.SqlDelete + DBNames.SqlFrom + DBNames.Database + "." + DBNames.UsersTable + DBNames.SqlWhere + DBNames.UsersFieldNameId + " = " + _userId + ";";

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open();

        using MySqlCommand cmd = new(sqlQuery, connection);

        int rowsAffected = cmd.ExecuteNonQuery();
    }
    #endregion

    #region Check for SubScores
    public static long CheckForSubScores(string _scoreNumber ) 
    {
        // Check how many Scores there are in the table with the given scorenumber, If there are more then 1 there is a set with SubNumbers
        var sqlQuery = DBNames.SqlSelect + DBNames.SqlCountAll + DBNames.SqlFrom + DBNames.Database + "." + DBNames.ScoresTable + DBNames.SqlWhere + DBNames.ScoresFieldNameScoreNumber + " = '" + _scoreNumber + "';";

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ();

        using MySqlCommand cmd = new(sqlQuery, connection);

        long numberOfScores = (long)cmd.ExecuteScalar();

        return numberOfScores;
    }
    #endregion

    #region Get Highest Subversion
    public static int getHighestSubNumber(string _scoreNumber )
    {
        //Select MAX ( SubNummer) FROM Bibliotheek Where Partituur = '000'
        var sqlQuery = DBNames.SqlSelect + DBNames.SqlMax + DBNames.ScoresFieldNameScoreSubNumber + ") " + DBNames.SqlFrom + DBNames.Database + "." + DBNames.ScoresTable + DBNames.SqlWhere + DBNames.ScoresFieldNameScoreNumber + " = '" + _scoreNumber + "';";

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ();

        using MySqlCommand cmd = new(sqlQuery, connection);

        string highestSubNumberValue = (string)cmd.ExecuteScalar();
        int highestSubNumber = int.Parse(highestSubNumberValue);

        return highestSubNumber;
    }
    #endregion

    #region Execute Delete
    static void ExecuteDeleteScore(string _table, string _scoreNumber ) 
    {
        // Delete Score without SubScores
        var sqlQuery = DBNames.SqlDelete + DBNames.SqlFrom + DBNames.Database + "." + _table + DBNames.SqlWhere + DBNames.ScoresFieldNameScoreNumber + " = '" + _scoreNumber + "';";

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ();

        using MySqlCommand cmd = new(sqlQuery, connection);

        int rowsAffected = cmd.ExecuteNonQuery();
    }

    static void ExecuteDeleteScore ( string _table, string _scoreNumber, string _scoreSubNumber ) 
    {
        // Delete Score with SubScoreNumber
        var sqlQuery = DBNames.SqlDelete + DBNames.SqlFrom + DBNames.Database + "." + _table + DBNames.SqlWhere + 
            DBNames.ScoresFieldNameScoreNumber + " = '" + _scoreNumber + "'";

        if ( _scoreSubNumber == "" )
        {
            sqlQuery += ";";
        }
        else
        {
            sqlQuery += DBNames.SqlAnd + DBNames.ScoresFieldNameScoreSubNumber + " = '" + _scoreSubNumber + "';";
        }

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ();

        using MySqlCommand cmd = new(sqlQuery, connection);

        int rowsAffected = cmd.ExecuteNonQuery();
    }
    #endregion

    #region Remove SubVersion from Score
    public static void RemoveSubScore(string _scoreNumber)
    {
        string sqlQuery = DBNames.SqlUpdate + DBNames.ScoresTable + DBNames.SqlSet + DBNames.ScoresFieldNameScoreSubNumber + " = ''" + DBNames.SqlWhere + DBNames.ScoresFieldNameScoreNumber + " = '" + _scoreNumber + "';";

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open();

        using MySqlCommand cmd = new(sqlQuery, connection);

        int rowsAffected = cmd.ExecuteNonQuery();
    }
    #endregion

    #region Add SubScore to Score
    public static void AddSubScore(string _scoreNumber, string _subScoreNumber)
    {
        string sqlQuery = DBNames.SqlUpdate + DBNames.ScoresTable + DBNames.SqlSet + DBNames.ScoresFieldNameScoreSubNumber + " = '" + _subScoreNumber + "'" + DBNames.SqlWhere + DBNames.ScoresFieldNameScoreNumber + " = '" + _scoreNumber + "';";

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open();

        using MySqlCommand cmd = new(sqlQuery, connection);

        int rowsAffected = cmd.ExecuteNonQuery();
    }
    #endregion

    #region Re Add Score
    public static void ReAddScore(string _scoreNumber ) 
    {
        var sqlQuery = DBNames.SqlInsert + DBNames.Database + "." + DBNames.ScoresTable + " ( " + DBNames.ScoresFieldNameScoreNumber + " ) " + DBNames.SqlValues + " ( '" + _scoreNumber + "' );";

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ();

        using MySqlCommand cmd = new(sqlQuery, connection);

        int rowsAffected = cmd.ExecuteNonQuery();
    }
    #endregion

    #region Add New Score
    public static void AddNewScore ( string _scoreNumber )
    {
        var repertoire = 1;

        if ( int.Parse( _scoreNumber ) >= 700 && int.Parse ( _scoreNumber ) <= 899 ) 
        {
            // Christmas Repertoire 4
            repertoire = 4;
        }
        else if ( int.Parse ( _scoreNumber ) >= 900 && int.Parse ( _scoreNumber ) <= 999 )
        {
            // Project Repertoire 5
            repertoire = 5;
        }
        else
        {
            // Base repertoire 2
            repertoire = 2;
        }

        var sqlQuery = DBNames.SqlUpdate + DBNames.Database + "." + DBNames.ScoresTable + DBNames.SqlSet + 
                DBNames.ScoresFieldNameTitle + " = + '" + DBNames.LogNew + "', " + 
                DBNames.ScoresFieldNameAccompanimentId + " = 1, " +
                DBNames.ScoresFieldNameArchiveId + " = 3, " +
                DBNames.ScoresFieldNameGenreId + " = 1, " +
                DBNames.ScoresFieldNameLanguageId + " = 1, " +
                DBNames.ScoresFieldNamePublisher1Id + " = 1, " +
                DBNames.ScoresFieldNamePublisher2Id + " = 1, " +
                DBNames.ScoresFieldNamePublisher3Id + " = 1, " +
                DBNames.ScoresFieldNamePublisher4Id + " = 1, " +
                DBNames.ScoresFieldNameRepertoireId + " = " + repertoire + 
                DBNames.SqlWhere + DBNames.ScoresFieldNameScoreNumber + " = '" + _scoreNumber + "';";

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ();

        using MySqlCommand cmd = new(sqlQuery, connection);

        int rowsAffected = cmd.ExecuteNonQuery();
    }
    #endregion

    #region Add New Score as Subscore
    public static void AddNewScoreAsSubscore ( ObservableCollection<ScoreModel> _score )
    {
        var repertoire = 1;

        var sqlQuery = DBNames.SqlInsert + DBNames.Database + "." + DBNames.ScoresTable + " ( " +
                DBNames.ScoresFieldNameTitle + ", " +
                DBNames.ScoresFieldNameAccompanimentId + ", " +
                DBNames.ScoresFieldNameArchiveId + ", " +
                DBNames.ScoresFieldNameGenreId + ", " +
                DBNames.ScoresFieldNameLanguageId + ", " +
                DBNames.ScoresFieldNamePublisher1Id + ", " +
                DBNames.ScoresFieldNamePublisher2Id + ", " +
                DBNames.ScoresFieldNamePublisher3Id + ", " +
                DBNames.ScoresFieldNamePublisher4Id + ", " +
                DBNames.ScoresFieldNameRepertoireId + ", " +
                DBNames.ScoresFieldNameScoreNumber + ", " +
                DBNames.ScoresFieldNameScoreSubNumber + ", " +
                DBNames.ScoresFieldNameMusicPiece + ", " +
                DBNames.ScoresFieldNameAmountPublisher1 + ", " +
                DBNames.ScoresFieldNameAmountPublisher2 + ", " +
                DBNames.ScoresFieldNameAmountPublisher3 + ", " +
                DBNames.ScoresFieldNameAmountPublisher4 + " )" +
                DBNames.SqlValues + "( '" + DBNames.LogNew + "', " +
                _score[0].AccompanimentId + ", " +
                _score[0].ArchiveId +", " +
                _score[0].GenreId +", " +
                _score[0].LanguageId +", " +
                _score[0].Publisher1Id +", " +
                _score[0].Publisher2Id +", " +
                _score[0].Publisher3Id +", " +
                _score[0].Publisher4Id +", " +
                _score[0].RepertoireId +", " +
                "'" + _score[0].ScoreNumber + "', " +
                "'" + _score[0].ScoreSubNumber + "', " +
                "'" + _score[0].MusicPiece + "', " +
                _score[0].AmountPublisher1 + ", " +
                _score[0].AmountPublisher2 + ", " +
                _score[0].AmountPublisher3 + ", " +
                _score[0].AmountPublisher4 + " );";

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ();

        using MySqlCommand cmd = new(sqlQuery, connection);

        int rowsAffected = cmd.ExecuteNonQuery();
    }
    #endregion

    #region Add New User
    public static void AddNewUser()
    {
        // Add a new user as Super User (UserRole 5)
        var sqlQuery = DBNames.SqlInsert + DBNames.Database + "." + DBNames.UsersTable +
            " ( " + DBNames.UsersFieldNameRoleId + " ) " + DBNames.SqlValues +
            " ( 5 ) ";

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open();

        using MySqlCommand cmd = new(sqlQuery, connection);

        int rowsAffected = cmd.ExecuteNonQuery();
    }
    #endregion

    #region Get Latest Added UserId
    public static int GetAddedUserId()
    {
        int userId = 0;
        //SELECT * FROM users ORDER BY id DESC LIMIT 1
        var sqlQuery = DBNames.SqlSelectAll +
            DBNames.SqlFrom + DBNames.Database + "." + DBNames.UsersTable +
            DBNames.SqlOrder + DBNames.UserRolesFieldNameId + DBNames.SqlDesc + DBNames.SqlLimit + "1";

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open();

        using MySqlCommand cmd = new(sqlQuery, connection);

        userId = (int)cmd.ExecuteScalar();

        return userId;
    }
    #endregion

    #region Get Empty Scores
    public static ObservableCollection<ScoreModel> GetEmptyScores ( string _table, string _orderByFieldName )
    {
        ObservableCollection<ScoreModel> Scores = new();

        DataTable dataTable = DBCommands.GetData(_table, _orderByFieldName);

        if ( dataTable.Rows.Count > 0 )
        {
            for ( int i = 0; i < dataTable.Rows.Count; i++ )
            {
                Scores.Add ( new ScoreModel
                {
                    ScoreId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString () ),
                    ScoreNumber = dataTable.Rows [ i ].ItemArray [ 1 ].ToString ()
                } );
            }
        }

        return Scores;
    }
    #endregion

    #region GetAccompaniments
    public static ObservableCollection<AccompanimentModel> GetAccompaniments ()
    {
        ObservableCollection<AccompanimentModel> Accompaniments = new();

        DataTable dataTable = DBCommands.GetData(DBNames.AccompanimentsTable, "NoSort");
        if ( dataTable.Rows.Count > 0 )
        {
            for ( int i = 0; i < dataTable.Rows.Count; i++ )
            {
                Accompaniments.Add ( new AccompanimentModel
                {
                    AccompanimentId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString () ),
                    AccompanimentName = dataTable.Rows [ i ].ItemArray [ 1 ].ToString ()
                } );
            }
        }
        return Accompaniments;
    }
    #endregion

    #region GetArchives
    public static ObservableCollection<ArchiveModel> GetArchives ()
    {
        ObservableCollection<ArchiveModel> Archives = new();

        DataTable dataTable = DBCommands.GetData(DBNames.ArchivesTable, "NoSort");
        if ( dataTable.Rows.Count > 0 )
        {
            for ( int i = 0; i < dataTable.Rows.Count; i++ )
            {
                Archives.Add ( new ArchiveModel
                {
                    ArchiveId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString () ),
                    ArchiveName = dataTable.Rows [ i ].ItemArray [ 1 ].ToString ()
                } );
            }
        }
        return Archives;
    }
    #endregion

    #region GetGenres
    public static ObservableCollection<GenreModel> GetGenres ()
    {
        ObservableCollection<GenreModel> Genres = new();

        DataTable dataTable = DBCommands.GetData(DBNames.GenresTable, "NoSort");
        if ( dataTable.Rows.Count > 0 )
        {
            for ( int i = 0; i < dataTable.Rows.Count; i++ )
            {
                Genres.Add ( new GenreModel
                {
                    GenreId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString () ),
                    GenreName = dataTable.Rows [ i ].ItemArray [ 1 ].ToString ()
                } );
            }
        }
        return Genres;
    }
    #endregion

    #region GetLanguages
    public static ObservableCollection<LanguageModel> GetLanguages ()
    {
        ObservableCollection<LanguageModel> Languages = new();

        DataTable dataTable = DBCommands.GetData(DBNames.LanguagesTable, "NoSort");
        if ( dataTable.Rows.Count > 0 )
        {
            for ( int i = 0; i < dataTable.Rows.Count; i++ )
            {
                Languages.Add ( new LanguageModel
                {
                    LanguageId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString () ),
                    LanguageName = dataTable.Rows [ i ].ItemArray [ 1 ].ToString ()
                } );
            }
        }
        return Languages;
    }
    #endregion

    #region GetPublishers
    public static ObservableCollection<PublisherModel> GetPublishers ()
    {
        ObservableCollection<PublisherModel> Publishers = new();

        DataTable dataTable = DBCommands.GetData(DBNames.PublishersTable, DBNames.PublishersFieldNameId);
        if ( dataTable.Rows.Count > 0 )
        {
            for ( int i = 0; i < dataTable.Rows.Count; i++ )
            {
                Publishers.Add ( new PublisherModel
                {
                    PublisherId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString () ),
                    PublisherName = dataTable.Rows [ i ].ItemArray [ 1 ].ToString ()
                } );
            }
        }
        return Publishers;
    }
    #endregion

    #region GetRepertoires
    public static ObservableCollection<RepertoireModel> GetRepertoires ()
    {
        ObservableCollection<RepertoireModel> Repertoires = new();

        DataTable dataTable = DBCommands.GetData(DBNames.RepertoiresTable, "NoSort");
        if ( dataTable.Rows.Count > 0 )
        {
            for ( int i = 0; i < dataTable.Rows.Count; i++ )
            {
                Repertoires.Add ( new RepertoireModel
                {
                    RepertoireId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString () ),
                    RepertoireName = dataTable.Rows [ i ].ItemArray [ 1 ].ToString (),
                    RepertoireRange = dataTable.Rows [i].ItemArray [ 4 ].ToString ()
                } );
            }
        }
        return Repertoires;
    }
    #endregion

    #region GetUserRoles
    public static ObservableCollection<UserRoleModel> GetUserRoles()
    {
        ObservableCollection<UserRoleModel> UserRoles = new();

        DataTable dataTable = DBCommands.GetData(DBNames.UserRolesTable, DBNames.UserRolesFieldNameOrder);
        if (dataTable.Rows.Count > 0)
        {
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                UserRoles.Add(new UserRoleModel
                {
                    RoleId = int.Parse(dataTable.Rows[i].ItemArray[0].ToString()),
                    RoleOrder = int.Parse(dataTable.Rows[i].ItemArray[1].ToString()),
                    RoleName = dataTable.Rows[i].ItemArray[2].ToString(),
                    RoleDescription = dataTable.Rows[i].ItemArray[3].ToString()
                });
            }
        }
        return UserRoles;
    }
    #endregion

    #region Update/Save Score
    public static void SaveScore(ObservableCollection<SaveScoreModel> scoreList)
    {

        string sqlQuery = DBNames.SqlUpdate + DBNames.ScoresTable + DBNames.SqlSet;

        if (scoreList[0].RepertoireId != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameRepertoireId + " = @" + DBNames.ScoresFieldNameRepertoireId; }
        if (scoreList[0].ArchiveId != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameArchiveId + " = @" + DBNames.ScoresFieldNameArchiveId; }
        if (scoreList[0].ByHeart != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameByHeart + " = @" + DBNames.ScoresFieldNameByHeart; }

        if (scoreList[0].TitleChanged != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameTitle + " = @" + DBNames.ScoresFieldNameTitle; }
        if (scoreList[0].SubTitleChanged != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameSubTitle + " = @" + DBNames.ScoresFieldNameSubTitle; }

        if (scoreList[0].ComposerChanged != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameComposer + " = @" + DBNames.ScoresFieldNameComposer; }
        if (scoreList[0].TextwriterChanged != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameTextwriter + " = @" + DBNames.ScoresFieldNameTextwriter; }
        if (scoreList[0].ArrangerChanged != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameArranger + " = @" + DBNames.ScoresFieldNameArranger; }

        if (scoreList[0].GenreId != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameGenreId + " = @" + DBNames.ScoresFieldNameGenreId; }
        if (scoreList[0].AccompanimentId != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameAccompanimentId + " = @" + DBNames.ScoresFieldNameAccompanimentId; }
        if (scoreList[0].LanguageId != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameLanguageId + " = @" + DBNames.ScoresFieldNameLanguageId; }

        if (scoreList[0].MusicPieceChanged != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameMusicPiece + " = @" + DBNames.ScoresFieldNameMusicPiece; }

        if (scoreList[0].DateDigitizedChanged != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameDigitized + " = @" + DBNames.ScoresFieldNameDigitized; }
        if (scoreList[0].DateModifiedChanged != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameModified + " = @" + DBNames.ScoresFieldNameModified; }
        if (scoreList[0].Checked != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameChecked + " = @" + DBNames.ScoresFieldNameChecked; }

        if (scoreList[0].MuseScoreORP != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameMuseScoreORP + " = @" + DBNames.ScoresFieldNameMuseScoreORP; }
        if (scoreList[0].MuseScoreORK != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameMuseScoreORK + " = @" + DBNames.ScoresFieldNameMuseScoreORK; }
        if (scoreList[0].MuseScoreTOP != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameMuseScoreTOP + " = @" + DBNames.ScoresFieldNameMuseScoreTOP; }
        if (scoreList[0].MuseScoreTOK != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameMuseScoreTOK + " = @" + DBNames.ScoresFieldNameMuseScoreTOK; }

        if (scoreList[0].PDFORP != -1) { sqlQuery += ", " + DBNames.ScoresFieldNamePDFORP + " = @" + DBNames.ScoresFieldNamePDFORP; }
        if (scoreList[0].PDFORK != -1) { sqlQuery += ", " + DBNames.ScoresFieldNamePDFORK + " = @" + DBNames.ScoresFieldNamePDFORK; }
        if (scoreList[0].PDFTOP != -1) { sqlQuery += ", " + DBNames.ScoresFieldNamePDFTOP + " = @" + DBNames.ScoresFieldNamePDFTOP; }
        if (scoreList[0].PDFTOK != -1) { sqlQuery += ", " + DBNames.ScoresFieldNamePDFTOK + " = @" + DBNames.ScoresFieldNamePDFTOK; }

        if (scoreList[0].MP3B1 != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameMP3B1 + " = @" + DBNames.ScoresFieldNameMP3B1; }
        if (scoreList[0].MP3B2 != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameMP3B2 + " = @" + DBNames.ScoresFieldNameMP3B2; }
        if (scoreList[0].MP3T1 != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameMP3T1 + " = @" + DBNames.ScoresFieldNameMP3T1; }
        if (scoreList[0].MP3T2 != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameMP3T2 + " = @" + DBNames.ScoresFieldNameMP3T2; }

        if (scoreList[0].MP3SOL != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameMP3SOL + " = @" + DBNames.ScoresFieldNameMP3SOL; }
        if (scoreList[0].MP3TOT != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameMP3TOT + " = @" + DBNames.ScoresFieldNameMP3TOT; }
        if (scoreList[0].MP3PIA != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameMP3PIA + " = @" + DBNames.ScoresFieldNameMP3PIA; }

        if (scoreList[0].MuseScoreOnline != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameOnline + " = @" + DBNames.ScoresFieldNameOnline; }

        if (scoreList[0].LyricsChanged != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameLyrics + " = @" + DBNames.ScoresFieldNameLyrics; }
        if (scoreList[0].NotesChanged != -1) { sqlQuery += ", " + DBNames.ScoresFieldNameNotes + " = @" + DBNames.ScoresFieldNameNotes; }

        if ( scoreList [ 0 ].AmountPublisher1 != -1 ) { sqlQuery += ", " + DBNames.ScoresFieldNameAmountPublisher1 + " = @" + DBNames.ScoresFieldNameAmountPublisher1; }
        if ( scoreList [ 0 ].AmountPublisher2 != -1 ) { sqlQuery += ", " + DBNames.ScoresFieldNameAmountPublisher2 + " = @" + DBNames.ScoresFieldNameAmountPublisher2; }
        if ( scoreList [ 0 ].AmountPublisher3 != -1 ) { sqlQuery += ", " + DBNames.ScoresFieldNameAmountPublisher3 + " = @" + DBNames.ScoresFieldNameAmountPublisher3; }
        if ( scoreList [ 0 ].AmountPublisher4 != -1 ) { sqlQuery += ", " + DBNames.ScoresFieldNameAmountPublisher4 + " = @" + DBNames.ScoresFieldNameAmountPublisher4; }

        if ( scoreList [ 0 ].Publisher1Id != -1 ) { sqlQuery += ", " + DBNames.ScoresFieldNamePublisher1Id + " = @" + DBNames.ScoresFieldNamePublisher1Id; }
        if ( scoreList [ 0 ].Publisher2Id != -1 ) { sqlQuery += ", " + DBNames.ScoresFieldNamePublisher2Id + " = @" + DBNames.ScoresFieldNamePublisher2Id; }
        if ( scoreList [ 0 ].Publisher3Id != -1 ) { sqlQuery += ", " + DBNames.ScoresFieldNamePublisher3Id + " = @" + DBNames.ScoresFieldNamePublisher3Id; }
        if ( scoreList [ 0 ].Publisher4Id != -1 ) { sqlQuery += ", " + DBNames.ScoresFieldNamePublisher4Id + " = @" + DBNames.ScoresFieldNamePublisher4Id; }

        // Add the filter to the sqlQuery
        sqlQuery += DBNames.SqlWhere + DBNames.ScoresFieldNameId + " = @" + DBNames.ScoresFieldNameId + ";";
        //sqlQuery += DBNames.SqlWhere + DBNames.ScoresFieldNameScoreNumber + " = @" + DBNames.ScoresFieldNameScoreNumber + ";";

        try
        {
            ExecuteNonQueryScoresTable ( sqlQuery.Replace ( "SET , ", "SET " ), scoreList );
        }
        catch ( MySqlException ex )
        {
            Debug.WriteLine ( "Fout (UpdateScoresTable - MySqlException): " + ex.Message );
            throw ex;
        }
        catch ( Exception ex )
        {
            Debug.WriteLine ( "Fout (UpdateScoresTable): " + ex.Message );
            throw;
        }
    }
    #endregion

    #region Update User
    public static void UpdateUser ( ObservableCollection<UserModel> modifiedUser )
    {

        string sqlQuery = DBNames.SqlUpdate + DBNames.UsersTable + DBNames.SqlSet;

        if( modifiedUser != null )
        {
            if ( modifiedUser [ 0 ].UserName != "" )
            { sqlQuery += DBNames.UsersFieldNameUserName + " = @" + DBNames.UsersFieldNameUserName; }

            if ( modifiedUser [0].UserFullName != "" )
            { sqlQuery += ", " + DBNames.UsersFieldNameFullName + " = @" + DBNames.UsersFieldNameFullName; }

            if ( modifiedUser [ 0 ].UserEmail != "" )
            { sqlQuery += ", " + DBNames.UsersFieldNameLogin + " = @" + DBNames.UsersFieldNameLogin; }

            if ( modifiedUser [ 0 ].UserPassword != "" )
            { sqlQuery += ", `" + DBNames.UsersFieldNamePW + "` = @" + DBNames.UsersFieldNamePW; }
        }

        // Add the filter to the sqlQuery
        sqlQuery += DBNames.SqlWhere + DBNames.UsersFieldNameId + " = @" + DBNames.UsersFieldNameId + ";";

        try
        {
            ExecuteNonQueryUsersTable ( sqlQuery.Replace ( "SET , ", "SET " ), modifiedUser );
        }
        catch ( MySqlException ex )
        {
            Debug.WriteLine ( "Fout (UpdateScoresTable - MySqlException): " + ex.Message );
            throw ex;
        }
        catch ( Exception ex )
        {
            Debug.WriteLine ( "Fout (UpdateScoresTable): " + ex.Message );
            throw;
        }
    }
    #endregion

    #region Get TargetId for renumbering Score
    /// <summary>
    /// Get the Id of the Score where the data should be renumbered to
    /// </summary>
    /// <param name="_scoreNumber"></param>
    /// <returns></returns>
    public static int GetTargetId ( string _scoreNumber )
    {
        var _targetId = 0;

        var sqlQuery = DBNames.SqlSelect + DBNames.ScoresFieldNameId + DBNames.SqlFrom + DBNames.Database + "." + DBNames.ScoresTable + DBNames.SqlWhere + DBNames.ScoresFieldNameScoreNumber + " = '" + _scoreNumber + "';";

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ();

        using MySqlCommand cmd = new(sqlQuery, connection);

        _targetId = (int)cmd.ExecuteScalar();

        return _targetId;
    }
    #endregion

    #region Renumber Score list
    public static void RenumberScoreList(DataTable scoreList, string newScoreNumber )
    {
        var sqlQuery = "";

        for ( int i = 0; i < scoreList.Rows.Count; i++ )
        {
            string[] _digitized = scoreList.Rows [ i ].ItemArray [ 14 ].ToString ().Split(" ");
            string[] _digitizedDate = _digitized[0].Split("-");
            string digitizedDate = $"{_digitizedDate[2]}-{String.Format("{0:00}", _digitizedDate[1])}-{String.Format("{0:00}", _digitizedDate[0])}";

            string[] _modified = scoreList.Rows [ i ].ItemArray [ 14 ].ToString ().Split(" ");
            string[] _modifiedDate = _modified[0].Split("-");
            string modifiedDate = $"{_modifiedDate[2]}-{String.Format("{0:00}", _modifiedDate[1])}-{String.Format("{0:00}", _modifiedDate[0])}";


            sqlQuery += DBNames.SqlInsert + DBNames.Database + "." + DBNames.ScoresTable + " ( " +
                DBNames.ScoresFieldNameArchiveId + ", " +
                DBNames.ScoresFieldNameRepertoireId + ", " +
                DBNames.ScoresFieldNameScoreNumber + ", " +
                DBNames.ScoresFieldNameScoreSubNumber + ", " +
                DBNames.ScoresFieldNameTitle + ", " +
                DBNames.ScoresFieldNameSubTitle + ", " +
                DBNames.ScoresFieldNameComposer + ", " +
                DBNames.ScoresFieldNameTextwriter + ", " +
                DBNames.ScoresFieldNameArranger + ", " +
                DBNames.ScoresFieldNameLanguageId + ", " +
                DBNames.ScoresFieldNameGenreId + ", " +
                DBNames.ScoresFieldNameLyrics + ", " +
                DBNames.ScoresFieldNameChecked + ", " +
                DBNames.ScoresFieldNameDigitized + ", " +
                DBNames.ScoresFieldNameModified + ", " +
                DBNames.ScoresFieldNameAccompanimentId + ", " +
                DBNames.ScoresFieldNamePDFORP + ", " +
                DBNames.ScoresFieldNamePDFORK + ", " +
                DBNames.ScoresFieldNamePDFTOP + ", " +
                DBNames.ScoresFieldNamePDFTOK + ", " +
                DBNames.ScoresFieldNameMuseScoreORP + ", " +
                DBNames.ScoresFieldNameMuseScoreORK + ", " +
                DBNames.ScoresFieldNameMuseScoreTOP + ", " +
                DBNames.ScoresFieldNameMuseScoreTOK + ", " +
                DBNames.ScoresFieldNameMP3TOT + ", " +
                DBNames.ScoresFieldNameMP3T1 + ", " +
                DBNames.ScoresFieldNameMP3T2 + ", " +
                DBNames.ScoresFieldNameMP3B1 + ", " +
                DBNames.ScoresFieldNameMP3B2 + ", " +
                DBNames.ScoresFieldNameMP3SOL + ", " +
                DBNames.ScoresFieldNameMP3PIA + ", " +
                DBNames.ScoresFieldNameOnline + ", " +
                DBNames.ScoresFieldNameByHeart + ", " +
                DBNames.ScoresFieldNameMusicPiece + ", " +
                DBNames.ScoresFieldNameNotes + ", " +
                DBNames.ScoresFieldNameAmountPublisher1 + ", " +
                DBNames.ScoresFieldNameAmountPublisher2 + ", " +
                DBNames.ScoresFieldNameAmountPublisher3 + ", " +
                DBNames.ScoresFieldNameAmountPublisher4 + ", " +
                DBNames.ScoresFieldNamePublisher1Id + ", " +
                DBNames.ScoresFieldNamePublisher2Id + ", " +
                DBNames.ScoresFieldNamePublisher3Id + ", " +
                DBNames.ScoresFieldNamePublisher4Id + " )" +
                DBNames.SqlValues + "( " +
                scoreList.Rows [ i ].ItemArray [ 1 ] + ", " +
                scoreList.Rows [ i ].ItemArray [ 2 ] + ", " +
                "'" + newScoreNumber + "', " +
                "'" + scoreList.Rows [ i ].ItemArray [ 4 ] + "', " +
                "'" + scoreList.Rows [ i ].ItemArray [ 5 ] + "', " +
                "'" + scoreList.Rows [ i ].ItemArray [ 6 ] + "', " +
                "'" + scoreList.Rows [ i ].ItemArray [ 7 ] + "', " +
                "'" + scoreList.Rows [ i ].ItemArray [ 8 ] + "', " +
                "'" + scoreList.Rows [ i ].ItemArray [ 9 ] + "', " +
                scoreList.Rows [ i ].ItemArray [ 10 ] + ", " +
                scoreList.Rows [ i ].ItemArray [ 11 ] + ", " +
                "'" + scoreList.Rows [ i ].ItemArray [ 12 ] + "', " +
                scoreList.Rows [ i ].ItemArray [ 13 ] + ", " +
                "'" + digitizedDate + "', " +
                "'" + modifiedDate + "', " +
                scoreList.Rows [ i ].ItemArray [ 16 ] + ", " +
                scoreList.Rows [ i ].ItemArray [ 17 ] + ", " +
                scoreList.Rows [ i ].ItemArray [ 18 ] + ", " +
                scoreList.Rows [ i ].ItemArray [ 19 ] + ", " +
                scoreList.Rows [ i ].ItemArray [ 20 ] + ", " +
                scoreList.Rows [ i ].ItemArray [ 21 ] + ", " +
                scoreList.Rows [ i ].ItemArray [ 22 ] + ", " +
                scoreList.Rows [ i ].ItemArray [ 23 ] + ", " +
                scoreList.Rows [ i ].ItemArray [ 24 ] + ", " +
                scoreList.Rows [ i ].ItemArray [ 25 ] + ", " +
                scoreList.Rows [ i ].ItemArray [ 26 ] + ", " +
                scoreList.Rows [ i ].ItemArray [ 27 ] + ", " +
                scoreList.Rows [ i ].ItemArray [ 28 ] + ", " +
                scoreList.Rows [ i ].ItemArray [ 29 ] + ", " +
                scoreList.Rows [ i ].ItemArray [ 30 ] + ", " +
                scoreList.Rows [ i ].ItemArray [ 31 ] + ", " +
                scoreList.Rows [ i ].ItemArray [ 32 ] + ", " +
                scoreList.Rows [ i ].ItemArray [ 33 ] + ", " +
                "'" + scoreList.Rows [ i ].ItemArray [ 34 ] + "', " +
                "'" + scoreList.Rows [ i ].ItemArray [ 35 ] + "', " + 
                scoreList.Rows [ i ].ItemArray [ 36 ] + ", " +
                scoreList.Rows [ i ].ItemArray [ 37 ] + ", " +
                scoreList.Rows [ i ].ItemArray [ 38 ] + ", " +
                scoreList.Rows [ i ].ItemArray [ 39 ] + ", " +
                scoreList.Rows [ i ].ItemArray [ 40 ] + ", " +
                scoreList.Rows [ i ].ItemArray [ 41 ] + ", " +
                scoreList.Rows [ i ].ItemArray [ 42 ] + ", " +
                scoreList.Rows [ i ].ItemArray [ 43 ] + " );";
        }

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ();

        using MySqlCommand cmd = new(sqlQuery, connection);

        int rowsAffected = cmd.ExecuteNonQuery();
    }
    #endregion

    #region Execute Non Query ScoresTable
    static void ExecuteNonQueryScoresTable ( string sqlQuery, ObservableCollection<SaveScoreModel> scoreList )
    {
        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ();

        using MySqlCommand cmd = new(sqlQuery, connection);

        if ( scoreList [ 0 ].RepertoireId != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameRepertoireId, MySqlDbType.Int32 ).Value = scoreList [ 0 ].RepertoireId; }
        if ( scoreList [ 0 ].ArchiveId != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameArchiveId, MySqlDbType.Int32 ).Value = scoreList [ 0 ].ArchiveId; }
        if ( scoreList [ 0 ].ByHeart != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameByHeart, MySqlDbType.Int32 ).Value = scoreList [ 0 ].ByHeart; }

        if ( scoreList [ 0 ].TitleChanged != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameTitle, MySqlDbType.VarChar ).Value = scoreList [ 0 ].Title; }
        if ( scoreList [ 0 ].SubTitleChanged != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameSubTitle, MySqlDbType.VarChar ).Value = scoreList [ 0 ].SubTitle; }

        if ( scoreList [ 0 ].ComposerChanged != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameComposer, MySqlDbType.VarChar ).Value = scoreList [ 0 ].Composer; }
        if ( scoreList [ 0 ].TextwriterChanged != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameTextwriter, MySqlDbType.VarChar ).Value = scoreList [ 0 ].Textwriter; }
        if ( scoreList [ 0 ].ArrangerChanged != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameArranger, MySqlDbType.VarChar ).Value = scoreList [ 0 ].Arranger; }

        if ( scoreList [ 0 ].GenreId != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameGenreId, MySqlDbType.Int32 ).Value = scoreList [ 0 ].GenreId; }
        if ( scoreList [ 0 ].AccompanimentId != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameAccompanimentId, MySqlDbType.Int32 ).Value = scoreList [ 0 ].AccompanimentId; }
        if ( scoreList [ 0 ].LanguageId != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameLanguageId, MySqlDbType.Int32 ).Value = scoreList [ 0 ].LanguageId; }

        if ( scoreList [ 0 ].MusicPieceChanged != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMusicPiece, MySqlDbType.VarChar ).Value = scoreList [ 0 ].MusicPiece; }

        if ( scoreList [ 0 ].DateDigitizedChanged != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameDigitized, MySqlDbType.String ).Value = scoreList [ 0 ].DateDigitized; }
        if ( scoreList [ 0 ].DateModifiedChanged != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameModified, MySqlDbType.String ).Value = scoreList [ 0 ].DateModified; }
        if ( scoreList [ 0 ].Checked != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameChecked, MySqlDbType.Int32 ).Value = scoreList [ 0 ].Checked; }

        if ( scoreList [ 0 ].MuseScoreORP != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMuseScoreORP, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MuseScoreORP; }
        if ( scoreList [ 0 ].MuseScoreORK != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMuseScoreORK, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MuseScoreORK; }
        if ( scoreList [ 0 ].MuseScoreTOP != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMuseScoreTOP, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MuseScoreTOP; }
        if ( scoreList [ 0 ].MuseScoreTOK != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMuseScoreTOK, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MuseScoreTOK; }

        if ( scoreList [ 0 ].PDFORP != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNamePDFORP, MySqlDbType.Int32 ).Value = scoreList [ 0 ].PDFORP; }
        if ( scoreList [ 0 ].PDFORK != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNamePDFORK, MySqlDbType.Int32 ).Value = scoreList [ 0 ].PDFORK; }
        if ( scoreList [ 0 ].PDFTOP != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNamePDFTOP, MySqlDbType.Int32 ).Value = scoreList [ 0 ].PDFTOP; }
        if ( scoreList [ 0 ].PDFTOK != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNamePDFTOK, MySqlDbType.Int32 ).Value = scoreList [ 0 ].PDFTOK; }

        if ( scoreList [ 0 ].MP3B1 != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3B1, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3B1; }
        if ( scoreList [ 0 ].MP3B2 != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3B2, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3B2; }
        if ( scoreList [ 0 ].MP3T1 != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3T1, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3T1; }
        if ( scoreList [ 0 ].MP3T2 != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3T2, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3T2; }

        if ( scoreList [ 0 ].MP3SOL != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3SOL, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3SOL; }
        if ( scoreList [ 0 ].MP3TOT != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3TOT, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3TOT; }
        if ( scoreList [ 0 ].MP3PIA != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3PIA, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3PIA; }

        if ( scoreList [ 0 ].MuseScoreOnline != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameOnline, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MuseScoreOnline; }

        // Rich text fields
        cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameLyrics, MySqlDbType.MediumText ).Value = scoreList [ 0 ].Lyrics;
        cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameNotes, MySqlDbType.MediumText ).Value = scoreList [ 0 ].Notes;

        if ( scoreList [ 0 ].AmountPublisher1 != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameAmountPublisher1, MySqlDbType.Int32 ).Value = scoreList [ 0 ].AmountPublisher1; }
        if ( scoreList [ 0 ].AmountPublisher2 != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameAmountPublisher2, MySqlDbType.Int32 ).Value = scoreList [ 0 ].AmountPublisher2; }
        if ( scoreList [ 0 ].AmountPublisher3 != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameAmountPublisher3, MySqlDbType.Int32 ).Value = scoreList [ 0 ].AmountPublisher3; }
        if ( scoreList [ 0 ].AmountPublisher4 != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameAmountPublisher4, MySqlDbType.Int32 ).Value = scoreList [ 0 ].AmountPublisher4; }

        if ( scoreList [ 0 ].Publisher1Id != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNamePublisher1Id, MySqlDbType.Int32 ).Value = scoreList [ 0 ].Publisher1Id; }
        if ( scoreList [ 0 ].Publisher2Id != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNamePublisher2Id, MySqlDbType.Int32 ).Value = scoreList [ 0 ].Publisher2Id; }
        if ( scoreList [ 0 ].Publisher3Id != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNamePublisher3Id, MySqlDbType.Int32 ).Value = scoreList [ 0 ].Publisher3Id; }
        if ( scoreList [ 0 ].Publisher4Id != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNamePublisher4Id, MySqlDbType.Int32 ).Value = scoreList [ 0 ].Publisher4Id; }

        // Add the Id value for the Score that has to be modified
        cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameId, MySqlDbType.Int32 ).Value = scoreList [ 0 ].ScoreId;

        //execute; returns the number of rows affected
        int rowsAffected = cmd.ExecuteNonQuery();
    }
    #endregion

    #region Execute Non Query UserssTable
    static void ExecuteNonQueryUsersTable ( string sqlQuery, ObservableCollection<UserModel> modifiedUser )
    {
        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ();

        using MySqlCommand cmd = new(sqlQuery, connection);

        if ( modifiedUser != null )
        {
            if ( modifiedUser [ 0 ].UserName != "" )
            { cmd.Parameters.Add ( "@" + DBNames.UsersFieldNameUserName, MySqlDbType.VarChar ).Value = modifiedUser [ 0 ].UserName; }

            if ( modifiedUser [ 0 ].UserFullName != "" )
            { cmd.Parameters.Add ( "@" + DBNames.UsersFieldNameFullName, MySqlDbType.VarChar ).Value = modifiedUser[0].UserFullName; }

            if ( modifiedUser [ 0 ].UserFullName != "" )
            { cmd.Parameters.Add ( "@" + DBNames.UsersFieldNameLogin, MySqlDbType.VarChar ).Value = modifiedUser [ 0 ].UserEmail; }

            if ( modifiedUser [ 0 ].UserPassword != "" )
            { cmd.Parameters.Add ( "@" + DBNames.UsersFieldNamePW, MySqlDbType.VarChar ).Value = modifiedUser [ 0 ].UserPassword; }
        }

        // Add the userId value for the user that has to be modified
        cmd.Parameters.Add ( "@" + DBNames.UsersFieldNameId, MySqlDbType.VarChar ).Value = modifiedUser [ 0 ].UserId;

        //execute; returns the number of rows affected
        int rowsAffected = cmd.ExecuteNonQuery();
    }
    #endregion

    #region Check if Number is in range
    bool ValueInRange ( int numberToCheck, int bottom, int top )
    {
        return ( numberToCheck >= bottom && numberToCheck <= top );
    }
    #endregion

    #region Check valid user password
    public static int CheckUserPassword(string login, string password)
    {
        int UserId = 0;
        var _pwLogedInUser = Helper.HashPepperPassword(password, login);

        ObservableCollection<UserModel> Users = GetUsers();
        
        foreach(var user in Users )
        {
            var _pwToCheck = Helper.HashPepperPassword(password, user.UserName);
            if ( _pwLogedInUser == _pwToCheck)
            {
                return user.UserId;
            }
        }
        return 0;    
    }
    #endregion

    #region Check valid username
    public static bool CheckUserName ( string userName, int userId )
    {
        // Returns false if UserName does not already exist and true if it is already used (bool UserExists = DBCommands.CheckUserName( tbUserName.Text );)
        ObservableCollection<UserModel> Users = GetUsers();

        foreach ( var user in Users )
        {
            if ( user.UserName.ToLower () == userName.ToLower () )
            {
                // Still valid if the Username belongs to the selected User
                if ( user.UserId == userId )
                { 
                    return false; 
                }
                else 
                { 
                    return true;
                }
            }
        }
        return false;
    }
    #endregion

    #region Check valid e-mail (Is it unique)
    public static bool CheckEMail ( string email, int userId )
    {
        ObservableCollection<UserModel> Users = GetUsers();

        foreach ( var user in Users )
        {
            if ( user.UserEmail.ToLower () == email.ToLower () )
            {
                // Still valid if the Username belongs to the selected User
                if ( user.UserId == userId )
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        return false;
    }
    #endregion

    #region Check if the e-mail address has the correct format
    public static bool IsValidEmail ( string email )
    {
        if ( string.IsNullOrWhiteSpace ( email ) )
            return false;

        try
        {
            // Normalize the domain
            email = Regex.Replace ( email, @"(@)(.+)$", DomainMapper,
                                  RegexOptions.None, TimeSpan.FromMilliseconds ( 200 ) );

            // Examines the domain part of the email and normalizes it.
            string DomainMapper ( Match match )
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                string domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups [ 1 ].Value + domainName;
            }
        }
        catch ( RegexMatchTimeoutException e )
        {
            return false;
        }
        catch ( ArgumentException e )
        {
            return false;
        }

        try
        {
            return Regex.IsMatch ( email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds ( 250 ) );
        }
        catch ( RegexMatchTimeoutException )
        {
            return false;
        }
    }
    #endregion

    #region Get UserInfo
    #region Get Userinfo for 1 user
    public static ObservableCollection<UserModel> GetUsers (int _userId)
    {
        ObservableCollection<UserModel> users = new();

        DataTable dataTable = DBCommands.GetData(DBNames.UsersTable, "nosort", DBNames.UsersFieldNameId, _userId.ToString());

        if ( dataTable.Rows.Count > 0 )
        {
            for ( int i = 0; i < dataTable.Rows.Count; i++ )
            {
                users.Add ( new UserModel
                {
                    UserId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString () ),
                    UserName = dataTable.Rows [ i ].ItemArray [ 2 ].ToString (),
                    UserEmail = dataTable.Rows [ i ].ItemArray [ 1 ].ToString (),
                    UserPassword = dataTable.Rows [ i ].ItemArray [ 3 ].ToString (),
                    UserFullName = dataTable.Rows [ i ].ItemArray [ 5 ].ToString (),
                    UserRoleId = int.Parse(dataTable.Rows [ i ].ItemArray [ 4 ].ToString ())
                } );
            }
        }
        return users;
    }
    #endregion

    #region Get Userinfo for all users
    public static ObservableCollection<UserModel> GetUsers ( )
    {
        ObservableCollection<UserModel> users = new();

        DataTable dataTable = DBCommands.GetData(DBNames.UsersView, DBNames.UsersFieldNameFullName);

        if ( dataTable.Rows.Count > 0 )
        {
            for ( int i = 0; i < dataTable.Rows.Count; i++ )
            {
                users.Add ( new UserModel
                {
                    UserId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString () ),
                    UserName = dataTable.Rows [ i ].ItemArray [ 2 ].ToString (),
                    UserEmail = dataTable.Rows [ i ].ItemArray [ 1 ].ToString (),
                    UserPassword = dataTable.Rows [ i ].ItemArray [ 3 ].ToString (),
                    UserFullName = dataTable.Rows [ i ].ItemArray [ 5 ].ToString (),
                    UserRoleId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 4 ].ToString () ),
                    RoleName= dataTable.Rows [ i ].ItemArray [7].ToString(),
                    RoleDescription= dataTable.Rows [ i ].ItemArray [ 8 ].ToString ()
                } );
            }
        }
        return users;
    }
    #endregion

    #endregion

    #region Write History Logging
    public static void WriteLog(int loggedInUser, string action, string description)
    {
        var sqlQuery = DBNames.SqlInsert + DBNames.Database + "." + DBNames.LogTable + " ( " + 
            DBNames.LogFieldNameUserId + ", " + 
            DBNames.LogFieldNameAction + ", " +
            DBNames.LogFieldNameDescription + " ) " + DBNames.SqlValues + " ( " +
            loggedInUser + ", '" + action + "', '" + description + "' );";

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open();

        using MySqlCommand cmd = new(sqlQuery, connection);

        int rowsAffected = cmd.ExecuteNonQuery();
    }
    #endregion

    #region Write History Detail Logging
    public static void WriteDetailLog(int logId, string field, string oldValue, string newValue)
    {
        var sqlQuery = DBNames.SqlInsert + DBNames.Database + "." + DBNames.LogDetailTable + " ( " +
            DBNames.LogDetailFieldNameLogId + ", " +
            DBNames.LogDetailFieldNameChanged + ", " +
            DBNames.LogDetailFieldNameOldValue + ", " + 
            DBNames.LogDetailFieldNameNewValue + " ) " + DBNames.SqlValues + " ( " +
            logId + ", '" + field + "', '" + oldValue + "', '" + newValue + "' );";

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open();

        using MySqlCommand cmd = new(sqlQuery, connection);

        int rowsAffected = cmd.ExecuteNonQuery();
    }
    #endregion

    #region Get Latest Added HistoryId
    public static int GetAddedHistoryId ()
    {
        int userId = 0;
        var sqlQuery = DBNames.SqlSelectAll +
            DBNames.SqlFrom + DBNames.Database + "." + DBNames.LogTable +
            DBNames.SqlOrder + DBNames.LogFieldNameLogId + DBNames.SqlDesc + DBNames.SqlLimit + "1";

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ();

        using MySqlCommand cmd = new(sqlQuery, connection);

        userId = (int) cmd.ExecuteScalar ();

        return userId;
    }
    #endregion

    #region Get HistoryLogging
    public static ObservableCollection<HistoryModel> GetHistoryLog()
    {
        ObservableCollection<HistoryModel> HistoryLog = new();
        DataTable dataTable = new();

        dataTable = GetData ( DBNames.LogView, DBNames.LogViewFieldNameLogid);

        if ( dataTable.Rows.Count > 0 )
        {

            for ( int i = 0; i < dataTable.Rows.Count; i++ )
            {
                //Split-up DateTimeStamp in a date and a time
                string[] _dateTime = dataTable.Rows[i].ItemArray[1].ToString().Split(' ');
                string[] _date = _dateTime[0].Split("-");
                string logDate = $"{int.Parse(_date[0]).ToString("00")}-{int.Parse(_date[1]).ToString("00")}-{_date[2]}";
                string logTime = _dateTime[1];

                HistoryLog.Add ( new HistoryModel
                {
                    LogId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString () ),
                    LogDate = logDate,
                    LogTime = logTime,
                    UserName= dataTable.Rows [ i ].ItemArray [2].ToString(),
                    PerformedAction = dataTable.Rows [ i ].ItemArray [3 ].ToString (),
                    Description = dataTable.Rows [ i ].ItemArray [ 4 ].ToString (),
                    ModifiedField = dataTable.Rows [ i ].ItemArray [ 5 ].ToString (),
                    OldValue = dataTable.Rows [ i ].ItemArray [ 6 ].ToString (),
                    NewValue = dataTable.Rows [ i ].ItemArray [ 7 ].ToString ()
                } );
            }
        }
        return HistoryLog;
    }
    #endregion
}
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604
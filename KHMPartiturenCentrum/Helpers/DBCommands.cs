using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Principal;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Xml;
using Google.Protobuf.WellKnownTypes;
using K4os.Compression.LZ4.Internal;

using KHMPartiturenCentrum.Models;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604

namespace KHMPartiturenCentrum.Helpers;

public class DBCommands
{
    #region GetData
    #region GetData unsorted
    public static DataTable GetData ( string _table )
    {
        string selectQuery = DBNames.SqlSelectAll + DBNames.SqlFrom + DBNames.Database + "." + _table;
        MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ();
        DataTable table = new();
        MySqlDataAdapter adapter = new(selectQuery, connection);
        adapter.Fill ( table );
        connection.Close ();
        return table;
    }
    #endregion

    #region GetData Sorted
    public static DataTable GetData ( string _table, string OrderByFieldName )
    {
        string selectQuery = "";
        if ( OrderByFieldName.ToLower () == "nosort" )
        {
            selectQuery = DBNames.SqlSelectAll + DBNames.SqlFrom + DBNames.Database + "." + _table;
        }
        else
        {
            selectQuery = DBNames.SqlSelectAll + DBNames.SqlFrom + DBNames.Database + "." + _table + DBNames.SqlOrder + OrderByFieldName;
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

    #region Get Available Scores (non Christmas)
    public static ObservableCollection<ScoreModel> GetAvailableScores ()
    {
        ObservableCollection<ScoreModel> Scores = new();

        DataTable dataTable = DBCommands.GetData(DBNames.AvailableScoresView, DBNames.AvailableScoresFieldNameNumber);

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

    #region Get Available Scores (Christmas)
    public static ObservableCollection<ScoreModel> GetAvailableChristmasScores ()
    {
        ObservableCollection<ScoreModel> Scores = new();

        DataTable dataTable = DBCommands.GetData(DBNames.AvailableChristmasScoresView, DBNames.AvailableChristmasScoresFieldNameNumber);

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

    #region Get Scores
    public static ObservableCollection<ScoreModel> GetScores ( string _table, string _orderByFieldName )
    {
        ObservableCollection<ScoreModel> Scores = new();

        DataTable dataTable = DBCommands.GetData(_table, _orderByFieldName);

        if ( dataTable.Rows.Count > 0 )
        {
            for ( int i = 0; i < dataTable.Rows.Count; i++ )
            {
                // Set the bools
                bool check = false, byHeart = false;
                bool pdfORP = false, pdfORK = false, pdfTOP = false, pdfTOK = false;
                bool mscORP = false, mscORK = false, mscTOP = false, mscTOK = false, mscOnline = false;
                bool mp3B1 = false, mp3B2 = false, mp3T1 = false, mp3T2 = false, mp3SOL = false, mp3TOT = false, mp3PIA = false;

                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 18 ].ToString () ) == 0 ) { check = false; } else { check = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 23 ].ToString () ) == 0 ) { pdfORP = false; } else { pdfORP = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 24 ].ToString () ) == 0 ) { pdfORK = false; } else { pdfORK = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 25 ].ToString () ) == 0 ) { pdfTOP = false; } else { pdfTOP = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 26 ].ToString () ) == 0 ) { pdfTOK = false; } else { pdfTOK = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 27 ].ToString () ) == 0 ) { mscORP = false; } else { mscORP = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 28 ].ToString () ) == 0 ) { mscORK = false; } else { mscORK = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 29 ].ToString () ) == 0 ) { mscTOP = false; } else { mscTOP = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 30 ].ToString () ) == 0 ) { mscTOK = false; } else { mscTOK = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 31 ].ToString () ) == 0 ) { mp3TOT = false; } else { mp3TOT = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 32 ].ToString () ) == 0 ) { mp3T1 = false; } else { mp3T1 = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 33 ].ToString () ) == 0 ) { mp3T2 = false; } else { mp3T2 = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 34 ].ToString () ) == 0 ) { mp3B1 = false; } else { mp3B1 = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 35 ].ToString () ) == 0 ) { mp3B2 = false; } else { mp3B2 = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 36 ].ToString () ) == 0 ) { mp3SOL = false; } else { mp3SOL = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 37 ].ToString () ) == 0 ) { mp3PIA = false; } else { mp3PIA = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 38 ].ToString () ) == 0 ) { mscOnline = false; } else { mscOnline = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 39 ].ToString () ) == 0 ) { byHeart = false; } else { byHeart = true; }

                // Set total
                var total = 0;

                total = int.Parse ( dataTable.Rows [ i ].ItemArray [ 42 ].ToString () ) +
                        int.Parse ( dataTable.Rows [ i ].ItemArray [ 43 ].ToString () ) +
                        int.Parse ( dataTable.Rows [ i ].ItemArray [ 44 ].ToString () ) +
                        int.Parse ( dataTable.Rows [ i ].ItemArray [ 45 ].ToString () );

                // Set the datestrings
                string dateCreated = "";
                if ( dataTable.Rows [ i ].ItemArray [ 19 ].ToString () != "" )
                {
                    string[] _tempCreated = dataTable.Rows[i].ItemArray[19].ToString().Split(" ");
                    dateCreated = _tempCreated [ 0 ];
                }

                string dateModified = "";
                if ( dataTable.Rows [ i ].ItemArray [ 20 ].ToString () != "" )
                {
                    string[] _tempModified = dataTable.Rows[i].ItemArray[20].ToString().Split(" ");
                    dateModified = _tempModified [ 0 ];
                }

                // When Title is empty don't add that row to the list
                if ( dataTable.Rows [ i ].ItemArray [ 4 ].ToString () != string.Empty )
                {
                    Scores.Add ( new ScoreModel
                    {
                        ScoreId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString () ),
                        Score = dataTable.Rows [ i ].ItemArray [ 1 ].ToString (),
                        ScoreNumber = dataTable.Rows [ i ].ItemArray [ 2 ].ToString (),
                        ScoreSubNumber = dataTable.Rows [ i ].ItemArray [ 3 ].ToString (),
                        ScoreTitle = dataTable.Rows [ i ].ItemArray [ 4 ].ToString (),
                        ScoreSubTitle = dataTable.Rows [ i ].ItemArray [ 5 ].ToString (),
                        Composer = dataTable.Rows [ i ].ItemArray [ 6 ].ToString (),
                        TextWriter = dataTable.Rows [ i ].ItemArray [ 7 ].ToString (),
                        Arranger = dataTable.Rows [ i ].ItemArray [ 8 ].ToString (),
                        ArchiveId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 9 ].ToString () ),
                        ArchiveName = dataTable.Rows [ i ].ItemArray [ 10 ].ToString (),
                        RepertoireId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 11 ].ToString () ),
                        RepertoireName = dataTable.Rows [ i ].ItemArray [ 12 ].ToString (),
                        LanguageId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 13 ].ToString () ),
                        LanguageName = dataTable.Rows [ i ].ItemArray [ 14 ].ToString (),
                        GenreId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 15 ].ToString () ),
                        GenreName = dataTable.Rows [ i ].ItemArray [ 16 ].ToString (),
                        Lyrics = dataTable.Rows [ i ].ItemArray [ 17 ].ToString (),
                        CheckInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 18 ].ToString () ),
                        DateCreatedString = dateCreated,
                        DateModifiedString = dateModified,
                        Check = check,
                        AccompanimentId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 21 ].ToString () ),
                        AccompanimentName = dataTable.Rows [ i ].ItemArray [ 22 ].ToString (),
                        PDFORPInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 23 ].ToString () ),
                        PDFORP = pdfORP,
                        PDFORKInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 24 ].ToString () ),
                        PDFORK = pdfORK,
                        PDFTOPInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 25 ].ToString () ),
                        PDFTOP = pdfTOP,
                        PDFTOKInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 26 ].ToString () ),
                        PDFTOK = pdfTOK,
                        MuseScoreORPInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 27 ].ToString () ),
                        MuseScoreORP = mscORP,
                        MuseScoreORKInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 28 ].ToString () ),
                        MuseScoreORK = mscORK,
                        MuseScoreTOPInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 29 ].ToString () ),
                        MuseScoreTOP = mscTOP,
                        MuseScoreTOKInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 30 ].ToString () ),
                        MuseScoreTOK = mscTOK,
                        MP3TOTInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 31 ].ToString () ),
                        MP3TOT = mp3TOT,
                        MP3T1Int = int.Parse ( dataTable.Rows [ i ].ItemArray [ 32 ].ToString () ),
                        MP3T1 = mp3T1,
                        MP3T2Int = int.Parse ( dataTable.Rows [ i ].ItemArray [ 33 ].ToString () ),
                        MP3T2 = mp3T2,
                        MP3B1Int = int.Parse ( dataTable.Rows [ i ].ItemArray [ 34 ].ToString () ),
                        MP3B1 = mp3B1,
                        MP3B2Int = int.Parse ( dataTable.Rows [ i ].ItemArray [ 35 ].ToString () ),
                        MP3B2 = mp3B2,
                        MP3SOLInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 36 ].ToString () ),
                        MP3SOL = mp3SOL,
                        MP3PIAInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 37 ].ToString () ),
                        MP3PIA = mp3PIA,
                        MuseScoreOnlineInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 38 ].ToString () ),
                        MuseScoreOnline = mscOnline,
                        ByHeartInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 39 ].ToString () ),
                        ByHeart = byHeart,
                        MusicPiece = dataTable.Rows [ i ].ItemArray [ 40 ].ToString (),
                        Notes = dataTable.Rows [ i ].ItemArray [ 41 ].ToString (),
                        NumberScoresPublisher1 = int.Parse ( dataTable.Rows [ i ].ItemArray [ 42 ].ToString () ),
                        NumberScoresPublisher2 = int.Parse ( dataTable.Rows [ i ].ItemArray [ 43 ].ToString () ),
                        NumberScoresPublisher3 = int.Parse ( dataTable.Rows [ i ].ItemArray [ 44 ].ToString () ),
                        NumberScoresPublisher4 = int.Parse ( dataTable.Rows [ i ].ItemArray [ 45 ].ToString () ),
                        NumberScoresTotal = total,
                        Publisher1Id = int.Parse ( dataTable.Rows [ i ].ItemArray [ 46 ].ToString () ),
                        Publisher1Name = dataTable.Rows [ i ].ItemArray [ 47 ].ToString (),
                        Publisher2Id = int.Parse ( dataTable.Rows [ i ].ItemArray [ 48 ].ToString () ),
                        Publisher2Name = dataTable.Rows [ i ].ItemArray [ 49 ].ToString (),
                        Publisher3Id = int.Parse ( dataTable.Rows [ i ].ItemArray [ 50 ].ToString () ),
                        Publisher3Name = dataTable.Rows [ i ].ItemArray [ 51 ].ToString (),
                        Publisher4Id = int.Parse ( dataTable.Rows [ i ].ItemArray [ 52 ].ToString () ),
                        Publisher4Name = dataTable.Rows [ i ].ItemArray [ 53 ].ToString ()
                    } );
                }
            }
        }
        return Scores;
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
                    Score = dataTable.Rows [ i ].ItemArray [ 1 ].ToString (),
                    ScoreNumber = dataTable.Rows [ i ].ItemArray [ 2 ].ToString (),
                    ArchiveId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 9 ].ToString () ),
                    ArchiveName = dataTable.Rows [ i ].ItemArray [ 10 ].ToString (),
                    RepertoireId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 11 ].ToString () ),
                    RepertoireName = dataTable.Rows [ i ].ItemArray [ 12 ].ToString (),
                    LanguageId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 13 ].ToString () ),
                    LanguageName = dataTable.Rows [ i ].ItemArray [ 14 ].ToString (),
                    GenreId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 15 ].ToString () ),
                    GenreName = dataTable.Rows [ i ].ItemArray [ 16 ].ToString (),
                    AccompanimentId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 21 ].ToString () ),
                    AccompanimentName = dataTable.Rows [ i ].ItemArray [ 22 ].ToString (),
                    Publisher1Id = int.Parse ( dataTable.Rows [ i ].ItemArray [ 46 ].ToString () ),
                    Publisher1Name = dataTable.Rows [ i ].ItemArray [ 47 ].ToString (),
                    Publisher2Id = int.Parse ( dataTable.Rows [ i ].ItemArray [ 48 ].ToString () ),
                    Publisher2Name = dataTable.Rows [ i ].ItemArray [ 49 ].ToString (),
                    Publisher3Id = int.Parse ( dataTable.Rows [ i ].ItemArray [ 50 ].ToString () ),
                    Publisher3Name = dataTable.Rows [ i ].ItemArray [ 51 ].ToString (),
                    Publisher4Id = int.Parse ( dataTable.Rows [ i ].ItemArray [ 52 ].ToString () ),
                    Publisher4Name = dataTable.Rows [ i ].ItemArray [ 53 ].ToString ()
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
                    RepertoireName = dataTable.Rows [ i ].ItemArray [ 1 ].ToString ()
                } );
            }
        }
        return Repertoires;
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

    #region Execute Non Query ScoresTable
    static void ExecuteNonQueryScoresTable ( string sqlQuery, ObservableCollection<SaveScoreModel> scoreList )
    {
        using MySqlConnection con = new(DBConnect.ConnectionString);
        con.Open ();

        using MySqlCommand cmd = new(sqlQuery, con);

        if ( scoreList [ 0 ].RepertoireId != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameRepertoireId, MySqlDbType.Int32 ).Value = scoreList [ 0 ].RepertoireId; }
        if ( scoreList [ 0 ].ArchiveId != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameArchiveId, MySqlDbType.Int32 ).Value = scoreList [ 0 ].ArchiveId; }
        if ( scoreList [ 0 ].ByHeart != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameByHeart, MySqlDbType.Int32 ).Value = scoreList [ 0 ].ByHeart; }

        if ( scoreList [ 0 ].TitleChanged != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameTitle, MySqlDbType.VarChar ).Value = scoreList [ 0 ].Title; }
        if ( scoreList [ 0 ].SubTitleChanged != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameSubTitle, MySqlDbType.VarChar ).Value = scoreList [ 0 ].SubTitle; }

        if ( scoreList [ 0 ].ComposerChanged != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameComposer, MySqlDbType.VarChar ).Value = scoreList [ 0 ].Composer; }
        if ( scoreList [ 0 ].TextwriterChanged != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameTextwriter, MySqlDbType.VarChar ).Value = scoreList [ 0 ].TextWriter; }
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
        if ( scoreList [ 0 ].MP3B2 != -1 ) { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3B1, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3B2; }
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
}
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604
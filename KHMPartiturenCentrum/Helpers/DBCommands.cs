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
    public static ObservableCollection<ScoreModel> GetAvailableScores (  )
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
    public static ObservableCollection<ScoreModel> GetAvailableChristmasScores (  )
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
                    } ) ;
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
                    AccompanimentId = int.Parse(dataTable.Rows [ i ].ItemArray [ 0 ].ToString ()),
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
    public static void SaveScore (ScoreModel score)
    {
        string sqlText = DBNames.SqlUpdate + DBNames.ScoresTable + DBNames.SqlSet +
            DBNames.ScoresFieldNameRepertoireId + " = @" + DBNames.ScoresFieldNameRepertoireId + ", " +
            DBNames.ScoresFieldNameArchiveId + " = @" + DBNames.ScoresFieldNameArchiveId + ", " +
            DBNames.ScoresFieldNameByHeart + " = @" + DBNames.ScoresFieldNameByHeart + ", " +

            DBNames.ScoresFieldNameTitle + " = @" + DBNames.ScoresFieldNameTitle + ", " +
            DBNames.ScoresFieldNameSubTitle + " = @" + DBNames.ScoresFieldNameSubTitle + ", " +

            DBNames.ScoresFieldNameComposer + " = @" + DBNames.ScoresFieldNameComposer + ", " +
            DBNames.ScoresFieldNameTextwriter + " = @" + DBNames.ScoresFieldNameTextwriter + ", " +
            DBNames.ScoresFieldNameArranger + " = @" + DBNames.ScoresFieldNameArranger + ", " +

            DBNames.ScoresFieldNameGenreId + " = @" + DBNames.ScoresFieldNameGenreId + ", " +
            DBNames.ScoresFieldNameAccompanimentId + " = @" + DBNames.ScoresFieldNameAccompanimentId + ", " +
            DBNames.ScoresFieldNameLanguageId + " = @" + DBNames.ScoresFieldNameLanguageId + ", " +

            DBNames.ScoresFieldNameMusicPiece + " = @" + DBNames.ScoresFieldNameMusicPiece + ", " +

            DBNames.ScoresFieldNameDigitized + " = @" + DBNames.ScoresFieldNameDigitized + ", " +
            DBNames.ScoresFieldNameModified + " = @" + DBNames.ScoresFieldNameModified + ", " +
            DBNames.ScoresFieldNameChecked + " = @" + DBNames.ScoresFieldNameChecked + ", " +

            DBNames.ScoresFieldNameMuseScoreORP + " = @" + DBNames.ScoresFieldNameMuseScoreORP + ", " +
            DBNames.ScoresFieldNameMuseScoreORK + " = @" + DBNames.ScoresFieldNameMuseScoreORK + ", " +
            DBNames.ScoresFieldNameMuseScoreTOP + " = @" + DBNames.ScoresFieldNameMuseScoreTOP + ", " +
            DBNames.ScoresFieldNameMuseScoreTOK + " = @" + DBNames.ScoresFieldNameMuseScoreTOK + ", " +

            DBNames.ScoresFieldNamePDFORP + " = @" + DBNames.ScoresFieldNamePDFORP + ", " +
            DBNames.ScoresFieldNamePDFORK + " = @" + DBNames.ScoresFieldNamePDFORK + ", " +
            DBNames.ScoresFieldNamePDFTOP + " = @" + DBNames.ScoresFieldNamePDFTOP + ", " +
            DBNames.ScoresFieldNamePDFTOK + " = @" + DBNames.ScoresFieldNamePDFTOK + ", " +

            DBNames.ScoresFieldNameMP3B1 + " = @" + DBNames.ScoresFieldNameMP3B1 + ", " +
            DBNames.ScoresFieldNameMP3B2 + " = @" + DBNames.ScoresFieldNameMP3B2 + ", " +
            DBNames.ScoresFieldNameMP3T1 + " = @" + DBNames.ScoresFieldNameMP3T1 + ", " +
            DBNames.ScoresFieldNameMP3T2 + " = @" + DBNames.ScoresFieldNameMP3T2 + ", " +

            DBNames.ScoresFieldNameMP3SOL + " = @" + DBNames.ScoresFieldNameMP3SOL + ", " +
            DBNames.ScoresFieldNameMP3TOT + " = @" + DBNames.ScoresFieldNameMP3TOT + ", " +
            DBNames.ScoresFieldNameMP3PIA + " = @" + DBNames.ScoresFieldNameMP3PIA + ", " +

            DBNames.ScoresFieldNameOnline + " = @" + DBNames.ScoresFieldNameOnline + ", " +

            DBNames.ScoresFieldNameLyrics + " = @" + DBNames.ScoresFieldNameLyrics + ", " +

            DBNames.ScoresFieldNameNotes + " = @" + DBNames.ScoresFieldNameNotes + ", " +

            DBNames.ScoresFieldNameAmountPublisher1 + " = @" + DBNames.ScoresFieldNameAmountPublisher1 + ", " +
            DBNames.ScoresFieldNameAmountPublisher2 + " = @" + DBNames.ScoresFieldNameAmountPublisher2 + ", " +
            DBNames.ScoresFieldNameAmountPublisher3 + " = @" + DBNames.ScoresFieldNameAmountPublisher3 + ", " +
            DBNames.ScoresFieldNameAmountPublisher4 + " = @" + DBNames.ScoresFieldNameAmountPublisher4 + ", " +

            DBNames.ScoresFieldNamePublisher1Id + " = @" + DBNames.ScoresFieldNamePublisher1Id + ", " +
            DBNames.ScoresFieldNamePublisher2Id + " = @" + DBNames.ScoresFieldNamePublisher2Id + ", " +
            DBNames.ScoresFieldNamePublisher3Id + " = @" + DBNames.ScoresFieldNamePublisher3Id + ", " +
            DBNames.ScoresFieldNamePublisher4Id + " = @" + DBNames.ScoresFieldNamePublisher4Id + ", " +
            DBNames.SqlWhere + DBNames.ScoresFieldNameId + " = @" + DBNames.ScoresFieldNameId + ";";

        try
        {
            ExecuteNonQueryScoresTable(sqlText, score);
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine("Fout (UpdateScoresTable - MySqlException): " + ex.Message);
            throw ex;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Fout (UpdateScoresTable): " + ex.Message);
            throw;
        }
    }
    #endregion

    #region Execute Non Query ScoresTable
    static void ExecuteNonQueryScoresTable(string sqlText, ScoreModel score)
    {
        int byHeart = 0, checkedScore = 0, musescoreORP = 0, musescoreORK = 0, musescoreTOP = 0, musescoreTOK = 0, musescoreOnline = 0;
        int pdfORP = 0, pdfORK = 0, pdfTOP = 0, pdfTOK = 0;
        int mp3B1 = 0, mp3B2 = 0, mp3T1 = 0, mp3T2 = 0, mp3SOL = 0, mp3TOT = 0, mp3PIA = 0;

        using MySqlConnection con = new(DBConnect.ConnectionString);
        con.Open();

        using MySqlCommand cmd = new(sqlText, con);

        // Convert booleans to int
        if ( score.ByHeart == true ) { byHeart = 1; } else { byHeart = 0; }
        if ( score.Check == true ) { checkedScore = 1; } else { checkedScore = 0; }
        if ( score.MuseScoreORP == true ) { musescoreORP = 1; } else { musescoreORP = 0; }
        if ( score.MuseScoreORK == true ) { musescoreORK = 1; } else { musescoreORK = 0; }
        if ( score.MuseScoreTOP == true ) { musescoreTOP = 1; } else { musescoreTOP = 0; }
        if ( score.MuseScoreTOK == true ) { musescoreTOK = 1; } else { musescoreTOK = 0; }

        if (score.PDFORP == true) { pdfORP = 1; } else { pdfORP = 0; }
        if (score.PDFORK == true) { pdfORK = 1; } else { pdfORK = 0; }
        if (score.PDFTOP == true) { pdfTOP = 1; } else { pdfTOP = 0; }
        if (score.PDFTOK == true) { pdfTOK = 1; } else { pdfTOK = 0; }

        if (score.MP3B1 == true) { mp3B1 = 1; } else { mp3B1 = 0; }
        if (score.MP3B2 == true) { mp3B2 = 1; } else { mp3B2 = 0; }
        if (score.MP3T1 == true) { mp3T1 = 1; } else { mp3T1 = 0; }
        if (score.MP3T2 == true) { mp3T2 = 1; } else { mp3T2 = 0; }

        if (score.MP3SOL == true) { mp3SOL = 1; } else { mp3SOL = 0; }
        if (score.MP3TOT == true) { mp3TOT = 1; } else { mp3TOT = 0; }
        if (score.MP3PIA == true) { mp3PIA = 1; } else { mp3PIA = 0; }

        if (score.MuseScoreOnline == true) { musescoreOnline = 1; } else { musescoreOnline = 0; }

        //add parameters setting string values to DBNull.Value
        // If it is a new record, Id will be 0, this parameter does not has te be set
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameRepertoireId, MySqlDbType.Int32).Value = score.RepertoireId;
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameArchiveId, MySqlDbType.Int32).Value = score.ArchiveId;
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameByHeart, MySqlDbType.Int32).Value = byHeart;
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameTitle, MySqlDbType.VarChar).Value = DBNull.Value;
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameSubTitle, MySqlDbType.VarChar).Value = DBNull.Value;
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameComposer, MySqlDbType.VarChar).Value = DBNull.Value;
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameTextwriter, MySqlDbType.VarChar).Value = DBNull.Value;
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameArranger, MySqlDbType.VarChar).Value = DBNull.Value;
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameGenreId, MySqlDbType.Int32).Value = score.GenreId;
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameAccompanimentId, MySqlDbType.Int32).Value = score.AccompanimentId;
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameLanguageId, MySqlDbType.Int32).Value = score.LanguageId;
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameMusicPiece, MySqlDbType.VarChar).Value = DBNull.Value;
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameDigitized, MySqlDbType.String).Value = DBNull.Value;
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameModified, MySqlDbType.String).Value = DBNull.Value;
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameChecked, MySqlDbType.Int32).Value = checkedScore;

        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameMuseScoreORP, MySqlDbType.Int32).Value = musescoreORP;
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameMuseScoreORK, MySqlDbType.Int32).Value = musescoreORK;
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameMuseScoreTOP, MySqlDbType.Int32).Value = musescoreTOP;
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameMuseScoreTOK, MySqlDbType.Int32).Value = musescoreTOK;

        cmd.Parameters.Add("@" + DBNames.ScoresFieldNamePDFORP, MySqlDbType.Int32).Value = pdfORP;
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNamePDFORK, MySqlDbType.Int32).Value = pdfORK;
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNamePDFTOP, MySqlDbType.Int32).Value = pdfTOP;
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNamePDFTOK, MySqlDbType.Int32).Value = pdfTOK;

        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameMP3B1, MySqlDbType.Int32).Value = mp3B1;
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameMP3B1, MySqlDbType.Int32).Value = mp3B2;
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameMP3T1, MySqlDbType.Int32).Value = mp3T1;
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameMP3T2, MySqlDbType.Int32).Value = mp3T2;

        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameMP3SOL, MySqlDbType.Int32).Value = mp3SOL;
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameMP3TOT, MySqlDbType.Int32).Value = mp3TOT;
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameMP3PIA, MySqlDbType.Int32).Value = mp3PIA;

        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameOnline, MySqlDbType.Int32).Value = musescoreOnline;
        
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameLyrics, MySqlDbType.MediumText).Value = DBNull.Value;
        cmd.Parameters.Add("@" + DBNames.ScoresFieldNameNotes, MySqlDbType.MediumText).Value = DBNull.Value;

        //set varchar/text values
        if (!String.IsNullOrEmpty(score.ScoreTitle))
        {
            cmd.Parameters["@" + DBNames.ScoresFieldNameTitle].Value = score.ScoreTitle;
        }

        if (!String.IsNullOrEmpty(score.ScoreSubTitle))
        {
            cmd.Parameters["@" + DBNames.ScoresFieldNameSubTitle].Value = score.ScoreSubTitle;
        }

        if (!String.IsNullOrEmpty(score.Lyrics))
        {
            cmd.Parameters["@" + DBNames.ScoresFieldNameLyrics].Value = score.Lyrics;
        }

        if (!String.IsNullOrEmpty(score.Notes))
        {
            cmd.Parameters["@" + DBNames.ScoresFieldNameNotes].Value = score.Notes;
        }


        //execute; returns the number of rows affected
        int rowsAffected = cmd.ExecuteNonQuery();
    }
#endregion

}
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604
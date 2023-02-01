using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Principal;
using System.Windows.Documents;
using System.Xml;
using Google.Protobuf.WellKnownTypes;
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
        string selectQuery = DBNames.SqlSelectAll + DBNames.SqlFrom + DBNames.Database + "." + _table + DBNames.SqlOrder + DBNames.EpicsFieldNameEpicName;
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
                    Id = int.Parse(dataTable.Rows [ i ].ItemArray [ 0 ].ToString ()),
                    AccompanimentName = dataTable.Rows [ i ].ItemArray [ 1 ].ToString ()
                } );
            }
        }
        return Accompaniments;
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
                    Id = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString () ),
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
                    Id = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString () ),
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

        DataTable dataTable = DBCommands.GetData(DBNames.PublishersTable, "NoSort");
        if ( dataTable.Rows.Count > 0 )
        {
            for ( int i = 0; i < dataTable.Rows.Count; i++ )
            {
                Publishers.Add ( new PublisherModel
                {
                    Id = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString () ),
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
                    Id = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString () ),
                    RepertoireName = dataTable.Rows [ i ].ItemArray [ 1 ].ToString ()
                } );
            }
        }
        return Repertoires;
    }
    #endregion

    #region GetScores
    public static ObservableCollection<ScoreModel> GetScores ()
    {
        ObservableCollection<ScoreModel> Scores = new();

        DataTable dataTable = DBCommands.GetData(DBNames.ScoresTable, "NoSort");
        if ( dataTable.Rows.Count > 0 )
        {
            for ( int i = 0; i < dataTable.Rows.Count; i++ )
            {
                Scores.Add ( new ScoreModel
                {
                    Id = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString () ),
                    ScoreTitle = dataTable.Rows [ i ].ItemArray [ 1 ].ToString ()
                } );
            }
        }
        return Scores;
    }
    #endregion


}
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604
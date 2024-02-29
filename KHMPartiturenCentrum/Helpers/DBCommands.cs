using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using System.Windows;

using KHM.Converters;
using KHM.Models;
using MySql.Data.MySqlClient;
using static KHM.App;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604
#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast

namespace KHM.Helpers;

public class DBCommands
{
	#region GetData
	#region GetData with Sorting option
	public static DataTable GetData ( string _table, string orderByFieldName )
	{
		string selectQuery = "";
		if ( orderByFieldName.ToLower ( ) == "nosort" )
		{
			selectQuery = DBNames.SqlSelectAll + DBNames.SqlFrom + DBNames.Database + "." + _table;
		}
		else
		{
			selectQuery = DBNames.SqlSelectAll + DBNames.SqlFrom + DBNames.Database + "." + _table + DBNames.SqlOrder + orderByFieldName;
		}

		MySqlConnection connection = new(DBConnect.ConnectionString);
		connection.Open ( );
		DataTable table = new();
		MySqlDataAdapter adapter = new(selectQuery, connection);
		adapter.Fill ( table );
		connection.Close ( );
		return table;
	}
	#endregion

	#region GetData Sorted and filtered
	public static DataTable GetData ( string _table, string orderByFieldName, string WhereFieldName, string WhereFieldValue )
	{
		string selectQuery = "";
		if ( orderByFieldName.ToLower ( ) == "nosort" )
		{
			selectQuery = DBNames.SqlSelectAll + DBNames.SqlFrom + DBNames.Database + "." + _table + DBNames.SqlWhere + WhereFieldName + " = '" + WhereFieldValue + "';";
		}
		else
		{
			selectQuery = DBNames.SqlSelectAll + DBNames.SqlFrom + DBNames.Database + "." + _table + DBNames.SqlWhere + WhereFieldName + " = '" + WhereFieldValue + "'" + DBNames.SqlOrder + orderByFieldName + ";";
		}

		MySqlConnection connection = new(DBConnect.ConnectionString);
		connection.Open ( );
		DataTable table = new();
		MySqlDataAdapter adapter = new(selectQuery, connection);
		adapter.Fill ( table );
		connection.Close ( );
		return table;
	}


	public static DataTable GetData ( string _table, string orderByFieldName, string WhereFieldName, string WhereFieldValue, string AndWhereFieldName, string AndWhereFieldValue )
	{
		string selectQuery = "";
		if ( orderByFieldName.ToLower ( ) == "nosort" )
		{
			selectQuery = DBNames.SqlSelectAll + DBNames.SqlFrom + DBNames.Database + "." + _table + DBNames.SqlWhere + WhereFieldName + " = '" + WhereFieldValue + "'" + DBNames.SqlAnd + AndWhereFieldName + " = '" + AndWhereFieldValue + "';";
		}
		else
		{
			selectQuery = DBNames.SqlSelectAll + DBNames.SqlFrom + DBNames.Database + "." + _table + DBNames.SqlWhere + WhereFieldName + " = '" + WhereFieldValue + "'" + DBNames.SqlAnd + AndWhereFieldName + " = '" + AndWhereFieldValue + "'" + DBNames.SqlOrder + orderByFieldName + ";";
		}

		MySqlConnection connection = new(DBConnect.ConnectionString);
		connection.Open ( );
		DataTable table = new();
		MySqlDataAdapter adapter = new(selectQuery, connection);
		adapter.Fill ( table );
		connection.Close ( );
		return table;
	}
	#endregion
	#endregion

	#region Get Available Scores
	public static ObservableCollection<ScoreModel> GetAvailableScores ( )
	{
		ObservableCollection<ScoreModel> Scores = new();

		DataTable dataTable = DBCommands.GetData(DBNames.AvailableScoresView, DBNames.AvailableScoresFieldNameNumber);

		if ( dataTable.Rows.Count > 0 )
		{
			for ( int i = 0 ; i < dataTable.Rows.Count ; i++ )
			{
				Scores.Add ( new ScoreModel
				{
					ScoreId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString ( ) ),
					ScoreNumber = dataTable.Rows [ i ].ItemArray [ 1 ].ToString ( )
				} );
			}
		}

		return Scores;
	}
	#endregion

	#region Get Scores
	public static ObservableCollection<ScoreModel> GetScores ( string _table, string _orderByFieldName, string _whereFieldName, string _whereFieldValue )
	{
		ObservableCollection<ScoreModel> Scores = new();
		DataTable dataTable = new();

		if ( _whereFieldName != null )
		{
			dataTable = GetData ( _table, _orderByFieldName, _whereFieldName, _whereFieldValue );
		}
		else
		{
			dataTable = GetData ( _table, _orderByFieldName );
		}


		 if ( dataTable.Rows.Count > 0 )
		{
			for ( int i = 0 ; i < dataTable.Rows.Count ; i++ )
			{
				// Set the bools
				bool check = false, byHeart = false;
				bool pdfORP = false, pdfORK = false, pdfTOP = false, pdfTOK = false, pdfPIA = false;
				bool mscORP = false, mscORK = false, mscTOP = false, mscTOK = false, mscOnline = false;
				bool mp3B1 = false, mp3B2 = false, mp3T1 = false, mp3T2 = false, mp3SOL1 = false, mp3SOL2 = false, mp3TOT = false, mp3PIA = false, mp3UITV = false;
				bool mp3B1Voice = false, mp3B2Voice = false, mp3T1Voice = false, mp3T2Voice = false, mp3SOL1Voice = false, mp3SOL2Voice = false, mp3TOTVoice = false, mp3UITVVoice = false;

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 18 ].ToString ( ) ) == 0 )
				{ check = false; }
				else
				{ check = true; }

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 23 ].ToString ( ) ) == 0 )
				{ pdfORP = false; }
				else
				{ pdfORP = true; }

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 24 ].ToString ( ) ) == 0 )
				{ pdfORK = false; }
				else
				{ pdfORK = true; }

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 25 ].ToString ( ) ) == 0 )
				{ pdfTOP = false; }
				else
				{ pdfTOP = true; }

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 26 ].ToString ( ) ) == 0 )
				{ pdfTOK = false; }
				else
				{ pdfTOK = true; }

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 27 ].ToString ( ) ) == 0 )
				{ pdfPIA = false; }
				else
				{ pdfPIA = true; }

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 28 ].ToString ( ) ) == 0 )
				{ mscORP = false; }
				else
				{ mscORP = true; }

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 29 ].ToString ( ) ) == 0 )
				{ mscORK = false; }
				else
				{ mscORK = true; }

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 30 ].ToString ( ) ) == 0 )
				{ mscTOP = false; }
				else
				{ mscTOP = true; }

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 31 ].ToString ( ) ) == 0 )
				{ mscTOK = false; }
				else
				{ mscTOK = true; }

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 32 ].ToString ( ) ) == 0 )
				{ mp3T1 = false; }
				else
				{ mp3T1 = true; }

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 33 ].ToString ( ) ) == 0 )
				{ mp3T2 = false; }
				else
				{ mp3T2 = true; }

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 34 ].ToString ( ) ) == 0 )
				{ mp3B1 = false; }
				else
				{ mp3B1 = true; }

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 35 ].ToString ( ) ) == 0 )
				{ mp3B2 = false; }
				else
				{ mp3B2 = true; }

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 36 ].ToString ( ) ) == 0 )
				{ mp3SOL1 = false; }
				else
				{ mp3SOL1 = true; }

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 37 ].ToString () ) == 0 )
				{ mp3SOL2 = false; }
				else
				{ mp3SOL2 = true; }

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 38 ].ToString () ) == 0 )
					{ mp3TOT = false; }
				else
					{ mp3TOT = true; }

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 39 ].ToString ( ) ) == 0 )
				{ mp3PIA = false; }
				else
				{ mp3PIA = true; }

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 40 ].ToString () ) == 0 )
				{ mp3T1Voice = false; }
				else
				{ mp3T1Voice = true; }

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 41 ].ToString () ) == 0 )
				{ mp3T2Voice = false; }
				else
				{ mp3T2Voice = true; }

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 42 ].ToString () ) == 0 )
				{ mp3B1Voice = false; }
				else
				{ mp3B1Voice = true; }

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 43 ].ToString () ) == 0 )
				{ mp3B2Voice = false; }
				else
				{ mp3B2Voice = true; }

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 44 ].ToString () ) == 0 )
				{ mp3SOL1Voice = false; }
				else
				{ mp3SOL1Voice = true; }

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 45 ].ToString () ) == 0 )
				{ mp3SOL2Voice = false; }
				else
				{ mp3SOL2Voice = true; }

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 46 ].ToString () ) == 0 )
					{ mp3TOTVoice = false; }
				else
					{ mp3TOTVoice = true; }

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 47 ].ToString ( ) ) == 0 )
				{ mp3UITVVoice = false; }
				else
				{ mp3UITVVoice = true; }

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 48 ].ToString ( ) ) == 0 )
				{ mscOnline = false; }
				else
				{ mscOnline = true; }

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 49 ].ToString ( ) ) == 0 )
				{ byHeart = false; }
				else
				{ byHeart = true; }

				// Set total
				var total = 0;

				total = int.Parse ( dataTable.Rows [ i ].ItemArray [ 52 ].ToString ( ) ) +
						int.Parse ( dataTable.Rows [ i ].ItemArray [ 53 ].ToString ( ) ) +
						int.Parse ( dataTable.Rows [ i ].ItemArray [ 54 ].ToString ( ) ) +
						int.Parse ( dataTable.Rows [ i ].ItemArray [ 55 ].ToString ( ) );

				// Set the datestrings
				string dateCreated = "";
				if ( dataTable.Rows [ i ].ItemArray [ 19 ].ToString ( ) != "" )
				{
					string[] _tempCreated = dataTable.Rows[i].ItemArray[19].ToString().Split(" ");
					dateCreated = _tempCreated [ 0 ];
				}

				string dateModified = "";
				if ( dataTable.Rows [ i ].ItemArray [ 20 ].ToString ( ) != "" )
				{
					string[] _tempModified = dataTable.Rows[i].ItemArray[20].ToString().Split(" ");
					dateModified = _tempModified [ 0 ];
				}

				//var _duration="";
				int _minutes=0, _seconds = 0, _duration = 0;

				if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 56 ].ToString ( ) ) != 0 )
				{
					_minutes = int.Parse ( dataTable.Rows [ i ].ItemArray [ 64 ].ToString ( ) ) / 60;
					_seconds = int.Parse ( dataTable.Rows [ i ].ItemArray [ 64 ].ToString ( ) ) % 60;
					//_duration = $"{_minutes}:{_seconds.ToString ( "00" )}";
					_duration = int.Parse ( dataTable.Rows [ i ].ItemArray [ 64 ].ToString ( ) );
				}

				// When Title is empty don't add that row to the list
				if ( dataTable.Rows [ i ].ItemArray [ 4 ].ToString ( ) != string.Empty )
				{
					Scores.Add ( new ScoreModel
					{
						ScoreId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString ( ) ),
						Score = dataTable.Rows [ i ].ItemArray [ 1 ].ToString ( ),
						ScoreNumber = dataTable.Rows [ i ].ItemArray [ 2 ].ToString ( ),
						ScoreSubNumber = dataTable.Rows [ i ].ItemArray [ 3 ].ToString ( ),
						ScoreTitle = dataTable.Rows [ i ].ItemArray [ 4 ].ToString ( ),
						ScoreSubTitle = dataTable.Rows [ i ].ItemArray [ 5 ].ToString ( ),
						Composer = dataTable.Rows [ i ].ItemArray [ 6 ].ToString ( ),
						Textwriter = dataTable.Rows [ i ].ItemArray [ 7 ].ToString ( ),
						Arranger = dataTable.Rows [ i ].ItemArray [ 8 ].ToString ( ),
						ArchiveId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 9 ].ToString ( ) ),
						ArchiveName = dataTable.Rows [ i ].ItemArray [ 10 ].ToString ( ),
						RepertoireId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 11 ].ToString ( ) ),
						RepertoireName = dataTable.Rows [ i ].ItemArray [ 12 ].ToString ( ),
						LanguageId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 13 ].ToString ( ) ),
						LanguageName = dataTable.Rows [ i ].ItemArray [ 14 ].ToString ( ),
						GenreId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 15 ].ToString ( ) ),
						GenreName = dataTable.Rows [ i ].ItemArray [ 16 ].ToString ( ),
						Lyrics = dataTable.Rows [ i ].ItemArray [ 17 ].ToString ( ),
						CheckInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 18 ].ToString ( ) ),
						DateCreatedString = dateCreated,
						DateModifiedString = dateModified,
						Checked = check,
						AccompanimentId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 21 ].ToString ( ) ),
						AccompanimentName = dataTable.Rows [ i ].ItemArray [ 22 ].ToString ( ),
						PDFORPInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 23 ].ToString ( ) ),
						PDFORP = pdfORP,
						PDFORKInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 24 ].ToString ( ) ),
						PDFORK = pdfORK,
						PDFTOPInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 25 ].ToString ( ) ),
						PDFTOP = pdfTOP,
						PDFTOKInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 26 ].ToString ( ) ),
						PDFTOK = pdfTOK,
						PDFPIAInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 28 ].ToString ( ) ),
						PDFPIA = pdfPIA,

						MuseScoreORPInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 28 ].ToString ( ) ),
						MuseScoreORP = mscORP,
						MuseScoreORKInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 29 ].ToString ( ) ),
						MuseScoreORK = mscORK,
						MuseScoreTOPInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 30 ].ToString ( ) ),
						MuseScoreTOP = mscTOP,
						MuseScoreTOKInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 31 ].ToString ( ) ),
						MuseScoreTOK = mscTOK,
						MP3T1Int = int.Parse ( dataTable.Rows [ i ].ItemArray [ 32 ].ToString ( ) ),
						MP3T1 = mp3T1,
						MP3T2Int = int.Parse ( dataTable.Rows [ i ].ItemArray [ 33 ].ToString ( ) ),
						MP3T2 = mp3T2,
						MP3B1Int = int.Parse ( dataTable.Rows [ i ].ItemArray [ 34 ].ToString ( ) ),
						MP3B1 = mp3B1,
						MP3B2Int = int.Parse ( dataTable.Rows [ i ].ItemArray [ 35 ].ToString ( ) ),
						MP3B2 = mp3B2,
						MP3SOL1Int = int.Parse ( dataTable.Rows [ i ].ItemArray [ 36 ].ToString () ),
						MP3SOL1 = mp3SOL1,
						MP3SOL2Int = int.Parse ( dataTable.Rows [ i ].ItemArray [ 37 ].ToString ( ) ),
						MP3SOL2 = mp3SOL2,
						MP3TOTInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 38 ].ToString () ),
						MP3TOT = mp3TOT,
						MP3PIAInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 39 ].ToString ( ) ),
						MP3PIA = mp3PIA,
						MP3T1VoiceInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 40 ].ToString () ),
						MP3T1Voice = mp3T1Voice,
						MP3T2VoiceInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 41 ].ToString () ),
						MP3T2Voice = mp3T2Voice,
						MP3B1VoiceInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 42 ].ToString () ),
						MP3B1Voice = mp3B1Voice,
						MP3B2VoiceInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 43 ].ToString () ),
						MP3B2Voice = mp3B2Voice,
						MP3SOL1VoiceInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 44 ].ToString () ),
						MP3SOL1Voice = mp3SOL1Voice,
						MP3SOL2VoiceInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 45 ].ToString () ),
						MP3SOL2Voice = mp3SOL2Voice,
						MP3TOTVoiceInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 46 ].ToString () ),
						MP3TOTVoice = mp3TOTVoice,
						MP3UITVInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 47 ].ToString ( ) ),
						MP3UITV = mp3UITV,
						MuseScoreOnlineInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 48 ].ToString ( ) ),
						MuseScoreOnline = mscOnline,
						ByHeartInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 49 ].ToString ( ) ),
						ByHeart = byHeart,
						MusicPiece = dataTable.Rows [ i ].ItemArray [ 50 ].ToString ( ),
						Notes = dataTable.Rows [ i ].ItemArray [ 51 ].ToString ( ),
						AmountPublisher1 = int.Parse ( dataTable.Rows [ i ].ItemArray [ 52 ].ToString ( ) ),
						AmountPublisher2 = int.Parse ( dataTable.Rows [ i ].ItemArray [ 53 ].ToString ( ) ),
						AmountPublisher3 = int.Parse ( dataTable.Rows [ i ].ItemArray [ 54 ].ToString ( ) ),
						AmountPublisher4 = int.Parse ( dataTable.Rows [ i ].ItemArray [ 55 ].ToString ( ) ),
						AmountTotal = total,
						Publisher1Id = int.Parse ( dataTable.Rows [ i ].ItemArray [ 56 ].ToString ( ) ),
						Publisher1Name = dataTable.Rows [ i ].ItemArray [ 57 ].ToString ( ),
						Publisher2Id = int.Parse ( dataTable.Rows [ i ].ItemArray [ 58 ].ToString ( ) ),
						Publisher2Name = dataTable.Rows [ i ].ItemArray [ 59 ].ToString ( ),
						Publisher3Id = int.Parse ( dataTable.Rows [ i ].ItemArray [ 60 ].ToString ( ) ),
						Publisher3Name = dataTable.Rows [ i ].ItemArray [ 61 ].ToString ( ),
						Publisher4Id = int.Parse ( dataTable.Rows [ i ].ItemArray [ 62 ].ToString ( ) ),
						Publisher4Name = dataTable.Rows [ i ].ItemArray [ 63 ].ToString ( ),
						Duration = _duration,
						DurationMinutes = _minutes,
						DurationSeconds = _seconds,
						SearchField = $"{dataTable.Rows [ i ].ItemArray [ 2 ].ToString ( )} {dataTable.Rows [ i ].ItemArray [ 4 ].ToString ( )} {dataTable.Rows [ i ].ItemArray [ 5 ].ToString ( )} {dataTable.Rows [ i ].ItemArray [ 6 ].ToString ( )} {dataTable.Rows [ i ].ItemArray [ 7 ].ToString ( )} {dataTable.Rows [ i ].ItemArray [ 8 ].ToString ( )} {dataTable.Rows [ i ].ItemArray [ 12 ].ToString ( )}"
					} );
					;
				}
			}
		}
		return Scores;
	}
	#endregion

	#region Delete Score
	public static void DeleteScore ( string ScoreNumber, string ScoreSubNumber )
	{
		// Check if the selected Score is used in a set of Scores
		var sqlQuery = DBNames.SqlSelect + DBNames.SqlCountAll + DBNames.SqlFrom + DBNames.Database + "." + DBNames.ScoresTable + DBNames.SqlWhere + DBNames.ScoresFieldNameScoreNumber + " = '" + ScoreNumber + "';";

		using MySqlConnection connection = new(DBConnect.ConnectionString);
		connection.Open ( );

		using MySqlCommand cmd = new(sqlQuery, connection);


		var NumberOfScores = int.Parse(cmd.ExecuteScalar().ToString());


		//var NumberOfScores = CheckForSubScores(DBNames.ScoresTable, ScoreNumber);

		if ( NumberOfScores == 1 || ScoreSubNumber == "" )
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
					var test = ScoreUsers.SelectedUserId;
					DBCommands.WriteLog ( ScoreUsers.SelectedUserId, DBNames.LogScoreRenumbered, $"Partituur: {ScoreNumber}" );

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
	public static void DeleteUser ( string _userId )
	{
		var sqlQuery = DBNames.SqlDelete + DBNames.Database + "." + DBNames.UsersTable + DBNames.SqlWhere + DBNames.UsersFieldNameId + " = " + _userId + ";";

		using MySqlConnection connection = new(DBConnect.ConnectionString);
		connection.Open ( );

		using MySqlCommand cmd = new(sqlQuery, connection);

		int rowsAffected = cmd.ExecuteNonQuery();
	}
	#endregion

	#region Check for SubScores
	public static long CheckForSubScores ( string _scoreNumber )
	{
		// Check how many Scores there are in the table with the given scorenumber, If there are more then 1 there is a set with SubNumbers
		var sqlQuery = DBNames.SqlSelect + DBNames.SqlCountAll + DBNames.SqlFrom + DBNames.Database + "." + DBNames.ScoresTable + DBNames.SqlWhere + DBNames.ScoresFieldNameScoreNumber + " = '" + _scoreNumber + "';";

		using MySqlConnection connection = new(DBConnect.ConnectionString);
		connection.Open ( );

		using MySqlCommand cmd = new(sqlQuery, connection);

		long numberOfScores = (long)cmd.ExecuteScalar();

		return numberOfScores;
	}
	#endregion

	#region Get Highest Subversion
	public static int getHighestSubNumber ( string _scoreNumber )
	{
		//Select MAX ( SubNummer) FROM Bibliotheek Where Partituur = '000'
		var sqlQuery = DBNames.SqlSelect + DBNames.SqlMax + DBNames.ScoresFieldNameScoreSubNumber + ") " + DBNames.SqlFrom + DBNames.Database + "." + DBNames.ScoresTable + DBNames.SqlWhere + DBNames.ScoresFieldNameScoreNumber + " = '" + _scoreNumber + "';";

		using MySqlConnection connection = new(DBConnect.ConnectionString);
		connection.Open ( );

		using MySqlCommand cmd = new(sqlQuery, connection);

		string highestSubNumberValue = (string)cmd.ExecuteScalar();
		int highestSubNumber = int.Parse(highestSubNumberValue);

		return highestSubNumber;
	}
	#endregion

	#region Execute Delete
	static void ExecuteDeleteScore ( string _table, string _scoreNumber )
	{
		// Delete Score without SubScores
		var sqlQuery = DBNames.SqlDelete + DBNames.Database + "." + _table + DBNames.SqlWhere + DBNames.ScoresFieldNameScoreNumber + " = '" + _scoreNumber + "';";

		using MySqlConnection connection = new(DBConnect.ConnectionString);
		connection.Open ( );

		using MySqlCommand cmd = new(sqlQuery, connection);

		int rowsAffected = cmd.ExecuteNonQuery();
	}

	static void ExecuteDeleteScore ( string _table, string _scoreNumber, string _scoreSubNumber )
	{
		// Delete Score with SubScoreNumber
		var sqlQuery = DBNames.SqlDelete + DBNames.Database + "." + _table + DBNames.SqlWhere +
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
		connection.Open ( );

		using MySqlCommand cmd = new(sqlQuery, connection);

		int rowsAffected = cmd.ExecuteNonQuery();
	}
	#endregion

	#region Remove SubVersion from Score
	public static void RemoveSubScore ( string _scoreNumber )
	{
		string sqlQuery = DBNames.SqlUpdate + DBNames.ScoresTable + DBNames.SqlSet + DBNames.ScoresFieldNameScoreSubNumber + " = ''" + DBNames.SqlWhere + DBNames.ScoresFieldNameScoreNumber + " = '" + _scoreNumber + "';";

		using MySqlConnection connection = new(DBConnect.ConnectionString);
		connection.Open ( );

		using MySqlCommand cmd = new(sqlQuery, connection);

		int rowsAffected = cmd.ExecuteNonQuery();
	}
	#endregion

	#region Add SubScore to Score
	public static void AddSubScore ( string _scoreNumber, string _subScoreNumber )
	{
		string sqlQuery = DBNames.SqlUpdate + DBNames.ScoresTable + DBNames.SqlSet + DBNames.ScoresFieldNameScoreSubNumber + " = '" + _subScoreNumber + "'" + DBNames.SqlWhere + DBNames.ScoresFieldNameScoreNumber + " = '" + _scoreNumber + "';";

		using MySqlConnection connection = new(DBConnect.ConnectionString);
		connection.Open ( );

		using MySqlCommand cmd = new(sqlQuery, connection);

		int rowsAffected = cmd.ExecuteNonQuery();
	}
	#endregion

	#region Re Add Score
	public static void ReAddScore ( string _scoreNumber )
	{
		var sqlQuery = DBNames.SqlInsert + DBNames.Database + "." + DBNames.ScoresTable + " ( " + DBNames.ScoresFieldNameScoreNumber + " ) " + DBNames.SqlValues + " ( '" + _scoreNumber + "' );";

		using MySqlConnection connection = new(DBConnect.ConnectionString);
		connection.Open ( );

		using MySqlCommand cmd = new(sqlQuery, connection);

		int rowsAffected = cmd.ExecuteNonQuery();
	}
	#endregion

	#region Add New Score
	public static void AddNewScore ( string _scoreNumber )
	{
		var repertoire = 1;

		if ( int.Parse ( _scoreNumber ) >= 700 && int.Parse ( _scoreNumber ) <= 899 )
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
		connection.Open ( );

		using MySqlCommand cmd = new(sqlQuery, connection);

		int rowsAffected = cmd.ExecuteNonQuery();
	}
	#endregion

	#region Add New Score as Subscore
	public static void AddNewScoreAsSubscore ( ObservableCollection<ScoreModel> _score )
	{
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
		connection.Open ( );

		using MySqlCommand cmd = new(sqlQuery, connection);

		int rowsAffected = cmd.ExecuteNonQuery();
	}
	#endregion

	#region Add New User
	public static void AddNewUser ( )
	{
		// Add a new user as Super User (UserRole 5)
		var sqlQuery = DBNames.SqlInsert + DBNames.Database + "." + DBNames.UsersTable +
			" ( " + DBNames.UsersFieldNameRoleId + " ) " + DBNames.SqlValues +
			" ( 5 ) ";

		using MySqlConnection connection = new(DBConnect.ConnectionString);
		connection.Open ( );

		using MySqlCommand cmd = new(sqlQuery, connection);

		int rowsAffected = cmd.ExecuteNonQuery();
	}
	#endregion

	#region Get Latest Added UserId
	public static int GetAddedUserId ( )
	{
		int userId = 0;
		//SELECT * FROM users ORDER BY id DESC LIMIT 1
		var sqlQuery = DBNames.SqlSelectAll +
			DBNames.SqlFrom + DBNames.Database + "." + DBNames.UsersTable +
			DBNames.SqlOrder + DBNames.UserRolesFieldNameId + DBNames.SqlDesc + DBNames.SqlLimit + "1";

		using MySqlConnection connection = new(DBConnect.ConnectionString);
		connection.Open ( );

		using MySqlCommand cmd = new(sqlQuery, connection);

		userId = ( int ) cmd.ExecuteScalar ( );

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
			for ( int i = 0 ; i < dataTable.Rows.Count ; i++ )
			{
				Scores.Add ( new ScoreModel
				{
					ScoreId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString ( ) ),
					ScoreNumber = dataTable.Rows [ i ].ItemArray [ 1 ].ToString ( )
				} );
			}
		}

		return Scores;
	}
	#endregion

	#region GetAccompaniments
	public static ObservableCollection<AccompanimentModel> GetAccompaniments ( )
	{
		ObservableCollection<AccompanimentModel> Accompaniments = new();

		DataTable dataTable = DBCommands.GetData(DBNames.AccompanimentsTable, "NoSort");
		if ( dataTable.Rows.Count > 0 )
		{
			for ( int i = 0 ; i < dataTable.Rows.Count ; i++ )
			{
				Accompaniments.Add ( new AccompanimentModel
				{
					AccompanimentId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString ( ) ),
					AccompanimentName = dataTable.Rows [ i ].ItemArray [ 1 ].ToString ( )
				} );
			}
		}
		return Accompaniments;
	}
	#endregion

	#region GetArchives
	public static ObservableCollection<ArchiveModel> GetArchives ( )
	{
		ObservableCollection<ArchiveModel> Archives = new();

		DataTable dataTable = DBCommands.GetData(DBNames.ArchivesTable, "NoSort");
		if ( dataTable.Rows.Count > 0 )
		{
			for ( int i = 0 ; i < dataTable.Rows.Count ; i++ )
			{
				Archives.Add ( new ArchiveModel
				{
					ArchiveId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString ( ) ),
					ArchiveName = dataTable.Rows [ i ].ItemArray [ 1 ].ToString ( )
				} );
			}
		}
		return Archives;
	}
	#endregion

	#region GetGenres
	public static ObservableCollection<GenreModel> GetGenres ( )
	{
		ObservableCollection<GenreModel> Genres = new();

		DataTable dataTable = DBCommands.GetData(DBNames.GenresTable, "NoSort");
		if ( dataTable.Rows.Count > 0 )
		{
			for ( int i = 0 ; i < dataTable.Rows.Count ; i++ )
			{
				Genres.Add ( new GenreModel
				{
					GenreId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString ( ) ),
					GenreName = dataTable.Rows [ i ].ItemArray [ 1 ].ToString ( )
				} );
			}
		}
		return Genres;
	}
	#endregion

	#region GetLanguages
	public static ObservableCollection<LanguageModel> GetLanguages ( )
	{
		ObservableCollection<LanguageModel> Languages = new();

		DataTable dataTable = DBCommands.GetData(DBNames.LanguagesTable, "NoSort");
		if ( dataTable.Rows.Count > 0 )
		{
			for ( int i = 0 ; i < dataTable.Rows.Count ; i++ )
			{
				Languages.Add ( new LanguageModel
				{
					LanguageId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString ( ) ),
					LanguageName = dataTable.Rows [ i ].ItemArray [ 1 ].ToString ( )
				} );
			}
		}
		return Languages;
	}
	#endregion

	#region GetPublishers
	public static ObservableCollection<PublisherModel> GetPublishers ( )
	{
		ObservableCollection<PublisherModel> Publishers = new();

		DataTable dataTable = DBCommands.GetData(DBNames.PublishersTable, DBNames.PublishersFieldNameId);
		if ( dataTable.Rows.Count > 0 )
		{
			for ( int i = 0 ; i < dataTable.Rows.Count ; i++ )
			{
				Publishers.Add ( new PublisherModel
				{
					PublisherId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString ( ) ),
					PublisherName = dataTable.Rows [ i ].ItemArray [ 1 ].ToString ( )
				} );
			}
		}
		return Publishers;
	}
	#endregion

	#region GetRepertoires
	public static ObservableCollection<RepertoireModel> GetRepertoires ( )
	{
		ObservableCollection<RepertoireModel> Repertoires = new();

		DataTable dataTable = DBCommands.GetData(DBNames.RepertoiresTable, "NoSort");
		if ( dataTable.Rows.Count > 0 )
		{
			for ( int i = 0 ; i < dataTable.Rows.Count ; i++ )
			{
				Repertoires.Add ( new RepertoireModel
				{
					RepertoireId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString ( ) ),
					RepertoireName = dataTable.Rows [ i ].ItemArray [ 1 ].ToString ( ),
					RepertoireRange = dataTable.Rows [ i ].ItemArray [ 4 ].ToString ( )
				} );
			}
		}
		return Repertoires;
	}
	#endregion

	#region GetUserRoles
	public static ObservableCollection<UserRoleModel> GetUserRoles ( )
	{
		ObservableCollection<UserRoleModel> UserRoles = new();

		DataTable dataTable = DBCommands.GetData(DBNames.UserRolesTable, DBNames.UserRolesFieldNameOrder);
		if ( dataTable.Rows.Count > 0 )
		{
			for ( int i = 0 ; i < dataTable.Rows.Count ; i++ )
			{
				UserRoles.Add ( new UserRoleModel
				{
					RoleId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString ( ) ),
					RoleOrder = int.Parse ( dataTable.Rows [ i ].ItemArray [ 1 ].ToString ( ) ),
					RoleName = dataTable.Rows [ i ].ItemArray [ 2 ].ToString ( ),
					RoleDescription = dataTable.Rows [ i ].ItemArray [ 3 ].ToString ( )
				} );
			}
		}
		return UserRoles;
	}
	#endregion

	#region Update/Save Score
	public static void SaveScore ( ObservableCollection<SaveScoreModel> scoreList )
	{

		string sqlQuery = DBNames.SqlUpdate + DBNames.ScoresTable + DBNames.SqlSet;

		if ( scoreList [ 0 ].RepertoireId != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameRepertoireId + " = @" + DBNames.ScoresFieldNameRepertoireId; }
		if ( scoreList [ 0 ].ArchiveId != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameArchiveId + " = @" + DBNames.ScoresFieldNameArchiveId; }
		if ( scoreList [ 0 ].ByHeart != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameByHeart + " = @" + DBNames.ScoresFieldNameByHeart; }

		if ( scoreList [ 0 ].TitleChanged != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameTitle + " = @" + DBNames.ScoresFieldNameTitle; }
		if ( scoreList [ 0 ].SubTitleChanged != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameSubTitle + " = @" + DBNames.ScoresFieldNameSubTitle; }

		if ( scoreList [ 0 ].ComposerChanged != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameComposer + " = @" + DBNames.ScoresFieldNameComposer; }
		if ( scoreList [ 0 ].TextwriterChanged != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameTextwriter + " = @" + DBNames.ScoresFieldNameTextwriter; }
		if ( scoreList [ 0 ].ArrangerChanged != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameArranger + " = @" + DBNames.ScoresFieldNameArranger; }

		if ( scoreList [ 0 ].GenreId != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameGenreId + " = @" + DBNames.ScoresFieldNameGenreId; }
		if ( scoreList [ 0 ].AccompanimentId != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameAccompanimentId + " = @" + DBNames.ScoresFieldNameAccompanimentId; }
		if ( scoreList [ 0 ].LanguageId != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameLanguageId + " = @" + DBNames.ScoresFieldNameLanguageId; }

		if ( scoreList [ 0 ].MusicPieceChanged != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameMusicPiece + " = @" + DBNames.ScoresFieldNameMusicPiece; }

		if ( scoreList [ 0 ].DateDigitizedChanged != -1 && scoreList [ 0 ].DateDigitized != "" )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameDigitized + " = @" + DBNames.ScoresFieldNameDigitized; }
		if ( scoreList [ 0 ].DateModifiedChanged != -1 && scoreList [ 0 ].DateModified != "" )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameModified + " = @" + DBNames.ScoresFieldNameModified; }
		if ( scoreList [ 0 ].Checked != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameChecked + " = @" + DBNames.ScoresFieldNameChecked; }

		if ( scoreList [ 0 ].MuseScoreORP != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameMuseScoreORP + " = @" + DBNames.ScoresFieldNameMuseScoreORP; }
		if ( scoreList [ 0 ].MuseScoreORK != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameMuseScoreORK + " = @" + DBNames.ScoresFieldNameMuseScoreORK; }
		if ( scoreList [ 0 ].MuseScoreTOP != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameMuseScoreTOP + " = @" + DBNames.ScoresFieldNameMuseScoreTOP; }
		if ( scoreList [ 0 ].MuseScoreTOK != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameMuseScoreTOK + " = @" + DBNames.ScoresFieldNameMuseScoreTOK; }

		if ( scoreList [ 0 ].PDFORP != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNamePDFORP + " = @" + DBNames.ScoresFieldNamePDFORP; }
		if ( scoreList [ 0 ].PDFORK != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNamePDFORK + " = @" + DBNames.ScoresFieldNamePDFORK; }
		if ( scoreList [ 0 ].PDFTOP != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNamePDFTOP + " = @" + DBNames.ScoresFieldNamePDFTOP; }
		if ( scoreList [ 0 ].PDFTOK != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNamePDFTOK + " = @" + DBNames.ScoresFieldNamePDFTOK; }
		if ( scoreList [ 0 ].PDFPIA != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNamePDFPIA + " = @" + DBNames.ScoresFieldNamePDFPIA; }

		if ( scoreList [ 0 ].MP3B1 != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameMP3B1 + " = @" + DBNames.ScoresFieldNameMP3B1; }
		if ( scoreList [ 0 ].MP3B2 != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameMP3B2 + " = @" + DBNames.ScoresFieldNameMP3B2; }
		if ( scoreList [ 0 ].MP3T1 != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameMP3T1 + " = @" + DBNames.ScoresFieldNameMP3T1; }
		if ( scoreList [ 0 ].MP3T2 != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameMP3T2 + " = @" + DBNames.ScoresFieldNameMP3T2; }

		if ( scoreList [ 0 ].MP3SOL1 != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameMP3SOL1 + " = @" + DBNames.ScoresFieldNameMP3SOL1; }
		if ( scoreList [ 0 ].MP3SOL2 != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameMP3SOL2 + " = @" + DBNames.ScoresFieldNameMP3SOL2; }
		if ( scoreList [ 0 ].MP3TOT != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameMP3TOT + " = @" + DBNames.ScoresFieldNameMP3TOT; }
		if ( scoreList [ 0 ].MP3PIA != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameMP3PIA + " = @" + DBNames.ScoresFieldNameMP3PIA; }

		if ( scoreList [ 0 ].MP3B1Voice != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameMP3B1Voice + " = @" + DBNames.ScoresFieldNameMP3B1Voice; }
		if ( scoreList [ 0 ].MP3B2Voice != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameMP3B2Voice + " = @" + DBNames.ScoresFieldNameMP3B2Voice; }
		if ( scoreList [ 0 ].MP3T1Voice != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameMP3T1Voice + " = @" + DBNames.ScoresFieldNameMP3T1Voice; }
		if ( scoreList [ 0 ].MP3T2Voice != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameMP3T2Voice + " = @" + DBNames.ScoresFieldNameMP3T2Voice; }

		if ( scoreList [ 0 ].MP3SOL1Voice != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameMP3SOL1Voice + " = @" + DBNames.ScoresFieldNameMP3SOL1Voice; }
		if ( scoreList [ 0 ].MP3SOL2Voice != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameMP3SOL2Voice + " = @" + DBNames.ScoresFieldNameMP3SOL2Voice; }
		if ( scoreList [ 0 ].MP3TOTVoice != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameMP3TOTVoice + " = @" + DBNames.ScoresFieldNameMP3TOTVoice; }
		if ( scoreList [ 0 ].MP3UITVVoice != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameMP3UITVVoice + " = @" + DBNames.ScoresFieldNameMP3UITVVoice; }

		if ( scoreList [ 0 ].MuseScoreOnline != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameOnline + " = @" + DBNames.ScoresFieldNameOnline; }

		if ( scoreList [ 0 ].LyricsChanged != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameLyrics + " = @" + DBNames.ScoresFieldNameLyrics; }
		if ( scoreList [ 0 ].NotesChanged != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameNotes + " = @" + DBNames.ScoresFieldNameNotes; }

		if ( scoreList [ 0 ].AmountPublisher1 != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameAmountPublisher1 + " = @" + DBNames.ScoresFieldNameAmountPublisher1; }
		if ( scoreList [ 0 ].AmountPublisher2 != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameAmountPublisher2 + " = @" + DBNames.ScoresFieldNameAmountPublisher2; }
		if ( scoreList [ 0 ].AmountPublisher3 != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameAmountPublisher3 + " = @" + DBNames.ScoresFieldNameAmountPublisher3; }
		if ( scoreList [ 0 ].AmountPublisher4 != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameAmountPublisher4 + " = @" + DBNames.ScoresFieldNameAmountPublisher4; }

		if ( scoreList [ 0 ].Publisher1Id != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNamePublisher1Id + " = @" + DBNames.ScoresFieldNamePublisher1Id; }
		if ( scoreList [ 0 ].Publisher2Id != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNamePublisher2Id + " = @" + DBNames.ScoresFieldNamePublisher2Id; }
		if ( scoreList [ 0 ].Publisher3Id != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNamePublisher3Id + " = @" + DBNames.ScoresFieldNamePublisher3Id; }
		if ( scoreList [ 0 ].Publisher4Id != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNamePublisher4Id + " = @" + DBNames.ScoresFieldNamePublisher4Id; }

		if ( scoreList [ 0 ].DurationChanged != -1 )
		{ sqlQuery += ", " + DBNames.ScoresFieldNameDuration + " = @" + DBNames.ScoresFieldNameDuration; }

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

		if ( modifiedUser != null )
		{
			if ( modifiedUser [ 0 ].UserName != "" )
			{ sqlQuery += DBNames.UsersFieldNameUserName + " = @" + DBNames.UsersFieldNameUserName; }

			if ( modifiedUser [ 0 ].UserFullName != "" )
			{ sqlQuery += ", " + DBNames.UsersFieldNameFullName + " = @" + DBNames.UsersFieldNameFullName; }

			if ( modifiedUser [ 0 ].UserEmail != "" )
			{ sqlQuery += ", " + DBNames.UsersFieldNameLogin + " = @" + DBNames.UsersFieldNameLogin; }

			if ( modifiedUser [ 0 ].UserRoleId != 0 )
			{ sqlQuery += ", " + DBNames.UsersFieldNameRoleId + " = @" + DBNames.UsersFieldNameRoleId; }

			if ( modifiedUser [ 0 ].UserPassword != "" )
			{ sqlQuery += ", `" + DBNames.UsersFieldNamePW + "` = @" + DBNames.UsersFieldNamePW; }

			if ( modifiedUser [ 0 ].CoverSheetFolder != "" )
			{ sqlQuery += ", " + DBNames.UsersFieldNameCoverSheetFolder + " = @" + DBNames.UsersFieldNameCoverSheetFolder; }

			if ( modifiedUser [ 0 ].DownloadFolder != "" )
			{ sqlQuery += ", " + DBNames.UsersFieldNameDownloadFolder + " = @" + DBNames.UsersFieldNameDownloadFolder; }
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
		connection.Open ( );

		using MySqlCommand cmd = new(sqlQuery, connection);

		_targetId = ( int ) cmd.ExecuteScalar ( );

		return _targetId;
	}
	#endregion

	#region Renumber Score list
	public static void RenumberScoreList ( DataTable scoreList, string newScoreNumber )
	{
		#region Loop trough ScoreList
		for ( int i = 0 ; i < scoreList.Rows.Count ; i++ )
		{
			string sqlQuery = "", sqlQueryFields = " ( ", sqlQueryValues = " ( ";
			string digitizedDate = "", modifiedDate = "", oldScoreLong = scoreList.Rows [ i ].ItemArray [ 3 ].ToString(), newScoreLong = newScoreNumber;

			#region get correct date strings
			if ( scoreList.Rows [ i ].ItemArray [ 14 ].ToString ( ) != "" )
			{
				string[] _digitized = scoreList.Rows [ i ].ItemArray [ 14 ].ToString ().Split(" ");
				string[] _digitizedDate = _digitized[0].Split("-");
				digitizedDate = $"{_digitizedDate [ 2 ]}-{String.Format ( "{0:00}", _digitizedDate [ 1 ] )}-{String.Format ( "{0:00}", _digitizedDate [ 0 ] )}";
			}
			else
			{ digitizedDate = ""; }

			if ( scoreList.Rows [ i ].ItemArray [ 15 ].ToString ( ) != "" )
			{
				string[] _modified = scoreList.Rows [ i ].ItemArray [ 15 ].ToString ().Split(" ");
				string[] _modifiedDate = _modified[0].Split("-");
				modifiedDate = $"{_modifiedDate [ 2 ]}-{String.Format ( "{0:00}", _modifiedDate [ 1 ] )}-{String.Format ( "{0:00}", _modifiedDate [ 0 ] )}";
			}
			else
			{ modifiedDate = ""; }
			#endregion

			#region ArchiveId
			if ( scoreList.Rows [ i ].ItemArray [ 1 ] != "" )
			{
				sqlQueryFields += DBNames.ScoresFieldNameArchiveId;
				sqlQueryValues += scoreList.Rows [ i ].ItemArray [ 1 ];
			}
			#endregion

			#region RepertoireId
			if ( scoreList.Rows [ i ].ItemArray [ 2 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameRepertoireId;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 2 ];
			}
			#endregion

			#region Score number
			sqlQueryFields += ", " + DBNames.ScoresFieldNameScoreNumber;
			sqlQueryValues += ", '" + newScoreNumber + "'";
			#endregion

			#region Score Sub Number
			if ( scoreList.Rows [ i ].ItemArray [ 4 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameScoreSubNumber;
				sqlQueryValues += ", '" + scoreList.Rows [ i ].ItemArray [ 4 ] + "'";
			}
			#endregion

			#region Title
			if ( scoreList.Rows [ i ].ItemArray [ 5 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameTitle;
				sqlQueryValues += ", '" + scoreList.Rows [ i ].ItemArray [ 5 ] + "'";
			}
			#endregion

			#region Sub Title
			if ( scoreList.Rows [ i ].ItemArray [ 6 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameSubTitle;
				sqlQueryValues += ", '" + scoreList.Rows [ i ].ItemArray [ 6 ] + "'";
			}
			#endregion

			#region Composer
			if ( scoreList.Rows [ i ].ItemArray [ 7 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameComposer;
				sqlQueryValues += ", '" + scoreList.Rows [ i ].ItemArray [ 7 ] + "'";
			}
			#endregion

			#region Text writer
			if ( scoreList.Rows [ i ].ItemArray [ 8 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameTextwriter;
				sqlQueryValues += ", '" + scoreList.Rows [ i ].ItemArray [ 8 ] + "'";
			}
			#endregion

			#region Arranger
			if ( scoreList.Rows [ i ].ItemArray [ 9 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameArranger;
				sqlQueryValues += ", '" + scoreList.Rows [ i ].ItemArray [ 9 ] + "'";
			}
			#endregion

			#region Language
			if ( scoreList.Rows [ i ].ItemArray [ 10 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameLanguageId;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 10 ];
			}
			#endregion

			#region Genre
			if ( scoreList.Rows [ i ].ItemArray [ 11 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameGenreId;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 11 ];
			}
			#endregion

			#region Lyrics
			if ( scoreList.Rows [ i ].ItemArray [ 12 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameLyrics;
				sqlQueryValues += ", '" + scoreList.Rows [ i ].ItemArray [ 12 ] + "'";
			}
			#endregion

			#region Checked
			if ( scoreList.Rows [ i ].ItemArray [ 13 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameChecked;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 13 ];
			}
			#endregion

			#region Date Digitized
			if ( digitizedDate != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameDigitized;
				sqlQueryValues += ", '" + digitizedDate + "'";
			}
			#endregion

			#region Date Modified
			if ( modifiedDate != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameModified;
				sqlQueryValues += ", '" + modifiedDate + "'";
			}
			#endregion

			#region Accompaniment
			if ( scoreList.Rows [ i ].ItemArray [ 16 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameAccompanimentId;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 16 ];
			}
			#endregion

			#region PDF ORP
			if ( scoreList.Rows [ i ].ItemArray [ 17 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNamePDFORP;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 17 ];
			}
			#endregion

			#region PDF ORK
			if ( scoreList.Rows [ i ].ItemArray [ 18 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNamePDFORK;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 18 ];
			}
			#endregion

			#region PDF TOP
			if ( scoreList.Rows [ i ].ItemArray [ 19 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNamePDFTOP;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 19 ];
			}
			#endregion

			#region PDF TOK
			if ( scoreList.Rows [ i ].ItemArray [ 20 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNamePDFTOK;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 20 ];
			}
			#endregion

			#region MuseScore ORP
			if ( scoreList.Rows [ i ].ItemArray [ 21 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameMuseScoreORP;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 21 ];
			}
			#endregion

			#region MuseScore ORK
			if ( scoreList.Rows [ i ].ItemArray [ 22 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameMuseScoreORK;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 22 ];
			}
			#endregion

			#region MuseScore TOP
			if ( scoreList.Rows [ i ].ItemArray [ 23 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameMuseScoreTOP;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 23 ];
			}
			#endregion

			#region MuseScore TOK
			if ( scoreList.Rows [ i ].ItemArray [ 24 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameMuseScoreTOK;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 24 ];
			}
			#endregion

			#region MP3 TOT
			if ( scoreList.Rows [ i ].ItemArray [ 25 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameMP3TOT;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 25 ];
			}
			#endregion

			#region MP3 T1
			if ( scoreList.Rows [ i ].ItemArray [ 26 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameMP3T1;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 26 ];
			}
			#endregion

			#region MP3 T2
			if ( scoreList.Rows [ i ].ItemArray [ 27 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameMP3T2;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 27 ];
			}
			#endregion

			#region MP3 B1
			if ( scoreList.Rows [ i ].ItemArray [ 28 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameMP3B1;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 28 ];
			}
			#endregion

			#region MP3 B2
			if ( scoreList.Rows [ i ].ItemArray [ 29 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameMP3B2;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 29 ];
			}
			#endregion

			#region MP3 SOL1
			if ( scoreList.Rows [ i ].ItemArray [ 30 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameMP3SOL1;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 30 ];
			}
			#endregion

			#region MP3 SOL2
			if ( scoreList.Rows [ i ].ItemArray [ 30 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameMP3SOL2;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 30 ];
			}
			#endregion

			#region MP3 PIA
			if ( scoreList.Rows [ i ].ItemArray [ 31 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameMP3PIA;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 31 ];
			}
			#endregion

			#region MuseScore Online
			if ( scoreList.Rows [ i ].ItemArray [ 32 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameOnline;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 32 ];
			}
			#endregion

			#region Sing By Heart
			if ( scoreList.Rows [ i ].ItemArray [ 33 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameByHeart;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 33 ];
			}
			#endregion

			#region Music Piece
			if ( scoreList.Rows [ i ].ItemArray [ 34 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameMusicPiece;
				sqlQueryValues += ", '" + scoreList.Rows [ i ].ItemArray [ 34 ] + "'";
			}
			#endregion

			#region Notes
			if ( scoreList.Rows [ i ].ItemArray [ 35 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameNotes;
				sqlQueryValues += ", '" + scoreList.Rows [ i ].ItemArray [ 35 ] + "'";
			}
			#endregion

			#region Amount Scores @ publisher 1
			if ( scoreList.Rows [ i ].ItemArray [ 36 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameAmountPublisher1;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 36 ];
			}
			#endregion

			#region Amount Scores @ publisher 2
			if ( scoreList.Rows [ i ].ItemArray [ 37 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameAmountPublisher2;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 37 ];
			}
			#endregion

			#region Amount Scores @ publisher 3
			if ( scoreList.Rows [ i ].ItemArray [ 38 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameAmountPublisher3;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 38 ];
			}
			#endregion

			#region Amount Scores @ publisher 4
			if ( scoreList.Rows [ i ].ItemArray [ 39 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNameAmountPublisher4;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 39 ];
			}
			#endregion

			#region Publisher 1
			if ( scoreList.Rows [ i ].ItemArray [ 40 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNamePublisher1Id;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 40 ];
			}
			#endregion

			#region Publisher 2
			if ( scoreList.Rows [ i ].ItemArray [ 41 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNamePublisher2Id;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 41 ];
			}
			#endregion

			#region Publisher 3
			if ( scoreList.Rows [ i ].ItemArray [ 42 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNamePublisher3Id;
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 42 ];
			}
			#endregion

			#region Publisher 4
			if ( scoreList.Rows [ i ].ItemArray [ 43 ] != "" )
			{
				sqlQueryFields += ", " + DBNames.ScoresFieldNamePublisher4Id + " ";
				sqlQueryValues += ", " + scoreList.Rows [ i ].ItemArray [ 43 ];
			}
			#endregion

			#region Close Fields and Values string
			sqlQueryFields = sqlQueryFields.Replace ( "( ,", "( " ) + " ) ";
			sqlQueryValues = sqlQueryValues.Replace ( "( ,", "( " ) + " ) ";
			#endregion

			#region Create Query String
			sqlQuery = DBNames.SqlInsert + DBNames.Database + "." + DBNames.ScoresTable + sqlQueryFields + DBNames.SqlValues + sqlQueryValues + ";";
			#endregion

			#region Connect to database and execute sqlQuery
			using MySqlConnection connection = new(DBConnect.ConnectionString);
			connection.Open ( );

			using MySqlCommand cmd = new(sqlQuery, connection);

			int rowsAffected = cmd.ExecuteNonQuery();
			#endregion

			#region Write Logging
			// Write log info
			if ( scoreList.Rows [ i ].ItemArray [ 4 ].ToString ( ) != "" )
			{
				oldScoreLong = $"{scoreList.Rows [ i ].ItemArray [ 3 ]}-{scoreList.Rows [ i ].ItemArray [ 4 ].ToString ( )}";
				newScoreLong = $"{newScoreNumber}-{scoreList.Rows [ i ].ItemArray [ 4 ].ToString ( )}";
			}
			DBCommands.WriteLog ( ScoreUsers.SelectedUserId, DBNames.LogScoreRenumbered, $"Partituur omgenummerd van {scoreList.Rows [ i ].ItemArray [ 3 ]}-{scoreList.Rows [ i ].ItemArray [ 4 ].ToString ( )} naar {newScoreLong}." );

			// GetHashCode History Id
			int _historyId = DBCommands.GetAddedHistoryId();

			// Write Detailed logging
			DBCommands.WriteDetailLog ( _historyId, DBNames.LogScoreNumber, oldScoreLong, newScoreLong );
			#endregion
		}
		#endregion
	}
	#endregion

	#region Execute Non Query ScoresTable
	static void ExecuteNonQueryScoresTable ( string sqlQuery, ObservableCollection<SaveScoreModel> scoreList )
	{
		using MySqlConnection connection = new(DBConnect.ConnectionString);
		connection.Open ( );

		using MySqlCommand cmd = new(sqlQuery, connection);

		if ( scoreList [ 0 ].RepertoireId != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameRepertoireId, MySqlDbType.Int32 ).Value = scoreList [ 0 ].RepertoireId; }
		if ( scoreList [ 0 ].ArchiveId != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameArchiveId, MySqlDbType.Int32 ).Value = scoreList [ 0 ].ArchiveId; }
		if ( scoreList [ 0 ].ByHeart != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameByHeart, MySqlDbType.Int32 ).Value = scoreList [ 0 ].ByHeart; }

		if ( scoreList [ 0 ].DurationChanged != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameDuration, MySqlDbType.Int32 ).Value = scoreList [ 0 ].Duration; }

		if ( scoreList [ 0 ].TitleChanged != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameTitle, MySqlDbType.VarChar ).Value = scoreList [ 0 ].Title; }
		if ( scoreList [ 0 ].SubTitleChanged != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameSubTitle, MySqlDbType.VarChar ).Value = scoreList [ 0 ].SubTitle; }

		if ( scoreList [ 0 ].ComposerChanged != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameComposer, MySqlDbType.VarChar ).Value = scoreList [ 0 ].Composer; }
		if ( scoreList [ 0 ].TextwriterChanged != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameTextwriter, MySqlDbType.VarChar ).Value = scoreList [ 0 ].Textwriter; }
		if ( scoreList [ 0 ].ArrangerChanged != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameArranger, MySqlDbType.VarChar ).Value = scoreList [ 0 ].Arranger; }

		if ( scoreList [ 0 ].GenreId != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameGenreId, MySqlDbType.Int32 ).Value = scoreList [ 0 ].GenreId; }
		if ( scoreList [ 0 ].AccompanimentId != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameAccompanimentId, MySqlDbType.Int32 ).Value = scoreList [ 0 ].AccompanimentId; }
		if ( scoreList [ 0 ].LanguageId != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameLanguageId, MySqlDbType.Int32 ).Value = scoreList [ 0 ].LanguageId; }

		if ( scoreList [ 0 ].MusicPieceChanged != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMusicPiece, MySqlDbType.VarChar ).Value = scoreList [ 0 ].MusicPiece; }

		if ( scoreList [ 0 ].DateDigitizedChanged != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameDigitized, MySqlDbType.String ).Value = scoreList [ 0 ].DateDigitized; }
		if ( scoreList [ 0 ].DateModifiedChanged != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameModified, MySqlDbType.String ).Value = scoreList [ 0 ].DateModified; }
		if ( scoreList [ 0 ].Checked != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameChecked, MySqlDbType.Int32 ).Value = scoreList [ 0 ].Checked; }

		if ( scoreList [ 0 ].MuseScoreORP != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMuseScoreORP, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MuseScoreORP; }
		if ( scoreList [ 0 ].MuseScoreORK != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMuseScoreORK, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MuseScoreORK; }
		if ( scoreList [ 0 ].MuseScoreTOP != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMuseScoreTOP, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MuseScoreTOP; }
		if ( scoreList [ 0 ].MuseScoreTOK != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMuseScoreTOK, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MuseScoreTOK; }

		if ( scoreList [ 0 ].PDFORP != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNamePDFORP, MySqlDbType.Int32 ).Value = scoreList [ 0 ].PDFORP; }
		if ( scoreList [ 0 ].PDFORK != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNamePDFORK, MySqlDbType.Int32 ).Value = scoreList [ 0 ].PDFORK; }
		if ( scoreList [ 0 ].PDFTOP != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNamePDFTOP, MySqlDbType.Int32 ).Value = scoreList [ 0 ].PDFTOP; }
		if ( scoreList [ 0 ].PDFTOK != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNamePDFTOK, MySqlDbType.Int32 ).Value = scoreList [ 0 ].PDFTOK; }
		if ( scoreList [ 0 ].PDFPIA != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNamePDFPIA, MySqlDbType.Int32 ).Value = scoreList [ 0 ].PDFPIA; }

		if ( scoreList [ 0 ].MP3B1 != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3B1, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3B1; }
		if ( scoreList [ 0 ].MP3B2 != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3B2, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3B2; }
		if ( scoreList [ 0 ].MP3T1 != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3T1, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3T1; }
		if ( scoreList [ 0 ].MP3T2 != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3T2, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3T2; }

		if ( scoreList [ 0 ].MP3SOL1 != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3SOL1, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3SOL1; }
		if ( scoreList [ 0 ].MP3SOL2 != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3SOL2, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3SOL2; }
		if ( scoreList [ 0 ].MP3TOT != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3TOT, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3TOT; }
		if ( scoreList [ 0 ].MP3PIA != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3PIA, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3PIA; }

		if ( scoreList [ 0 ].MP3B1Voice != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3B1Voice, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3B1Voice; }
		if ( scoreList [ 0 ].MP3B2Voice != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3B2Voice, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3B2Voice; }
		if ( scoreList [ 0 ].MP3T1Voice != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3T1Voice, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3T1Voice; }
		if ( scoreList [ 0 ].MP3T2Voice != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3T2Voice, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3T2Voice; }

		if ( scoreList [ 0 ].MP3SOL1Voice != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3SOL1Voice, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3SOL1Voice; }
		if ( scoreList [ 0 ].MP3SOL2Voice != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3SOL2Voice, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3SOL2Voice; }
		if ( scoreList [ 0 ].MP3TOTVoice != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3TOTVoice, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3TOTVoice; }
		if ( scoreList [ 0 ].MP3UITVVoice != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3UITVVoice, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3UITVVoice; }

		if ( scoreList [ 0 ].MuseScoreOnline != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameOnline, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MuseScoreOnline; }

		// Rich text fields
		cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameLyrics, MySqlDbType.MediumText ).Value = scoreList [ 0 ].Lyrics;
		cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameNotes, MySqlDbType.MediumText ).Value = scoreList [ 0 ].Notes;

		if ( scoreList [ 0 ].AmountPublisher1 != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameAmountPublisher1, MySqlDbType.Int32 ).Value = scoreList [ 0 ].AmountPublisher1; }
		if ( scoreList [ 0 ].AmountPublisher2 != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameAmountPublisher2, MySqlDbType.Int32 ).Value = scoreList [ 0 ].AmountPublisher2; }
		if ( scoreList [ 0 ].AmountPublisher3 != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameAmountPublisher3, MySqlDbType.Int32 ).Value = scoreList [ 0 ].AmountPublisher3; }
		if ( scoreList [ 0 ].AmountPublisher4 != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameAmountPublisher4, MySqlDbType.Int32 ).Value = scoreList [ 0 ].AmountPublisher4; }

		if ( scoreList [ 0 ].Publisher1Id != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNamePublisher1Id, MySqlDbType.Int32 ).Value = scoreList [ 0 ].Publisher1Id; }
		if ( scoreList [ 0 ].Publisher2Id != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNamePublisher2Id, MySqlDbType.Int32 ).Value = scoreList [ 0 ].Publisher2Id; }
		if ( scoreList [ 0 ].Publisher3Id != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNamePublisher3Id, MySqlDbType.Int32 ).Value = scoreList [ 0 ].Publisher3Id; }
		if ( scoreList [ 0 ].Publisher4Id != -1 )
		{ cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNamePublisher4Id, MySqlDbType.Int32 ).Value = scoreList [ 0 ].Publisher4Id; }

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
		connection.Open ( );

		using MySqlCommand cmd = new(sqlQuery, connection);

		if ( modifiedUser != null )
		{
			if ( modifiedUser [ 0 ].UserName != "" )
			{ cmd.Parameters.Add ( "@" + DBNames.UsersFieldNameUserName, MySqlDbType.VarChar ).Value = modifiedUser [ 0 ].UserName; }

			if ( modifiedUser [ 0 ].UserFullName != "" )
			{ cmd.Parameters.Add ( "@" + DBNames.UsersFieldNameFullName, MySqlDbType.VarChar ).Value = modifiedUser [ 0 ].UserFullName; }

			if ( modifiedUser [ 0 ].UserFullName != "" )
			{ cmd.Parameters.Add ( "@" + DBNames.UsersFieldNameLogin, MySqlDbType.VarChar ).Value = modifiedUser [ 0 ].UserEmail; }

			if ( modifiedUser [ 0 ].UserRoleId != 0 )
			{ cmd.Parameters.Add ( "@" + DBNames.UsersFieldNameRoleId, MySqlDbType.Int32 ).Value = modifiedUser [ 0 ].UserRoleId; }

			if ( modifiedUser [ 0 ].UserPassword != "" )
			{ cmd.Parameters.Add ( "@" + DBNames.UsersFieldNamePW, MySqlDbType.VarChar ).Value = modifiedUser [ 0 ].UserPassword; }

			if ( modifiedUser [ 0 ].CoverSheetFolder != "" )
			{ cmd.Parameters.Add ( "@" + DBNames.UsersFieldNameCoverSheetFolder, MySqlDbType.VarChar ).Value = modifiedUser [ 0 ].CoverSheetFolder; }

			if ( modifiedUser [ 0 ].DownloadFolder != "" )
			{ cmd.Parameters.Add ( "@" + DBNames.UsersFieldNameDownloadFolder, MySqlDbType.VarChar ).Value = modifiedUser [ 0 ].DownloadFolder; }
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
	public static int CheckUserPassword ( string login, string password )
	{
		var _pwLogedInUser = Helper.HashPepperPassword(password, login);

		ObservableCollection<UserModel> Users = GetUsers();

		foreach ( var user in Users )
		{
			var _pwToCheck = Helper.HashPepperPassword(password, user.UserName);
			if ( _pwLogedInUser == _pwToCheck )
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
			if ( user.UserName.ToLower ( ) == userName.ToLower ( ) )
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
			if ( user.UserEmail.ToLower ( ) == email.ToLower ( ) )
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
			Console.WriteLine(e);
			return false;
		}
		catch ( ArgumentException e )
		{
			Console.WriteLine(e);
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
	public static ObservableCollection<UserModel> GetUsers ( int _userId )
	{
		ObservableCollection<UserModel> users = new();

		DataTable dataTable = DBCommands.GetData(DBNames.UsersTable, "nosort", DBNames.UsersFieldNameId, _userId.ToString());

		if ( dataTable.Rows.Count > 0 )
		{
			for ( int i = 0 ; i < dataTable.Rows.Count ; i++ )
			{
				users.Add ( new UserModel
				{
					UserId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString ( ) ),
					UserName = dataTable.Rows [ i ].ItemArray [ 2 ].ToString ( ),
					UserEmail = dataTable.Rows [ i ].ItemArray [ 1 ].ToString ( ),
					UserPassword = dataTable.Rows [ i ].ItemArray [ 3 ].ToString ( ),
					UserFullName = dataTable.Rows [ i ].ItemArray [ 5 ].ToString ( ),
					UserRoleId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 4 ].ToString ( ) )
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
			for ( int i = 0 ; i < dataTable.Rows.Count ; i++ )
			{
				string CoverSheetPath = "", DownloadPath = "";
				if ( dataTable.Rows [ i ].ItemArray [ 9 ].ToString ( ) == "" )
				{
					CoverSheetPath = System.Environment.GetFolderPath ( Environment.SpecialFolder.MyDocuments );
				}
				else
				{
					CoverSheetPath = dataTable.Rows [ i ].ItemArray [ 9 ].ToString ( );
				}

				if ( dataTable.Rows [ i ].ItemArray [ 10 ].ToString ( ) == "" )
				{
					DownloadPath = System.Environment.GetFolderPath ( Environment.SpecialFolder.MyDocuments );
				}
				else
				{
					DownloadPath = dataTable.Rows [ i ].ItemArray [ 10 ].ToString ( );
				}


				users.Add ( new UserModel
				{
					UserId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString ( ) ),
					UserName = dataTable.Rows [ i ].ItemArray [ 2 ].ToString ( ),
					UserEmail = dataTable.Rows [ i ].ItemArray [ 1 ].ToString ( ),
					UserPassword = dataTable.Rows [ i ].ItemArray [ 3 ].ToString ( ),
					UserFullName = dataTable.Rows [ i ].ItemArray [ 5 ].ToString ( ),
					UserRoleId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 4 ].ToString ( ) ),
					RoleName = dataTable.Rows [ i ].ItemArray [ 7 ].ToString ( ),
					RoleDescription = dataTable.Rows [ i ].ItemArray [ 8 ].ToString ( ),
					CoverSheetFolder = CoverSheetPath,
					DownloadFolder = DownloadPath
				} );
			}
		}
		return users;
	}
	#endregion

	#endregion

	#region Store file in database table
	public static void StoreFile ( string _table, string _fileType, string _extensionType, int _scoreId, string _path, string _fileName )
		{
		int fileSize;
		string sqlQuery, _fieldName = "";
		byte[] rawData;
		FileStream fs;

		try
			{
			fs = new FileStream ( @_path, FileMode.Open, FileAccess.Read );
			fileSize = Convert.ToInt32 ( fs.Length );

			rawData = new byte [ fileSize ];
			fs.Read ( rawData, 0, Convert.ToInt32 ( fs.Length ) );
			fs.Close ();

			using MySqlConnection connection = new(DBConnect.ConnectionString);
			connection.Open ();

			sqlQuery = $"{DBNames.SqlInsert} {_table} ( {DBNames.FilesFieldNameFileName}, {DBNames.FilesFieldNameContentType}, {DBNames.FilesFieldNameFileSize}, {DBNames.FilesFieldNameFile} ) {DBNames.SqlValues} ( @{DBNames.FilesFieldNameFileName}, @{DBNames.FilesFieldNameContentType}, @{DBNames.FilesFieldNameFileSize}, @{DBNames.FilesFieldNameFile} )";

			using MySqlCommand cmd = new(sqlQuery, connection);

			cmd.Connection = connection;
			cmd.CommandText = sqlQuery;
			cmd.Parameters.AddWithValue ( $"@{DBNames.FilesFieldNameFileName}", _fileName );
			cmd.Parameters.AddWithValue( $"@{DBNames.FilesFieldNameContentType}" , _fileType );
			cmd.Parameters.AddWithValue ( $"@{DBNames.FilesFieldNameFileSize}", fileSize );
			cmd.Parameters.AddWithValue ( $"@{DBNames.FilesFieldNameFile}", rawData );

			cmd.ExecuteNonQuery ();

			// Get the fieldname where to store the File Id of the just saved File
			switch ( _extensionType.ToLower() )
				{
				case "mscz":
					switch (_fileType.ToLower())
					{
						case "orp":
							_fieldName = DBNames.ScoresFieldNameMuseScoreORP;
							break;
						case "ork":
							_fieldName = DBNames.ScoresFieldNameMuseScoreORK;
							break;
						case "top":
							_fieldName = DBNames.ScoresFieldNameMuseScoreTOP;
							break;
						case "tok":
							_fieldName = DBNames.ScoresFieldNameMuseScoreTOK;
							break;
					}
					break;
				case "pdf":
					switch ( _fileType.ToLower () )
					{
						case "orp":
							_fieldName = DBNames.ScoresFieldNamePDFORP;
							break;
						case "ork":
							_fieldName = DBNames.ScoresFieldNamePDFORK;
							break;
						case "top":
							_fieldName = DBNames.ScoresFieldNamePDFTOP;
							break;
						case "tok":
							_fieldName = DBNames.ScoresFieldNamePDFTOK;
							break;
						case "pia":
							_fieldName = DBNames.ScoresFieldNamePDFPIA;
							break;
					}

					break;
				case "mp3":
					switch ( _fileType.ToLower () )
					{
						case "b1":
							_fieldName = DBNames.ScoresFieldNameMP3B1;
							break;
						case "b2":
							_fieldName = DBNames.ScoresFieldNameMP3B2;
							break;
						case "t1":
							_fieldName = DBNames.ScoresFieldNameMP3T1;
							break;
						case "t2":
							_fieldName = DBNames.ScoresFieldNameMP3T2;
							break;
						case "sol1":
							_fieldName = DBNames.ScoresFieldNameMP3SOL1;
							break;
						case "sol2":
							_fieldName = DBNames.ScoresFieldNameMP3SOL2;
							break;
						case "tot":
							_fieldName = DBNames.ScoresFieldNameMP3TOT;
							break;
						case "pia":
							_fieldName = DBNames.ScoresFieldNameMP3PIA;
							break;
					}

					break;
				case "voice":
					switch ( _fileType.ToLower () )
					{
						case "b1":
							_fieldName = DBNames.ScoresFieldNameMP3B1Voice;
							break;
						case "b2":
							_fieldName = DBNames.ScoresFieldNameMP3B2Voice;
							break;
						case "t1":
							_fieldName = DBNames.ScoresFieldNameMP3T1Voice;
							break;
						case "t2":
							_fieldName = DBNames.ScoresFieldNameMP3T2Voice;
							break;
						case "sol1":
							_fieldName = DBNames.ScoresFieldNameMP3SOL1Voice;
							break;
						case "sol2":
							_fieldName = DBNames.ScoresFieldNameMP3SOL2Voice;
							break;
						case "tot":
							_fieldName = DBNames.ScoresFieldNameMP3TOTVoice;
							break;
						case "uitv":
							_fieldName = DBNames.ScoresFieldNameMP3UITVVoice;
							break;
					}

					break;
				}
			//int _fileId = GetLatestFileId(_table);

			//if ( _fieldName != "" )
			//	{
			//	SetFileIdInLibrary ( _scoreId, _fileId, _fieldName );
			//	}
			//connection.Close ();
			}
		catch ( MySql.Data.MySqlClient.MySqlException ex )
			{
			System.Windows.Forms.MessageBox.Show ( "Error " + ex.Number + " is opgetreden: " + ex.Message,
				"Error", ( MessageBoxButtons ) MessageBoxButton.OK, ( MessageBoxIcon ) MessageBoxImage.Error );
			}

		// Store the Id in the Library Table
		}
	#endregion

	#region Get Latest Added File Id
	public static int GetLatestFileId ( string _table )
	{
		var sqlQuery = $"{DBNames.SqlSelectAll}{DBNames.SqlFrom}{DBNames.Database}.{_table}{DBNames.SqlOrder}Id{DBNames.SqlDesc}{DBNames.SqlLimit}1";

		using MySqlConnection connection = new(DBConnect.ConnectionString);
		connection.Open ();

		using MySqlCommand cmd = new(sqlQuery, connection);

		var fileId = (int)cmd.ExecuteScalar();

		return fileId;
	}
	#endregion

	#region Set fileId in library
	public static void SetFileIdInLibrary ( int _scoreId, int _id, string _idField )
	{
		using MySqlConnection connection = new(DBConnect.ConnectionString);
		connection.Open ();

		string sqlQuery = $"{DBNames.SqlUpdate}{DBNames.ScoresTable}{DBNames.SqlSet}{_idField} = @id{DBNames.SqlWhere}{DBNames.ScoresFieldNameId} = @scoreid";

		using MySqlCommand cmd = new(sqlQuery, connection);

		cmd.Connection = connection;
		cmd.CommandText = sqlQuery;
		cmd.Parameters.AddWithValue ( "@id", _id );
		cmd.Parameters.AddWithValue ( "@scoreid", _scoreId );

		cmd.ExecuteNonQuery ();
	}
	#endregion

	#region Write History Logging
	public static void WriteLog ( int loggedInUser, string action, string description )
	{
		var sqlQuery = DBNames.SqlInsert + DBNames.Database + "." + DBNames.LogTable + " ( " +
			DBNames.LogFieldNameUserId + ", " +
			DBNames.LogFieldNameAction + ", " +
			DBNames.LogFieldNameDescription + " ) " + DBNames.SqlValues + " ( " +
			loggedInUser + ", '" + action + "', '" + description + "' );";

		using MySqlConnection connection = new(DBConnect.ConnectionString);
		connection.Open ( );

		using MySqlCommand cmd = new(sqlQuery, connection);

		int rowsAffected = cmd.ExecuteNonQuery();
	}
	#endregion

	#region Write History Detail Logging
	public static void WriteDetailLog ( int logId, string field, string oldValue, string newValue )
	{
		var sqlQuery = DBNames.SqlInsert + DBNames.Database + "." + DBNames.LogDetailTable + " ( " +
			DBNames.LogDetailFieldNameLogId + ", " +
			DBNames.LogDetailFieldNameChanged + ", " +
			DBNames.LogDetailFieldNameOldValue + ", " +
			DBNames.LogDetailFieldNameNewValue + " ) " + DBNames.SqlValues + " ( " +
			logId + ", '" + field + "', '" + oldValue + "', '" + newValue + "' );";

		using MySqlConnection connection = new(DBConnect.ConnectionString);
		connection.Open ( );

		using MySqlCommand cmd = new(sqlQuery, connection);

		int rowsAffected = cmd.ExecuteNonQuery();
	}
	#endregion

	#region Get Latest Added HistoryId
	public static int GetAddedHistoryId ( )
	{
		int userId = 0;
		var sqlQuery = DBNames.SqlSelectAll +
			DBNames.SqlFrom + DBNames.Database + "." + DBNames.LogTable +
			DBNames.SqlOrder + DBNames.LogFieldNameLogId + DBNames.SqlDesc + DBNames.SqlLimit + "1";

		using MySqlConnection connection = new(DBConnect.ConnectionString);
		connection.Open ( );

		using MySqlCommand cmd = new(sqlQuery, connection);

		userId = ( int ) cmd.ExecuteScalar ( );

		return userId;
	}
	#endregion

	#region Get HistoryLogging
	public static ObservableCollection<HistoryModel> GetHistoryLog ( )
	{
		ObservableCollection<HistoryModel> HistoryLog = new();
		DataTable dataTable = new();

		dataTable = GetData ( DBNames.LogView, DBNames.LogViewFieldNameLogid );

		if ( dataTable.Rows.Count > 0 )
		{

			for ( int i = 0 ; i < dataTable.Rows.Count ; i++ )
			{
				//Split-up DateTimeStamp in a date and a time
				string[] _dateTime = dataTable.Rows[i].ItemArray[1].ToString().Split(' ');
				string[] _date = _dateTime[0].Split("-");
				string logDate = $"{int.Parse(_date[0]).ToString("00")}-{int.Parse(_date[1]).ToString("00")}-{_date[2]}";
				string logTime = _dateTime[1];

				HistoryLog.Add ( new HistoryModel
				{
					LogId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString ( ) ),
					LogDate = logDate,
					LogTime = logTime,
					UserName = dataTable.Rows [ i ].ItemArray [ 2 ].ToString ( ),
					PerformedAction = dataTable.Rows [ i ].ItemArray [ 3 ].ToString ( ),
					Description = dataTable.Rows [ i ].ItemArray [ 4 ].ToString ( ),
					ModifiedField = dataTable.Rows [ i ].ItemArray [ 5 ].ToString ( ),
					OldValue = dataTable.Rows [ i ].ItemArray [ 6 ].ToString ( ),
					NewValue = dataTable.Rows [ i ].ItemArray [ 7 ].ToString ( )
				} );
			}
		}
		return HistoryLog;
	}
	#endregion
}
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604
#pragma warning restore CS0252 // Possible unintended reference comparison; left hand side needs cast

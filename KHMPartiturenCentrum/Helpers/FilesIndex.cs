using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

using MySql.Data.MySqlClient;

using Org.BouncyCastle.Asn1.Pkcs;
using KHM.Models;
using System.Security.Cryptography;
using System.Collections.ObjectModel;
using System.Data;

namespace KHM.Helpers
	{
	public static class FilesIndex
		{
		public static void Store ( int _scoreId, string _fieldName, int _fileId )
			{
			var sqlQuery = $"" +
				$"{DBNames.SqlInsert}{DBNames.Database}.{DBNames.FilesIndexTable} " +
				$"( {DBNames.FilesFieldNameScoreId}, {_fieldName} )" +
				$"{DBNames.SqlValues}" +
				$"( @{DBNames.FilesFieldNameScoreId}, @{_fieldName} )";

			try
				{
				using MySqlConnection connection = new(DBConnect.ConnectionString);
				connection.Open ();

				using MySqlCommand cmd = new(sqlQuery, connection);

				cmd.Connection = connection;
				cmd.CommandText = sqlQuery;
				cmd.Parameters.AddWithValue ( $"@{DBNames.FilesFieldNameScoreId}", _scoreId );
				cmd.Parameters.AddWithValue ( $"@{_fieldName}", _fileId );

				cmd.ExecuteNonQuery ();
				}
			catch ( MySql.Data.MySqlClient.MySqlException ex )
				{
				System.Windows.Forms.MessageBox.Show ( "Error " + ex.Number + " is opgetreden: " + ex.Message,
					"Error", ( MessageBoxButtons ) MessageBoxButton.OK, ( MessageBoxIcon ) MessageBoxImage.Error );
				}
			}

		public static void Update ( int _scoreId, string _fieldName, int _fileId )
			{
			var sqlQuery = $"" +
				$"{DBNames.SqlUpdate}{DBNames.Database}.{DBNames.FilesIndexTable} " +
				$"{DBNames.SqlSet}" +
				$"( {_fieldName} = '@{_fieldName}' )" +
				$"{DBNames.SqlWhere}" +
				$"{DBNames.FilesFieldNameScoreId} = '@{DBNames.FilesFieldNameScoreId}'";

			try
				{
				using MySqlConnection connection = new(DBConnect.ConnectionString);
				connection.Open ();

				using MySqlCommand cmd = new(sqlQuery, connection);

				cmd.Connection = connection;
				cmd.CommandText = sqlQuery;
				cmd.Parameters.AddWithValue ( $"@{DBNames.FilesFieldNameScoreId}", _scoreId );
				cmd.Parameters.AddWithValue ( $"@{_fieldName}", _fileId );

				cmd.ExecuteNonQuery ();
				}
			catch ( MySql.Data.MySqlClient.MySqlException ex )
				{
				System.Windows.Forms.MessageBox.Show ( "Error " + ex.Number + " is opgetreden: " + ex.Message,
					"Error", ( MessageBoxButtons ) MessageBoxButton.OK, ( MessageBoxIcon ) MessageBoxImage.Error );
				}
			}

		public static FileIndexModel GetFileIds( int _scoreId )
			{
				FileIndexModel fileIndex = new();
				
			var sqlQuery = $"" +
				$"{DBNames.SqlSelectAll}{DBNames.SqlFrom}{DBNames.Database}.{DBNames.FilesIndexTable} " +
				$"{DBNames.SqlWhere}" +
				$"{DBNames.FilesFieldNameScoreId} = '{_scoreId}'";

				using MySqlConnection connection = new(DBConnect.ConnectionString);
				connection.Open();

			using (MySqlCommand cmd = new(sqlQuery, connection ) )
				{
				using (MySqlDataReader reader = cmd.ExecuteReader())
					{
					if ( reader.Read () )
						{
						fileIndex = new ()
							{
								Id = Convert.ToInt32(reader["Id"]),
								ScoreId = Convert.ToInt32(reader["ScoreId"]),
								MuseScoreORPId = Convert.ToInt32(reader["MuseScoreORPId"]),
								MuseScoreORKId = Convert.ToInt32(reader["MuseScoreORKId"]),
								MuseScoreTOPId = Convert.ToInt32(reader["MuseScoreTOPId"]),
								MuseScoreTOKId = Convert.ToInt32(reader["MuseScoreTOKId"]),
								PDFORPId = Convert.ToInt32(reader["PDFORPId"]),
								PDFORKId = Convert.ToInt32(reader["PDFORKId"]),
								PDFTOPId = Convert.ToInt32(reader["PDFTOPId"]),
								PDFTOKId = Convert.ToInt32(reader["PDFTOKId"]),
								PDFPIAId = Convert.ToInt32(reader["PDFPIAId"]),
								MP3B1Id = Convert.ToInt32(reader["MP3B1Id"]),
								MP3B2Id = Convert.ToInt32(reader["MP3B2Id"]),
								MP3T1Id = Convert.ToInt32(reader["MP3T1Id"]),
								MP3T2Id = Convert.ToInt32(reader["MP3T2Id"]),
								MP3SOL1Id = Convert.ToInt32(reader["MP3SOL1Id"]),
								MP3SOL2Id = Convert.ToInt32(reader["MP3SOL2Id"]),
								MP3TOTId = Convert.ToInt32(reader["MP3TOTId"]),
								MP3PIAId = Convert.ToInt32(reader["MP3PIAId"]),
								MP3B1VoiceId = Convert.ToInt32(reader["MP3B1VoiceId"]),
								MP3B2VoiceId = Convert.ToInt32(reader["MP3B2VoiceId"]),
								MP3T1VoiceId = Convert.ToInt32(reader["MP3T1VoiceId"]),
								MP3T2VoiceId = Convert.ToInt32(reader["MP3T2VoiceId"]),
								MP3SOL1VoiceId = Convert.ToInt32(reader["MP3SOL1VoiceId"]),
								MP3SOL2VoiceId = Convert.ToInt32(reader["MP3SOL2VoiceId"]),
								MP3TOTVoiceId = Convert.ToInt32(reader["MP3TOTVoiceId"]),
								MP3UITVVoiceId = Convert.ToInt32(reader["MP3UITVVoiceId"])
							};
						}
					}
				}

			return fileIndex;
			}
		}
	}
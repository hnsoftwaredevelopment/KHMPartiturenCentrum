using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

using MySql.Data.MySqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using System.Data;
using static KHM.App;

namespace KHM.Helpers
	{
	public static class Files
		{
		#region Store a file in the database
		public static void Store ( string _table, string _fileType, string _extensionType, int _scoreId, string _path, string _fileName )
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

				sqlQuery = $"" +
					$"{DBNames.SqlInsert}{DBNames.Database}.{_table} " +
					$"( {DBNames.FilesFieldNameFileName}, {DBNames.FilesFieldNameContentType}, {DBNames.FilesFieldNameFileSize}, " +
					$"{DBNames.FilesFieldNameFile} ) " +
					$"{DBNames.SqlValues}" +
					$"( @{DBNames.FilesFieldNameFileName}, @{DBNames.FilesFieldNameContentType}, @{DBNames.FilesFieldNameFileSize}, " +
					$"@{DBNames.FilesFieldNameFile} )";

				using MySqlCommand cmd = new(sqlQuery, connection);

				cmd.Connection = connection;
				cmd.CommandText = sqlQuery;
				cmd.Parameters.AddWithValue ( $"@{DBNames.FilesFieldNameFileName}", _fileName );
				cmd.Parameters.AddWithValue ( $"@{DBNames.FilesFieldNameContentType}", _fileType );
				cmd.Parameters.AddWithValue ( $"@{DBNames.FilesFieldNameFileSize}", fileSize );
				cmd.Parameters.AddWithValue ( $"@{DBNames.FilesFieldNameFile}", rawData );

				cmd.ExecuteNonQuery ();
				}
			catch ( MySql.Data.MySqlClient.MySqlException ex )
				{
				System.Windows.Forms.MessageBox.Show ( "Error " + ex.Number + " is opgetreden: " + ex.Message,
					"Error", ( MessageBoxButtons ) MessageBoxButton.OK, ( MessageBoxIcon ) MessageBoxImage.Error );
				}
			}
		#endregion

		#region Replace an existing file in the database with a new file
		public static void Update ( string _table, int _fileId, string _path )
			{
			int fileSize;
			string sqlQuery;
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

				sqlQuery = $"" +
					$"{DBNames.SqlUpdate}{DBNames.Database}.{_table}" +
					$"{DBNames.SqlSet}" +
					$"( {DBNames.FilesFieldNameFile} = '@{DBNames.FilesFieldNameFile}',  {DBNames.FilesFieldNameFileSize} = '@{DBNames.FilesFieldNameFileSize}')" +
					$"{DBNames.SqlWhere}" +
					$"{DBNames.FilesFieldNameId} = '@{DBNames.FilesFieldNameId}'";

				using MySqlCommand cmd = new(sqlQuery, connection);

				cmd.Connection = connection;
				cmd.CommandText = sqlQuery;
				cmd.Parameters.AddWithValue ( $"@{DBNames.FilesFieldNameFileSize}", fileSize );
				cmd.Parameters.AddWithValue ( $"@{DBNames.FilesFieldNameFile}", rawData );
				cmd.Parameters.AddWithValue ( $"@{DBNames.FilesFieldNameId}", _fileId );

				cmd.ExecuteNonQuery ();
				}
			catch ( MySql.Data.MySqlClient.MySqlException ex )
				{
				System.Windows.Forms.MessageBox.Show ( "Error " + ex.Number + " is opgetreden: " + ex.Message,
					"Error", ( MessageBoxButtons ) MessageBoxButton.OK, ( MessageBoxIcon ) MessageBoxImage.Error );
				}
			}
		#endregion

		#region Check existence of DownloadPath, if not exists create it
		public static void CheckFolder ( string _path )
			{
			if ( !Directory.Exists ( _path ) )
				{
				Directory.CreateDirectory ( _path );
				}
			}
		#endregion

		#region Download File
		public static void DownloadFile ( int _fileId, string _fileTable, string _filePathSuffix, string _fileName )
			{
			// _fileTable should be PDF, Capella, MuseScore etc, it will be used to get the correct File
			string? _downloadPath, selectQuery;
			
			_downloadPath = $"{ScoreUsers.SelectedUserDownloadFolder}\\{_filePathSuffix}";
			CheckFolder ( _downloadPath );

			selectQuery = $"{DBNames.SqlSelect}File{DBNames.SqlFrom}{DBNames.Database}.{_fileTable.ToLower ()}{DBNames.SqlWhere}Id = @Id;";

			// Verbinding maken met de MySQL-database
			using ( MySqlConnection connection = new MySqlConnection ( DBConnect.ConnectionString ) )
				{
				connection.Open ();

				using ( MySqlCommand command = new MySqlCommand ( selectQuery, connection ) )
					{
					command.Parameters.AddWithValue ( $"@Id", _fileId );

					using ( MySqlDataReader reader = command.ExecuteReader ( CommandBehavior.SingleRow ) )
						{
						if ( reader.Read () )
							{
							using ( FileStream fileStream = new ( @$"{_downloadPath}\{_fileName}", FileMode.Create, FileAccess.Write ) )
								{
								using ( BinaryWriter binaryWriter = new ( fileStream ) )
									{
									long startIndex = 0;
									const int bufferSize = 1024;
									byte[] buffer = new byte[bufferSize];

									long bytesRead = reader.GetBytes(0, startIndex, buffer, 0, bufferSize);

									while ( bytesRead == bufferSize )
										{
										binaryWriter.Write ( buffer );
										binaryWriter.Flush ();

										startIndex += bufferSize;
										bytesRead = reader.GetBytes ( 0, startIndex, buffer, 0, bufferSize );
										}

									// Schrijf de resterende bytes naar het bestand
									binaryWriter.Write ( buffer, 0, ( int ) bytesRead );
									binaryWriter.Flush ();
									}
								}
							}
						}
					}
				connection.Close ();
				}
			}
		#endregion
		}
	}

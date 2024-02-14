using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

using MySql.Data.MySqlClient;

using Org.BouncyCastle.Asn1.Pkcs;

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
		}
	}
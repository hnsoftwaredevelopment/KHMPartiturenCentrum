using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

using MySql.Data.MySqlClient;

namespace KHM.Helpers
{
    public static class Files
    {
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

        public static void Update ( string _table, int _fileId, string _path)
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
		}
}

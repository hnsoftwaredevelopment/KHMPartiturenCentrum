using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

using MySql.Data.MySqlClient;

namespace KHM.Helpers
{
    public static class Library
    {
        public static void UpdateFiles ( int _scoreId, string _fieldName, int _fileExists )
            {
			// If file does not exist anymore value should be set to -1
			var sqlQuery = $"" +
				$"{DBNames.SqlUpdate}{DBNames.Database}.{DBNames.ScoresTable} " +
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
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

using Org.BouncyCastle.Asn1.Pkcs;

namespace KHM.Helpers
{
	public static class GetFileInfo
	{
		public static int Id(string _table, int _scoreId, string _fileType = "")
			{
			var fileId = -1;
			if(_fileType=="")
			{
				var sqlQuery = $"" +
				$"{DBNames.SqlSelect}{DBNames.FilesFieldNameId}" +
				$"{DBNames.SqlFrom}{DBNames.Database}.{_table}" +
				$"{DBNames.SqlWhere}" +
				$"{DBNames.FilesFieldNameScoreId} = '{_scoreId}'";
			}
			else
				{
				var sqlQuery = $"" +
				$"{DBNames.SqlSelect}{DBNames.FilesFieldNameId}" +
				$"{DBNames.SqlFrom}{DBNames.Database}.{_table}" +
				$"{DBNames.SqlWhere}" +
				$"{DBNames.FilesFieldNameScoreId} = '{_scoreId}'" +
				$"{DBNames.SqlAnd}" +
				$"{DBNames.FilesFieldNameContentType} = '{_fileType}'";
				}

			using MySqlConnection connection = new(DBConnect.ConnectionString);
			connection.Open ();

			using MySqlCommand cmd = new(sqlQuery, connection);
			using MySqlCommand cmd2 = new(query2, connection);

			try { fileId = (int)cmd.ExecuteScalar(); }
			catch { return -1; }
			
			return fileId;
			}

		public static string FileIndexField (string _extensionType, string _fileType)
			{
			var FieldName = "";

			switch ( _extensionType.ToLower () )
				{
				// MuseScore file
				case "mscz":
					switch ( _fileType.ToLower () )
						{
						case "orp":
							FieldName = DBNames.ScoresFieldNameMuseScoreORP;
							break;
						case "ork":
							FieldName = DBNames.ScoresFieldNameMuseScoreORK;
							break;
						case "top":
							FieldName = DBNames.ScoresFieldNameMuseScoreTOP;
							break;
						case "tok":
							FieldName = DBNames.ScoresFieldNameMuseScoreTOK;
							break;
						}
					break;
				
				// PDF File
				case "pdf":
					switch ( _fileType.ToLower () )
						{
						case "orp":
							FieldName = DBNames.ScoresFieldNamePDFORP;
							break;
						case "ork":
							FieldName = DBNames.ScoresFieldNamePDFORK;
							break;
						case "top":
							FieldName = DBNames.ScoresFieldNamePDFTOP;
							break;
						case "tok":
							FieldName = DBNames.ScoresFieldNamePDFTOK;
							break;
						case "pia":
							FieldName = DBNames.ScoresFieldNamePDFPIA;
							break;
						}
					break;

				//MP3 Instrumental file
				case "mp3":
					switch ( _fileType.ToLower () )
						{
						case "b1":
							FieldName = DBNames.ScoresFieldNameMP3B1;
							break;
						case "b2":
							FieldName = DBNames.ScoresFieldNameMP3B2;
							break;
						case "t1":
							FieldName = DBNames.ScoresFieldNameMP3T1;
							break;
						case "t2":
							FieldName = DBNames.ScoresFieldNameMP3T2;
							break;
						case "sol1":
							FieldName = DBNames.ScoresFieldNameMP3SOL1;
							break;
						case "sol2":
							FieldName = DBNames.ScoresFieldNameMP3SOL2;
							break;
						case "tot":
							FieldName = DBNames.ScoresFieldNameMP3TOT;
							break;
						case "pia":
							FieldName = DBNames.ScoresFieldNameMP3PIA;
							break;
						}
					break;

				//MP3 Voice file
				case "voice":
					switch ( _fileType.ToLower () )
						{
						case "b1":
							FieldName = DBNames.ScoresFieldNameMP3B1Voice;
							break;
						case "b2":
							FieldName = DBNames.ScoresFieldNameMP3B2Voice;
							break;
						case "t1":
							FieldName = DBNames.ScoresFieldNameMP3T1Voice;
							break;
						case "t2":
							FieldName = DBNames.ScoresFieldNameMP3T2Voice;
							break;
						case "sol1":
							FieldName = DBNames.ScoresFieldNameMP3SOL1Voice;
							break;
						case "sol2":
							FieldName = DBNames.ScoresFieldNameMP3SOL2Voice;
							break;
						case "tot":
							FieldName = DBNames.ScoresFieldNameMP3TOTVoice;
							break;
						case "uitv":
							FieldName = DBNames.ScoresFieldNameMP3UITVVoice;
							break;
						}

					break;
				}

			return FieldName;
			}
	}

}

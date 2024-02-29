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
			var sqlQuery = "";
			var fileId = -1;

			if(_fileType=="")
			{
				sqlQuery = $"" +
				$"{DBNames.SqlSelect}{DBNames.FilesFieldNameId}" +
				$"{DBNames.SqlFrom}{DBNames.Database}.{_table}" +
				$"{DBNames.SqlWhere}" +
				$"{DBNames.FilesFieldNameScoreId} = {_scoreId}";
			}
			else
				{
				sqlQuery = $"" +
				$"{DBNames.SqlSelect}{DBNames.FilesFieldNameId}" +
				$"{DBNames.SqlFrom}{DBNames.Database}.{_table}" +
				$"{DBNames.SqlWhere}" +
				$"{DBNames.FilesFieldNameScoreId} = {_scoreId}" +
				$"{DBNames.SqlAnd}" +
				$"{DBNames.FilesFieldNameContentType} = '{_fileType}'";
				}

			using MySqlConnection connection = new(DBConnect.ConnectionString);
			connection.Open ();

			using MySqlCommand cmd = new(sqlQuery, connection);

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
							FieldName = DBNames.FilesIndexFieldNameMSCORPId;
							break;
						case "ork":
							FieldName = DBNames.FilesIndexFieldNameMSCORKId;
							break;
						case "top":
							FieldName = DBNames.FilesIndexFieldNameMSCTOPId;
							break;
						case "tok":
							FieldName = DBNames.FilesIndexFieldNameMSCTOKId;
							break;
						}
					break;
				
				// PDF File
				case "pdf":
					switch ( _fileType.ToLower () )
						{
						case "orp":
							FieldName = DBNames.FilesIndexFieldNamePDFORPId;
							break;
						case "ork":
							FieldName = DBNames.FilesIndexFieldNamePDFORKId;
							break;
						case "top":
							FieldName = DBNames.FilesIndexFieldNamePDFTOPId;
							break;
						case "tok":
							FieldName = DBNames.FilesIndexFieldNamePDFTOKId;
							break;
						case "pia":
							FieldName = DBNames.FilesIndexFieldNamePDFPIAId;
							break;
						}
					break;

				//MP3 Instrumental file
				case "mp3":
					switch ( _fileType.ToLower () )
						{
						case "b1":
							FieldName = DBNames.FilesIndexFieldNameMP3B1Id;
							break;
						case "b2":
							FieldName = DBNames.FilesIndexFieldNameMP3B2Id;
							break;
						case "t1":
							FieldName = DBNames.FilesIndexFieldNameMP3T1Id;
							break;
						case "t2":
							FieldName = DBNames.FilesIndexFieldNameMP3T2Id;
							break;
						case "sol1":
							FieldName = DBNames.FilesIndexFieldNameMP3SOL1Id;
							break;
						case "sol2":
							FieldName = DBNames.FilesIndexFieldNameMP3SOL2Id;
							break;
						case "tot":
							FieldName = DBNames.FilesIndexFieldNameMP3TOTId;
							break;
						case "pia":
							FieldName = DBNames.FilesIndexFieldNameMP3PIAId;
							break;
						}
					break;

				//MP3 Voice file
				case "voice":
					switch ( _fileType.ToLower () )
						{
						case "b1":
							FieldName = DBNames.FilesIndexFieldNameMP3B1VoiceId;
							break;
						case "b2":
							FieldName = DBNames.FilesIndexFieldNameMP3B2VoiceId;
							break;
						case "t1":
							FieldName = DBNames.FilesIndexFieldNameMP3T1VoiceId;
							break;
						case "t2":
							FieldName = DBNames.FilesIndexFieldNameMP3T2VoiceId;
							break;
						case "sol1":
							FieldName = DBNames.FilesIndexFieldNameMP3SOL1VoiceId;
							break;
						case "sol2":
							FieldName = DBNames.FilesIndexFieldNameMP3SOL2VoiceId;
							break;
						case "tot":
							FieldName = DBNames.FilesIndexFieldNameMP3TOTVoiceId;
							break;
						case "uitv":
							FieldName = DBNames.FilesIndexFieldNameMP3UITVVoiceId;
							break;
						}

					break;
				}

			return FieldName;
			}
	}

}

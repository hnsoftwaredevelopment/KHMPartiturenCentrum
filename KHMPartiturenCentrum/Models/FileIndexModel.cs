using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHM.Models
	{
	public class FileIndexModel
		{
		public int Id{get; set;}
		public int ScoreId {get; set;}
		public int MuseScoreORPId { get; set; }
		public int MuseScoreORKId { get; set; }
		public int MuseScoreTOPId { get; set; }
		public int MuseScoreTOKId { get; set; }
		public int PDFORPId { get; set; }
		public int PDFORKId { get; set; }
		public int PDFTOPId { get; set; }
		public int PDFTOKId { get; set; }
		public int PDFPIAId { get; set; }
		public int MP3B1Id { get; set; }
		public int MP3B2Id { get; set; }
		public int MP3T1Id { get; set; }
		public int MP3T2Id { get; set; }
		public int MP3SOL1Id { get; set; }
		public int MP3SOL2Id { get; set; }
		public int MP3TOTId { get; set; }
		public int MP3PIAId { get; set; }
		public int MP3B1VoiceId { get; set; }
		public int MP3B2VoiceId { get; set; }
		public int MP3T1VoiceId { get; set; }
		public int MP3T2VoiceId { get; set; }
		public int MP3SOL1VoiceId { get; set; }
		public int MP3SOL2VoiceId { get; set; }
		public int MP3TOTVoiceId { get; set; }
		public int MP3UITVVoiceId { get; set; }
		}
	}

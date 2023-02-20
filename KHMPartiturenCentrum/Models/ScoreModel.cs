using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHMPartiturenCentrum.Models;

public class ScoreModel
{
    public int ScoreId { get; set; }
    public string? ScoreNumber { get; set; } // Only the number
    public string? ScoreSubNumber { get; set; } // Only the SubNumber
    public string? Score { get; set; } // ScoreMainNumber and SubNumber combined (XXX-YY Id SubNr exists else XXX)
    public string? ScoreTitle { get; set; }
    public string? ScoreSubTitle { get; set; }
    public string? Composer { get; set; }
    public string? TextWriter{ get; set; }
    public string? Arranger { get; set; }
    public int RepertoireId { get; set; }
    public string? RepertoireName { get; set; }
    public int ArchiveId { get; set; }
    public string? ArchiveName { get; set; }
    public int GenreId { get; set; }
    public string? GenreName { get; set; }
    public int AccompanimentId { get; set; }
    public string? AccompanimentName { get; set; }
    public int LanguageId { get; set; }
    public string? LanguageName { get; set; }
    public DateOnly DateCreated { get; set; }
    public string? DateCreatedString { get; set; }
    public DateOnly DateModified { get; set; }
    public string? DateModifiedString { get; set; }
    public bool Check { get; set; }
    public int CheckInt { get; set; }
    public bool MuseScoreORP { get; set; }
    public int MuseScoreORPInt { get; set; }
    public bool MuseScoreORK { get; set; }
    public int MuseScoreORKInt { get; set; }
    public bool MuseScoreTOP { get; set; }
    public int MuseScoreTOPInt { get; set; }
    public bool MuseScoreTOK { get; set; }
    public int MuseScoreTOKInt { get; set; }
    public bool PDFORP { get; set; }
    public int PDFORPInt { get; set; }
    public bool PDFORK { get; set; }
    public int PDFORKInt { get; set; }
    public bool PDFTOP { get; set; }
    public int PDFTOPInt { get; set; }
    public bool PDFTOK { get; set; }
    public int PDFTOKInt { get; set; }
    public bool MP3B1 { get; set; }
    public int MP3B1Int { get; set; }
    public bool MP3B2 { get; set; }
    public int MP3B2Int { get; set; }
    public bool MP3T1 { get; set; }
    public int MP3T1Int { get; set; }
    public bool MP3T2 { get; set; }
    public int MP3T2Int { get; set; }
    public bool MP3SOL { get; set; }
    public int MP3SOLInt { get; set; }
    public bool MP3TOT { get; set; }
    public int MP3TOTInt { get; set; }
    public bool MP3PIA { get; set; }
    public int MP3PIAInt { get; set; }
    public bool MuseScoreOnline { get; set; }
    public int MuseScoreOnlineInt { get; set; }
    public bool ByHeart { get; set; }
    public int ByHeartInt { get; set; }
    public string? MusicPiece { get; set; }
    public string? Notes { get; set; }
    public int NumberScoresPublisher1 { get; set; }
    public string? Lyrics { get; set; }
    public int Publisher1Id { get; set; }
    public string? Publisher1Name { get; set; }
    public int NumberScoresPublisher2 { get; set; }
    public int Publisher2Id { get; set; }
    public string? Publisher2Name { get; set; }
    public int NumberScoresPublisher3 { get; set; }
    public int Publisher3Id { get; set; }
    public string? Publisher3Name { get; set; }
    public int NumberScoresPublisher4 { get; set; }
    public int Publisher4Id { get; set; }
    public string? Publisher4Name { get; set; }
    public int NumberScoresTotal { get; set; }
}

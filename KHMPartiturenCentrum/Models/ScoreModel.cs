using System;

namespace KHMPartiturenCentrum.Models;

public class ScoreModel
{
    public bool ByHeart { get; set; }
    public bool Checked { get; set; }
    public bool MP3B1 { get; set; }
    public bool MP3B2 { get; set; }
    public bool MP3PIA { get; set; }
    public bool MP3SOL { get; set; }
    public bool MP3T1 { get; set; }
    public bool MP3T2 { get; set; }
    public bool MP3TOT { get; set; }
    public bool MuseScoreOnline { get; set; }
    public bool MuseScoreORK { get; set; }
    public bool MuseScoreORP { get; set; }
    public bool MuseScoreTOK { get; set; }
    public bool MuseScoreTOP { get; set; }
    public bool PDFORK { get; set; }
    public bool PDFORP { get; set; }
    public bool PDFTOK { get; set; }
    public bool PDFTOP { get; set; }
    public DateOnly DateDigitized { get; set; }
    public DateOnly DateModified { get; set; }
    public int AccompanimentId { get; set; }
    public int ArchiveId { get; set; }
    public int ByHeartInt { get; set; }
    public int CheckInt { get; set; }
    public int GenreId { get; set; }
    public int LanguageId { get; set; }
    public int MP3B1Int { get; set; }
    public int MP3B2Int { get; set; }
    public int MP3PIAInt { get; set; }
    public int MP3SOLInt { get; set; }
    public int MP3T1Int { get; set; }
    public int MP3T2Int { get; set; }
    public int MP3TOTInt { get; set; }
    public int MuseScoreOnlineInt { get; set; }
    public int MuseScoreORKInt { get; set; }
    public int MuseScoreORPInt { get; set; }
    public int MuseScoreTOKInt { get; set; }
    public int MuseScoreTOPInt { get; set; }
    public int AmountPublisher1 { get; set; }
    public int AmountPublisher2 { get; set; }
    public int AmountPublisher3 { get; set; }
    public int AmountPublisher4 { get; set; }
    public int AmountTotal { get; set; }
    public int PDFORKInt { get; set; }
    public int PDFORPInt { get; set; }
    public int PDFTOKInt { get; set; }
    public int PDFTOPInt { get; set; }
    public int Publisher1Id { get; set; }
    public int Publisher2Id { get; set; }
    public int Publisher3Id { get; set; }
    public int Publisher4Id { get; set; }
    public int RepertoireId { get; set; }
    public int ScoreId { get; set; }
    public string? AccompanimentName { get; set; }
    public string? ArchiveName { get; set; }
    public string? Arranger { get; set; }
    public string? Composer { get; set; }
    public string? DateCreatedString { get; set; }
    public string? DateModifiedString { get; set; }
    public string? GenreName { get; set; }
    public string? LanguageName { get; set; }
    public string? Lyrics { get; set; }
    public string? MusicPiece { get; set; }
    public string? Notes { get; set; }
    public string? Publisher1Name { get; set; }
    public string? Publisher2Name { get; set; }
    public string? Publisher3Name { get; set; }
    public string? Publisher4Name { get; set; }
    public string? RepertoireName { get; set; }
    public string? Score { get; set; } // ScoreMainNumber and SubNumber combined (XXX-YY Id SubNr exists else XXX)
    public string? ScoreNumber { get; set; } // Only the number
    public string? ScoreSubNumber { get; set; } // Only the SubNumber
    public string? ScoreSubTitle { get; set; }
    public string? ScoreTitle { get; set; }
    public string? Textwriter { get; set; }
    public int Duration { get; set; }
    public string SearchField { get; set; }
}

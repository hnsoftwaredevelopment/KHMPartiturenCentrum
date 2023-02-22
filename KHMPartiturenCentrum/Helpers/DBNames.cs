namespace KHMPartiturenCentrum.Helpers;

public class DBNames
{
    #region Sql commands
    public static readonly string SqlSelect = "SELECT ";
    public static readonly string SqlSelectAll = "SELECT *";
    public static readonly string SqlDelete = "DELETE ";
    public static readonly string SqlSelectDistinct = "SELECT DISTINCT ";
    public static readonly string SqlCast = "CAST( ";
    public static readonly string SqlMax = "MAX( ";
    public static readonly string SqlMin = "MIN( ";
    public static readonly string SqlUnsigned = " as UNSIGNED) ";
    public static readonly string SqlDeleteFrom = "DELETE FROM ";
    public static readonly string SqlInsert = "INSERT INTO ";
    public static readonly string SqlUpdate = "UPDATE ";
    public static readonly string SqlFrom = " FROM ";
    public static readonly string SqlWhere = " WHERE ";
    public static readonly string SqlValues = " VALUES ";
    public static readonly string SqlOrder = " ORDER BY ";
    public static readonly string SqlSet = " SET ";
    public static readonly string SqlAnd = " AND ";
    public static readonly string SqlOr = " OR ";
    public static readonly string SqlIsNull = " IS NULL ";
    public static readonly string SqlCount = " COUNT( ";
    public static readonly string SqlCountAll = " COUNT(*) ";
    public static readonly string SqlBetween = " BETWEEN ";
    #endregion

    #region Database
    public static readonly string Database = "KHMMuziekbibliotheek";
    public static readonly string UsersDatabase = "KHM";
    #endregion

    #region Table/View Accompaniment
    public static readonly string AccompanimentsTable = "Begeleiding";
    public static readonly string AccompanimentsFieldNameId = "ArchiveId";
    public static readonly string AccompanimentsFieldNameName = "Begeleiding";

    public static readonly string AccompanimentsFieldTypeId = "int";
    public static readonly string AccompanimentsFieldTypeName = "varchar";
    #endregion

    #region Table/View Archive
    public static readonly string ArchivesTable = "Archief";
    public static readonly string ArchivesFieldNameId = "ArchiveId";
    public static readonly string ArchivesFieldNameName = "Genre";

    public static readonly string ArchivesFieldTypeId = "int";
    public static readonly string ArchivesFieldTypeName = "varchar";
    #endregion

    #region Table/View Genre
    public static readonly string GenresTable = "Genre";
    public static readonly string GenresFieldNameId = "ArchiveId";
    public static readonly string GenresFieldNameName = "Genre";

    public static readonly string GenresFieldTypeId = "int";
    public static readonly string GenresFieldTypeName = "varchar";
    #endregion

    #region Table/View Repertoire
    public static readonly string RepertoiresTable = "Repertoire";
    public static readonly string RepertoiresFieldNameId = "ArchiveId";
    public static readonly string RepertoiresFieldNameName = "Repertoire";

    public static readonly string repertoiresFieldTypeId = "int";
    public static readonly string repertoiresFieldTypeName = "varchar";
    #endregion

    #region Table/View Languages
    public static readonly string LanguagesTable = "Taal";
    public static readonly string LanguagesFieldNameId = "ArchiveId";
    public static readonly string LanguagesFieldNameName = "Taal";

    public static readonly string LanguagesFieldTypeId = "int";
    public static readonly string LanguagesFieldTypeName = "varchar";
    #endregion

    #region Table/View Publisher
    public static readonly string PublishersTable = "Uitgever";
    public static readonly string PublishersFieldNameId = "Id";
    public static readonly string PublishersFieldNameName = "Naam";
    public static readonly string PublishersFieldNameAddress1 = "Adres1";
    public static readonly string PublishersFieldNameAddress2 = "Adres2";
    public static readonly string PublishersFieldNameZip = "Postcode";
    public static readonly string PublishersFieldNameCity = "Plaats";
    public static readonly string PublishersFieldNamePhone = "Telefoon";
    public static readonly string PublishersFieldNameURL = "Website";
    public static readonly string PublishersFieldNameCustomerNumber = "Klantnummer";
    public static readonly string PublishersFieldNameMemo = "Notities";

    public static readonly string PublishersFieldTypeId = "int";
    public static readonly string PublishersFieldTypeName = "varchar";
    public static readonly string PublishersFieldTypeAddress1 = "varchar";
    public static readonly string PublishersFieldTypeAddress2 = "varchar";
    public static readonly string PublishersFieldTypeZip = "varchar";
    public static readonly string PublishersFieldTypeCity = "varchar";
    public static readonly string PublishersFieldTypePhone = "varchar";
    public static readonly string PublishersFieldTypeURL = "varchar";
    public static readonly string PublishersFieldTypeCustomerNumber = "varchar";
    public static readonly string PublishersFieldTypeMemo = "mediumtext";
    #endregion

    #region Table/View Scores
    public static readonly string ScoresTable = "Bibliotheek";
    public static readonly string ScoresFieldNameId = "Id";
    public static readonly string ScoresFieldNameArchiveId = "ArchiefId";
    public static readonly string ScoresFieldNameRepertoireId = "RepertoireId";
    public static readonly string ScoresFieldNameScoreNumber = "Partituur";
    public static readonly string ScoresFieldNameScoreSubNumber = "SubNummer";
    public static readonly string ScoresFieldNameTitle = "Titel";
    public static readonly string ScoresFieldNameSubTitle = "Ondertitel";
    public static readonly string ScoresFieldNameComposer = "Componist";
    public static readonly string ScoresFieldNameTextwriter = "Tekstschrijver";
    public static readonly string ScoresFieldNameArranger = "Arrangement";
    public static readonly string ScoresFieldNameLanguageId = "TaalId";
    public static readonly string ScoresFieldNameGenreId = "GenreId";
    public static readonly string ScoresFieldNameLyrics = "Lyrics";
    public static readonly string ScoresFieldNameChecked = "Gecontroleerd";
    public static readonly string ScoresFieldNameDigitized = "Gedigitaliseerd";
    public static readonly string ScoresFieldNameModified = "Revisie";
    public static readonly string ScoresFieldNameAccompanimentId = "BegeleidingId";
    public static readonly string ScoresFieldNamePDFORP = "PDFORP";
    public static readonly string ScoresFieldNamePDFORK = "PDFORK";
    public static readonly string ScoresFieldNamePDFTOP = "PDFTOP";
    public static readonly string ScoresFieldNamePDFTOK = "PDFTOK";
    public static readonly string ScoresFieldNameMuseScoreORP = "MuseScoreORP";
    public static readonly string ScoresFieldNameMuseScoreORK = "MuseScoreORK";
    public static readonly string ScoresFieldNameMuseScoreTOP = "MuseScoreTOP";
    public static readonly string ScoresFieldNameMuseScoreTOK = "MuseScoreTOK";
    public static readonly string ScoresFieldNameMP3TOT = "MP3TOT";
    public static readonly string ScoresFieldNameMP3T1 = "MP3T1";
    public static readonly string ScoresFieldNameMP3T2 = "MP3T2";
    public static readonly string ScoresFieldNameMP3B1 = "MP3B1";
    public static readonly string ScoresFieldNameMP3B2 = "MP3B2";
    public static readonly string ScoresFieldNameMP3SOL = "MP3SOL";
    public static readonly string ScoresFieldNameMP3PIA = "MP3PIA";
    public static readonly string ScoresFieldNameOnline = "Online";
    public static readonly string ScoresFieldNameByHeart = "UHH";
    public static readonly string ScoresFieldNameMusicPiece = "Muziekstuk";
    public static readonly string ScoresFieldNameNotes = "Opmerkingen";
    public static readonly string ScoresFieldNameAmountPublisher1 = "AantalUitgever1";
    public static readonly string ScoresFieldNameAmountPublisher2 = "AantalUitgever2";
    public static readonly string ScoresFieldNameAmountPublisher3 = "AantalUitgever3";
    public static readonly string ScoresFieldNameAmountPublisher4 = "AantalUitgever4";
    public static readonly string ScoresFieldNamePublisher1Id = "Uitgever1Id";
    public static readonly string ScoresFieldNamePublisher2Id = "Uitgever2Id";
    public static readonly string ScoresFieldNamePublisher3Id = "Uitgever3Id";
    public static readonly string ScoresFieldNamePublisher4Id = "Uitgever4Id";

    public static readonly string ScoresFieldTypeId = "int";
    public static readonly string ScoresFieldTypeArchiveNaam = "int";
    public static readonly string ScoresFieldTypeRepertoireNaam = "int";
    public static readonly string ScoresFieldTypeScoreNumber = "varchar";
    public static readonly string ScoresFieldTypeScoreSubNumber = "varchar";
    public static readonly string ScoresFieldTypeTitle = "varchar";
    public static readonly string ScoresFieldTypeSubTitle = "varchar";
    public static readonly string ScoresFieldTypeComposer = "varchar";
    public static readonly string ScoresFieldTypeTextwriter = "varchar";
    public static readonly string ScoresFieldTypeArranger = "varchar";
    public static readonly string ScoresFieldTypeLanguageNaam = "int";
    public static readonly string ScoresFieldTypeGenreNaam = "int";
    public static readonly string ScoresFieldTypeLyrics = "mediumtext";
    public static readonly string ScoresFieldTypeChecked = "int";
    public static readonly string ScoresFieldTypeDigitized = "date";
    public static readonly string ScoresFieldTypeModified = "date";
    public static readonly string ScoresFieldTypeAccompanimentNaam = "int";
    public static readonly string ScoresFieldTypePDFORP = "int";
    public static readonly string ScoresFieldTypePDFORK = "int";
    public static readonly string ScoresFieldTypePDFTOP = "int";
    public static readonly string ScoresFieldTypePDFTOK = "int";
    public static readonly string ScoresFieldTypeMuseScoreORP = "int";
    public static readonly string ScoresFieldTypeMuseScoreORK = "int";
    public static readonly string ScoresFieldTypeMuseScoreTOP = "int";
    public static readonly string ScoresFieldTypeMuseScoreTOK = "int";
    public static readonly string ScoresFieldTypeMP3B1 = "int";
    public static readonly string ScoresFieldTypeMP3B2 = "int";
    public static readonly string ScoresFieldTypeMP3T1 = "int";
    public static readonly string ScoresFieldTypeMP3T2 = "int";
    public static readonly string ScoresFieldTypeMP3SOL = "int";
    public static readonly string ScoresFieldTypeMP3TOT = "int";
    public static readonly string ScoresFieldTypeMP3PIA = "int";
    public static readonly string ScoresFieldTypeOnline = "int";
    public static readonly string ScoresFieldTypeByHeart = "int";
    public static readonly string ScoresFieldTypeMusicPiece = "varchar";
    public static readonly string ScoresFieldTypeNotes = "mediumtext";
    public static readonly string ScoresFieldTypeAmountPublisher1 = "int";
    public static readonly string ScoresFieldTypeAmountPublisher2 = "int";
    public static readonly string ScoresFieldTypeAmountPublisher3 = "int";
    public static readonly string ScoresFieldTypeAmountPublisher4 = "int";
    public static readonly string ScoresFieldTypePublisher1Naam = "int";
    public static readonly string ScoresFieldTypePublisher2Naam = "int";
    public static readonly string ScoresFieldTypePublisher3Naam = "int";
    public static readonly string ScoresFieldTypePublisher4Naam = "int";

    public static readonly string ScoresView = "Bibliotheek_View";
    public static readonly string ScoresViewFieldNameScore = "PartituurNummer";
    public static readonly string ScoresViewFieldNameArchiveName = "ArchiefNaam";
    public static readonly string ScoresViewFieldNameRepertoireName = "RepertoireNaam";
    public static readonly string ScoresViewFieldNameLanguageName = "TaalNaam";
    public static readonly string ScoresViewFieldNameGenreName = "GenreNaam";
    public static readonly string ScoresViewFieldNameAccompanimentName = "BegeleidingNaam";
    public static readonly string ScoresViewFieldNamePublisher1Name = "Uitgever1Naam";
    public static readonly string ScoresViewFieldNamePublisher2Name = "Uitgever2Naam";
    public static readonly string ScoresViewFieldNamePublisher3Name = "Uitgever3Naam";
    public static readonly string ScoresViewFieldNamePublisher4Name = "Uitgever4Naam";

    public static readonly string ScoresViewFieldTypeScore = "varchar";
    public static readonly string ScoresViewFieldTypeArchiveName = "varchar";
    public static readonly string ScoresViewFieldTypeRepertoireName = "varchar";
    public static readonly string ScoresViewFieldTypeLanguageName = "varchar";
    public static readonly string ScoresViewFieldTypeGenreName = "varchar";
    public static readonly string ScoresViewFieldTypeAccompanimentName = "varchar";
    public static readonly string ScoresViewFieldTypePublisher1Name = "varchar";
    public static readonly string ScoresViewFieldTypePublisher2Name = "varchar";
    public static readonly string ScoresViewFieldTypePublisher3Name = "varchar";
    public static readonly string ScoresViewFieldTypePublisher4Name = "varchar";
    #endregion

    #region Table/View Scores
    public static readonly string NewScoresView = "NewScoresView";
    // Fields and types are same as for the ScoreNumber table/view
    #endregion

    #region Table/View Available Scores (Non Christmas)
    public static readonly string AvailableScoresView = "AvailableScoresView";
    public static readonly string AvailableScoresFieldNameId = "Id";
    public static readonly string AvailableScoresFieldNameNumber = "Partituur";

    public static readonly string AvailableScoresFieldTypeId = "int";
    public static readonly string AvailableScoresFieldTypeNumber = "varchar";
    #endregion

    #region Table/View Available Scores (Christmas)
    public static readonly string AvailableChristmasScoresView = "AvailableChristmasScoresView";
    public static readonly string AvailableChristmasScoresFieldNameId = "Id";
    public static readonly string AvailableChristmasScoresFieldNameNumber = "Partituur";

    public static readonly string AvailableChristmasScoresFieldTypeId = "int";
    public static readonly string AvailableChristmasScoresFieldTypeNumber = "varchar";
    #endregion

    #region Table/View Available Numbers (All available scores Christmas and Non Christmas)
    public static readonly string AvailableNumbersView = "AvailableNumbersView";
    public static readonly string AvailableNumbersFieldNameId = "Id";
    public static readonly string AvailableNumbersFieldNameNumber = "Partituur";

    public static readonly string AvailableNumbersFieldTypeId = "int";
    public static readonly string AvailableNumbersFieldTypeNumber = "varchar";
    #endregion

    #region Table/View Users (From KHM User Database)
    public static readonly string UsersTable = "Users";
    public static readonly string UsersFieldNameId = "ArchiveId";
    public static readonly string UsersFieldNameUserName = "E-Mail";
    public static readonly string UsersFieldNameFullName = "Fullname";
    public static readonly string UsersFieldNamePW = "Password";
    public static readonly string UsersFieldNameRoleId = "RoleId";

    public static readonly string UsersFieldTypeId = "int";
    public static readonly string UsersFieldTypeUserName = "varchar";
    public static readonly string UsersFieldTypeFullName = "varchar";
    public static readonly string UsersFieldTypePW = "varchar";
    public static readonly string UsersFieldTypeRoleId = "int";

    public static readonly string UsersView = "view_Users";
    public static readonly string UsersViewFieldNameRoleName = "RoleText";

    public static readonly string UsersViewFieldTypeRoleName = "varchar";
    #endregion

    #region Table/View Settings
    public static readonly string SettingsTable= "Settings";
    #endregion

    #region Log history
    #region Table/View History
    public static readonly string LogKHMTable = "History";
    public static readonly string LogKHMFieldNameNaam = "Naam";
    public static readonly string LogKHMFieldNameAction = "Action";
    public static readonly string LogKHMFieldNameTimeStamp = "TimeStamp";
    public static readonly string LogKHMFieldNameReleaseNaam = "ReleaseNaam";
    public static readonly string LogKHMFieldNameKHMNaam = "KHMNaam";
    public static readonly string LogKHMFieldNameResult = "Result";

    public static readonly string LogKHMFieldTypeNaam = "int";
    public static readonly string LogKHMFieldTypeAction = "varchar";
    public static readonly string LogKHMFieldTypeTimeStamp = "datetime";
    public static readonly string LogKHMFieldTypeReleaseNaam = "int";
    public static readonly string LogKHMFieldTypeKHMNaam = "int";
    public static readonly string LogKHMFieldTypeResult = "varchar";

    public static readonly string LogKHMView = "History";
    public static readonly string LogKHMFieldNameReleaseName = "ReleaseName";
    public static readonly string LogKHMFieldNameKHMName = "KHMName";

    public static readonly string LogKHMFieldTypeReleaseName = "varchar";
    public static readonly string LogKHMFieldTypeKHMName = "varchar";
    #endregion

    #region Log Actions
    public static readonly string LogKHMAdded = "Partituur toegevoegd";
    public static readonly string LogKHMChanged = "Partituur gewijzigd";
    public static readonly string LogKHMDeleted = "Partituur verwijderd";
    #endregion
    #endregion
}

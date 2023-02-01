namespace KHMPartiturenCentrum.Helpers;

public class DBNames
{
    #region Sql commands
    public static readonly string SqlSelect = "SELECT ";
    public static readonly string SqlSelectAll = "SELECT *";
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
    public static readonly string SqlCount = "COUNT";
    public static readonly string SqlCountAll = "COUNT(*)";
    public static readonly string SqlBetween = " BETWEEN ";
    #endregion

    #region Database
    public static readonly string Database = "AlureFactsheets";
    #endregion

    #region Table/View Accompaniment
    public static readonly string AccompanimentsTable = "Begeleiding";
    public static readonly string AccompanimentsFieldNameId = "Id";
    public static readonly string AccompanimentsFieldNameName = "Begeleiding";

    public static readonly string AccompanimentsFieldTypeId = "int";
    public static readonly string AccompanimentsFieldTypeName = "varchar";
    #endregion

    #region Table/View Genre
    public static readonly string GenresTable = "Genre";
    public static readonly string GenresFieldNameId = "Id";
    public static readonly string GenresFieldNameName = "Genre";

    public static readonly string GenresFieldTypeId = "int";
    public static readonly string GenresFieldTypeName = "varchar";
    #endregion

    #region Table/View Repertoire
    public static readonly string RepertoiresTable = "Genre";
    public static readonly string RepertoiresFieldNameId = "Id";
    public static readonly string RepertoiresFieldNameName = "Repertoire";

    public static readonly string repertoiresFieldTypeId = "int";
    public static readonly string repertoiresFieldTypeName = "varchar";
    #endregion

    #region Table/View Languages
    public static readonly string LanguagesTable = "Taal";
    public static readonly string LanguagesFieldNameId = "Id";
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
    public static readonly string ScoresFieldNameScoreTitle = "Titel";
    public static readonly string ScoresFieldNameScoreSubTitle = "Ondertitel";

    public static readonly string ScoresFieldTypeId = "int";
    public static readonly string ScoresFieldTypeArchiveId = "int";
    public static readonly string ScoresFieldTypeRepertoireId = "int";
    public static readonly string ScoresFieldTypeScoreNumber = "varchar";
    public static readonly string ScoresFieldTypeScoreTitle = "varchar";
    public static readonly string ScoresFieldTypeScoreSubTitle = "varchar";
    #endregion


    #region Table/View Settings
    public static readonly string SettingsTable= "Settings";
    public static readonly string SettingsFieldNameId = "Id";
    public static readonly string SettingsFieldNameFolder = "FactsheetsFolder";
    public static readonly string SettingsFieldNameTemplateFolder = "TemplateFolderName";
    public static readonly string SettingsFieldNameOutputFolder = "OutputFolderName";
    public static readonly string SettingsFieldNameServiceFolder = "FactsheetsServiceFolder";
    public static readonly string SettingsFieldNameMonitorFolder = "FactsheetsServiceMonitorFolder";

    public static readonly string SettingsFieldTypeId = "int";
    public static readonly string SettingsFieldTypeFolder = "varchar";
    public static readonly string SettingsFieldTypeTemplateFolder = "varchar";
    public static readonly string SettingsFieldTypeOutputFolder = "varchar";
    public static readonly string SettingsFieldTypeServiceFolder = "varchar";
    public static readonly string SettingsFieldTypeMonitorFolder = "varchar";
    #endregion

    #region Log history
    #region Table/View History
    public static readonly string LogFactsheetTable = "History";
    public static readonly string LogFactsheetFieldNameId = "Id";
    public static readonly string LogFactsheetFieldNameAction = "Action";
    public static readonly string LogFactsheetFieldNameTimeStamp = "TimeStamp";
    public static readonly string LogFactsheetFieldNameReleaseId = "ReleaseId";
    public static readonly string LogFactsheetFieldNameFactsheetId = "FactsheetId";
    public static readonly string LogFactsheetFieldNameResult = "Result";

    public static readonly string LogFactsheetFieldTypeId = "int";
    public static readonly string LogFactsheetFieldTypeAction = "varchar";
    public static readonly string LogFactsheetFieldTypeTimeStamp = "datetime";
    public static readonly string LogFactsheetFieldTypeReleaseId = "int";
    public static readonly string LogFactsheetFieldTypeFactsheetId = "int";
    public static readonly string LogFactsheetFieldTypeResult = "varchar";

    public static readonly string LogFactsheetView = "History";
    public static readonly string LogFactsheetFieldNameReleaseName = "ReleaseName";
    public static readonly string LogFactsheetFieldNameFactsheetName = "FactsheetName";

    public static readonly string LogFactsheetFieldTypeReleaseName = "varchar";
    public static readonly string LogFactsheetFieldTypeFactsheetName = "varchar";
    #endregion

    #region Log Actions
    public static readonly string LogFactsheetAdded = "Factsheet added";
    public static readonly string LogFactsheetChanged = "Factsheet modified";
    public static readonly string LogFactsheetDeleted = "Factsheet deleted";
    public static readonly string LogFactsheetGenerationStart = "Started Factsheet generation";
    public static readonly string LogFactsheetGenerationEnd = "Ended Factsheet generation";
    public static readonly string LogFactsheetGenerated = "Generated Factsheet";
    #endregion
    #endregion
}

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

    #region Table/View Factsheets
    public static readonly string FactsheetsTable= "Factsheets";
    public static readonly string FactsheetsFieldNameId = "Id";
    public static readonly string FactsheetsFieldNameFactsheetName = "FactsheetName";
    public static readonly string FactsheetsFieldNameDocumentName = "DocumentName";
    public static readonly string FactsheetsFieldNameStartRelease = "StartRelease";
    public static readonly string FactsheetsFieldNameEndRelease = "EndRelease";

    public static readonly string FactsheetsFieldTypeId = "int";
    public static readonly string FactsheetsFieldTypeFactsheetName = "varchar";
    public static readonly string FactsheetsFieldTypeDocumentName = "varchar";
    public static readonly string FactsheetsFieldTypeStartRelease = "int";
    public static readonly string FactsheetsFieldTypeEndRelease = "int";
    #endregion

    #region Table/View Generated Factsheets
    public static readonly string GeneratedFactsheetsTable= "GeneratedFactsheets";
    public static readonly string GeneratedFactsheetsFieldNameId = "Id";
    public static readonly string GeneratedFactsheetsFieldNameReleaseId = "ReleaseId";
    public static readonly string GeneratedFactsheetsFieldNameFactsheetId = "FactsheetId";
    public static readonly string GeneratedFactsheetsFieldNameGenerated = "Generated";

    public static readonly string GeneratedFactsheetsFieldTypeId = "int";
    public static readonly string GeneratedFactsheetsFieldTypeReleaseId = "int";
    public static readonly string GeneratedFactsheetsFieldTypeFactsheetId = "int";
    public static readonly string GeneratedFactsheetsFieldTypeGenerated = "date";
    #endregion

    #region Table/View Releases
    public static readonly string ReleasesTable= "Releases";
    public static readonly string ReleasesFieldNameId = "Id";
    public static readonly string ReleasesFieldNameName = "Release";
    public static readonly string ReleasesFieldNameYear = "Year";
    public static readonly string ReleasesFieldNameMonth = "Month";
    public static readonly string ReleasesFieldNameSubVersion = "SubVersion";

    public static readonly string ReleasesFieldTypeId = "int";
    public static readonly string ReleasesFieldTypeName = "varchar";
    public static readonly string ReleasesFieldTypeYear = "varchar";
    public static readonly string ReleasesFieldTypeMonth = "varchar";
    public static readonly string ReleasesFieldTypeSubVersion = "varchar";
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

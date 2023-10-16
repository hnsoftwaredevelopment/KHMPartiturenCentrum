using System.IO;
using System.Windows.Input;

namespace KHM.Views;
/// <summary>
/// Interaction logic for UploadFile.xaml
/// </summary>
public partial class UploadFile : Window
{
    ObservableCollection<FileUploadErrorModel> UploadErrorFiles = new();
    public UploadFile(string[] files)
    {
        InitializeComponent();

        if (files.Length == 1)
        {
            lblHeader.Content = "Bestand Uploaden";
        }

        foreach (var file in files)
        {
            tbCurrentFile.Text = $"Controleren: {file}";
            var ExtentionOk = CheckExtention(file);

            if (ExtentionOk != "")
            {
                UploadErrorFiles.Add(new FileUploadErrorModel { FileName = file, Reason = $"Ongeldig bestandstype ({ExtentionOk})" });
                //UploadedFilesErrorDataGrid.Items.Add ( file );
            }
            tbCurrentFile.Text = $"Uploaden: {file}";
        }

        //CheckFiles ( files );
    }

    public string CheckExtention(string _file)
    {
        var _result = "";

        if (_file != null)
        {
            var _fileName = Path.GetFileName(_file);
            string[] _fileNameSplitup = _fileName.ToLower().Split('.');

            if (_fileNameSplitup[1] != "pdf" && _fileNameSplitup[1] != "mscz" && _fileNameSplitup[1] != "mp3")
            {
                _result = _fileNameSplitup[1];
            }
        }
        return _result;
    }

    public void CheckFiles(string[] files)
    {
        var hasVoiceString = "(Ingezongen)";
        bool Exists, hasVoice;
        uint fileSize, copiedFileSize = 0;
        int scoreId;
        string scoreNumber, scoreTitleSuffix, scoreTitle, _fileName, _newFileName, scorePart, filePathName, fileType;
        //List<string> ErrorList = new();
        ObservableCollection<FileUploadOkModel> FilesUploadOk = new();
        ObservableCollection<FileUploadErrorModel> FilesUploadError = new();

        foreach (var file in files)
        {
            _fileName = System.IO.Path.GetFileName(file);
            filePathName = file;
            string[] fileNameSplitup = _fileName.Split('.');
            string[] fileInfo = fileNameSplitup[0].Split('-');

            fileType = fileNameSplitup[1].Trim().ToLower();
            scoreNumber = fileInfo[0].Trim().Substring(0, 3);

            // Check if file Contains SubNumber
            if (fileNameSplitup[0].Substring(3, 1) == "-")
            {
                scoreNumber += fileNameSplitup[0].Substring(3, 3);

                //The Documenttype also contains the sub number, trim this
                fileInfo[1] = fileInfo[1].Substring(2, fileInfo[1].Length - 3);
            }

            scoreTitle = DBCommands.GetScoreField(DBNames.ScoresView, "scoretitle", DBNames.ScoresViewFieldNameScore, scoreNumber);
            scoreId = int.Parse(DBCommands.GetScoreField(DBNames.ScoresView, "scoreid", DBNames.ScoresViewFieldNameScore, scoreNumber));
            if (scoreTitle != "")
            { Exists = true; }
            else
            { Exists = false; }

            // Check If ScoreNumber is a valid Number And if it is a valid file type And the uploaded Score Exists in the Library
            if (scoreId > 0 && (fileType.ToLower() == "mscz" || fileType.ToLower() == "pdf" || fileType.ToLower() == "mp3") && Exists)
            {
                scorePart = fileInfo[0].Trim().Substring(3);

                if (fileInfo[1].Trim().ToLower().Contains(hasVoiceString.Trim().ToLower()))
                {
                    hasVoice = true;
                    scoreTitleSuffix = " " + hasVoiceString;
                }
                else
                {
                    hasVoice = false;
                    scoreTitleSuffix = "";
                };

                scoreTitle += scoreTitleSuffix;

                fileSize = Convert.ToUInt32(new System.IO.FileInfo(file).Length);
                copiedFileSize += fileSize;
                FileInfo fileDetails = new FileInfo(file);

                _newFileName = $"{scoreNumber}{scorePart} - {scoreTitle}.{fileType}";

                Upload(filePathName, _newFileName, fileType, scoreId, scoreNumber, scorePart, hasVoice);
                FilesUploadOk.Add(new FileUploadOkModel { FileName = _fileName });
            }
            else
            {
                // Incorrect filename
                FilesUploadError.Add(new FileUploadErrorModel { FileName = _fileName, Reason = "Ongeldig bestand" });
            }
        }
    }
    public static void Upload(string _filePath, string _fileName, string _fileType, int _scoreId, string _scoreNumber, string _scorePart, bool _hasVoice)
    {
        string _tableName = "", _fieldName, _fieldNamePrefix = "";
        int _fieldId, _filesIndexId, _fileId;

        // Steps for adding a file to a database table
        // Determine file type to have the  right database table
        // CHeck if the file exists in the File Index
        // Store the file in the correct Database table (Insert on new, update on existing)
        // Get the Id of that newly added record
        // Store fileId in FileIndex Table

        switch (_fileType.ToLower())
        {
            case "mscz":
                _tableName = DBNames.FilesMSCTable;
                break;
            case "pdf":
                _tableName = DBNames.FilesPDFTable;
                break;
            case "mp3":
                switch (_hasVoice)
                {
                    case true:
                        _tableName = DBNames.FilesMP3VoiceTable;
                        _fieldNamePrefix = "voice";
                        break;
                    case false:
                        _tableName = DBNames.FilesMP3Table;
                        _fieldNamePrefix = "";
                        break;
                }
                break;
        }

        if (_tableName != "")
        {
            // Check if ScoreId exists in the FIlesIndex, if not a new record can be created
            _filesIndexId = DBCommands.GetFileIndexIfFromScoreId(_scoreId);

            if (_filesIndexId == -1)
            {
                // Record does not exist add it
                DBCommands.AddNewFileIndex(_scoreId);
                _filesIndexId = DBCommands.GetFileIndexIfFromScoreId(_scoreId);
            }

            // Check if the file to upload is already in the DataBase by checking Files Index 
            _fieldName = GetFieldName(_fileType.ToLower() + _scorePart.ToLower() + _fieldNamePrefix, "filesindex");
            _fieldId = DBCommands.GetFileIdFromFilesIndex(_filesIndexId, _fieldName);

            if (_fieldId == -1)
            {
                // New file to upload, not currently in database
                DBCommands.StoreFile(_tableName, _scoreId, _filePath, _fileName);
                _fileId = DBCommands.GetAddedFileId(_tableName);
            }
            else
            {
                // File in database has to be replaced
                DBCommands.UpdateFile(_tableName, _filePath, _fileName, _fieldId);
                _fileId = DBCommands.GetFileId(_tableName, DBNames.FilesFieldNameId, _fileName, _fieldId);
            }

            // Store the file Id in the FileIndex table
            DBCommands.UpdateFilesIndex(_fieldName, _fileId, DBNames.FilesIndexFieldNameId, _filesIndexId);

            // Set the correct Checkbox in the Library table, not needed for Voice files
            if (!_hasVoice)
            {
                var _libraryFieldName = GetFieldName(_fileType.ToLower() + _scorePart.ToLower() + _fieldNamePrefix, "library");

                DBCommands.UpdateLibraryForFile(_libraryFieldName, 1, DBNames.ScoresFieldNameId, _scoreId);
            }

        }
    }

    #region Give the FilesIndex FieldName to check based on FileType and ScorePart
    public static string GetFieldName(string _check, string CheckType)
    {
        // Determine what field to check depending on file type and file part
        string _fieldName = "", _libraryFieldName = "";

        switch (_check)
        {
            case "msczorp":
                _fieldName = DBNames.FilesIndexFieldNameMuseScoreORPId;
                _libraryFieldName = DBNames.ScoresFieldNameMuseScoreORP;
                break;
            case "msczork":
                _fieldName = DBNames.FilesIndexFieldNameMuseScoreORKId;
                _libraryFieldName = DBNames.ScoresFieldNameMuseScoreORK;
                break;
            case "mscztop":
                _fieldName = DBNames.FilesIndexFieldNameMuseScoreTOPId;
                _libraryFieldName = DBNames.ScoresFieldNameMuseScoreTOP;
                break;
            case "mscztok":
                _fieldName = DBNames.FilesIndexFieldNameMuseScoreTOKId;
                _libraryFieldName = DBNames.ScoresFieldNameMuseScoreTOK;
                break;
            case "pdforp":
                _fieldName = DBNames.FilesIndexFieldNamePDFORPId;
                _libraryFieldName = DBNames.ScoresFieldNamePDFORP;
                break;
            case "pdfork":
                _fieldName = DBNames.FilesIndexFieldNamePDFORKId;
                _libraryFieldName = DBNames.ScoresFieldNamePDFORK;
                break;
            case "pdftop":
                _fieldName = DBNames.FilesIndexFieldNamePDFTOPId;
                _libraryFieldName = DBNames.ScoresFieldNamePDFTOP;
                break;
            case "pdftok":
                _fieldName = DBNames.FilesIndexFieldNamePDFTOKId;
                _libraryFieldName = DBNames.ScoresFieldNamePDFTOK;
                break;
            case "mp3b1":
                _fieldName = DBNames.FilesIndexFieldNameMP3B1Id;
                _libraryFieldName = DBNames.ScoresFieldNameMP3B1;
                break;
            case "mp3b2":
                _fieldName = DBNames.FilesIndexFieldNameMP3B2Id;
                _libraryFieldName = DBNames.ScoresFieldNameMP3B2;
                break;
            case "mp3t1":
                _fieldName = DBNames.FilesIndexFieldNameMP3T1Id;
                _libraryFieldName = DBNames.ScoresFieldNameMP3T1;
                break;
            case "mp3t2":
                _fieldName = DBNames.FilesIndexFieldNameMP3T2Id;
                _libraryFieldName = DBNames.ScoresFieldNameMP3T2;
                break;
            case "mp3sol":
                _fieldName = DBNames.FilesIndexFieldNameMP3SOLId;
                _libraryFieldName = DBNames.ScoresFieldNameMP3SOL;
                break;
            case "mp3tot":
                _fieldName = DBNames.FilesIndexFieldNameMP3TOTId;
                _libraryFieldName = DBNames.ScoresFieldNameMP3TOT;
                break;
            case "mp3pia":
                _fieldName = DBNames.FilesIndexFieldNameMP3PIAId;
                _libraryFieldName = DBNames.ScoresFieldNameMP3PIA;
                break;
            case "mp3b1voice":
                _fieldName = DBNames.FilesIndexFieldNameMP3VoiceB1Id;
                _libraryFieldName = "";
                break;
            case "mp3b2voice":
                _fieldName = DBNames.FilesIndexFieldNameMP3VoiceB2Id;
                _libraryFieldName = "";
                break;
            case "mp3t1voice":
                _fieldName = DBNames.FilesIndexFieldNameMP3VoiceT1Id;
                _libraryFieldName = "";
                break;
            case "mp3t2voice":
                _fieldName = DBNames.FilesIndexFieldNameMP3VoiceT2Id;
                _libraryFieldName = "";
                break;
            case "mp3solvoice":
                _fieldName = DBNames.FilesIndexFieldNameMP3VoiceSOLId;
                _libraryFieldName = "";
                break;
            case "mp3totvoice":
                _fieldName = DBNames.FilesIndexFieldNameMP3VoiceTOTId;
                _libraryFieldName = "";
                break;
        }

        switch (CheckType)
        {
            case "filesindex":
                return _fieldName;
            case "library":
                return _libraryFieldName;
            default:
                return _fieldName;
        }
    }
    #endregion

    #region Drag Widow
    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            DragMove();
        }
    }
    #endregion

    private void Close_Click(object sender, RoutedEventArgs e)
    {

    }
}

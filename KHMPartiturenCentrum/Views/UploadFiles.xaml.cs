using Microsoft.Win32;

using System.IO;

using File = System.IO.File;

namespace KHM.Views;
/// <summary>
/// Interaction logic for UploadFiles.xaml
/// </summary>
public partial class UploadFiles : Page
{
    public ScoreViewModel? scores;
    public FileUploadOkViewModel? fileUploadOk;
    public FileUploadErrorViewModel? fileUploadError;
    ObservableCollection<FileUploadErrorModel> UploadErrorFiles = new();
    ObservableCollection<FileUploadOkModel> FileUpload = new();

    public ScoreModel? SelectedScore;
    private string[]? files;
    private long totalSize = 0;
    private long fileSize = 0;
    private long copiedFileSize = 0;

    public UploadFiles()
    {
        InitializeComponent();
        fileUploadOk = new FileUploadOkViewModel();
        fileUploadError = new FileUploadErrorViewModel();
        UploadedFilesDataGrid.ItemsSource = fileUploadOk.FilesUploadOk;
    }

    #region Select Files Button
    private void BtnSelectFiles(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog() { Multiselect = true };
        openFileDialog.Filter = "Muziek bestanden (MSCZ, MSCX,PDF,MP3)|*.MSC?;*.PDF;*.MP3;*.*";
        bool? response = openFileDialog.ShowDialog();
        if (response == true)
        {
            //Get Selected Files
            files = openFileDialog.FileNames;
            ProcessFiles(files);
        }
    }
    #endregion

    #region Calculate total file size
    private void CalculateTotalFilesize(string[] files)
    {
        foreach (var file in files)
        {
            long fileSize = new System.IO.FileInfo(file).Length;
            totalSize += fileSize;
        }

        //progressBar.Maximum = totalSize;
    }
    #endregion

    #region Start processing dropped files
    private void Files_Drop(object sender, DragEventArgs e)
    {
        string[] files;

        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            files = (string[])e.Data.GetData(DataFormats.FileDrop);
            ProcessFiles(files);
        }
    }
    #endregion

    #region Process file from validation check till file upload to database
    private void ProcessFiles(string[] files)
    {
        if (files.Length == 1)
        {
            UploadBox.Header = "Bestand Uploaden";
        }
        else
        {
            UploadBox.Header = "Bestanden Uploaden";
        }

        foreach (var file in files)
        {
            tbCurrentFile.Text = $"Controleren: {file}";
            string? _fileExtension = CheckExtention(file);
            string? _fileName = Path.GetFileName(file), _uploadFileName;
            string[] fileNameSplitup = _fileName.Split('.');
            string[] fileInfo = fileNameSplitup[0].Split('-');
            string? scoreNumber = fileInfo[0].Trim().Substring(0, 3);
            string? filePathName = file;
            string? scoreTitle, filePart;
            string hasVoiceString = "(Ingezongen)", scoreTitleSuffix;
            bool hasVoice;
            int scoreId;

            if (_fileExtension == "")
            {
                UploadErrorFiles.Add(new FileUploadErrorModel { FileName = _fileName, Reason = $"Ongeldig bestandstype ({_fileExtension})" });
                tbCurrentFile.Text = $"Upload afgebroken: {file}";

                ErrorFilesDataGrid.ItemsSource = UploadErrorFiles;
            }
            else
            {
                tbCurrentFile.Text = $"Uploaden: {file}";


                // Check if file Contains SubNumber
                if (fileInfo.Length == 3)
                {
                    scoreNumber += fileNameSplitup[0].Substring(3, 3);

                    //The Documenttype also contains the sub number, trim this
                    filePart = fileInfo[1].Substring(2);
                }
                else
                {
                    filePart = fileInfo[0].Substring(3);
                }

                scoreId = DBCommands.GetId(DBNames.ScoresView, DBNames.ScoresFieldNameId, DBNames.ScoresViewFieldNameScore, scoreNumber);

                if (scoreId == -1)
                {
                    // Unable to retrieve score Id based on the Score number
                    // Can be that in the filename the Sub number is not used Get the scoreId Based on the Title
                    scoreId = DBCommands.GetId(DBNames.ScoresView, DBNames.ScoresFieldNameId, DBNames.ScoresFieldNameTitle, fileInfo[1].Trim());
                    if (scoreId == -1)
                    {
                        // Score also not found based on title
                        UploadErrorFiles.Add(new FileUploadErrorModel { FileName = _fileName, Reason = $"Partituur niet gevonden ({scoreNumber})" });
                        tbCurrentFile.Text = $"Upload afgebroken: {file}";
                        ErrorFilesDataGrid.ItemsSource = UploadErrorFiles;
                    }
                    else
                    {
                        var _oldScoreNumber = scoreNumber;
                        scoreNumber = DBCommands.GetField(DBNames.ScoresView, DBNames.ScoresViewFieldNameScore, DBNames.ScoresFieldNameId, scoreId.ToString());
                        // Score is found based on Title, this means ScoreNumber in the FileName is incorrect Rename File

                        var _newFilePathName = filePathName.Replace(_oldScoreNumber, scoreNumber);
                        File.Move(filePathName, _newFilePathName);
                        _fileName = _fileName.Replace(_oldScoreNumber, scoreNumber);
                        filePathName = filePathName.Replace(_oldScoreNumber, scoreNumber);
                    }
                }

                // only continue when a scoreId is retrieved
                if (scoreId != -1)
                {
                    // Continue with file upload
                    scoreTitle = DBCommands.GetField(DBNames.ScoresView, DBNames.ScoresFieldNameTitle, DBNames.ScoresViewFieldNameScore, scoreNumber);

                    // Check if the title contains HasVoice String (for audio files)
                    if (_fileName.Trim().ToLower().Contains(hasVoiceString.Trim().ToLower()))
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
                    fileSize = Convert.ToUInt32(new System.IO.FileInfo(filePathName).Length);
                    copiedFileSize += fileSize;
                    FileInfo fileDetails = new FileInfo(filePathName);

                    _uploadFileName = $"{scoreNumber.Trim()}{filePart.Trim()} - {scoreTitle.Trim()}.{_fileExtension.Trim()}";

                    UploadFile(filePathName, _uploadFileName, _fileExtension, scoreId, scoreNumber, filePart, hasVoice);
                    FileUpload.Add(new FileUploadOkModel { FileName = _uploadFileName });

                    tbCurrentFile.Text = $"Upload {_fileExtension.ToUpper()}-bestand voltooid: {file}";
                    UploadedFilesDataGrid.ItemsSource = FileUpload;
                }
            }
        }
    }
    #endregion

    #region Upload file to database
    public static void UploadFile(string _filePath, string _fileName, string _fileType, int _scoreId, string _scoreNumber, string _scorePart, bool _hasVoice)
    {
        string _tableName = "", _fieldName, _fieldNamePrefix = "";
        int _fieldId, _filesIndexId, _fileId;

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
            _fieldName = GetFieldName(_fileType.Trim().ToLower() + _scorePart.Trim().ToLower() + _fieldNamePrefix.Trim(), "filesindex");
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
    #endregion

    #region Give the FilesIndex FieldName to check based on FileType and ScorePart
    public static string GetFieldName(string _check, string CheckType)
    {
        // Determine what field to check depending on file type and file part
        string _fieldName = "", _libraryFieldName = "";

        switch (_check.Trim())
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

        switch (CheckType.Trim())
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

    #region Check if valid file extension
    public string CheckExtention(string _file)
    {
        var _result = "";

        if (_file != null)
        {
            var _fileName = Path.GetFileName(_file);
            string[] _fileNameSplitup = _fileName.ToLower().Split('.');
            var _length = _fileNameSplitup.Length - 1;

            if (_fileNameSplitup[_length] == "pdf" || _fileNameSplitup[_length] == "mscz" || _fileNameSplitup[_length] == "mp3")
            {
                _result = _fileNameSplitup[1];
            }
        }
        return _result;
    }
    #endregion
}

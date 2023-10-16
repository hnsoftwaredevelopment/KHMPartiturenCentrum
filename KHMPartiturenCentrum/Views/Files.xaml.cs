#pragma warning disable CS8629 // Nullable value type may be null.
#pragma warning disable CS8602
#pragma warning disable CS8618

using static KHM.App;

namespace KHM.Views;
public partial class Files : Page
{
    #region declaration
    public MusicFilesViewModel? scores;
    public MusicFilesModel? SelectedScore;
    public bool orgMSCORP, orgMSCORK, orgMSCTOP, orgMSCTOK, orgPDFORP, orgPDFORK, orgPDFTOP, orgPDFTOK, orgPDFPIA;
    public bool orgMP3B1, orgMP3B2, orgMP3T1, orgMP3T2, orgMP3SOL, orgMP3TOT, orgMP3UITV, orgMP3PIA;
    public bool Changed;
    public bool changedPDFORP, changedPDFORK, changedPDFTOP, changedPDFTOK, changedPDFPIA;
    public bool changedMSCORP, changedMSCORK, changedMSCTOP, changedMSCTOK, changedOnline;
    public bool changedMP3B1, changedMP3B2, changedMP3T1, changedMP3T2, changedMP3SOL, changedMP3TOT, changedMP3PIA, changedMP3UITV;
    public int MSCORPFileId, MSSCORKFileId, MSCTOPFileId, MSCTOKFileId, PDFORPFileId, PDFORKFileId, PDFTOPFileId, PDFTOKFileId, PDFPIAFileId;
    public int MP3B1FileId, MP3B2FileId, MP3T1FileId, MP3T2FileId, MP3SOLFileId, MP3TOTFileId, MP3UITVFileId, MP3PIAFileId;
    public string FileName;
    public bool playerClosed = false;
    #endregion

    public Files ( )
    {
        InitializeComponent ( );
        scores = new MusicFilesViewModel ( );
        ScoresDataGrid.ItemsSource = scores.Scores;
        var _screenHeight = Application.Current.MainWindow.ActualHeight - 70 - 30 - 10 - 26 - 80;
        ScoresDataGrid.Height = _screenHeight;
    }

    #region Save Changes
    private void BtnSaveClick ( object sender, RoutedEventArgs e )
    {

    }
    #endregion

    #region Changed status of a checkbox
    private void CheckBoxChanged ( object sender, RoutedEventArgs e )
    {

    }
    #endregion

    #region Clicked: GoTo Next Record
    private void BtnNextClick ( object sender, RoutedEventArgs e )
    {
        if ( ScoresDataGrid.SelectedIndex + 1 < ScoresDataGrid.Items.Count )
        {
            ScoresDataGrid.SelectedIndex += 1;
        }
        else
        {
            ScoresDataGrid.SelectedIndex = 0;
        }

        // Scroll to the item in the GridView
        ScoresDataGrid.ScrollIntoView ( ScoresDataGrid.Items [ ScoresDataGrid.SelectedIndex ] );
    }
    #endregion

    #region Clicked: Goto Previous Record
    private void BtnPreviousClick ( object sender, RoutedEventArgs e )
    {
        if ( ScoresDataGrid.SelectedIndex > 0 )
        {
            ScoresDataGrid.SelectedIndex -= 1;
        }
        else
        {
            ScoresDataGrid.SelectedIndex = ScoresDataGrid.Items.Count - 1;
        }

        // Scroll to the item in the GridView
        ScoresDataGrid.ScrollIntoView ( ScoresDataGrid.Items [ ScoresDataGrid.SelectedIndex ] );
    }
    #endregion

    #region Clicked: GoTo Last Record
    private void BtnLastClick ( object sender, RoutedEventArgs e )
    {
        ScoresDataGrid.SelectedIndex = ScoresDataGrid.Items.Count - 1;

        // Scroll to the item in the GridView
        ScoresDataGrid.ScrollIntoView ( ScoresDataGrid.Items [ ScoresDataGrid.SelectedIndex ] );
    }
    #endregion

    #region Clicked: GoTo First Record
    private void BtnFirstClick ( object sender, RoutedEventArgs e )
    {
        ScoresDataGrid.SelectedIndex = 0;

        // Scroll to the item in the GridView
        ScoresDataGrid.ScrollIntoView ( ScoresDataGrid.Items [ ScoresDataGrid.SelectedIndex ] );
    }
    #endregion

    #region Create a cover sheet document
    private void CreateCoverSheet ( object sender, RoutedEventArgs e )
    {

    }
    #endregion

    #region Clear content of the Search box
    private void btnClearSearch_Click ( object sender, RoutedEventArgs e )
    {
        tbSearch.Text = "";
        ScoresDataGrid.ItemsSource = scores.Scores;
    }
    #endregion

    #region Filter DataGrid on content of Search box
    private void tbSearch_TextChanged ( object sender, TextChangedEventArgs e )
    {
        var search = sender as TextBox;

        if ( search.Text.Length > 1 )
        {
            if ( !string.IsNullOrEmpty ( search.Text ) )
            {
                var filteredList = scores.Scores.Where(x => x.SearchField.ToLower().Contains(tbSearch.Text.ToLower()));
                ScoresDataGrid.ItemsSource = filteredList;
            }
            else
            {
                ScoresDataGrid.ItemsSource = scores.Scores;
            }
        }
    }
    #endregion

    #region Selected row in DataGrid changed
    private void SelectedScoreChanged ( object sender, SelectionChangedEventArgs e )
    {
        DataGrid dg = (DataGrid)sender;

        MusicFilesModel selectedRow = (MusicFilesModel)dg.SelectedItem;

        if ( selectedRow == null )
        {
            object item = dg.Items[0];
            dg.SelectedItem = item;
            selectedRow = ( MusicFilesModel ) dg.SelectedItem;

            // Scroll to the item in the DataGrid
            dg.ScrollIntoView ( dg.Items [ 0 ] );
        }

        #region Score Number and Title
        tbScoreNumber.Text = selectedRow.ScoreNumber;
        tbTitle.Text = selectedRow.ScoreTitle;
        #endregion

        #region MuseScore check boxes
        chkMSCORP.IsChecked = selectedRow.MSCORP;
        chkMSCORK.IsChecked = selectedRow.MSCORK;
        chkMSCTOP.IsChecked = selectedRow.MSCTOP;
        chkMSCTOK.IsChecked = selectedRow.MSCTOK;
        #endregion

        #region PDF check boxes
        chkPDFORP.IsChecked = selectedRow.PDFORP;
        chkPDFORK.IsChecked = selectedRow.PDFORK;
        chkPDFTOP.IsChecked = selectedRow.PDFTOP;
        chkPDFTOK.IsChecked = selectedRow.PDFTOK;
        chkPDFPIA.IsChecked = selectedRow.PDFPIA;
        #endregion

        #region MP3 check boxes
        chkMP3B1.IsChecked = selectedRow.MP3B1;
        chkMP3B2.IsChecked = selectedRow.MP3B2;
        chkMP3T1.IsChecked = selectedRow.MP3T1;
        chkMP3T2.IsChecked = selectedRow.MP3T2;

        chkMP3SOL.IsChecked = selectedRow.MP3SOL;
        chkMP3TOT.IsChecked = selectedRow.MP3TOT;
        chkMP3UITV.IsChecked = selectedRow.MP3UITV;
        chkMP3PIA.IsChecked = selectedRow.MP3PIA;
        #endregion

        #region MuseScore action buttons visibility
        if ( selectedRow.MSCORPId > 0 )
        {
            btnMSCORPDownload.Visibility = Visibility.Visible;
            btnMSCORPDownload.ToolTip = $"'{selectedRow.ScoreNumber}ORP - {selectedRow.ScoreTitle}.mscx' downloaden";
        }
        else
        { btnMSCORPDownload.Visibility = Visibility.Collapsed; }

        if ( selectedRow.MSCORKId > 0 )
        {
            btnMSCORKDownload.Visibility = Visibility.Visible;
            btnMSCORKDownload.ToolTip = $"'{selectedRow.ScoreNumber}ORK - {selectedRow.ScoreTitle}.mscx' downloaden";
        }
        else
        { btnMSCORKDownload.Visibility = Visibility.Collapsed; }

        if ( selectedRow.MSCTOPId > 0 )
        {
            btnMSCTOPDownload.Visibility = Visibility.Visible;
            btnMSCTOPDownload.ToolTip = $"'{selectedRow.ScoreNumber}TOP - {selectedRow.ScoreTitle}.mscx' downloaden";
        }
        else
        { btnMSCTOPDownload.Visibility = Visibility.Collapsed; }

        if ( selectedRow.MSCTOKId > 0 )
        {
            btnMSCTOKDownload.Visibility = Visibility.Visible;
            btnMSCTOKDownload.ToolTip = $"'{selectedRow.ScoreNumber}TOK - {selectedRow.ScoreTitle}.mscx' downloaden";
        }
        else
        { btnMSCTOKDownload.Visibility = Visibility.Collapsed; }
        #endregion

        #region PDF action buttons visibility
        if ( selectedRow.PDFORPId > 0 )
        {
            btnPDFORPDownload.Visibility = Visibility.Visible;
            btnPDFORPView.Visibility = Visibility.Visible;
            btnPDFORPDownload.ToolTip = $"'{selectedRow.ScoreNumber}ORP - {selectedRow.ScoreTitle}.pdf' downloaden";
            btnPDFORPView.ToolTip = $"'{selectedRow.ScoreNumber}ORP - {selectedRow.ScoreTitle}.pdf' tonen";
        }
        else
        {
            btnPDFORPDownload.Visibility = Visibility.Collapsed;
            btnPDFORPView.Visibility = Visibility.Collapsed;
        }

        if ( selectedRow.PDFORKId > 0 )
        {
            btnPDFORKDownload.Visibility = Visibility.Visible;
            btnPDFORKView.Visibility = Visibility.Visible;
            btnPDFORKDownload.ToolTip = $"'{selectedRow.ScoreNumber}ORK - {selectedRow.ScoreTitle}.pdf' downloaden";
            btnPDFORKView.ToolTip = $"'{selectedRow.ScoreNumber}ORK - {selectedRow.ScoreTitle}.pdf' tonen";
        }
        else
        {
            btnPDFORKDownload.Visibility = Visibility.Collapsed;
            btnPDFORKView.Visibility = Visibility.Collapsed;
        }

        if ( selectedRow.PDFTOPId > 0 )
        {
            btnPDFTOPDownload.Visibility = Visibility.Visible;
            btnPDFTOPView.Visibility = Visibility.Visible;
            btnPDFTOPDownload.ToolTip = $"'{selectedRow.ScoreNumber}TOP - {selectedRow.ScoreTitle}.pdf' downloaden";
            btnPDFTOPView.ToolTip = $"'{selectedRow.ScoreNumber}TOP - {selectedRow.ScoreTitle}.pdf' tonen";
        }
        else
        {
            btnPDFTOPDownload.Visibility = Visibility.Collapsed;
            btnPDFTOPView.Visibility = Visibility.Collapsed;
        }

        if ( selectedRow.PDFTOKId > 0 )
        {
            btnPDFTOKDownload.Visibility = Visibility.Visible;
            btnPDFTOKView.Visibility = Visibility.Visible;
            btnPDFTOKDownload.ToolTip = $"'{selectedRow.ScoreNumber}TOK - {selectedRow.ScoreTitle}.pdf' downloaden";
            btnPDFTOKView.ToolTip = $"'{selectedRow.ScoreNumber}TOK - {selectedRow.ScoreTitle}.pdf' tonen";
        }
        else
        {
            btnPDFTOKDownload.Visibility = Visibility.Collapsed;
            btnPDFTOKView.Visibility = Visibility.Collapsed;
        }

        if ( selectedRow.PDFPIAId > 0 )
        {
            btnPDFPIADownload.Visibility = Visibility.Visible;
            btnPDFPIAView.Visibility = Visibility.Visible;
            btnPDFPIADownload.ToolTip = $"'{selectedRow.ScoreNumber}PIA - {selectedRow.ScoreTitle}.pdf' downloaden";
            btnPDFPIAView.ToolTip = $"'{selectedRow.ScoreNumber}PIA - {selectedRow.ScoreTitle}.pdf' tonen";
        }
        else
        {
            btnPDFPIADownload.Visibility = Visibility.Collapsed;
            btnPDFPIAView.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region MP3 action buttons visibility
        if ( selectedRow.MP3B1Id > 0 )
        {
            btnMP3B1Download.Visibility = Visibility.Visible;
            btnMP3B1Play.Visibility = Visibility.Visible;
            btnMP3B1Download.ToolTip = $"'{selectedRow.ScoreNumber}B1 - {selectedRow.ScoreTitle}.mp3' downloaden";
            btnMP3B1Play.ToolTip = $"'{selectedRow.ScoreNumber}B1 - {selectedRow.ScoreTitle}.mp3' afspelen";
        }
        else
        {
            btnMP3B1Download.Visibility = Visibility.Collapsed;
            btnMP3B1Play.Visibility = Visibility.Collapsed;
        }

        if ( selectedRow.MP3B2Id > 0 )
        {
            btnMP3B2Download.Visibility = Visibility.Visible;
            btnMP3B2Play.Visibility = Visibility.Visible;
            btnMP3B2Download.ToolTip = $"'{selectedRow.ScoreNumber}B2 - {selectedRow.ScoreTitle}.mp3' downloaden";
            btnMP3B2Play.ToolTip = $"'{selectedRow.ScoreNumber}B2 - {selectedRow.ScoreTitle}.mp3' afspelen";
        }
        else
        {
            btnMP3B2Download.Visibility = Visibility.Collapsed;
            btnMP3B2Play.Visibility = Visibility.Collapsed;
        }

        if ( selectedRow.MP3T1Id > 0 )
        {
            btnMP3T1Download.Visibility = Visibility.Visible;
            btnMP3T1Play.Visibility = Visibility.Visible;
            btnMP3T1Download.ToolTip = $"'{selectedRow.ScoreNumber}T1 - {selectedRow.ScoreTitle}.mp3' downloaden";
            btnMP3T1Play.ToolTip = $"'{selectedRow.ScoreNumber}T1 - {selectedRow.ScoreTitle}.mp3' afspelen";
        }
        else
        {
            btnMP3T1Download.Visibility = Visibility.Collapsed;
            btnMP3T1Play.Visibility = Visibility.Collapsed;
        }

        if ( selectedRow.MP3T2Id > 0 )
        {
            btnMP3T2Download.Visibility = Visibility.Visible;
            btnMP3T2Play.Visibility = Visibility.Visible;
            btnMP3T2Download.ToolTip = $"'{selectedRow.ScoreNumber}T2 - {selectedRow.ScoreTitle}.mp3' downloaden";
            btnMP3T2Play.ToolTip = $"'{selectedRow.ScoreNumber}T2 - {selectedRow.ScoreTitle}.mp3' afspelen";
        }
        else
        {
            btnMP3T2Download.Visibility = Visibility.Collapsed;
            btnMP3T2Play.Visibility = Visibility.Collapsed;
        }

        if ( selectedRow.MP3TOTId > 0 )
        {
            btnMP3TOTDownload.Visibility = Visibility.Visible;
            btnMP3TOTPlay.Visibility = Visibility.Visible;
            btnMP3TOTDownload.ToolTip = $"'{selectedRow.ScoreNumber}TOT - {selectedRow.ScoreTitle}.mp3' downloaden";
            btnMP3TOTPlay.ToolTip = $"'{selectedRow.ScoreNumber}TOT - {selectedRow.ScoreTitle}.mp3' afspelen";
        }
        else
        {
            btnMP3TOTDownload.Visibility = Visibility.Collapsed;
            btnMP3TOTPlay.Visibility = Visibility.Collapsed;
        }

        if ( selectedRow.MP3SOLId > 0 )
        {
            btnMP3SOLDownload.Visibility = Visibility.Visible;
            btnMP3SOLPlay.Visibility = Visibility.Visible;
            btnMP3SOLDownload.ToolTip = $"'{selectedRow.ScoreNumber}SOL - {selectedRow.ScoreTitle}.mp3' downloaden";
            btnMP3SOLPlay.ToolTip = $"'{selectedRow.ScoreNumber}SOL - {selectedRow.ScoreTitle}.mp3' afspelen";
        }
        else
        {
            btnMP3SOLDownload.Visibility = Visibility.Collapsed;
            btnMP3SOLPlay.Visibility = Visibility.Collapsed;
        }

        if ( selectedRow.MP3PIAId > 0 )
        {
            btnMP3PIADownload.Visibility = Visibility.Visible;
            btnMP3PIAPlay.Visibility = Visibility.Visible;
            btnMP3PIADownload.ToolTip = $"'{selectedRow.ScoreNumber}PIA - {selectedRow.ScoreTitle}.mp3' downloaden";
            btnMP3PIAPlay.ToolTip = $"'{selectedRow.ScoreNumber}PIA - {selectedRow.ScoreTitle}.mp3' afspelen";
        }
        else
        {
            btnMP3PIADownload.Visibility = Visibility.Collapsed;
            btnMP3PIAPlay.Visibility = Visibility.Collapsed;
        }

        if ( selectedRow.MP3UITVId > 0 )
        {
            btnMP3UITVDownload.Visibility = Visibility.Visible;
            btnMP3UITVPlay.Visibility = Visibility.Visible;
            btnMP3UITVDownload.ToolTip = $"'{selectedRow.ScoreNumber}UITV - {selectedRow.ScoreTitle}.mp3' downloaden";
            btnMP3UITVPlay.ToolTip = $"'{selectedRow.ScoreNumber}UITV - {selectedRow.ScoreTitle}.mp3' afspelen";
        }
        else
        {
            btnMP3UITVDownload.Visibility = Visibility.Collapsed;
            btnMP3UITVPlay.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region MP3 Voice action buttons visibility
        if ( selectedRow.MP3B1VoiceId > 0 )
        {
            btnMP3B1VoiceDownload.Visibility = Visibility.Visible;
            btnMP3B1VoicePlay.Visibility = Visibility.Visible;
            btnMP3B1Download.ToolTip = $"'{selectedRow.ScoreNumber}B1 - {selectedRow.ScoreTitle} (Ingezongen).mp3' downloaden";
            btnMP3B1Play.ToolTip = $"'{selectedRow.ScoreNumber}B1 - {selectedRow.ScoreTitle} (Ingezongen).mp3' afspelen";
        }
        else
        {
            btnMP3B1VoiceDownload.Visibility = Visibility.Collapsed;
            btnMP3B1VoicePlay.Visibility = Visibility.Collapsed;
        }

        if ( selectedRow.MP3B2VoiceId > 0 )
        {
            btnMP3B2VoiceDownload.Visibility = Visibility.Visible;
            btnMP3B2VoicePlay.Visibility = Visibility.Visible;
            btnMP3B2Download.ToolTip = $"'{selectedRow.ScoreNumber}B2 - {selectedRow.ScoreTitle} (Ingezongen).mp3' downloaden";
            btnMP3B2Play.ToolTip = $"'{selectedRow.ScoreNumber}B2 - {selectedRow.ScoreTitle} (Ingezongen).mp3' afspelen";
        }
        else
        {
            btnMP3B2VoiceDownload.Visibility = Visibility.Collapsed;
            btnMP3B2VoicePlay.Visibility = Visibility.Collapsed;
        }

        if ( selectedRow.MP3T1VoiceId > 0 )
        {
            btnMP3T1VoiceDownload.Visibility = Visibility.Visible;
            btnMP3T1VoicePlay.Visibility = Visibility.Visible;
            btnMP3T1Download.ToolTip = $"'{selectedRow.ScoreNumber}T1 - {selectedRow.ScoreTitle} (Ingezongen).mp3' downloaden";
            btnMP3T1Play.ToolTip = $"'{selectedRow.ScoreNumber}T1 - {selectedRow.ScoreTitle} (Ingezongen).mp3' afspelen";
        }
        else
        {
            btnMP3T1VoiceDownload.Visibility = Visibility.Collapsed;
            btnMP3T1VoicePlay.Visibility = Visibility.Collapsed;
        }

        if ( selectedRow.MP3T2VoiceId > 0 )
        {
            btnMP3T2VoiceDownload.Visibility = Visibility.Visible;
            btnMP3T2VoicePlay.Visibility = Visibility.Visible;
            btnMP3T2Download.ToolTip = $"'{selectedRow.ScoreNumber}T2 - {selectedRow.ScoreTitle} (Ingezongen).mp3' downloaden";
            btnMP3T2Play.ToolTip = $"'{selectedRow.ScoreNumber}T2 - {selectedRow.ScoreTitle} (Ingezongen).mp3' afspelen";
        }
        else
        {
            btnMP3T2VoiceDownload.Visibility = Visibility.Collapsed;
            btnMP3T2VoicePlay.Visibility = Visibility.Collapsed;
        }

        if ( selectedRow.MP3TOTVoiceId > 0 )
        {
            btnMP3TOTVoiceDownload.Visibility = Visibility.Visible;
            btnMP3TOTVoicePlay.Visibility = Visibility.Visible;
            btnMP3TOTDownload.ToolTip = $"'{selectedRow.ScoreNumber}TOT - {selectedRow.ScoreTitle} (Ingezongen).mp3' downloaden";
            btnMP3TOTPlay.ToolTip = $"'{selectedRow.ScoreNumber}TOT - {selectedRow.ScoreTitle} (Ingezongen).mp3' afspelen";
        }
        else
        {
            btnMP3TOTVoiceDownload.Visibility = Visibility.Collapsed;
            btnMP3TOTVoicePlay.Visibility = Visibility.Collapsed;
        }

        if ( selectedRow.MP3SOLVoiceId > 0 )
        {
            btnMP3SOLVoiceDownload.Visibility = Visibility.Visible;
            btnMP3SOLVoicePlay.Visibility = Visibility.Visible;
            btnMP3SOLDownload.ToolTip = $"'{selectedRow.ScoreNumber}SOL - {selectedRow.ScoreTitle} (Ingezongen).mp3' downloaden";
            btnMP3SOLPlay.ToolTip = $"'{selectedRow.ScoreNumber}SOL - {selectedRow.ScoreTitle} (Ingezongen).mp3' afspelen";
        }
        else
        {
            btnMP3SOLVoiceDownload.Visibility = Visibility.Collapsed;
            btnMP3SOLVoicePlay.Visibility = Visibility.Collapsed;
        }
        #endregion

        SelectedScore = selectedRow;
        DataContext = selectedRow;
    }
    #endregion

    private void chkMusicFiles_Changed ( object sender, RoutedEventArgs e )
    {
        // Check the change of the Checkboxes, to enable/disable the save button

    }

    #region Download selected File
    private void btnDownloadClick ( object sender, RoutedEventArgs e )
    {
        // Download the file of the selected row/column
        int fileId=0;
        string fileTable = "", filePathSuffix = "", fileNameExtension = "", fileNamePrefix = "", fileNameSuffix = "" ;

        if ( SelectedScore != null )
        {
            if ( sender is Button button )
            {
                switch ( button.Name )
                {
                    case "btnMSCORPDownload":
                        fileId = ( int ) SelectedScore.MSCORPId;
                        fileTable = DBNames.FilesMSCTable;
                        filePathSuffix = "MuseScore";
                        fileNameExtension = "mscz";
                        fileNamePrefix = "ORP";
                        fileNameSuffix = "";
                        break;
                    case "btnMSCORKDownload":
                        fileId = ( int ) SelectedScore.MSCORKId;
                        fileTable = DBNames.FilesMSCTable;
                        filePathSuffix = "MuseScore";
                        fileNameExtension = "mscz";
                        fileNamePrefix = "ORK";
                        fileNameSuffix = "";
                        break;
                    case "btnMSCTOPDownload":
                        fileId = ( int ) SelectedScore.MSCTOPId;
                        fileTable = DBNames.FilesMSCTable;
                        filePathSuffix = "MuseScore";
                        fileNameExtension = "mscz";
                        fileNamePrefix = "TOP";
                        fileNameSuffix = "";
                        break;
                    case "btnMSCTOKDownload":
                        fileId = ( int ) SelectedScore.MSCTOKId;
                        fileTable = DBNames.FilesMSCTable;
                        filePathSuffix = "MuseScore";
                        fileNameExtension = "mscz";
                        fileNamePrefix = "TOK";
                        fileNameSuffix = "";
                        break;
                    case "btnPDFORPDownload":
                        fileId = ( int ) SelectedScore.PDFORPId;
                        fileTable = DBNames.FilesPDFTable;
                        filePathSuffix = "Partituren";
                        fileNameExtension = "pdf";
                        fileNamePrefix = "ORP";
                        fileNameSuffix = "";
                        break;
                    case "btnPDFORKDownload":
                        fileId = ( int ) SelectedScore.PDFORKId;
                        fileTable = DBNames.FilesPDFTable;
                        filePathSuffix = "Partituren";
                        fileNameExtension = "pdf";
                        fileNamePrefix = "ORK";
                        fileNameSuffix = "";
                        break;
                    case "btnPDFTOPDownload":
                        fileId = ( int ) SelectedScore.PDFTOPId;
                        fileTable = DBNames.FilesPDFTable;
                        filePathSuffix = "Partituren";
                        fileNameExtension = "pdf";
                        fileNamePrefix = "TOP";
                        fileNameSuffix = "";
                        break;
                    case "btnPDFTOKDownload":
                        fileId = ( int ) SelectedScore.PDFTOKId;
                        fileTable = DBNames.FilesPDFTable;
                        filePathSuffix = "Partituren";
                        fileNameExtension = "pdf";
                        fileNamePrefix = "TOK";
                        fileNameSuffix = "";
                        break;
                    case "btnPDFPIADownload":
                        fileId = ( int ) SelectedScore.PDFPIAId;
                        fileTable = DBNames.FilesPDFTable;
                        filePathSuffix = "Partituren";
                        fileNameExtension = "pdf";
                        fileNamePrefix = "PIA";
                        fileNameSuffix = "";
                        break;
                    case "btnMP3B1Download":
                        fileId = ( int ) SelectedScore.MP3B1Id;
                        fileTable = DBNames.FilesMP3Table;
                        filePathSuffix = "Audio";
                        fileNameExtension = "mp3";
                        fileNamePrefix = "B1";
                        fileNameSuffix = "";
                        break;
                    case "btnMP3B2Download":
                        fileId = ( int ) SelectedScore.MP3B2Id;
                        fileTable = DBNames.FilesMP3Table;
                        filePathSuffix = "Audio";
                        fileNameExtension = "mp3";
                        fileNamePrefix = "B2";
                        fileNameSuffix = "";
                        break;
                    case "btnMP3T1Download":
                        fileId = ( int ) SelectedScore.MP3T1Id;
                        fileTable = DBNames.FilesMP3Table;
                        filePathSuffix = "Audio";
                        fileNameExtension = "mp3";
                        fileNamePrefix = "T1";
                        fileNameSuffix = "";
                        break;
                    case "btnMP3T2Download":
                        fileId = ( int ) SelectedScore.MP3T2Id;
                        fileTable = DBNames.FilesMP3Table;
                        filePathSuffix = "Audio";
                        fileNameExtension = "mp3";
                        fileNamePrefix = "T2";
                        fileNameSuffix = "";
                        break;
                    case "btnMP3TOTDownload":
                        fileId = ( int ) SelectedScore.MP3TOTId;
                        fileTable = DBNames.FilesMP3Table;
                        filePathSuffix = "Audio";
                        fileNameExtension = "mp3";
                        fileNamePrefix = "TOT";
                        fileNameSuffix = "";
                        break;
                    case "btnMP3SOLDownload":
                        fileId = ( int ) SelectedScore.MP3SOLId;
                        fileTable = DBNames.FilesMP3Table;
                        filePathSuffix = "Audio";
                        fileNameExtension = "mp3";
                        fileNamePrefix = "SOL";
                        fileNameSuffix = "";
                        break;
                    case "btnMP3PIADownload":
                        fileId = ( int ) SelectedScore.MP3PIAId;
                        fileTable = DBNames.FilesMP3Table;
                        filePathSuffix = "Audio";
                        fileNameExtension = "mp3";
                        fileNamePrefix = "PIA";
                        fileNameSuffix = "";
                        break;
                    case "btnMP3UITVDownload":
                        fileId = ( int ) SelectedScore.MP3UITVId;
                        fileTable = DBNames.FilesMP3Table;
                        filePathSuffix = "Audio";
                        fileNameExtension = "mp3";
                        fileNamePrefix = "UITV";
                        fileNameSuffix = "";
                        break;
                    case "btnMP3B1VoiceDownload":
                        fileId = ( int ) SelectedScore.MP3B1VoiceId;
                        fileTable = DBNames.FilesMP3VoiceTable;
                        filePathSuffix = "Audio";
                        fileNameExtension = "mp3";
                        fileNamePrefix = "B1";
                        fileNameSuffix = " (Ingezongen)";
                        break;
                    case "btnMP3B2VoiceDownload":
                        fileId = ( int ) SelectedScore.MP3B2VoiceId;
                        fileTable = DBNames.FilesMP3VoiceTable;
                        filePathSuffix = "Audio";
                        fileNameExtension = "mp3";
                        fileNamePrefix = "B2";
                        fileNameSuffix = " (Ingezongen)";
                        break;
                    case "btnMP3T1VoiceDownload":
                        fileId = ( int ) SelectedScore.MP3T1VoiceId;
                        fileTable = DBNames.FilesMP3VoiceTable;
                        filePathSuffix = "Audio";
                        fileNameExtension = "mp3";
                        fileNamePrefix = "T1";
                        fileNameSuffix = " (Ingezongen)";
                        break;
                    case "btnMP3T2VoiceDownload":
                        fileId = ( int ) SelectedScore.MP3T2VoiceId;
                        fileTable = DBNames.FilesMP3VoiceTable;
                        filePathSuffix = "Audio";
                        fileNameExtension = "mp3";
                        fileNamePrefix = "T2";
                        fileNameSuffix = " (Ingezongen)";
                        break;
                    case "btnMP3TOTVoiceDownload":
                        fileId = ( int ) SelectedScore.MP3TOTVoiceId;
                        fileTable = DBNames.FilesMP3VoiceTable;
                        filePathSuffix = "Audio";
                        fileNameExtension = "mp3";
                        fileNamePrefix = "TOT";
                        fileNameSuffix = " (Ingezongen)";
                        break;
                    case "btnMP3SOLVoiceDownload":
                        fileId = ( int ) SelectedScore.MP3SOLVoiceId;
                        fileTable = DBNames.FilesMP3VoiceTable;
                        filePathSuffix = "Audio";
                        fileNameExtension = "mp3";
                        fileNamePrefix = "SOL";
                        fileNameSuffix = " (Ingezongen)";
                        break;
                }
            }
            var fileName = $"{SelectedScore.ScoreNumber}{fileNamePrefix} - {SelectedScore.ScoreTitle}{fileNameSuffix}.{fileNameExtension}";
            DBCommands.DownloadFile ( fileId, fileTable, filePathSuffix, fileName );
        }
    }
    #endregion

    #region View selected PDF File
    private void btnViewClick ( object sender, RoutedEventArgs e )
    {
        int fileId=0;
        string fileTable = DBNames.FilesPDFTable, filePathSuffix = "Partituur", fileNameExtension = "pdf", fileNamePrefix = "";

        // View the PDF file in the selected row/column
        if ( sender is Button button )
        {
            switch ( button.Name )
            {
                case "btnPDFORPView":
                    fileId = ( int ) SelectedScore.PDFORPId;
                    fileNamePrefix = "ORP";
                    break;
                case "btnPDFORKView":
                    fileId = ( int ) SelectedScore.PDFORKId;
                    fileNamePrefix = "ORK";
                    break;
                case "btnPDFTOPView":
                    fileId = ( int ) SelectedScore.PDFTOPId;
                    fileNamePrefix = "TOP";
                    break;
                case "btnPDFTOKView":
                    fileId = ( int ) SelectedScore.PDFTOKId;
                    fileNamePrefix = "TOK";
                    break;
                case "btnPDFPIAView":
                    fileId = ( int ) SelectedScore.PDFPIAId;
                    fileNamePrefix = "PIA";
                    break;
            }

            var fileName = $"{SelectedScore.ScoreNumber}{fileNamePrefix} - {SelectedScore.ScoreTitle}.{fileNameExtension}";

            // First Download the File
            DBCommands.DownloadFile ( fileId, fileTable, filePathSuffix, fileName );

            string uri = $"{FilePaths.DownloadPath}\\{filePathSuffix}\\{fileName}";

            PDFView viewer = new(uri, fileName);
            viewer.Show ( );
        }
    }
    #endregion

    #region Play selected MP3 File
    private void btnPlayClick ( object sender, RoutedEventArgs e )
    {
        int fileId=0;
        string fileTable = DBNames.FilesMP3Table,
            filePathSuffix = "Audio", fileNameExtension = "mp3",
            fileNamePrefix = "", fileNameSuffix = "" ;

        // Play the MP3 file in the selected row/column
        if ( sender is Button button )
        {
            switch ( button.Name )
            {
                case "btnMP3B1Play":
                    fileId = ( int ) SelectedScore.MP3B1Id;
                    fileNamePrefix = "B1";
                    break;
                case "btnMP3B2Play":
                    fileId = ( int ) SelectedScore.MP3B2Id;
                    fileNamePrefix = "B2";
                    break;
                case "btnMP3T1Play":
                    fileId = ( int ) SelectedScore.MP3T1Id;
                    fileNamePrefix = "T1";
                    break;
                case "btnMP3T2Play":
                    fileId = ( int ) SelectedScore.MP3T2Id;
                    fileNamePrefix = "T2";
                    break;
                case "btnMP3TOTPlay":
                    fileId = ( int ) SelectedScore.MP3TOTId;
                    fileNamePrefix = "TOT";
                    break;
                case "btnMP3SOLPlay":
                    fileId = ( int ) SelectedScore.MP3SOLId;
                    fileNamePrefix = "SOL";
                    break;
                case "btnMP3PIAPlay":
                    fileId = ( int ) SelectedScore.MP3PIAId;
                    fileNamePrefix = "PIA";
                    break;
                case "btnMP3UITVPlay":
                    fileId = ( int ) SelectedScore.MP3UITVId;
                    fileNamePrefix = "UITV";
                    break;
                case "btnMP3B1VoicePlay":
                    fileId = ( int ) SelectedScore.MP3B1VoiceId;
                    fileTable = DBNames.FilesMP3VoiceTable;
                    fileNamePrefix = "B1";
                    fileNameSuffix = " (Ingezongen)";
                    break;
                case "btnMP3B2VoicePlay":
                    fileId = ( int ) SelectedScore.MP3B2VoiceId;
                    fileTable = DBNames.FilesMP3VoiceTable;
                    fileNamePrefix = "B2";
                    fileNameSuffix = " (Ingezongen)";
                    break;
                case "btnMP3T1VoicePlay":
                    fileId = ( int ) SelectedScore.MP3T1VoiceId;
                    fileTable = DBNames.FilesMP3VoiceTable;
                    fileNamePrefix = "T1";
                    fileNameSuffix = " (Ingezongen)";
                    break;
                case "btnMP3T2VoicePlay":
                    fileId = ( int ) SelectedScore.MP3T2VoiceId;
                    fileTable = DBNames.FilesMP3VoiceTable;
                    fileNamePrefix = "T2";
                    fileNameSuffix = " (Ingezongen)";
                    break;
                case "btnMP3TOTVoicePlay":
                    fileId = ( int ) SelectedScore.MP3TOTVoiceId;
                    fileTable = DBNames.FilesMP3VoiceTable;
                    fileNamePrefix = "TOT";
                    fileNameSuffix = " (Ingezongen)";
                    break;
                case "btnMP3SOLVoicePlay":
                    fileId = ( int ) SelectedScore.MP3SOLVoiceId;
                    fileTable = DBNames.FilesMP3VoiceTable;
                    fileNamePrefix = "SOL";
                    fileNameSuffix = " (Ingezongen)";
                    break;
            }
        }
        var fileName = $"{SelectedScore.ScoreNumber}{fileNamePrefix} - {SelectedScore.ScoreTitle}{fileNameSuffix}.{fileNameExtension}";

        // First Download the File
        DBCommands.DownloadFile ( fileId, fileTable, filePathSuffix, fileName );

        string uri = $"{FilePaths.DownloadPath}\\{filePathSuffix}\\{fileName}";

        MediaPlayerView player = new(uri, fileName);
        player.Show ( );
    }
    #endregion
}
#pragma warning restore CS8629 // Nullable value type may be null.
#pragma warning restore CS8602
#pragma warning restore CS8618

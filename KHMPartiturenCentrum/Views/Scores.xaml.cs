using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KHMPartiturenCentrum.Helpers;
using KHMPartiturenCentrum.Models;
using KHMPartiturenCentrum.ViewModels;

using static System.Net.Mime.MediaTypeNames;

namespace KHMPartiturenCentrum.Views;

/// <summary>
/// Interaction logic for Scores.xaml
/// </summary>
public partial class Scores : Page
{
    public ScoreViewModel? scores;
    public ScoreModel? SelectedScore;
    public Scores ()
    {
        InitializeComponent ();
        scores = new ScoreViewModel ();
        DataContext = scores;
    }
    private void PageLoaded ( object sender, RoutedEventArgs e )
    {
        comAccompaniment.ItemsSource = DBCommands.GetAccompaniments ();
        comArchive.ItemsSource = DBCommands.GetArchives ();
        comGenre.ItemsSource = DBCommands.GetGenres ();
        comLanguage.ItemsSource = DBCommands.GetLanguages ();
        comRepertoire.ItemsSource = DBCommands.GetRepertoires ();
        comPublisher1.ItemsSource = DBCommands.GetPublishers ();
        comPublisher2.ItemsSource = DBCommands.GetPublishers ();
        comPublisher3.ItemsSource = DBCommands.GetPublishers ();
        comPublisher4.ItemsSource = DBCommands.GetPublishers ();
    }
    private void SelectedScoreChanged ( object sender, SelectionChangedEventArgs e )
    {
        DataGrid dg = (DataGrid)sender;

        ScoreModel selectedRow = (ScoreModel)dg.SelectedItem;
        SelectedScore = selectedRow;

        #region TAB Score Information
        #region 1st Row (ScoreNumber, Repertoire, Archive, and sing by heart)
        tbScoreNumber.Text = selectedRow.Score;

        #region Repertoire Combobox
        comRepertoire.Text = selectedRow.RepertoireName;
        foreach ( RepertoireModel repertoire in comRepertoire.Items )
        {
            if ( comRepertoire.Text == null ) { comRepertoire.Text = ""; }
            if ( repertoire.RepertoireName == comRepertoire.Text.ToString () )
            {
                comRepertoire.SelectedItem = repertoire;
            }
        }
        #endregion

        #region Archive Combobox
        comArchive.Text = selectedRow.ArchiveName;
        foreach ( ArchiveModel archive in comArchive.Items )
        {
            if ( comArchive.Text == null ) { comArchive.Text = ""; }
            if ( archive.ArchiveName == comArchive.Text.ToString () )
            {
                comArchive.SelectedItem = archive;
            }
        }
        #endregion

        #region Sing By Heart checkbox
        chkByHeart.IsChecked = selectedRow.ByHeart;
        #endregion
        #endregion

        #region 2th Row (Title and SubTitle)
        tbTitle.Text = selectedRow.ScoreTitle;
        tbSubTitle.Text = selectedRow.ScoreSubTitle;
        #endregion

        #region 3th Row (Composer, Textwriter and Arranger)
        tbComposer.Text = selectedRow.Composer;
        tbTextwriter.Text = selectedRow.Textwriter;
        tbArranger.Text = selectedRow.Arranger;
        #endregion

        #region 4th Row (Genre, Accompaniment and Language)
        #region Genre Combobox
        comGenre.Text = selectedRow.GenreName;
        foreach ( GenreModel genre in comGenre.Items )
        {
            if ( comGenre.Text == null ) { comGenre.Text = ""; }
            if ( genre.GenreName == comGenre.Text.ToString () )
            {
                comGenre.SelectedItem = genre;
            }
        }
        #endregion

        #region Accompaniment Combobox
        comAccompaniment.Text = selectedRow.AccompanimentName;
        foreach ( AccompanimentModel accompaniment in comAccompaniment.Items )
        {
            if ( comAccompaniment.Text == null ) { comAccompaniment.Text = ""; }
            if ( accompaniment.AccompanimentName == comAccompaniment.Text.ToString () )
            {
                comAccompaniment.SelectedItem = accompaniment;
            }
        }
        #endregion

        #region Language Combobox
        comLanguage.Text = selectedRow.LanguageName;
        foreach ( LanguageModel language in comLanguage.Items )
        {
            if ( comLanguage.Text == null ) { comLanguage.Text = ""; }
            if ( language.LanguageName == comLanguage.Text.ToString () )
            {
                comLanguage.SelectedItem = language;
            }
        }
        #endregion
        #endregion

        #region 5th Row (Music Piece)
        tbMusicPiece.Text = selectedRow.MusicPiece;
        #endregion

        #region 6th Row (Date created, Date Modified and Checked)
        #region Date Digitized
        if (selectedRow.DateCreatedString != "")
        {
            dpDigitized.SelectedDate = selectedRow.DateCreated.ToDateTime(TimeOnly.Parse("00:00 AM"));
            dpDigitized.Text = selectedRow.DateCreatedString;
        }
        #endregion

        #region Date Modified
        if (selectedRow.DateModifiedString != "")
        {
            dpModified.SelectedDate = selectedRow.DateModified.ToDateTime(TimeOnly.Parse("00:00 AM"));
            dpModified.Text = selectedRow.DateModifiedString;
        }
        #endregion

        #region Checked
        chkChecked.IsChecked = selectedRow.Check;
        #endregion
        #endregion

        #region 7th Row (Checkboxes for MuseScore, PDF and MP3)
        #region MuseScore checkboxes
        chkMSCORP.IsChecked = selectedRow.MuseScoreORP;
        chkMSCORK.IsChecked = selectedRow.MuseScoreORK;
        chkMSCTOP.IsChecked = selectedRow.MuseScoreTOP;
        chkMSCTOK.IsChecked = selectedRow.MuseScoreTOK;
        #endregion

        #region PDF checkboxes
        chkPDFORP.IsChecked = selectedRow.PDFORP;
        chkPDFORK.IsChecked = selectedRow.PDFORK;
        chkPDFTOP.IsChecked = selectedRow.PDFTOP;
        chkPDFTOK.IsChecked = selectedRow.PDFTOK;
        #endregion

        #region MP3 checkboxes
        chkMP3B1.IsChecked = selectedRow.MP3B1;
        chkMP3B2.IsChecked = selectedRow.MP3B2;
        chkMP3T1.IsChecked = selectedRow.MP3T1;
        chkMP3T2.IsChecked = selectedRow.MP3T2;

        chkMP3SOL.IsChecked = selectedRow.MP3SOL;
        chkMP3TOT.IsChecked = selectedRow.MP3TOT;
        chkMP3PIA.IsChecked = selectedRow.MP3PIA;
        #endregion

        #region MuseScore Online checkbox
        chkMSCOnline.IsChecked = selectedRow.MuseScoreOnline;
        #endregion
        #endregion
        #endregion

        #region TAB Lyrics
        // Unknown how to set the memo field
        #endregion

        #region TAB Notes
        // Unknown how to set the memo field
        #endregion

        #region TAB: Licenses
        #region Publisher 1
        tbAmountPublisher1.Text = selectedRow.NumberScoresPublisher1.ToString ();

        #region Publisher1 Combobox
        comPublisher1.Text = selectedRow.Publisher1Name;
        foreach ( PublisherModel publisher in comPublisher1.Items )
        {
            comPublisher1.Text ??= "";
            if ( publisher.PublisherName == comPublisher1.Text.ToString () )
            {
                comPublisher1.SelectedItem = publisher;
            }
        }
        #endregion
        #endregion

        #region Publisher 2
        tbAmountPublisher2.Text = selectedRow.NumberScoresPublisher2.ToString ();

        #region Publisher2 Combobox
        comPublisher2.Text = selectedRow.Publisher2Name;
        foreach ( PublisherModel publisher in comPublisher2.Items )
        {
            comPublisher2.Text ??= "";
            if ( publisher.PublisherName == comPublisher2.Text.ToString () )
            {
                comPublisher2.SelectedItem = publisher;
            }
        }
        #endregion
        #endregion

        #region Publisher 3
        tbAmountPublisher3.Text = selectedRow.NumberScoresPublisher3.ToString ();

        #region Publisher3 Combobox
        comPublisher3.Text = selectedRow.Publisher3Name;
        foreach ( PublisherModel publisher in comPublisher3.Items )
        {
            comPublisher3.Text ??= "";
            if ( publisher.PublisherName == comPublisher3.Text.ToString () )
            {
                comPublisher3.SelectedItem = publisher;
            }
        }
        #endregion
        #endregion

        #region Publisher 4
        tbAmountPublisher4.Text = selectedRow.NumberScoresPublisher4.ToString ();

        #region Publisherr4 Combobox
        comPublisher4.Text = selectedRow.Publisher4Name;
        foreach ( PublisherModel publisher in comPublisher4.Items )
        {
            comPublisher4.Text ??= "";
            if ( publisher.PublisherName == comPublisher4.Text.ToString () )
            {
                comPublisher4.SelectedItem = publisher;
            }
        }
        #endregion
        #endregion

        #region Total
        var Total = selectedRow.NumberScoresPublisher1 + selectedRow.NumberScoresPublisher2 + selectedRow.NumberScoresPublisher3 + selectedRow.NumberScoresPublisher4;
        tbAmountSupplierTotal.Text = Total.ToString ();
        #endregion
        #endregion
    }
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
    }
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
    }
    private void BtnLastClick ( object sender, RoutedEventArgs e )
    {
        ScoresDataGrid.SelectedIndex = ScoresDataGrid.Items.Count - 1;
    }
    private void BtnFirstClick ( object sender, RoutedEventArgs e )
    {
        ScoresDataGrid.SelectedIndex = 0;
    }
    private void TextBoxChanged(object sender, TextChangedEventArgs e)
    {
        var propertyName = ((TextBox)sender).Name;


        if ( SelectedScore != null )
        {
            switch ( propertyName )
            {
                case "tbTitle":
                    if ( tbTitle.Text == SelectedScore.ScoreTitle ) { cbTitle.IsChecked = false; } else { cbTitle.IsChecked = true; }
                    break;
                case "tbSubTitle":
                    if ( tbSubTitle.Text == SelectedScore.ScoreSubTitle ) { cbSubTitle.IsChecked = false; } else { cbSubTitle.IsChecked = true; }
                    break;
                case "tbComposer":
                    if ( tbComposer.Text == SelectedScore.Composer ) { cbComposer.IsChecked = false; } else { cbComposer.IsChecked = true; }
                    break;
                case "tbTextwriter":
                    if ( tbTextwriter.Text == SelectedScore.Textwriter ) { cbTextwriter.IsChecked = false; } else { cbTextwriter.IsChecked = true; }
                    break;
                case "tbArranger":
                    if ( tbArranger.Text == SelectedScore.Arranger ) { cbArranger.IsChecked = false; } else { cbArranger.IsChecked = true; }
                    break;
                case "tbMusicPiece":
                    if ( tbMusicPiece.Text == SelectedScore.MusicPiece ) { cbMusicPiece.IsChecked = false; } else { cbMusicPiece.IsChecked = true; }
                    break;
                case "tbAmountPublisher1":
                    if ( tbAmountPublisher1.Text == SelectedScore.NumberScoresPublisher1.ToString () ) { cbAmountPublisher1.IsChecked = false; } else { cbAmountPublisher1.IsChecked = true; }
                    break;
                case "tbAmountPublisher2":
                    if ( tbAmountPublisher2.Text == SelectedScore.NumberScoresPublisher2.ToString () ) { cbAmountPublisher2.IsChecked = false; } else { cbAmountPublisher2.IsChecked = true; }
                    break;
                case "tbAmountPublisher3":
                    if ( tbAmountPublisher3.Text == SelectedScore.NumberScoresPublisher3.ToString () ) { cbAmountPublisher3.IsChecked = false; } else { cbAmountPublisher3.IsChecked = true; }
                    break;
                case "tbAmountPublisher4":
                    if ( tbAmountPublisher4.Text == SelectedScore.NumberScoresPublisher4.ToString () ) { cbAmountPublisher4.IsChecked = false; } else { cbAmountPublisher4.IsChecked = true; }
                    break;
            }
        }
        CheckChanged();
    }
    private void ComboBoxChanged(object sender, SelectionChangedEventArgs e)
    {
        var propertyName = ((ComboBox)sender).Name;

        if ( SelectedScore != null )
        {
            switch ( propertyName )
            {
                case "comRepertoire":
                    if (comRepertoire.SelectedItem != null)
                    {
                        if (((RepertoireModel)comRepertoire.SelectedItem).RepertoireId == SelectedScore.RepertoireId) { cbRepertoire.IsChecked = false; } else { cbRepertoire.IsChecked = true; }
                    }
                    break;
                case "comArchive":
                    if (comArchive.SelectedItem != null)
                    {
                        if (((ArchiveModel)comArchive.SelectedItem).ArchiveId == SelectedScore.ArchiveId) { cbArchive.IsChecked = false; } else { cbArchive.IsChecked = true; }
                    }
                    break;
                case "comGenre":
                    if (comGenre.SelectedItem != null)
                    {
                        if (((GenreModel)comGenre.SelectedItem).GenreId == SelectedScore.GenreId) { cbGenre.IsChecked = false; } else { cbGenre.IsChecked = true; }
                    }
                    break;
                case "comAccompaniment":
                    if (comAccompaniment.SelectedItem != null)
                    {
                        if (((AccompanimentModel)comAccompaniment.SelectedItem).AccompanimentId == SelectedScore.AccompanimentId) { cbAccompaniment.IsChecked = false; } else { cbAccompaniment.IsChecked = true; }
                    }
                    break;
                case "comLanguage":
                    if (comLanguage.SelectedItem != null)
                    {
                        if (((LanguageModel)comLanguage.SelectedItem).LanguageId == SelectedScore.LanguageId) { cbLanguage.IsChecked = false; } else { cbLanguage.IsChecked = true; }
                    }
                    break;
                case "comPublisher1":
                    if (comPublisher1.SelectedItem != null)
                    {
                        if (((PublisherModel)comPublisher1.SelectedItem).PublisherId == SelectedScore.Publisher1Id) { cbPublisher1.IsChecked = false; } else { cbPublisher1.IsChecked = true; }
                    }
                    break;
                case "comPublisher2":
                    if (comPublisher2.SelectedItem != null)
                    {
                        if (((PublisherModel)comPublisher2.SelectedItem).PublisherId == SelectedScore.Publisher2Id) { cbPublisher2.IsChecked = false; } else { cbPublisher2.IsChecked = true; }
                    }
                    break;
                case "comPublisher3":
                    if (comPublisher3.SelectedItem != null)
                    {
                        if (((PublisherModel)comPublisher3.SelectedItem).PublisherId == SelectedScore.Publisher3Id) { cbPublisher3.IsChecked = false; } else { cbPublisher3.IsChecked = true; }
                    }
                    break;
                case "comPublisher4":
                    if (comPublisher4.SelectedItem != null)
                    {
                        if (((PublisherModel)comPublisher4.SelectedItem).PublisherId == SelectedScore.Publisher4Id) { cbPublisher4.IsChecked = false; } else { cbPublisher4.IsChecked = true; }
                    }
                    break;
            }
        }
        CheckChanged ();
    }
    private void CheckChanged()
    {
        if(cbAccompaniment.IsChecked == true ||
            cbRepertoire.IsChecked  ==  true ||
            cbArchive.IsChecked  ==  true ||
            cbByHeart.IsChecked  == true ||
            cbTitle.IsChecked  == true ||
            cbSubTitle.IsChecked  == true ||
            cbComposer.IsChecked  == true ||
            cbTextwriter.IsChecked  == true ||
            cbArranger.IsChecked  == true ||
            cbGenre.IsChecked  == true ||
            cbAccompaniment.IsChecked  == true ||
            cbLanguage.IsChecked  == true ||
            cbMusicPiece.IsChecked  == true ||
            cbDigitized.IsChecked  == true ||
            cbModified.IsChecked  == true ||
            cbChecked.IsChecked  == true ||
            cbPDFORP.IsChecked  == true ||
            cbPDFORK.IsChecked == true ||
            cbPDFTOP.IsChecked == true ||
            cbPDFTOK.IsChecked == true ||
            cbMSCORP.IsChecked == true ||
            cbMSCORK.IsChecked == true ||
            cbMSCTOP.IsChecked == true ||
            cbMSCTOK.IsChecked  == true ||
            cbMP3B1.IsChecked == true ||
            cbMP3B2.IsChecked == true ||
            cbMP3T1.IsChecked == true ||
            cbMP3T2.IsChecked  == true ||
            cbMP3SOL.IsChecked == true ||
            cbMP3TOT.IsChecked == true ||
            cbMP3PIA.IsChecked == true ||
            cbOnline.IsChecked == true ||
            cbLyrics.IsChecked  == true ||
            cbNotes.IsChecked  == true ||
            cbAmountPublisher1.IsChecked == true ||
            cbAmountPublisher2.IsChecked == true ||
            cbAmountPublisher3.IsChecked == true ||
            cbAmountPublisher4.IsChecked  == true ||
            cbPublisher1.IsChecked == true ||
            cbPublisher2.IsChecked == true ||
            cbPublisher3.IsChecked == true ||
            cbPublisher4.IsChecked  == true)
        {
            btnSave.IsEnabled = true;
            btnSave.ToolTip = "Sla de gewijzigde gegevens op";
        }
        else
        {
            btnSave.IsEnabled = false;
            btnSave.ToolTip = "Er zijn geen gegevens aangepast, opslaan niet mogelijk";
        }

    }
    private void RichTextBoxChanged(object sender, TextChangedEventArgs e)
    {
        var propertyName = ((RichTextBox)sender).Name;

        if ( SelectedScore != null )
        {
            //It is hard to check if the content of a richtextbox really differs, for that reason the changed is always set when triggered
            switch ( propertyName )
            {
                case "memoLyrics":
                    cbLyrics.IsChecked = true;
                    break;
                case "memoNotes":
                    cbNotes.IsChecked = true;
                    break;
            }
        }
    }
    private void CheckBoxChanged ( object sender, RoutedEventArgs e )
    {
        var propertyName = ((CheckBox)sender).Name;

        if ( SelectedScore != null )
        {
            switch ( propertyName )
            {
                case "chkByHeart":
                    if ( chkByHeart.IsChecked == SelectedScore.ByHeart ) { cbByHeart.IsChecked = false; } else { cbByHeart.IsChecked = true; }
                    break;
                case "chkChecked":
                    if ( chkChecked.IsChecked == SelectedScore.Check ) { cbChecked.IsChecked = false; } else { cbChecked.IsChecked = true; }
                    break;
                case "chkMSCORP":
                    if ( chkMSCORP.IsChecked == SelectedScore.MuseScoreORP ) { cbMSCORP.IsChecked = false; } else { cbMSCORP.IsChecked = true; }
                    break;
                case "chkMSCORK":
                    if ( chkMSCORK.IsChecked == SelectedScore.MuseScoreORK ) { cbMSCORK.IsChecked = false; } else { cbMSCORK.IsChecked = true; }
                    break;
                case "chkMSCTOP":
                    if ( chkMSCTOP.IsChecked == SelectedScore.MuseScoreTOP ) { cbMSCTOP.IsChecked = false; } else { cbMSCTOP.IsChecked = true; }
                    break;
                case "chkMSCTOK":
                    if ( chkMSCTOK.IsChecked == SelectedScore.MuseScoreTOK ) { cbMSCTOK.IsChecked = false; } else { cbMSCTOK.IsChecked = true; }
                    break;
                case "chkPDFORP":
                    if ( chkPDFORP.IsChecked == SelectedScore.PDFORP ) { cbPDFORP.IsChecked = false; } else { cbPDFORP.IsChecked = true; }
                    break;
                case "chkPDFORK":
                    if ( chkPDFORK.IsChecked == SelectedScore.PDFORK ) { cbPDFORK.IsChecked = false; } else { cbPDFORK.IsChecked = true; }
                    break;
                case "chkPDFTOP":
                    if ( chkPDFTOP.IsChecked == SelectedScore.PDFTOP ) { cbPDFTOP.IsChecked = false; } else { cbPDFTOP.IsChecked = true; }
                    break;
                case "chkPDFTOK":
                    if ( chkPDFTOK.IsChecked == SelectedScore.PDFTOK ) { cbPDFTOK.IsChecked = false; } else { cbPDFTOK.IsChecked = true; }
                    break;
                case "chkMP3B1":
                    if ( chkMP3B1.IsChecked == SelectedScore.MP3B1 ) { cbMP3B1.IsChecked = false; } else { cbMP3B1.IsChecked = true; }
                    break;
                case "chkMP3B2":
                    if ( chkMP3B2.IsChecked == SelectedScore.MP3B2 ) { cbMP3B2.IsChecked = false; } else { cbMP3B2.IsChecked = true; }
                    break;
                case "chkMP3T1":
                    if ( chkMP3T1.IsChecked == SelectedScore.MP3T1 ) { cbMP3T1.IsChecked = false; } else { cbMP3T1.IsChecked = true; }
                    break;
                case "chkMP3T2":
                    if ( chkMP3T2.IsChecked == SelectedScore.MP3T2 ) { cbMP3T2.IsChecked = false; } else { cbMP3T2.IsChecked = true; }
                    break;
                case "chkMP3SOL":
                    if ( chkMP3SOL.IsChecked == SelectedScore.MP3SOL ) { cbMP3SOL.IsChecked = false; } else { cbMP3SOL.IsChecked = true; }
                    break;
                case "chkMP3TOT":
                    if ( chkMP3TOT.IsChecked == SelectedScore.MP3TOT ) { cbMP3TOT.IsChecked = false; } else { cbMP3TOT.IsChecked = true; }
                    break;
                case "chkMP3PIA":
                    if ( chkMP3PIA.IsChecked == SelectedScore.MP3PIA ) { cbMP3PIA.IsChecked = false; } else { cbMP3PIA.IsChecked = true; }
                    break;
                case "chkMSCOnline":
                    if ( chkMSCOnline.IsChecked == SelectedScore.MuseScoreOnline ) { cbOnline.IsChecked = false; } else { cbOnline.IsChecked = true; }
                    break;
            }
        }
        CheckChanged ();
    }
    private void DatePickerChanged ( object sender, SelectionChangedEventArgs e )
    {
        var propertyName = ((DatePicker)sender).Name;

        if ( SelectedScore != null )
        {
            switch ( propertyName )
            {
                case "dpDigitized":
                    DateTime _CreatedDateTime = new ();

                    // If the change event is triggered a data has been entered, this always differs if no date is in the database
                    if(SelectedScore.DateCreatedString != "" )
                    {
                        var _selectedDateTime = SelectedScore.DateCreatedString.ToString () + " 00:00:00 AM";
                        _CreatedDateTime = DateTime.Parse ( _selectedDateTime );

                        if ( dpDigitized.SelectedDate == _CreatedDateTime ) { cbDigitized.IsChecked = false; } else { cbDigitized.IsChecked = true; }
                    }
                    else
                    {
                        // If the change event is triggered a data has been entered, this always differs if no date is in the database
                        cbDigitized.IsChecked = false;

                    }
                    break;
                case "dpModified":
                    DateTime _ModifiedDateTime = new ();

                    // If the change event is triggered a data has been entered, this always differs if no date is in the database
                    if ( SelectedScore.DateModifiedString != "" )
                    {
                        var _selectedDateTime = SelectedScore.DateModifiedString.ToString () + " 00:00:00 AM";
                        _ModifiedDateTime = DateTime.Parse ( _selectedDateTime );

                        if ( dpModified.SelectedDate == _ModifiedDateTime ) { cbModified.IsChecked = false; } else { cbModified.IsChecked = true; }
                    }
                    else
                    {
                        // If the change event is triggered a data has been entered, this always differs if no date is in the database
                        cbModified.IsChecked = false;
                    }
                    break;
            }
        }
        CheckChanged ();
    }
    private void BtnSaveClick(object sender, RoutedEventArgs e)
    {
        ObservableCollection<SaveScoreModel> ScoreList = new();
        if (SelectedScore != null)
        {
            string Title = "", SubTitle = "", Composer = "", Textwriter = "", Arranger = "", MusicPiece = "",
                    DateDigitized = "", DateModified = "";

            int TitleChanged = -1, SubTitleChanged = -1, 
                ComposerChanged = -1, TextwriterChanged = -1, ArrangerChanged = -1,
                DateDigitizedChanged = -1, DateModifiedChanged = -1, 
                LyricsChanged = -1, MusicPieceChanged = -1, NotesChanged = -1,
                AccompanimentId = -1, ArchiveId = -1, RepertoireId = -1, LanguageId = -1, GenreId = -1, Check = -1, ByHeart = -1, Publisher1Id = -1, Publisher2Id = -1, Publisher3Id = -1, Publisher4Id = -1,
                MuseScoreORP = -1, MuseScoreORK = -1, MuseScoreTOP = -1, MuseScoreTOK = -1, MuseScoreOnline = -1,
                PDFORP = -1, PDFORK = -1, PDFTOP = -1, PDFTOK = -1,
                MP3B1 = -1, MP3B2 = -1, MP3T1 = -1, MP3T2 = -1, MP3SOL = -1, MP3TOT = -1, MP3PIA = -1,
                AmountPublisher1 = -1, AmountPublisher2 = -1, AmountPublisher3 = -1, AmountPublisher4 = -1;

            // How to SAve changed values back to the DataGrid
            // SelectedScore.ScoreSubTitle = tbSubTitle.Text;

            if ( ( bool ) cbAccompaniment.IsChecked) { AccompanimentId = (int)comAccompaniment.SelectedItem; SelectedScore.AccompanimentId = AccompanimentId; }
            if ( ( bool ) cbAmountPublisher1.IsChecked ) { AmountPublisher1 = int.Parse ( tbAmountPublisher1.Text ); SelectedScore.NumberScoresPublisher1 = AmountPublisher1; }
            if ( ( bool ) cbAmountPublisher2.IsChecked ) { AmountPublisher1 = int.Parse ( tbAmountPublisher2.Text ); SelectedScore.NumberScoresPublisher2 = AmountPublisher2; }
            if ( ( bool ) cbAmountPublisher3.IsChecked ) { AmountPublisher1 = int.Parse ( tbAmountPublisher3.Text ); SelectedScore.NumberScoresPublisher3 = AmountPublisher3; }
            if ( ( bool ) cbAmountPublisher4.IsChecked ) { AmountPublisher1 = int.Parse ( tbAmountPublisher4.Text ); SelectedScore.NumberScoresPublisher4 = AmountPublisher4; }
            if ( ( bool ) cbArchive.IsChecked) { ArchiveId = (int)comArchive.SelectedItem; SelectedScore.ArchiveId = ArchiveId; }
            if ( ( bool ) cbArranger.IsChecked) { Arranger = tbArranger.Text; ArrangerChanged = 1; SelectedScore.Arranger = Arranger; }

            if ( ( bool ) cbByHeart.IsChecked) { if ((bool)chkByHeart.IsChecked) { ByHeart = 1; SelectedScore.ByHeart = true; } else { ByHeart = 0; SelectedScore.ByHeart = false; } }

            if ( ( bool ) cbChecked.IsChecked) { if ((bool)chkChecked.IsChecked) { Check = 1; SelectedScore.Check = true; } else { Check = 0; SelectedScore.Check = false; } }
            if ( ( bool ) cbComposer.IsChecked) { Composer = tbComposer.Text; ComposerChanged = 1; SelectedScore.Composer = Composer; }

            if ( ( bool ) cbDigitized.IsChecked)
            {
                string year = dpDigitized.SelectedDate.Value.Year.ToString();
                string month = "0" + (dpDigitized.SelectedDate.Value.Month.ToString());
                string day = "0" + (dpDigitized.SelectedDate.Value.Day.ToString());
                if ( year == "1900" ) { DateDigitized = ""; } 
                else 
                { 
                    DateDigitized = $"{year}-{month.Substring ( month.Length - 2, 2 )}-{day.Substring ( day.Length - 2, 2 )}"; 
                }

                DateTime _created = DateTime.Parse(DateDigitized + "00:00:00 AM");
                SelectedScore.DateCreated = DateOnly.FromDateTime(_created);
            }

            if ( ( bool ) cbGenre.IsChecked) { GenreId = (int)comGenre.SelectedItem; SelectedScore.GenreId = GenreId; }

            if ( ( bool ) cbLanguage.IsChecked) { LanguageId = (int)comLanguage.SelectedItem; SelectedScore.LanguageId = LanguageId; }

            if ( ( bool ) cbModified.IsChecked)
            {
                string year = dpModified.SelectedDate.Value.Year.ToString();
                string month = "0" + dpModified.SelectedDate.Value.Month.ToString();
                string day = "0" + dpModified.SelectedDate.Value.Day.ToString();
                if ( year == "1900" ) { DateModified = ""; }
                else
                {
                    DateModified = $"{year}-{month.Substring ( month.Length - 2, 2 )}-{day.Substring ( day.Length - 2, 2 )}";
                }

                DateTime _modified = DateTime.Parse(DateModified + "00:00:00 AM");
                SelectedScore.DateModified = DateOnly.FromDateTime(_modified);
            }
            if ( ( bool ) cbMP3B1.IsChecked ) { if ( ( bool ) chkMP3B1.IsChecked ) { MP3B1 = 1; SelectedScore.MP3B1 = true; } else { MP3B1 = 0; SelectedScore.MP3B1 = false; } }
            if ( ( bool ) cbMP3B2.IsChecked ) { if ( ( bool ) chkMP3B2.IsChecked ) { MP3B2 = 1; SelectedScore.MP3B2 = true; } else { MP3B2 = 0; SelectedScore.MP3B2 = false; } }
            if ( ( bool ) cbMP3PIA.IsChecked ) { if ( ( bool ) chkMP3PIA.IsChecked ) { MP3PIA = 1; SelectedScore.MP3PIA = true; } else { MP3PIA = 0; SelectedScore.MP3PIA = false; } }
            if ( ( bool ) cbMP3SOL.IsChecked ) { if ( ( bool ) chkMP3SOL.IsChecked ) { MP3SOL = 1; SelectedScore.MP3SOL = true; } else { MP3SOL = 0; SelectedScore.MP3SOL = false; } }
            if ( ( bool ) cbMP3T1.IsChecked ) { if ( ( bool ) chkMP3T1.IsChecked ) { MP3T1 = 1; SelectedScore.MP3T1 = true; } else { MP3T1 = 0; SelectedScore.MP3T1 = false; } }
            if ( ( bool ) cbMP3T2.IsChecked ) { if ( ( bool ) chkMP3T2.IsChecked ) { MP3T2 = 1; SelectedScore.MP3T2 = true; } else { MP3T2 = 0; SelectedScore.MP3T2 = false; } }
            if ( ( bool ) cbMP3TOT.IsChecked ) { if ( ( bool ) chkMP3TOT.IsChecked ) { MP3TOT = 1; SelectedScore.MP3TOT = true; } else { MP3TOT = 0; SelectedScore.MP3TOT = false; } }

            if ( ( bool ) cbMSCORK.IsChecked) { if ( ( bool ) chkMSCORK.IsChecked ) { MuseScoreORK = 1; SelectedScore.MuseScoreORK = true; } else { MuseScoreORK = 0; SelectedScore.MuseScoreORK = false; } }
            if ( ( bool ) cbMSCORP.IsChecked ) { if ( ( bool ) chkMSCORP.IsChecked ) { MuseScoreORP = 1; SelectedScore.MuseScoreORP = true; } else { MuseScoreORP = 0; SelectedScore.MuseScoreORP = false; } }
            if ( ( bool ) cbMSCTOK.IsChecked ) { if ( ( bool ) chkMSCTOK.IsChecked ) { MuseScoreTOK = 1; SelectedScore.MuseScoreTOK = true; } else { MuseScoreTOK = 0; SelectedScore.MuseScoreTOK = false; } }
            if ( ( bool ) cbMSCTOP.IsChecked ) { if ( ( bool ) chkMSCTOP.IsChecked ) { MuseScoreTOP = 1; SelectedScore.MuseScoreORK = true; } else { MuseScoreTOP = 0; SelectedScore.MuseScoreTOP = false; } }
            if ( ( bool ) cbMusicPiece.IsChecked ) { MusicPiece = tbMusicPiece.Text; MusicPieceChanged = 1; SelectedScore.MusicPiece = MusicPiece; }

            if ( ( bool ) cbOnline.IsChecked ) { if ( ( bool ) chkMSCOnline.IsChecked ) { MuseScoreOnline = 1; SelectedScore.MuseScoreOnline = true; } else { MuseScoreOnline = 0; SelectedScore.MuseScoreOnline = false; } }

            if ( ( bool ) cbPDFORK.IsChecked ) { if ( ( bool ) chkPDFORK.IsChecked ) { PDFORK = 1; SelectedScore.PDFORK = true; } else { PDFORK = 0; SelectedScore.PDFORK = false; } }
            if ( ( bool ) cbPDFORP.IsChecked ) { if ( ( bool ) chkPDFORP.IsChecked ) { PDFORP = 1; SelectedScore.PDFORP = true; } else { PDFORP = 0; SelectedScore.PDFORP = false;} }
            if ( ( bool ) cbPDFTOK.IsChecked ) { if ( ( bool ) chkPDFTOK.IsChecked ) { PDFTOK = 1; SelectedScore.PDFTOK = true; } else { PDFTOK = 0; SelectedScore.PDFTOK = false; } }
            if ( ( bool ) cbPDFTOP.IsChecked ) { if ( ( bool ) chkPDFTOP.IsChecked ) { PDFTOP = 1; SelectedScore.PDFTOP = true; } else { PDFTOP = 0; SelectedScore.PDFTOP = false; } }
            if ( ( bool ) cbPublisher1.IsChecked ) { Publisher1Id = ( int ) comPublisher1.SelectedItem; SelectedScore.Publisher1Id = Publisher1Id; }
            if ( ( bool ) cbPublisher2.IsChecked ) { Publisher1Id = ( int ) comPublisher2.SelectedItem; SelectedScore.Publisher2Id = Publisher2Id;}
            if ( ( bool ) cbPublisher3.IsChecked ) { Publisher1Id = ( int ) comPublisher3.SelectedItem; SelectedScore.Publisher3Id = Publisher3Id;}
            if ( ( bool ) cbPublisher4.IsChecked ) { Publisher1Id = ( int ) comPublisher4.SelectedItem; SelectedScore.Publisher4Id = Publisher4Id; }

            if ( ( bool ) cbRepertoire.IsChecked) { RepertoireId = (int)comRepertoire.SelectedItem; SelectedScore.RepertoireId = RepertoireId; }

            if ( ( bool ) cbSubTitle.IsChecked) { SubTitle = tbSubTitle.Text; SubTitleChanged = 1; SelectedScore.ScoreSubTitle = SubTitle; }

            if ( ( bool ) cbTextwriter.IsChecked) { Textwriter = tbTextwriter.Text; TextwriterChanged = 1; SelectedScore.Textwriter = Textwriter; }
            if ( ( bool ) cbTitle.IsChecked ) { Title = tbTitle.Text; TitleChanged = 1; SelectedScore.ScoreTitle = Title; }

            if ( ( bool ) cbLyrics.IsChecked ) { Lyrics = (DataGridTextColumn)memoLyrics.DataContext; LyricsChanged = 1; SelectedScore.Lyrics = Lyrics.ToString(); }
            if ( ( bool ) cbNotes.IsChecked ) { Notes = (DataGridTextColumn)memoNotes.DataContext; NotesChanged = 1; SelectedScore.Notes = Notes.ToString(); }

            ScoreList.Add ( new SaveScoreModel
            {
                AccompanimentId = AccompanimentId,
                ArchiveId = ArchiveId,
                Arranger = Arranger,
                ArrangerChanged = ArrangerChanged,
                ByHeart = ByHeart,
                Checked = Check,
                Composer = Composer,
                ComposerChanged = ComposerChanged,
                DateDigitized = DateDigitized,
                DateDigitizedChanged = DateDigitizedChanged,
                DateModified = DateModified,
                DateModifiedChanged = DateModifiedChanged,
                GenreId = GenreId,
                LanguageId = LanguageId,
                Lyrics = ( string ) memoLyrics.DataContext,
                LyricsChanged = LyricsChanged,
                MP3B1 = MP3B1,
                MP3B2 = MP3B2,
                MP3PIA = MP3PIA,
                MP3SOL = MP3SOL,
                MP3T1 = MP3T1,
                MP3T2 = MP3T2,
                MP3TOT = MP3TOT,
                MuseScoreOnline = MuseScoreOnline,
                MuseScoreORK = MuseScoreORK,
                MuseScoreORP = MuseScoreORP,
                MuseScoreTOK = MuseScoreTOK,
                MuseScoreTOP = MuseScoreTOP,
                MusicPiece = MusicPiece,
                MusicPieceChanged = MusicPieceChanged,
                Notes = ( string ) memoNotes.DataContext,
                NotesChanged = NotesChanged,
                AmountPublisher1 = AmountPublisher1,
                AmountPublisher2 = AmountPublisher2,
                AmountPublisher3 = AmountPublisher3,
                AmountPublisher4 = AmountPublisher4,
                PDFORK = PDFORK,
                PDFORP = PDFORP,
                PDFTOK = PDFTOK,
                PDFTOP = PDFTOP,
                Publisher1Id = Publisher1Id,
                Publisher2Id = Publisher2Id,
                Publisher3Id = Publisher3Id,
                Publisher4Id = Publisher4Id,
                RepertoireId = RepertoireId,
                ScoreNumber = SelectedScore.Score,
                ScoreId = SelectedScore.ScoreId,
                ScoreMainNumber = SelectedScore.ScoreNumber,
                ScoreSubNumber = SelectedScore.ScoreSubNumber,
                SubTitle = SubTitle,
                SubTitleChanged = SubTitleChanged,
                Title = Title,
                TitleChanged = TitleChanged,
                TextWriter = Textwriter,
                TextwriterChanged = TextwriterChanged
            } ) ;

            DBCommands.SaveScore ( ScoreList );
            DBCommands.GetScores ( DBNames.ScoresView, DBNames.ScoresFieldNameScoreNumber );

            ResetChanged ();
        }
    }
    public void ResetChanged ()
    {
        cbAccompaniment.IsChecked = false;
        cbRepertoire.IsChecked = false;
        cbArchive.IsChecked = false;
        cbByHeart.IsChecked = false;
        cbTitle.IsChecked = false;
        cbSubTitle.IsChecked = false;
        cbComposer.IsChecked = false;
        cbTextwriter.IsChecked = false;
        cbArranger.IsChecked = false;
        cbGenre.IsChecked = false;
        cbAccompaniment.IsChecked = false;
        cbLanguage.IsChecked = false;
        cbMusicPiece.IsChecked = false;
        cbDigitized.IsChecked = false;
        cbModified.IsChecked = false;
        cbChecked.IsChecked = false;
        cbPDFORP.IsChecked = false;
        cbPDFORK.IsChecked = false;
        cbPDFTOP.IsChecked = false;
        cbPDFTOK.IsChecked = false;
        cbMSCORP.IsChecked = false;
        cbMSCORK.IsChecked = false;
        cbMSCTOP.IsChecked = false;
        cbMSCTOK.IsChecked = false;
        cbMP3B1.IsChecked = false;
        cbMP3B2.IsChecked = false;
        cbMP3T1.IsChecked = false;
        cbMP3T2.IsChecked = false;
        cbMP3SOL.IsChecked = false;
        cbMP3TOT.IsChecked = false;
        cbMP3PIA.IsChecked = false;
        cbOnline.IsChecked = false;
        cbLyrics.IsChecked = false;
        cbNotes.IsChecked = false;
        cbAmountPublisher1.IsChecked = false;
        cbAmountPublisher2.IsChecked = false;
        cbAmountPublisher3.IsChecked = false;
        cbAmountPublisher4.IsChecked = false;
        cbPublisher1.IsChecked = false;
        cbPublisher2.IsChecked = false;
        cbPublisher3.IsChecked = false;
        cbPublisher4.IsChecked = false;
        btnSave.IsEnabled = false;
        btnSave.ToolTip = "Er zijn geen gegevens aangepast, opslaan niet mogelijk";
    }

    private void DeleteScore ( object sender, RoutedEventArgs e ) 
    {
        if ( SelectedScore != null ) 
        {
            MessageBoxResult messageBoxResult = MessageBox.Show ( $"Weet je zeker dat je {SelectedScore.ScoreNumber} - {SelectedScore.ScoreTitle} wilt verwijderen?", $"Verwijder partituur {SelectedScore.ScoreNumber}", MessageBoxButton.YesNoCancel );

            switch ( messageBoxResult ) 
            {
                case MessageBoxResult.Yes:
                    // Continue Deleting Score
                    if(SelectedScore.ScoreNumber != null)
                    { 
                        DBCommands.DeleteScore ( SelectedScore.ScoreNumber, SelectedScore.ScoreSubNumber ); 
                    }
                    
                    break;
                case MessageBoxResult.No:
                    // Do nothing no deletion wanted
                    break;
                case MessageBoxResult.Cancel:
                    // Do Nothing Deletion canceled
                    break;

            }
        }
    }
}

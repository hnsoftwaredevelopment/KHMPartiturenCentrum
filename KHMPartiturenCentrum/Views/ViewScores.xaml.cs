using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using KHM.Helpers;
using KHM.Models;
using KHM.ViewModels;
using Syncfusion.DocIO;
using static KHM.App;

namespace KHM.Views;

/// <summary>
/// Interaction logic for ViewScores.xaml
/// </summary>
public partial class ViewScores : Page
{
    public ScoreViewModel? scores;

    public ScoreModel? SelectedScore;
    public int LyricsMaxWidthRow, LyricsMaxWidth;

    public ViewScores ( )
    {
        InitializeComponent ( );

        tbLogedInUserName.Text = LibraryUsers.SelectedUserName;
        tbLogedInFullName.Text = LibraryUsers.SelectedUserFullName;

        scores = new ScoreViewModel ( );
        ScoresDataGrid.ItemsSource = scores.Scores;
        //DataContext = scores;

        if ( LibraryUsers.SelectedUserRoleId == 4 || LibraryUsers.SelectedUserRoleId == 6 || LibraryUsers.SelectedUserRoleId == 8 || LibraryUsers.SelectedUserRoleId == 10 || LibraryUsers.SelectedUserRoleId == 11 || LibraryUsers.SelectedUserRoleId == 13 || LibraryUsers.SelectedUserRoleId == 14 || LibraryUsers.SelectedUserRoleId == 15 )
        {
            tbEnableEditFields.Text = "True";
            tbAdminMode.Text = "Visible";
        }
        else
        {
            tbEnableEdit.Text = "Collapsed";
            tbAdminMode.Text = "Collapsed";
            tbEnableEditFields.Text = "False";
        }
    }

    private void PageLoaded ( object sender, RoutedEventArgs e )
    {
        comAccompaniment.ItemsSource = DBCommands.GetAccompaniments ( );
        comArchive.ItemsSource = DBCommands.GetArchives ( );
        comGenre.ItemsSource = DBCommands.GetGenres ( );
        comLanguage.ItemsSource = DBCommands.GetLanguages ( );
        comRepertoire.ItemsSource = DBCommands.GetRepertoires ( );
        comPublisher1.ItemsSource = DBCommands.GetPublishers ( );
        comPublisher2.ItemsSource = DBCommands.GetPublishers ( );
        comPublisher3.ItemsSource = DBCommands.GetPublishers ( );
        comPublisher4.ItemsSource = DBCommands.GetPublishers ( );
        ResetChanged ( );
    }

    private void SelectedScoreChanged ( object sender, SelectionChangedEventArgs e )
    {
        ResetFields ( );
        DataGrid dg = (DataGrid)sender;

        ScoreModel selectedRow = (ScoreModel)dg.SelectedItem;

        if ( selectedRow == null )
        {
            object item = dg.Items[0];
            dg.SelectedItem = item;
            selectedRow = ( ScoreModel ) dg.SelectedItem;

            // Scroll to the item in the DataGrid
            dg.ScrollIntoView ( dg.Items [ 0 ] );
        }

        SelectedScore = selectedRow;

        #region TAB Score Information
        #region 1st Row (ScoreNumber, Repertoire, Archive, and sing by heart)

        tbScoreNumber.Text = selectedRow.Score;

        #region Repertoire Combobox

        comRepertoire.Text = selectedRow.RepertoireName;
        foreach ( RepertoireModel repertoire in comRepertoire.Items )
        {
            if ( comRepertoire.Text == null )
            { comRepertoire.Text = ""; }
            if ( repertoire.RepertoireName == comRepertoire.Text.ToString ( ) )
            {
                comRepertoire.SelectedItem = repertoire;
            }
        }

        #endregion Repertoire Combobox

        #region Archive Combobox

        comArchive.Text = selectedRow.ArchiveName;
        foreach ( ArchiveModel archive in comArchive.Items )
        {
            if ( comArchive.Text == null )
            { comArchive.Text = ""; }
            if ( archive.ArchiveName == comArchive.Text.ToString ( ) )
            {
                comArchive.SelectedItem = archive;
            }
        }

        #endregion Archive Combobox

        #region Sing By Heart checkbox

        chkByHeart.IsChecked = selectedRow.ByHeart;

        #endregion Sing By Heart checkbox

        #endregion 1st Row (ScoreNumber, Repertoire, Archive, and sing by heart)

        #region 2th Row (Title and SubTitle)

        tbTitle.Text = selectedRow.ScoreTitle;
        tbSubTitle.Text = selectedRow.ScoreSubTitle;

        #endregion 2th Row (Title and SubTitle)

        #region 3th Row (Composer, Textwriter and Arranger)

        tbComposer.Text = selectedRow.Composer;
        tbTextwriter.Text = selectedRow.Textwriter;
        tbArranger.Text = selectedRow.Arranger;

        #endregion 3th Row (Composer, Textwriter and Arranger)

        #region 4th Row (Genre, Accompaniment and Language)

        #region Genre Combobox

        comGenre.Text = selectedRow.GenreName;
        foreach ( GenreModel genre in comGenre.Items )
        {
            if ( comGenre.Text == null )
            { comGenre.Text = ""; }
            if ( genre.GenreName == comGenre.Text.ToString ( ) )
            {
                comGenre.SelectedItem = genre;
            }
        }

        #endregion Genre Combobox

        #region Accompaniment Combobox

        comAccompaniment.Text = selectedRow.AccompanimentName;
        foreach ( AccompanimentModel accompaniment in comAccompaniment.Items )
        {
            if ( comAccompaniment.Text == null )
            { comAccompaniment.Text = ""; }
            if ( accompaniment.AccompanimentName == comAccompaniment.Text.ToString ( ) )
            {
                comAccompaniment.SelectedItem = accompaniment;
            }
        }

        #endregion Accompaniment Combobox

        #region Language Combobox

        comLanguage.Text = selectedRow.LanguageName;
        foreach ( LanguageModel language in comLanguage.Items )
        {
            if ( comLanguage.Text == null )
            { comLanguage.Text = ""; }
            if ( language.LanguageName == comLanguage.Text.ToString ( ) )
            {
                comLanguage.SelectedItem = language;
            }
        }

        #endregion Language Combobox

        #endregion 4th Row (Genre, Accompaniment and Language)

        #region 5th Row (Music Piece)

        tbMusicPiece.Text = selectedRow.MusicPiece;

        #endregion

        #region 6th Row (Duration)

        tbMinutes.Text = selectedRow.DurationMinutes.ToString ( );
        tbSeconds.Text = selectedRow.DurationSeconds.ToString ( "00 " );

        #endregion
        #endregion

        #region TAB Digitizing information
        #region 1th Row (Date created, Date Modified and Checked)

        #region Date Digitized

        if ( selectedRow.DateCreatedString != "" )
        {
            dpDigitized.SelectedDate = selectedRow.DateDigitized.ToDateTime ( TimeOnly.Parse ( "00:00 AM" ) );
            dpDigitized.Text = selectedRow.DateCreatedString;
        }

        #endregion Date Digitized

        #region Date Modified

        if ( selectedRow.DateModifiedString != "" )
        {
            dpModified.SelectedDate = selectedRow.DateModified.ToDateTime ( TimeOnly.Parse ( "00:00 AM" ) );
            dpModified.Text = selectedRow.DateModifiedString;
        }

        #endregion Date Modified

        #region Checked

        chkChecked.IsChecked = selectedRow.Checked;

        #endregion Checked

        #endregion

        #region 2th Row (Checkboxes for MuseScore, PDF and MP3)

        #region MuseScore checkboxes

        chkMSCORP.IsChecked = selectedRow.MuseScoreORP;
        chkMSCORK.IsChecked = selectedRow.MuseScoreORK;
        chkMSCTOP.IsChecked = selectedRow.MuseScoreTOP;
        chkMSCTOK.IsChecked = selectedRow.MuseScoreTOK;

        #endregion MuseScore checkboxes

        #region PDF checkboxes

        chkPDFORP.IsChecked = selectedRow.PDFORP;
        chkPDFORK.IsChecked = selectedRow.PDFORK;
        chkPDFTOP.IsChecked = selectedRow.PDFTOP;
        chkPDFTOK.IsChecked = selectedRow.PDFTOK;

        #endregion PDF checkboxes

        #region MP3 checkboxes

        chkMP3B1.IsChecked = selectedRow.MP3B1;
        chkMP3B2.IsChecked = selectedRow.MP3B2;
        chkMP3T1.IsChecked = selectedRow.MP3T1;
        chkMP3T2.IsChecked = selectedRow.MP3T2;

        chkMP3SOL.IsChecked = selectedRow.MP3SOL;
        chkMP3TOT.IsChecked = selectedRow.MP3TOT;
        chkMP3PIA.IsChecked = selectedRow.MP3PIA;

        #endregion MP3 checkboxes

        #region MuseScore Online checkbox

        chkMSCOnline.IsChecked = selectedRow.MuseScoreOnline;

        #endregion MuseScore Online checkbox

        #endregion
        #endregion

        #region TAB Lyrics
        tbLyrics.Text = selectedRow.Lyrics;
        GetLyricsInfo ( );
        #endregion TAB Lyrics

        #region TAB Notes

        GetNotes ( );

        #endregion TAB Notes

        #region TAB: Licenses

        #region Publisher 1

        tbAmountPublisher1.Text = selectedRow.AmountPublisher1.ToString ( );

        #region Publisher1 Combobox

        comPublisher1.Text = selectedRow.Publisher1Name;
        foreach ( PublisherModel publisher in comPublisher1.Items )
        {
            comPublisher1.Text ??= "";
            if ( publisher.PublisherName == comPublisher1.Text.ToString ( ) )
            {
                comPublisher1.SelectedItem = publisher;
            }
        }

        #endregion Publisher1 Combobox

        #endregion Publisher 1

        #region Publisher 2

        tbAmountPublisher2.Text = selectedRow.AmountPublisher2.ToString ( );

        #region Publisher2 Combobox

        comPublisher2.Text = selectedRow.Publisher2Name;
        foreach ( PublisherModel publisher in comPublisher2.Items )
        {
            comPublisher2.Text ??= "";
            if ( publisher.PublisherName == comPublisher2.Text.ToString ( ) )
            {
                comPublisher2.SelectedItem = publisher;
            }
        }

        #endregion Publisher2 Combobox

        #endregion Publisher 2

        #region Publisher 3

        tbAmountPublisher3.Text = selectedRow.AmountPublisher3.ToString ( );

        #region Publisher3 Combobox

        comPublisher3.Text = selectedRow.Publisher3Name;
        foreach ( PublisherModel publisher in comPublisher3.Items )
        {
            comPublisher3.Text ??= "";
            if ( publisher.PublisherName == comPublisher3.Text.ToString ( ) )
            {
                comPublisher3.SelectedItem = publisher;
            }
        }

        #endregion Publisher3 Combobox

        #endregion Publisher 3

        #region Publisher 4

        tbAmountPublisher4.Text = selectedRow.AmountPublisher4.ToString ( );

        #region Publisherr4 Combobox

        comPublisher4.Text = selectedRow.Publisher4Name;
        foreach ( PublisherModel publisher in comPublisher4.Items )
        {
            comPublisher4.Text ??= "";
            if ( publisher.PublisherName == comPublisher4.Text.ToString ( ) )
            {
                comPublisher4.SelectedItem = publisher;
            }
        }

        #endregion Publisherr4 Combobox

        #endregion Publisher 4

        #region Total

        var Total = selectedRow.AmountPublisher1 + selectedRow.AmountPublisher2 + selectedRow.AmountPublisher3 + selectedRow.AmountPublisher4;
        tbAmountSupplierTotal.Text = Total.ToString ( );

        #endregion Total

        #endregion TAB: Licenses

        ResetChanged ( );
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

        // Scroll to the item in the GridView
        ScoresDataGrid.ScrollIntoView ( ScoresDataGrid.Items [ ScoresDataGrid.SelectedIndex ] );
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

        // Scroll to the item in the GridView
        ScoresDataGrid.ScrollIntoView ( ScoresDataGrid.Items [ ScoresDataGrid.SelectedIndex ] );
    }

    private void BtnLastClick ( object sender, RoutedEventArgs e )
    {
        ScoresDataGrid.SelectedIndex = ScoresDataGrid.Items.Count - 1;

        // Scroll to the item in the GridView
        ScoresDataGrid.ScrollIntoView ( ScoresDataGrid.Items [ ScoresDataGrid.SelectedIndex ] );
    }

    private void BtnFirstClick ( object sender, RoutedEventArgs e )
    {
        ScoresDataGrid.SelectedIndex = 0;

        // Scroll to the item in the GridView
        ScoresDataGrid.ScrollIntoView ( ScoresDataGrid.Items [ ScoresDataGrid.SelectedIndex ] );
    }

    private void TextBoxChanged ( object sender, TextChangedEventArgs e )
    {
        var propertyName = ((TextBox)sender).Name;

        if ( SelectedScore != null )
        {
            switch ( propertyName )
            {
                case "tbTitle":
                    if ( tbTitle.Text == SelectedScore.ScoreTitle )
                    { cbTitle.IsChecked = false; }
                    else
                    { cbTitle.IsChecked = true; }
                    break;

                case "tbSubTitle":
                    if ( tbSubTitle.Text == SelectedScore.ScoreSubTitle )
                    { cbSubTitle.IsChecked = false; }
                    else
                    { cbSubTitle.IsChecked = true; }
                    break;

                case "tbComposer":
                    if ( tbComposer.Text == SelectedScore.Composer )
                    { cbComposer.IsChecked = false; }
                    else
                    { cbComposer.IsChecked = true; }
                    break;

                case "tbTextwriter":
                    if ( tbTextwriter.Text == SelectedScore.Textwriter )
                    { cbTextwriter.IsChecked = false; }
                    else
                    { cbTextwriter.IsChecked = true; }
                    break;

                case "tbArranger":
                    if ( tbArranger.Text == SelectedScore.Arranger )
                    { cbArranger.IsChecked = false; }
                    else
                    { cbArranger.IsChecked = true; }
                    break;

                case "tbMusicPiece":
                    if ( tbMusicPiece.Text == SelectedScore.MusicPiece )
                    { cbMusicPiece.IsChecked = false; }
                    else
                    { cbMusicPiece.IsChecked = true; }
                    break;

                case "tbAmountPublisher1":
                    if ( tbAmountPublisher1.Text == SelectedScore.AmountPublisher1.ToString ( ) )
                    { cbAmountPublisher1.IsChecked = false; }
                    else
                    { cbAmountPublisher1.IsChecked = true; }
                    CalculateTotal ( );
                    break;

                case "tbAmountPublisher2":
                    if ( tbAmountPublisher2.Text == SelectedScore.AmountPublisher2.ToString ( ) )
                    { cbAmountPublisher2.IsChecked = false; }
                    else
                    { cbAmountPublisher2.IsChecked = true; }
                    CalculateTotal ( );
                    break;

                case "tbAmountPublisher3":
                    if ( tbAmountPublisher3.Text == SelectedScore.AmountPublisher3.ToString ( ) )
                    { cbAmountPublisher3.IsChecked = false; }
                    else
                    { cbAmountPublisher3.IsChecked = true; }
                    CalculateTotal ( );
                    break;

                case "tbAmountPublisher4":
                    if ( tbAmountPublisher4.Text == SelectedScore.AmountPublisher4.ToString ( ) )
                    { cbAmountPublisher4.IsChecked = false; }
                    else
                    { cbAmountPublisher4.IsChecked = true; }
                    CalculateTotal ( );
                    break;
                case "tbLyrics":
                    if ( tbLyrics.Text == SelectedScore.Lyrics )
                    {
                        cbLyrics.IsChecked = false;
                    }
                    else
                    {
                        cbLyrics.IsChecked = true;
                    }

                    GetLyricsInfo ( );
                    break;
                case "tbMinutes":
                    if ( tbMinutes.Text == SelectedScore.DurationMinutes.ToString ( ) )
                    { cbDurationMinutes.IsChecked = false; }
                    else
                    { cbDurationMinutes.IsChecked = true; }
                    break;
                case "tbSeconds":
                    int value;
                    if ( !int.TryParse ( tbSeconds.Text, out value ) || value < 0 || value > 59 )
                    {
                        MessageBox.Show ( "Gelieve een numerieke waarde tussen 0 en 59 in te voeren." );
                        tbSeconds.Focus ( );
                        return;
                    }

                    if ( tbSeconds.Text == SelectedScore.DurationSeconds.ToString ( "00" ) )
                    { cbDurationSeconds.IsChecked = false; }
                    else
                    { cbDurationSeconds.IsChecked = true; }
                    break;
            }
        }
        CheckChanged ( );
    }

    private void GetLyricsInfo ( )
    {
        //tbLyrics.Text = SelectedScore.Lyrics.ToString();
        // Fill the general information fields
        if ( tbLyrics.Text != "" )
        {
            SelectedScore.Lyrics = tbLyrics.Text;
            // Number of lines
            tbLyricsRows.Text = SelectedScore.Lyrics.Split ( "\r\n" ).Length.ToString ( );

            // Check if number of lines exceeds 98 if yes, show warning
            if ( SelectedScore.Lyrics.Split ( "\r\n" ).Length > 98 )
            {
                LyricsWarning.Visibility = Visibility.Visible;
            }
            else
            {
                LyricsWarning.Visibility = Visibility.Collapsed;
            }

            // Number of Words
            tbLyricsWords.Text = SelectedScore.Lyrics.Split ( " " ).Length.ToString ( );

            // Number of Columns
            if ( SelectedScore.Lyrics.Split ( "\r\n" ).Length == 0 )
            { tbLyricsColumns.Text = "0"; }
            if ( SelectedScore.Lyrics.Split ( "\r\n" ).Length > 0 && SelectedScore.Lyrics.Split ( "\r\n" ).Length <= 36 )
            { tbLyricsColumns.Text = "1"; }
            if ( SelectedScore.Lyrics.Split ( "\r\n" ).Length > 36 )
            { tbLyricsColumns.Text = "2"; }

            //Most Characters on row
            LyricsMaxWidth = 0;
            LyricsMaxWidthRow = 0;

            var _rowNumber = 0;

            var rows = SelectedScore.Lyrics.Split ( "\r\n" );
            foreach ( var row in rows )
            {
                var _rowSize = row.Length;
                if ( _rowSize > LyricsMaxWidth )
                {
                    LyricsMaxWidth = _rowSize;
                    LyricsMaxWidthRow = _rowNumber + 1;

                    tbLyricsMaxWidth.Text = LyricsMaxWidth.ToString ( );
                    tbLyricsMaxWidthRow.Text = $"(Regel {LyricsMaxWidthRow})";
                }
                _rowNumber++;
            }

            // Check if the Number of characters on row doesn't exceed the maximum
            if ( int.Parse ( tbLyricsColumns.Text ) == 2 )
            {
                // When 2 Columns are used
                if ( LyricsMaxWidth > 55 )
                {
                    LyricsWidthWarning2.Visibility = Visibility.Visible;
                    LyricsWidthWarning1.Visibility = Visibility.Collapsed;
                }
                else
                {
                    LyricsWidthWarning2.Visibility = Visibility.Collapsed;
                    LyricsWidthWarning1.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                // When 1 Column is used
                if ( LyricsMaxWidth > 199 )
                {
                    LyricsWidthWarning1.Visibility = Visibility.Visible;
                    LyricsWidthWarning2.Visibility = Visibility.Collapsed;
                }
                else
                {
                    LyricsWidthWarning1.Visibility = Visibility.Collapsed;
                    LyricsWidthWarning2.Visibility = Visibility.Collapsed;
                }
            }

        }
        else
        {
            tbLyricsRows.Text = "Nog geen liedtekst ingevoerd";
            tbLyricsWords.Text = "";
            tbLyricsColumns.Text = "Sjabloon zonder liedtekst kolom zal worden gebruikt";
            tbLyricsMaxWidth.Text = "";
            tbLyricsMaxWidthRow.Text = "";
        }
    }

    private void CalculateTotal ( )
    {
        int _total = 0;
        if ( tbAmountPublisher1.Text != "" )
        { _total += int.Parse ( tbAmountPublisher1.Text ); }
        if ( tbAmountPublisher2.Text != "" )
        { _total += int.Parse ( tbAmountPublisher2.Text ); }
        if ( tbAmountPublisher3.Text != "" )
        { _total += int.Parse ( tbAmountPublisher3.Text ); }
        if ( tbAmountPublisher4.Text != "" )
        { _total += int.Parse ( tbAmountPublisher4.Text ); }

        tbAmountSupplierTotal.Text = _total.ToString ( );
    }

    private void ComboBoxChanged ( object sender, SelectionChangedEventArgs e )
    {
        var propertyName = ((ComboBox)sender).Name;

        if ( SelectedScore != null )
        {
            switch ( propertyName )
            {
                case "comRepertoire":
                    if ( comRepertoire.SelectedItem != null )
                    {
                        if ( ( ( RepertoireModel ) comRepertoire.SelectedItem ).RepertoireId == SelectedScore.RepertoireId )
                        { cbRepertoire.IsChecked = false; }
                        else
                        { cbRepertoire.IsChecked = true; }
                    }
                    break;

                case "comArchive":
                    if ( comArchive.SelectedItem != null )
                    {
                        if ( ( ( ArchiveModel ) comArchive.SelectedItem ).ArchiveId == SelectedScore.ArchiveId )
                        { cbArchive.IsChecked = false; }
                        else
                        { cbArchive.IsChecked = true; }
                    }
                    break;

                case "comGenre":
                    if ( comGenre.SelectedItem != null )
                    {
                        if ( ( ( GenreModel ) comGenre.SelectedItem ).GenreId == SelectedScore.GenreId )
                        { cbGenre.IsChecked = false; }
                        else
                        { cbGenre.IsChecked = true; }
                    }
                    break;

                case "comAccompaniment":
                    if ( comAccompaniment.SelectedItem != null )
                    {
                        if ( ( ( AccompanimentModel ) comAccompaniment.SelectedItem ).AccompanimentId == SelectedScore.AccompanimentId )
                        { cbAccompaniment.IsChecked = false; }
                        else
                        { cbAccompaniment.IsChecked = true; }
                    }
                    break;

                case "comLanguage":
                    if ( comLanguage.SelectedItem != null )
                    {
                        if ( ( ( LanguageModel ) comLanguage.SelectedItem ).LanguageId == SelectedScore.LanguageId )
                        { cbLanguage.IsChecked = false; }
                        else
                        { cbLanguage.IsChecked = true; }
                    }
                    break;

                case "comPublisher1":
                    if ( comPublisher1.SelectedItem != null )
                    {
                        if ( ( ( PublisherModel ) comPublisher1.SelectedItem ).PublisherId == SelectedScore.Publisher1Id )
                        { cbPublisher1.IsChecked = false; }
                        else
                        { cbPublisher1.IsChecked = true; }
                    }
                    break;

                case "comPublisher2":
                    if ( comPublisher2.SelectedItem != null )
                    {
                        if ( ( ( PublisherModel ) comPublisher2.SelectedItem ).PublisherId == SelectedScore.Publisher2Id )
                        { cbPublisher2.IsChecked = false; }
                        else
                        { cbPublisher2.IsChecked = true; }
                    }
                    break;

                case "comPublisher3":
                    if ( comPublisher3.SelectedItem != null )
                    {
                        if ( ( ( PublisherModel ) comPublisher3.SelectedItem ).PublisherId == SelectedScore.Publisher3Id )
                        { cbPublisher3.IsChecked = false; }
                        else
                        { cbPublisher3.IsChecked = true; }
                    }
                    break;

                case "comPublisher4":
                    if ( comPublisher4.SelectedItem != null )
                    {
                        if ( ( ( PublisherModel ) comPublisher4.SelectedItem ).PublisherId == SelectedScore.Publisher4Id )
                        { cbPublisher4.IsChecked = false; }
                        else
                        { cbPublisher4.IsChecked = true; }
                    }
                    break;
            }
        }
        CheckChanged ( );
    }

    private void CheckChanged ( )
    {
        if ( cbDurationMinutes.IsChecked == true || cbDurationSeconds.IsChecked == true )
        { cbDuration.IsChecked = true; }
        else
        { cbDuration.IsChecked = false; }

        if ( cbAccompaniment.IsChecked == true ||
            cbRepertoire.IsChecked == true ||
            cbArchive.IsChecked == true ||
            cbByHeart.IsChecked == true ||
            cbTitle.IsChecked == true ||
            cbSubTitle.IsChecked == true ||
            cbComposer.IsChecked == true ||
            cbTextwriter.IsChecked == true ||
            cbArranger.IsChecked == true ||
            cbGenre.IsChecked == true ||
            cbAccompaniment.IsChecked == true ||
            cbLanguage.IsChecked == true ||
            cbMusicPiece.IsChecked == true ||
            cbDigitized.IsChecked == true ||
            cbModified.IsChecked == true ||
            cbChecked.IsChecked == true ||
            cbPDFORP.IsChecked == true ||
            cbPDFORK.IsChecked == true ||
            cbPDFTOP.IsChecked == true ||
            cbPDFTOK.IsChecked == true ||
            cbMSCORP.IsChecked == true ||
            cbMSCORK.IsChecked == true ||
            cbMSCTOP.IsChecked == true ||
            cbMSCTOK.IsChecked == true ||
            cbMP3B1.IsChecked == true ||
            cbMP3B2.IsChecked == true ||
            cbMP3T1.IsChecked == true ||
            cbMP3T2.IsChecked == true ||
            cbMP3SOL.IsChecked == true ||
            cbMP3TOT.IsChecked == true ||
            cbMP3PIA.IsChecked == true ||
            cbOnline.IsChecked == true ||
            cbLyrics.IsChecked == true ||
            cbNotes.IsChecked == true ||
            cbAmountPublisher1.IsChecked == true ||
            cbAmountPublisher2.IsChecked == true ||
            cbAmountPublisher3.IsChecked == true ||
            cbAmountPublisher4.IsChecked == true ||
            cbPublisher1.IsChecked == true ||
            cbPublisher2.IsChecked == true ||
            cbPublisher3.IsChecked == true ||
            cbPublisher4.IsChecked == true ||
            cbDuration.IsChecked == true )

        {
            if ( LibraryUsers.SelectedUserRoleId == 4 || LibraryUsers.SelectedUserRoleId == 6 || LibraryUsers.SelectedUserRoleId == 8 || LibraryUsers.SelectedUserRoleId == 10 || LibraryUsers.SelectedUserRoleId == 11 || LibraryUsers.SelectedUserRoleId == 13 || LibraryUsers.SelectedUserRoleId == 14 || LibraryUsers.SelectedUserRoleId == 15 )
            {
                tbEnableEdit.Text = "Visible";
                btnSave.IsEnabled = true;
                btnSave.ToolTip = "Sla de gewijzigde gegevens op";
            }
            else
            {
                tbEnableEdit.Text = "Collapsed";
            }
        }
        else
        {
            tbEnableEdit.Text = "Collapsed";
            btnSave.IsEnabled = false;
            btnSave.ToolTip = "Er zijn geen gegevens aangepast, opslaan niet mogelijk";
        }
    }

    private void RichTextBoxChanged ( object sender, TextChangedEventArgs e )
    {
        var propertyName = ((RichTextBox)sender).Name;

        if ( SelectedScore != null )
        {
            //It is hard to check if the content of a rich text box really differs, for that reason the changed is always set when triggered
            switch ( propertyName )
            {
                case "memoNotes":
                    cbNotes.IsChecked = true;
                    break;
            }
        }
        CheckChanged ( );
    }

    private void CheckBoxChanged ( object sender, RoutedEventArgs e )
    {
        var propertyName = ((CheckBox)sender).Name;

        if ( SelectedScore != null )
        {
            switch ( propertyName )
            {
                case "chkByHeart":
                    if ( chkByHeart.IsChecked == SelectedScore.ByHeart )
                    { cbByHeart.IsChecked = false; }
                    else
                    { cbByHeart.IsChecked = true; }
                    break;

                case "chkChecked":
                    if ( chkChecked.IsChecked == SelectedScore.Checked )
                    { cbChecked.IsChecked = false; }
                    else
                    { cbChecked.IsChecked = true; }
                    break;

                case "chkMSCORP":
                    if ( chkMSCORP.IsChecked == SelectedScore.MuseScoreORP )
                    { cbMSCORP.IsChecked = false; }
                    else
                    { cbMSCORP.IsChecked = true; }
                    break;

                case "chkMSCORK":
                    if ( chkMSCORK.IsChecked == SelectedScore.MuseScoreORK )
                    { cbMSCORK.IsChecked = false; }
                    else
                    { cbMSCORK.IsChecked = true; }
                    break;

                case "chkMSCTOP":
                    if ( chkMSCTOP.IsChecked == SelectedScore.MuseScoreTOP )
                    { cbMSCTOP.IsChecked = false; }
                    else
                    { cbMSCTOP.IsChecked = true; }
                    break;

                case "chkMSCTOK":
                    if ( chkMSCTOK.IsChecked == SelectedScore.MuseScoreTOK )
                    { cbMSCTOK.IsChecked = false; }
                    else
                    { cbMSCTOK.IsChecked = true; }
                    break;

                case "chkPDFORP":
                    if ( chkPDFORP.IsChecked == SelectedScore.PDFORP )
                    { cbPDFORP.IsChecked = false; }
                    else
                    { cbPDFORP.IsChecked = true; }
                    break;

                case "chkPDFORK":
                    if ( chkPDFORK.IsChecked == SelectedScore.PDFORK )
                    { cbPDFORK.IsChecked = false; }
                    else
                    { cbPDFORK.IsChecked = true; }
                    break;

                case "chkPDFTOP":
                    if ( chkPDFTOP.IsChecked == SelectedScore.PDFTOP )
                    { cbPDFTOP.IsChecked = false; }
                    else
                    { cbPDFTOP.IsChecked = true; }
                    break;

                case "chkPDFTOK":
                    if ( chkPDFTOK.IsChecked == SelectedScore.PDFTOK )
                    { cbPDFTOK.IsChecked = false; }
                    else
                    { cbPDFTOK.IsChecked = true; }
                    break;

                case "chkMP3B1":
                    if ( chkMP3B1.IsChecked == SelectedScore.MP3B1 )
                    { cbMP3B1.IsChecked = false; }
                    else
                    { cbMP3B1.IsChecked = true; }
                    break;

                case "chkMP3B2":
                    if ( chkMP3B2.IsChecked == SelectedScore.MP3B2 )
                    { cbMP3B2.IsChecked = false; }
                    else
                    { cbMP3B2.IsChecked = true; }
                    break;

                case "chkMP3T1":
                    if ( chkMP3T1.IsChecked == SelectedScore.MP3T1 )
                    { cbMP3T1.IsChecked = false; }
                    else
                    { cbMP3T1.IsChecked = true; }
                    break;

                case "chkMP3T2":
                    if ( chkMP3T2.IsChecked == SelectedScore.MP3T2 )
                    { cbMP3T2.IsChecked = false; }
                    else
                    { cbMP3T2.IsChecked = true; }
                    break;

                case "chkMP3SOL":
                    if ( chkMP3SOL.IsChecked == SelectedScore.MP3SOL )
                    { cbMP3SOL.IsChecked = false; }
                    else
                    { cbMP3SOL.IsChecked = true; }
                    break;

                case "chkMP3TOT":
                    if ( chkMP3TOT.IsChecked == SelectedScore.MP3TOT )
                    { cbMP3TOT.IsChecked = false; }
                    else
                    { cbMP3TOT.IsChecked = true; }
                    break;

                case "chkMP3PIA":
                    if ( chkMP3PIA.IsChecked == SelectedScore.MP3PIA )
                    { cbMP3PIA.IsChecked = false; }
                    else
                    { cbMP3PIA.IsChecked = true; }
                    break;

                case "chkMSCOnline":
                    if ( chkMSCOnline.IsChecked == SelectedScore.MuseScoreOnline )
                    { cbOnline.IsChecked = false; }
                    else
                    { cbOnline.IsChecked = true; }
                    break;
            }
        }
        CheckChanged ( );
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
                    if ( SelectedScore.DateCreatedString != "" )
                    {
                        var _selectedDateTime = SelectedScore.DateCreatedString.ToString () + " 00:00:00 AM";
                        _CreatedDateTime = DateTime.Parse ( _selectedDateTime );

                        if ( dpDigitized.SelectedDate == _CreatedDateTime )
                        { cbDigitized.IsChecked = false; }
                        else
                        { cbDigitized.IsChecked = true; }
                    }
                    else
                    {
                        // If the change event is triggered a data has been entered, this always differs if no date is in the database
                        cbDigitized.IsChecked = true;
                    }
                    break;

                case "dpModified":
                    DateTime _ModifiedDateTime = new ();

                    // If the change event is triggered a data has been entered, this always differs if no date is in the database
                    if ( SelectedScore.DateModifiedString != "" )
                    {
                        var _selectedDateTime = SelectedScore.DateModifiedString.ToString () + " 00:00:00 AM";
                        _ModifiedDateTime = DateTime.Parse ( _selectedDateTime );

                        if ( dpModified.SelectedDate == _ModifiedDateTime )
                        { cbModified.IsChecked = false; }
                        else
                        { cbModified.IsChecked = true; }
                    }
                    else
                    {
                        // If the change event is triggered a data has been entered, this always differs if no date is in the database
                        cbModified.IsChecked = true;
                    }
                    break;
            }
        }
        CheckChanged ( );
    }

    private void BtnSaveClick ( object sender, RoutedEventArgs e )
    {
        ObservableCollection<SaveScoreModel> ScoreList = new();
        ObservableCollection<ScoreModel> OldScoreValues = new();

        // Add an empty row in the OldScoreValues
        OldScoreValues.Add ( new ScoreModel { } );

        //OldScoreValues [ 0 ].AccompanimentId = SelectedScore.AccompanimentId;
        if ( SelectedScore != null )
        {
            string DateDigitized = "", DateModified = "";

            int TitleChanged = -1, SubTitleChanged = -1,
                ComposerChanged = -1, TextwriterChanged = -1, ArrangerChanged = -1,
                DateDigitizedChanged = -1, DateModifiedChanged = -1,
                LyricsChanged = -1, MusicPieceChanged = -1, NotesChanged = -1,
                AccompanimentChanged = -1, ArchiveChanged = -1, RepertoireChanged = -1, LanguageChanged = -1, GenreChanged = -1, Check = -1, ByHeart = -1,
                Publisher1Changed = -1, Publisher2Changed = -1, Publisher3Changed = -1, Publisher4Changed = -1,
                MuseScoreORP = -1, MuseScoreORK = -1, MuseScoreTOP = -1, MuseScoreTOK = -1, MuseScoreOnline = -1,
                PDFORP = -1, PDFORK = -1, PDFTOP = -1, PDFTOK = -1,
                MP3B1 = -1, MP3B2 = -1, MP3T1 = -1, MP3T2 = -1, MP3SOL = -1, MP3TOT = -1, MP3PIA = -1,
                AmountPublisher1Changed = -1, AmountPublisher2Changed = -1, AmountPublisher3Changed = -1, AmountPublisher4Changed = -1, DurationChanged = -1,DurationMinutesChanged = -1, DurationSecondsChanged = -1;

            if ( ( bool ) cbAccompaniment.IsChecked )
            {
                AccompanimentChanged = 1;
                OldScoreValues [ 0 ].AccompanimentId = SelectedScore.AccompanimentId;
                OldScoreValues [ 0 ].AccompanimentName = SelectedScore.AccompanimentName;
                SelectedScore.AccompanimentId = ( ( AccompanimentModel ) comAccompaniment.SelectedItem ).AccompanimentId;
                SelectedScore.AccompanimentName = ( ( AccompanimentModel ) comAccompaniment.SelectedItem ).AccompanimentName;
            }

            if ( ( bool ) cbAmountPublisher1.IsChecked )
            {
                AmountPublisher1Changed = 1;
                OldScoreValues [ 0 ].AmountPublisher1 = SelectedScore.AmountPublisher1;
                SelectedScore.AmountPublisher1 = int.Parse ( tbAmountPublisher1.Text );
            }

            if ( ( bool ) cbAmountPublisher2.IsChecked )
            {
                AmountPublisher2Changed = 1;
                OldScoreValues [ 0 ].AmountPublisher2 = SelectedScore.AmountPublisher2;
                SelectedScore.AmountPublisher2 = int.Parse ( tbAmountPublisher2.Text );
            }

            if ( ( bool ) cbAmountPublisher3.IsChecked )
            {
                AmountPublisher3Changed = 1;
                OldScoreValues [ 0 ].AmountPublisher3 = SelectedScore.AmountPublisher3;
                SelectedScore.AmountPublisher3 = int.Parse ( tbAmountPublisher3.Text );
            }

            if ( ( bool ) cbAmountPublisher4.IsChecked )
            {
                AmountPublisher4Changed = 1;
                OldScoreValues [ 0 ].AmountPublisher4 = SelectedScore.AmountPublisher4;
                SelectedScore.AmountPublisher4 = int.Parse ( tbAmountPublisher4.Text );
            }

            if ( ( bool ) cbArchive.IsChecked )
            {
                ArchiveChanged = 1;
                OldScoreValues [ 0 ].ArchiveId = SelectedScore.ArchiveId;
                OldScoreValues [ 0 ].ArchiveName = SelectedScore.ArchiveName;
                SelectedScore.ArchiveId = ( ( ArchiveModel ) comArchive.SelectedItem ).ArchiveId;
                SelectedScore.ArchiveName = ( ( ArchiveModel ) comArchive.SelectedItem ).ArchiveName;
            }

            if ( ( bool ) cbDurationMinutes.IsChecked )
            {
                DurationMinutesChanged = 1;
                DurationChanged = 1;
                OldScoreValues [ 0 ].DurationMinutes = SelectedScore.DurationMinutes;
                SelectedScore.DurationMinutes = int.Parse ( tbMinutes.Text );
                SelectedScore.Duration = ( int.Parse ( tbMinutes.Text ) * 60 ) + int.Parse ( tbSeconds.Text );
            }

            if ( ( bool ) cbDurationSeconds.IsChecked )
            {
                DurationSecondsChanged = 1;
                DurationChanged = 1;
                OldScoreValues [ 0 ].DurationSeconds = SelectedScore.DurationSeconds;
                SelectedScore.DurationSeconds = int.Parse ( tbSeconds.Text );
                SelectedScore.Duration = ( int.Parse ( tbMinutes.Text ) * 60 ) + int.Parse ( tbSeconds.Text );
            }


            if ( ( bool ) cbArranger.IsChecked )
            {
                ArrangerChanged = 1;
                OldScoreValues [ 0 ].Arranger = SelectedScore.Arranger;
                SelectedScore.Arranger = tbArranger.Text;
            }

            if ( ( bool ) cbByHeart.IsChecked )
            {
                OldScoreValues [ 0 ].ByHeart = SelectedScore.ByHeart;
                if ( ( bool ) chkByHeart.IsChecked )
                {
                    ByHeart = 1;
                    SelectedScore.ByHeart = true;
                }
                else
                {
                    ByHeart = 0;
                    SelectedScore.ByHeart = false;
                }
            }

            if ( ( bool ) cbChecked.IsChecked )
            {
                OldScoreValues [ 0 ].Checked = SelectedScore.Checked;
                if ( ( bool ) chkChecked.IsChecked )
                {
                    Check = 1;
                    SelectedScore.Checked = true;
                }
                else
                {
                    Check = 0;
                    SelectedScore.Checked = false;
                }
            }

            if ( ( bool ) cbComposer.IsChecked )
            {
                ComposerChanged = 1;
                OldScoreValues [ 0 ].Composer = SelectedScore.Composer;
                SelectedScore.Composer = tbComposer.Text;
            }

            if ( ( bool ) cbDigitized.IsChecked )
            {
                if ( dpDigitized.SelectedDate != null )
                {
                    string year = dpDigitized.SelectedDate.Value.Year.ToString();
                    string month = "0" + (dpDigitized.SelectedDate.Value.Month.ToString());
                    string day = "0" + (dpDigitized.SelectedDate.Value.Day.ToString());
                    if ( year == "1900" )
                    { DateDigitized = ""; }
                    else
                    {
                        DateDigitized = $"{year}-{month.Substring ( month.Length - 2, 2 )}-{day.Substring ( day.Length - 2, 2 )}";
                    }

                    DateDigitizedChanged = 1;
                    DateTime _created = DateTime.Parse(DateDigitized + " 00:00:00 AM");
                    OldScoreValues [ 0 ].DateDigitized = SelectedScore.DateDigitized;
                    SelectedScore.DateDigitized = DateOnly.FromDateTime ( _created );
                }
                else
                {
                    DateDigitized = string.Empty;
                    DateDigitizedChanged = 1;
                    DateTime _created = DateTime.Parse(DateDigitized + " 00:00:00 AM");
                    OldScoreValues [ 0 ].DateDigitized = SelectedScore.DateDigitized;
                }
            }

            if ( ( bool ) cbGenre.IsChecked )
            {
                GenreChanged = 1;
                OldScoreValues [ 0 ].GenreId = SelectedScore.GenreId;
                OldScoreValues [ 0 ].GenreName = SelectedScore.GenreName;
                SelectedScore.GenreId = ( ( GenreModel ) comGenre.SelectedItem ).GenreId;
                SelectedScore.GenreName = ( ( GenreModel ) comGenre.SelectedItem ).GenreName;
            }

            if ( ( bool ) cbLanguage.IsChecked )
            {
                LanguageChanged = 1;
                OldScoreValues [ 0 ].LanguageId = SelectedScore.LanguageId;
                OldScoreValues [ 0 ].LanguageName = SelectedScore.LanguageName;
                SelectedScore.LanguageId = ( ( LanguageModel ) comLanguage.SelectedItem ).LanguageId;
                SelectedScore.LanguageName = ( ( LanguageModel ) comLanguage.SelectedItem ).LanguageName;
            }

            if ( ( bool ) cbModified.IsChecked )
            {
                string year = dpModified.SelectedDate.Value.Year.ToString();
                string month = "0" + dpModified.SelectedDate.Value.Month.ToString();
                string day = "0" + dpModified.SelectedDate.Value.Day.ToString();
                if ( year == "1900" )
                { DateModified = ""; }
                else
                {
                    DateModified = $"{year}-{month.Substring ( month.Length - 2, 2 )}-{day.Substring ( day.Length - 2, 2 )}";
                }
                DateModifiedChanged = 1;
                DateTime _modified = DateTime.Parse(DateModified + " 00:00:00 AM");
                OldScoreValues [ 0 ].DateModified = SelectedScore.DateModified;
                SelectedScore.DateModified = DateOnly.FromDateTime ( _modified );
            }

            if ( ( bool ) cbMP3B1.IsChecked )
            {
                OldScoreValues [ 0 ].MP3B1 = SelectedScore.MP3B1;
                if ( ( bool ) chkMP3B1.IsChecked )
                {
                    MP3B1 = 1;
                    SelectedScore.MP3B1 = true;
                }
                else
                {
                    MP3B1 = 0;
                    SelectedScore.MP3B1 = false;
                }
            }

            if ( ( bool ) cbMP3B2.IsChecked )
            {
                OldScoreValues [ 0 ].MP3B2 = SelectedScore.MP3B2;
                if ( ( bool ) chkMP3B2.IsChecked )
                {
                    MP3B2 = 1;
                    SelectedScore.MP3B2 = true;
                }
                else
                {
                    MP3B2 = 0;
                    SelectedScore.MP3B2 = false;
                }
            }

            if ( ( bool ) cbMP3PIA.IsChecked )
            {
                OldScoreValues [ 0 ].MP3PIA = SelectedScore.MP3PIA;
                if ( ( bool ) chkMP3PIA.IsChecked )
                {
                    MP3PIA = 1;
                    SelectedScore.MP3PIA = true;
                }
                else
                {
                    MP3PIA = 0;
                    SelectedScore.MP3PIA = false;
                }
            }

            if ( ( bool ) cbMP3SOL.IsChecked )
            {
                OldScoreValues [ 0 ].MP3SOL = SelectedScore.MP3SOL;
                if ( ( bool ) chkMP3SOL.IsChecked )
                {
                    MP3SOL = 1;
                    SelectedScore.MP3SOL = true;
                }
                else
                {
                    MP3SOL = 0;
                    SelectedScore.MP3SOL = false;
                }
            }

            if ( ( bool ) cbMP3T1.IsChecked )
            {
                OldScoreValues [ 0 ].MP3T1 = SelectedScore.MP3T1;
                if ( ( bool ) chkMP3T1.IsChecked )
                {
                    MP3T1 = 1;
                    SelectedScore.MP3T1 = true;
                }
                else
                {
                    MP3T1 = 0;
                    SelectedScore.MP3T1 = false;
                }
            }

            if ( ( bool ) cbMP3T2.IsChecked )
            {
                OldScoreValues [ 0 ].MP3T2 = SelectedScore.MP3T2;
                if ( ( bool ) chkMP3T2.IsChecked )
                {
                    MP3T2 = 1;
                    SelectedScore.MP3T2 = true;
                }
                else
                {
                    MP3T2 = 0;
                    SelectedScore.MP3T2 = false;
                }
            }

            if ( ( bool ) cbMP3TOT.IsChecked )
            {
                OldScoreValues [ 0 ].MP3TOT = SelectedScore.MP3TOT;
                if ( ( bool ) chkMP3TOT.IsChecked )
                {
                    MP3TOT = 1;
                    SelectedScore.MP3TOT = true;
                }
                else
                {
                    MP3TOT = 0;
                    SelectedScore.MP3TOT = false;
                }
            }

            if ( ( bool ) cbMSCORK.IsChecked )
            {
                OldScoreValues [ 0 ].MuseScoreORK = SelectedScore.MuseScoreORK;
                if ( ( bool ) chkMSCORK.IsChecked )
                {
                    MuseScoreORK = 1;
                    SelectedScore.MuseScoreORK = true;
                }
                else
                {
                    MuseScoreORK = 0;
                    SelectedScore.MuseScoreORK = false;
                }
            }

            if ( ( bool ) cbMSCORP.IsChecked )
            {
                OldScoreValues [ 0 ].MuseScoreORP = SelectedScore.MuseScoreORP;
                if ( ( bool ) chkMSCORP.IsChecked )
                {
                    MuseScoreORP = 1;
                    SelectedScore.MuseScoreORP = true;
                }
                else
                {
                    MuseScoreORP = 0;
                    SelectedScore.MuseScoreORP = false;
                }
            }

            if ( ( bool ) cbMSCTOK.IsChecked )
            {
                OldScoreValues [ 0 ].MuseScoreTOK = SelectedScore.MuseScoreTOK;
                if ( ( bool ) chkMSCTOK.IsChecked )
                {
                    MuseScoreTOK = 1;
                    SelectedScore.MuseScoreTOK = true;
                }
                else
                {
                    MuseScoreTOK = 0;
                    SelectedScore.MuseScoreTOK = false;
                }
            }

            if ( ( bool ) cbMSCTOP.IsChecked )
            {
                OldScoreValues [ 0 ].MuseScoreTOP = SelectedScore.MuseScoreTOP;
                if ( ( bool ) chkMSCTOP.IsChecked )
                {
                    MuseScoreTOP = 1;
                    SelectedScore.MuseScoreORK = true;
                }
                else
                {
                    MuseScoreTOP = 0;
                    SelectedScore.MuseScoreTOP = false;
                }
            }

            if ( ( bool ) cbMusicPiece.IsChecked )
            {
                MusicPieceChanged = 1;
                OldScoreValues [ 0 ].MusicPiece = SelectedScore.MusicPiece;
                SelectedScore.MusicPiece = tbMusicPiece.Text;
            }

            if ( ( bool ) cbOnline.IsChecked )
            {
                OldScoreValues [ 0 ].MuseScoreOnline = SelectedScore.MuseScoreOnline;
                if ( ( bool ) chkMSCOnline.IsChecked )
                {
                    MuseScoreOnline = 1;
                    SelectedScore.MuseScoreOnline = true;
                }
                else
                {
                    MuseScoreOnline = 0;
                    SelectedScore.MuseScoreOnline = false;
                }
            }

            if ( ( bool ) cbPDFORK.IsChecked )
            {
                OldScoreValues [ 0 ].PDFORK = SelectedScore.PDFORK;
                if ( ( bool ) chkPDFORK.IsChecked )
                {
                    PDFORK = 1;
                    SelectedScore.PDFORK = true;
                }
                else
                {
                    PDFORK = 0;
                    SelectedScore.PDFORK = false;
                }
            }

            if ( ( bool ) cbPDFORP.IsChecked )
            {
                OldScoreValues [ 0 ].PDFORP = SelectedScore.PDFORP;
                if ( ( bool ) chkPDFORP.IsChecked )
                {
                    PDFORP = 1;
                    SelectedScore.PDFORP = true;
                }
                else
                {
                    PDFORP = 0;
                    SelectedScore.PDFORP = false;
                }
            }

            if ( ( bool ) cbPDFTOK.IsChecked )
            {
                OldScoreValues [ 0 ].PDFTOK = SelectedScore.PDFTOK;
                if ( ( bool ) chkPDFTOK.IsChecked )
                {
                    PDFTOK = 1;
                    SelectedScore.PDFTOK = true;
                }
                else
                {
                    PDFTOK = 0;
                    SelectedScore.PDFTOK = false;
                }
            }

            if ( ( bool ) cbPDFTOP.IsChecked )
            {
                OldScoreValues [ 0 ].PDFTOP = SelectedScore.PDFTOP;
                if ( ( bool ) chkPDFTOP.IsChecked )
                {
                    PDFTOP = 1;
                    SelectedScore.PDFTOP = true;
                }
                else
                {
                    PDFTOP = 0;
                    SelectedScore.PDFTOP = false;
                }
            }

            if ( ( bool ) cbPublisher1.IsChecked )
            {
                Publisher1Changed = 1;
                OldScoreValues [ 0 ].Publisher1Id = SelectedScore.Publisher1Id;
                OldScoreValues [ 0 ].Publisher1Name = SelectedScore.Publisher1Name;
                SelectedScore.Publisher1Id = ( ( PublisherModel ) comPublisher1.SelectedItem ).PublisherId;
                SelectedScore.Publisher1Name = ( ( PublisherModel ) comPublisher1.SelectedItem ).PublisherName;
            }

            if ( ( bool ) cbPublisher2.IsChecked )
            {
                Publisher2Changed = 1;
                OldScoreValues [ 0 ].Publisher2Id = SelectedScore.Publisher2Id;
                OldScoreValues [ 0 ].Publisher2Name = SelectedScore.Publisher2Name;
                SelectedScore.Publisher2Id = ( ( PublisherModel ) comPublisher2.SelectedItem ).PublisherId;
                SelectedScore.Publisher2Name = ( ( PublisherModel ) comPublisher2.SelectedItem ).PublisherName;
            }

            if ( ( bool ) cbPublisher3.IsChecked )
            {
                Publisher3Changed = 1;
                OldScoreValues [ 0 ].Publisher3Id = SelectedScore.Publisher3Id;
                OldScoreValues [ 0 ].Publisher3Name = SelectedScore.Publisher3Name;
                SelectedScore.Publisher3Id = ( ( PublisherModel ) comPublisher3.SelectedItem ).PublisherId;
                SelectedScore.Publisher3Name = ( ( PublisherModel ) comPublisher3.SelectedItem ).PublisherName;
            }

            if ( ( bool ) cbPublisher4.IsChecked )
            {
                Publisher4Changed = 1;
                OldScoreValues [ 0 ].Publisher4Id = SelectedScore.Publisher4Id;
                OldScoreValues [ 0 ].Publisher4Name = SelectedScore.Publisher4Name;
                SelectedScore.Publisher4Id = ( ( PublisherModel ) comPublisher3.SelectedItem ).PublisherId;
                SelectedScore.Publisher4Name = ( ( PublisherModel ) comPublisher4.SelectedItem ).PublisherName;
            }

            if ( ( bool ) cbArchive.IsChecked )
            {
                ArchiveChanged = 1;
                OldScoreValues [ 0 ].ArchiveId = SelectedScore.ArchiveId;
                OldScoreValues [ 0 ].ArchiveName = SelectedScore.ArchiveName;
                SelectedScore.ArchiveId = ( ( ArchiveModel ) comArchive.SelectedItem ).ArchiveId;
                SelectedScore.ArchiveName = ( ( ArchiveModel ) comArchive.SelectedItem ).ArchiveName;
            }

            if ( ( bool ) cbRepertoire.IsChecked )
            {
                RepertoireChanged = 1;
                OldScoreValues [ 0 ].RepertoireId = SelectedScore.RepertoireId;
                OldScoreValues [ 0 ].RepertoireName = SelectedScore.RepertoireName;
                SelectedScore.RepertoireId = ( ( RepertoireModel ) comRepertoire.SelectedItem ).RepertoireId;
                SelectedScore.RepertoireName = ( ( RepertoireModel ) comRepertoire.SelectedItem ).RepertoireName;
            }

            if ( ( bool ) cbSubTitle.IsChecked )
            {
                SubTitleChanged = 1;
                OldScoreValues [ 0 ].ScoreSubTitle = SelectedScore.ScoreSubTitle;
                SelectedScore.ScoreSubTitle = tbSubTitle.Text;
            }

            if ( ( bool ) cbTextwriter.IsChecked )
            {
                TextwriterChanged = 1;
                OldScoreValues [ 0 ].Textwriter = SelectedScore.Textwriter;
                SelectedScore.Textwriter = tbTextwriter.Text;
            }

            if ( ( bool ) cbTitle.IsChecked )
            {
                TitleChanged = 1;
                OldScoreValues [ 0 ].ScoreTitle = SelectedScore.ScoreTitle;
                SelectedScore.ScoreTitle = tbTitle.Text;
            }

            if ( ( bool ) cbLyrics.IsChecked )
            {
                LyricsChanged = 1;
                OldScoreValues [ 0 ].Lyrics = SelectedScore.Lyrics;
                SelectedScore.Lyrics = tbLyrics.Text;
            }

            if ( ( bool ) cbNotes.IsChecked )
            {
                NotesChanged = 1;
                OldScoreValues [ 0 ].Notes = SelectedScore.Notes;
                SelectedScore.Notes = GetRichTextFromFlowDocument ( memoNotes.Document ).ToString ( );
            }

            ScoreList.Add ( new SaveScoreModel
            {
                AccompanimentId = SelectedScore.AccompanimentId,
                AccompanimentName = SelectedScore.AccompanimentName,
                AccompanimentChanged = AccompanimentChanged,
                ArchiveId = SelectedScore.ArchiveId,
                ArchiveName = SelectedScore.ArchiveName,
                ArchiveChanged = ArchiveChanged,
                Arranger = SelectedScore.Arranger,
                ArrangerChanged = ArrangerChanged,
                ByHeart = ByHeart,
                Checked = Check,
                Composer = SelectedScore.Composer,
                ComposerChanged = ComposerChanged,
                DateDigitized = DateDigitized,
                DateDigitizedChanged = DateDigitizedChanged,
                DateModified = DateModified,
                DateModifiedChanged = DateModifiedChanged,
                GenreId = SelectedScore.GenreId,
                GenreName = SelectedScore.GenreName,
                GenreChanged = GenreChanged,
                LanguageId = SelectedScore.LanguageId,
                LanguageChanged = LanguageChanged,
                Lyrics = SelectedScore.Lyrics,
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
                MusicPiece = SelectedScore.MusicPiece,
                MusicPieceChanged = MusicPieceChanged,
                Notes = SelectedScore.Notes,
                NotesChanged = NotesChanged,
                AmountPublisher1 = SelectedScore.AmountPublisher1,
                AmountPublisher1Changed = AmountPublisher1Changed,
                AmountPublisher2 = SelectedScore.AmountPublisher2,
                AmountPublisher2Changed = AmountPublisher2Changed,
                AmountPublisher3 = SelectedScore.AmountPublisher3,
                AmountPublisher3Changed = AmountPublisher3Changed,
                AmountPublisher4 = SelectedScore.AmountPublisher4,
                AmountPublisher4Changed = AmountPublisher4Changed,
                PDFORK = PDFORK,
                PDFORP = PDFORP,
                PDFTOK = PDFTOK,
                PDFTOP = PDFTOP,
                Publisher1Id = SelectedScore.Publisher1Id,
                Publisher1Name = SelectedScore.Publisher1Name,
                Publisher1Changed = Publisher1Changed,
                Publisher2Id = SelectedScore.Publisher2Id,
                Publisher2Name = SelectedScore.Publisher2Name,
                Publisher2Changed = Publisher2Changed,
                Publisher3Id = SelectedScore.Publisher3Id,
                Publisher3Name = SelectedScore.Publisher3Name,
                Publisher3Changed = Publisher3Changed,
                Publisher4Id = SelectedScore.Publisher4Id,
                Publisher4Name = SelectedScore.Publisher4Name,
                Publisher4Changed = Publisher4Changed,
                RepertoireId = SelectedScore.RepertoireId,
                RepertoireName = SelectedScore.RepertoireName,
                RepertoireChanged = RepertoireChanged,
                ScoreNumber = SelectedScore.Score,
                ScoreId = SelectedScore.ScoreId,
                ScoreMainNumber = SelectedScore.ScoreNumber,
                ScoreSubNumber = SelectedScore.ScoreSubNumber,
                SubTitle = SelectedScore.ScoreSubTitle,
                SubTitleChanged = SubTitleChanged,
                Title = SelectedScore.ScoreTitle,
                TitleChanged = TitleChanged,
                Textwriter = SelectedScore.Textwriter,
                TextwriterChanged = TextwriterChanged,
                Duration = SelectedScore.Duration,
                DurationChanged = DurationChanged,
                DurationMinutes = SelectedScore.DurationMinutes,
                DurationMinutesChanged = DurationMinutesChanged,
                DurationSeconds = SelectedScore.DurationSeconds,
                DurationSecondsChanged = DurationSecondsChanged
            } );

            DBCommands.SaveScore ( ScoreList );
            DBCommands.GetScores ( DBNames.ScoresView, DBNames.ScoresFieldNameScoreNumber, null, null );

            SaveHistory ( ScoreList, OldScoreValues );

            ResetChanged ( );
        }
    }

    public void SaveHistory ( ObservableCollection<SaveScoreModel> _scoreList, ObservableCollection<ScoreModel> _oldScoreList )
    {

        string _action = DBNames.LogScoreChanged;
        if ( ( bool ) cbNewScore.IsChecked )
        { _action = DBNames.LogScoreAdded; }

        // Write log info
        DBCommands.WriteLog ( LibraryUsers.SelectedUserId, _action, $"Partituur: {tbScoreNumber.Text}" );

        // Get Added History Id
        int _historyId = DBCommands.GetAddedHistoryId();

        #region Log Accompaniment changes
        if ( ( bool ) ( cbAccompaniment.IsChecked ) )
        {
            DBCommands.WriteDetailLog ( _historyId, DBNames.LogAccompaniment, _oldScoreList [ 0 ].AccompanimentName, _scoreList [ 0 ].AccompanimentName );
        }
        #endregion

        #region Log Repertoire Changes
        if ( ( bool ) ( cbRepertoire.IsChecked ) )
        {
            DBCommands.WriteDetailLog ( _historyId, DBNames.LogRepertoire, _oldScoreList [ 0 ].RepertoireName, _scoreList [ 0 ].RepertoireName );
        }
        #endregion

        #region Log Archive Changes
        if ( ( bool ) ( cbArchive.IsChecked ) )
        {
            DBCommands.WriteDetailLog ( _historyId, DBNames.LogArchive, _oldScoreList [ 0 ].ArchiveName, _scoreList [ 0 ].ArchiveName );
        }
        #endregion

        #region Log Sing By Heart Changes
        if ( ( bool ) ( cbByHeart.IsChecked ) )
        {
            string oldValue = "", newValue = "";

            if ( ( bool ) ( _oldScoreList [ 0 ].ByHeart == true ) )
            { oldValue = DBNames.LogYes; }
            else
            { oldValue = DBNames.LogNo; }

            if ( _scoreList [ 0 ].ByHeart == 1 )
            { newValue = DBNames.LogYes; }
            else
            { newValue = DBNames.LogNo; }

            DBCommands.WriteDetailLog ( _historyId, DBNames.LogByHeart, oldValue, newValue );
        }
        #endregion

        #region Log Title Changes
        if ( ( bool ) ( cbTitle.IsChecked ) )
        {
            // Prevent the Title "<Nieuw>" (for new scores) to be written to the log
            string? _oldTitle = _oldScoreList [ 0 ].ScoreTitle;
            if ( _oldTitle == DBNames.LogNew )
            { _oldTitle = ""; }

            DBCommands.WriteDetailLog ( _historyId, DBNames.LogTitle, _oldTitle, _scoreList [ 0 ].Title );
        }
        #endregion

        #region Log SubTitle Changes
        if ( ( bool ) ( cbSubTitle.IsChecked ) )
        {
            DBCommands.WriteDetailLog ( _historyId, DBNames.LogSubTitle, _oldScoreList [ 0 ].ScoreSubTitle, _scoreList [ 0 ].SubTitle );
        }
        #endregion

        #region Log Composer Changes
        if ( ( bool ) ( cbComposer.IsChecked ) )
        {
            DBCommands.WriteDetailLog ( _historyId, DBNames.LogComposer, _oldScoreList [ 0 ].Composer, _scoreList [ 0 ].Composer );
        }
        #endregion

        #region Log Textwriter Changes
        if ( ( bool ) ( cbTextwriter.IsChecked ) )
        {
            DBCommands.WriteDetailLog ( _historyId, DBNames.LogTextwriter, _oldScoreList [ 0 ].Textwriter, _scoreList [ 0 ].Textwriter );
        }
        #endregion

        #region Log Arranger Changes
        if ( ( bool ) ( cbArranger.IsChecked ) )
        {
            DBCommands.WriteDetailLog ( _historyId, DBNames.LogArranger, _oldScoreList [ 0 ].Arranger, _scoreList [ 0 ].Arranger );
        }
        #endregion

        #region Log Genre Changes
        if ( ( bool ) ( cbGenre.IsChecked ) )
        {
            DBCommands.WriteDetailLog ( _historyId, DBNames.LogGenre, _oldScoreList [ 0 ].GenreName, _scoreList [ 0 ].GenreName );
        }
        #endregion

        #region Log Language Changes
        if ( ( bool ) ( cbLanguage.IsChecked ) )
        {
            DBCommands.WriteDetailLog ( _historyId, DBNames.LogLanguage, _oldScoreList [ 0 ].LanguageName, _scoreList [ 0 ].LanguageName );
        }
        #endregion

        #region Log Music piece Changes
        if ( ( bool ) ( cbMusicPiece.IsChecked ) )
        {
            DBCommands.WriteDetailLog ( _historyId, DBNames.LogMusicPiece, _oldScoreList [ 0 ].MusicPiece, _scoreList [ 0 ].MusicPiece );
        }
        #endregion

        #region Log Date Digitized Changes
        if ( ( bool ) ( cbDigitized.IsChecked ) )
        {
            DBCommands.WriteDetailLog ( _historyId, DBNames.LogDigitized, ( _oldScoreList [ 0 ].DateDigitized.ToDateTime ( TimeOnly.Parse ( "00:00 AM" ) ).ToString ( ) ), _scoreList [ 0 ].DateDigitized );
        }
        #endregion

        #region Log Date Modified Changes
        if ( ( bool ) ( cbModified.IsChecked ) )
        {
            DBCommands.WriteDetailLog ( _historyId, DBNames.LogModified, ( _oldScoreList [ 0 ].DateModified.ToDateTime ( TimeOnly.Parse ( "00:00 AM" ) ).ToString ( ) ), _scoreList [ 0 ].DateModified );
        }
        #endregion

        #region Log Checked digitized score Changes
        if ( ( bool ) ( cbChecked.IsChecked ) )
        {
            string oldValue = "", newValue = "";

            if ( ( bool ) ( _oldScoreList [ 0 ].Checked == true ) )
            { oldValue = DBNames.LogYes; }
            else
            { oldValue = DBNames.LogNo; }

            if ( _scoreList [ 0 ].Checked == 1 )
            { newValue = DBNames.LogYes; }
            else
            { newValue = DBNames.LogNo; }

            DBCommands.WriteDetailLog ( _historyId, DBNames.LogChecked, oldValue, newValue );
        }
        #endregion

        #region Log PDF ORP Changes
        if ( ( bool ) ( cbPDFORP.IsChecked ) )
        {
            string oldValue = "", newValue = "";

            if ( ( bool ) ( _oldScoreList [ 0 ].PDFORP == true ) )
            { oldValue = DBNames.LogYes; }
            else
            { oldValue = DBNames.LogNo; }

            if ( _scoreList [ 0 ].PDFORP == 1 )
            { newValue = DBNames.LogYes; }
            else
            { newValue = DBNames.LogNo; }

            DBCommands.WriteDetailLog ( _historyId, DBNames.LogPDFORP, oldValue, newValue );
        }
        #endregion

        #region Log PDF ORK Changes
        if ( ( bool ) ( cbPDFORK.IsChecked ) )
        {
            string oldValue = "", newValue = "";

            if ( ( bool ) ( _oldScoreList [ 0 ].PDFORK == true ) )
            { oldValue = DBNames.LogYes; }
            else
            { oldValue = DBNames.LogNo; }

            if ( _scoreList [ 0 ].PDFORK == 1 )
            { newValue = DBNames.LogYes; }
            else
            { newValue = DBNames.LogNo; }

            DBCommands.WriteDetailLog ( _historyId, DBNames.LogPDFORK, oldValue, newValue );
        }
        #endregion

        #region Log PDF TOP Changes
        if ( ( bool ) ( cbPDFTOP.IsChecked ) )
        {
            string oldValue = "", newValue = "";

            if ( ( bool ) ( _oldScoreList [ 0 ].PDFTOP == true ) )
            { oldValue = DBNames.LogYes; }
            else
            { oldValue = DBNames.LogNo; }

            if ( _scoreList [ 0 ].PDFTOP == 1 )
            { newValue = DBNames.LogYes; }
            else
            { newValue = DBNames.LogNo; }

            DBCommands.WriteDetailLog ( _historyId, DBNames.LogPDFTOP, oldValue, newValue );
        }
        #endregion

        #region Log PDF TOK Changes
        if ( ( bool ) ( cbPDFTOK.IsChecked ) )
        {
            string oldValue = "", newValue = "";

            if ( ( bool ) ( _oldScoreList [ 0 ].PDFTOK == true ) )
            { oldValue = DBNames.LogYes; }
            else
            { oldValue = DBNames.LogNo; }

            if ( _scoreList [ 0 ].PDFTOK == 1 )
            { newValue = DBNames.LogYes; }
            else
            { newValue = DBNames.LogNo; }

            DBCommands.WriteDetailLog ( _historyId, DBNames.LogPDFTOK, oldValue, newValue );
        }
        #endregion

        #region Log MuseScore ORP Changes
        if ( ( bool ) ( cbMSCORP.IsChecked ) )
        {
            string oldValue = "", newValue = "";

            if ( ( bool ) ( _oldScoreList [ 0 ].MuseScoreORP == true ) )
            { oldValue = DBNames.LogYes; }
            else
            { oldValue = DBNames.LogNo; }

            if ( _scoreList [ 0 ].MuseScoreORP == 1 )
            { newValue = DBNames.LogYes; }
            else
            { newValue = DBNames.LogNo; }

            DBCommands.WriteDetailLog ( _historyId, DBNames.LogMSCORP, oldValue, newValue );
        }
        #endregion

        #region Log MuseScore ORK Changes
        if ( ( bool ) ( cbMSCORK.IsChecked ) )
        {
            string oldValue = "", newValue = "";

            if ( ( bool ) ( _oldScoreList [ 0 ].MuseScoreORK == true ) )
            { oldValue = DBNames.LogYes; }
            else
            { oldValue = DBNames.LogNo; }

            if ( _scoreList [ 0 ].MuseScoreORK == 1 )
            { newValue = DBNames.LogYes; }
            else
            { newValue = DBNames.LogNo; }

            DBCommands.WriteDetailLog ( _historyId, DBNames.LogMSCORK, oldValue, newValue );
        }
        #endregion

        #region Log MuseScore TOP Changes
        if ( ( bool ) ( cbMSCTOP.IsChecked ) )
        {
            string oldValue = "", newValue = "";

            if ( ( bool ) ( _oldScoreList [ 0 ].MuseScoreTOP == true ) )
            { oldValue = DBNames.LogYes; }
            else
            { oldValue = DBNames.LogNo; }

            if ( _scoreList [ 0 ].MuseScoreTOP == 1 )
            { newValue = DBNames.LogYes; }
            else
            { newValue = DBNames.LogNo; }

            DBCommands.WriteDetailLog ( _historyId, DBNames.LogMSCTOP, oldValue, newValue );
        }
        #endregion

        #region Log MuseScore TOK Changes
        if ( ( bool ) ( cbMSCTOK.IsChecked ) )
        {
            string oldValue = "", newValue = "";

            if ( ( bool ) ( _oldScoreList [ 0 ].MuseScoreTOK == true ) )
            { oldValue = DBNames.LogYes; }
            else
            { oldValue = DBNames.LogNo; }

            if ( _scoreList [ 0 ].MuseScoreTOK == 1 )
            { newValue = DBNames.LogYes; }
            else
            { newValue = DBNames.LogNo; }

            DBCommands.WriteDetailLog ( _historyId, DBNames.LogMSCTOK, oldValue, newValue );
        }

        #endregion

        #region Log MP3 B1 Changes
        if ( ( bool ) ( cbMP3B1.IsChecked ) )
        {
            string oldValue = "", newValue = "";

            if ( ( bool ) ( _oldScoreList [ 0 ].MP3B1 == true ) )
            { oldValue = DBNames.LogYes; }
            else
            { oldValue = DBNames.LogNo; }

            if ( _scoreList [ 0 ].MP3B1 == 1 )
            { newValue = DBNames.LogYes; }
            else
            { newValue = DBNames.LogNo; }

            DBCommands.WriteDetailLog ( _historyId, DBNames.LogMP3B1, oldValue, newValue );
        }
        #endregion

        #region Log MP3 B2 Changes
        if ( ( bool ) ( cbMP3B2.IsChecked ) )
        {
            string oldValue = "", newValue = "";

            if ( ( bool ) ( _oldScoreList [ 0 ].MP3B2 == true ) )
            { oldValue = DBNames.LogYes; }
            else
            { oldValue = DBNames.LogNo; }

            if ( _scoreList [ 0 ].MP3B2 == 1 )
            { newValue = DBNames.LogYes; }
            else
            { newValue = DBNames.LogNo; }

            DBCommands.WriteDetailLog ( _historyId, DBNames.LogMP3B2, oldValue, newValue );
        }
        #endregion

        #region Log MP3 T1 Changes
        if ( ( bool ) ( cbMP3T1.IsChecked ) )
        {
            string oldValue = "", newValue = "";

            if ( ( bool ) ( _oldScoreList [ 0 ].MP3T1 == true ) )
            { oldValue = DBNames.LogYes; }
            else
            { oldValue = DBNames.LogNo; }

            if ( _scoreList [ 0 ].MP3T1 == 1 )
            { newValue = DBNames.LogYes; }
            else
            { newValue = DBNames.LogNo; }

            DBCommands.WriteDetailLog ( _historyId, DBNames.LogMP3T1, oldValue, newValue );
        }
        #endregion

        #region Log MP3 T2 Changes
        if ( ( bool ) ( cbMP3T2.IsChecked ) )
        {
            string oldValue = "", newValue = "";

            if ( ( bool ) ( _oldScoreList [ 0 ].MP3T2 == true ) )
            { oldValue = DBNames.LogYes; }
            else
            { oldValue = DBNames.LogNo; }

            if ( _scoreList [ 0 ].MP3T2 == 1 )
            { newValue = DBNames.LogYes; }
            else
            { newValue = DBNames.LogNo; }

            DBCommands.WriteDetailLog ( _historyId, DBNames.LogMP3T2, oldValue, newValue );
        }
        #endregion

        #region Log MP3 SOL Changes
        if ( ( bool ) ( cbMP3SOL.IsChecked ) )
        {
            string oldValue = "", newValue = "";

            if ( ( bool ) ( _oldScoreList [ 0 ].MP3SOL == true ) )
            { oldValue = DBNames.LogYes; }
            else
            { oldValue = DBNames.LogNo; }

            if ( _scoreList [ 0 ].MP3SOL == 1 )
            { newValue = DBNames.LogYes; }
            else
            { newValue = DBNames.LogNo; }

            DBCommands.WriteDetailLog ( _historyId, DBNames.LogMP3SOL, oldValue, newValue );
        }
        #endregion

        #region Log MP3 TOT Changes
        if ( ( bool ) ( cbMP3TOT.IsChecked ) )
        {
            string oldValue = "", newValue = "";

            if ( ( bool ) ( _oldScoreList [ 0 ].MP3TOT == true ) )
            { oldValue = DBNames.LogYes; }
            else
            { oldValue = DBNames.LogNo; }

            if ( _scoreList [ 0 ].MP3TOT == 1 )
            { newValue = DBNames.LogYes; }
            else
            { newValue = DBNames.LogNo; }

            DBCommands.WriteDetailLog ( _historyId, DBNames.LogMP3TOT, oldValue, newValue );
        }
        #endregion

        #region Log MP3 PIA Changes
        if ( ( bool ) ( cbMP3PIA.IsChecked ) )
        {
            string oldValue = "", newValue = "";

            if ( ( bool ) ( _oldScoreList [ 0 ].MP3PIA == true ) )
            { oldValue = DBNames.LogYes; }
            else
            { oldValue = DBNames.LogNo; }

            if ( _scoreList [ 0 ].MP3PIA == 1 )
            { newValue = DBNames.LogYes; }
            else
            { newValue = DBNames.LogNo; }

            DBCommands.WriteDetailLog ( _historyId, DBNames.LogMP3PIA, oldValue, newValue );
        }
        #endregion

        #region Log MuseScore Online Changes
        if ( ( bool ) ( cbOnline.IsChecked ) )
        {
            string oldValue = "", newValue = "";

            if ( ( bool ) ( _oldScoreList [ 0 ].MuseScoreOnline == true ) )
            { oldValue = DBNames.LogYes; }
            else
            { oldValue = DBNames.LogNo; }

            if ( _scoreList [ 0 ].MuseScoreOnline == 1 )
            { newValue = DBNames.LogYes; }
            else
            { newValue = DBNames.LogNo; }

            DBCommands.WriteDetailLog ( _historyId, DBNames.LogMSCOnline, oldValue, newValue );
        }
        #endregion

        #region Log Lyrics Changes
        if ( ( bool ) ( cbLyrics.IsChecked ) )
        {
            DBCommands.WriteDetailLog ( _historyId, DBNames.LogLyrics, "", "" );
        }
        #endregion

        #region Log Notes Changes
        if ( ( bool ) ( cbNotes.IsChecked ) )
        {
            DBCommands.WriteDetailLog ( _historyId, DBNames.LogNotes, "", "" );
        }
        #endregion

        #region Log Amount Publisher 1 Changes
        if ( ( bool ) ( cbAmountPublisher1.IsChecked ) )
        {
            DBCommands.WriteDetailLog ( _historyId, DBNames.LogAmountPublisher, _oldScoreList [ 0 ].AmountPublisher1.ToString ( ), _scoreList [ 0 ].AmountPublisher1.ToString ( ) );
        }
        #endregion

        #region Log Amount Publisher 2 Changes
        if ( ( bool ) ( cbAmountPublisher2.IsChecked ) )
        {
            DBCommands.WriteDetailLog ( _historyId, DBNames.LogAmountPublisher, _oldScoreList [ 0 ].AmountPublisher2.ToString ( ), _scoreList [ 0 ].AmountPublisher2.ToString ( ) );
        }
        #endregion

        #region Log Amount Publisher 3 Changes
        if ( ( bool ) ( cbAmountPublisher3.IsChecked ) )
        {
            DBCommands.WriteDetailLog ( _historyId, DBNames.LogAmountPublisher, _oldScoreList [ 0 ].AmountPublisher3.ToString ( ), _scoreList [ 0 ].AmountPublisher3.ToString ( ) );
        }
        #endregion

        #region Log Amount Publisher 4 Changes
        if ( ( bool ) ( cbAmountPublisher4.IsChecked ) )
        {
            DBCommands.WriteDetailLog ( _historyId, DBNames.LogAmountPublisher, _oldScoreList [ 0 ].AmountPublisher4.ToString ( ), _scoreList [ 0 ].AmountPublisher4.ToString ( ) );
        }
        #endregion

        #region Log Publisher 1 Changes
        if ( ( bool ) ( cbPublisher1.IsChecked ) )
        {
            DBCommands.WriteDetailLog ( _historyId, DBNames.LogPublisher, _oldScoreList [ 0 ].Publisher1Name, _scoreList [ 0 ].Publisher1Name );
        }

        #endregion

        #region Log Publisher 2 Changes
        if ( ( bool ) ( cbPublisher2.IsChecked ) )
        {
            DBCommands.WriteDetailLog ( _historyId, DBNames.LogPublisher, _oldScoreList [ 0 ].Publisher2Name, _scoreList [ 0 ].Publisher2Name );
        }
        #endregion

        #region Log Publisher 3 Changes
        if ( ( bool ) ( cbPublisher3.IsChecked ) )
        {
            DBCommands.WriteDetailLog ( _historyId, DBNames.LogPublisher, _oldScoreList [ 0 ].Publisher3Name, _scoreList [ 0 ].Publisher3Name );
        }
        #endregion

        #region Log Publisher 4 Changes
        if ( ( bool ) ( cbPublisher4.IsChecked ) )
        {
            DBCommands.WriteDetailLog ( _historyId, DBNames.LogPublisher, _oldScoreList [ 0 ].Publisher4Name, _scoreList [ 0 ].Publisher4Name );
        }
        #endregion
    }

    public void ResetFields ( )
    {
        tbAmountPublisher1.Text = "0";
        tbAmountPublisher2.Text = "0";
        tbAmountPublisher3.Text = "0";
        tbAmountPublisher4.Text = "0";
        tbAmountSupplierTotal.Text = "0";
        tbArranger.Text = "";
        tbComposer.Text = "";
        tbMusicPiece.Text = "";
        tbScoreNumber.Text = "";
        tbSubTitle.Text = "";
        tbTextwriter.Text = "";
        tbTitle.Text = "";
        tbLyrics.Text = "";
        comAccompaniment.SelectedIndex = 1;
        comArchive.SelectedIndex = 1;
        comGenre.SelectedIndex = 1;
        comLanguage.SelectedIndex = 1;
        comPublisher1.SelectedIndex = 1;
        comPublisher2.SelectedIndex = 1;
        comPublisher3.SelectedIndex = 1;
        comPublisher4.SelectedIndex = 1;
        comRepertoire.SelectedIndex = 1;
        chkByHeart.IsChecked = false;
        chkChecked.IsChecked = false;
        chkMP3B1.IsChecked = false;
        chkMP3B2.IsChecked = false;
        chkMP3T1.IsChecked = false;
        chkMP3T2.IsChecked = false;
        chkMP3TOT.IsChecked = false;
        chkMP3SOL.IsChecked = false;
        chkMP3PIA.IsChecked = false;
        chkMSCOnline.IsChecked = false;
        chkMSCORK.IsChecked = false;
        chkMSCORP.IsChecked = false;
        chkMSCTOK.IsChecked = false;
        chkMSCTOP.IsChecked = false;
        chkPDFORK.IsChecked = false;
        chkPDFORP.IsChecked = false;
        chkPDFTOK.IsChecked = false;
        chkPDFTOP.IsChecked = false;
        dpDigitized.SelectedDate = null;
        dpModified.SelectedDate = null;

        ResetChanged ( );
    }

    public void ResetChanged ( )
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
        cbNewScore.IsChecked = false;
        cbDuration.IsChecked = false;
        cbDurationMinutes.IsChecked = false;
        cbDurationSeconds.IsChecked = false;
        tbEnableEdit.Text = "Collapsed";
        btnSave.IsEnabled = false;
        btnSave.ToolTip = "Er zijn geen gegevens aangepast, opslaan niet mogelijk";
    }

    private void DeleteScore ( object sender, RoutedEventArgs e )
    {
        if ( SelectedScore != null )
        {
            MessageBoxResult messageBoxResult = MessageBox.Show ( $"Weet je zeker dat je {SelectedScore.Score} - {SelectedScore.ScoreTitle} wilt verwijderen?", $"Verwijder partituur {SelectedScore.ScoreNumber}", MessageBoxButton.YesNoCancel );

            switch ( messageBoxResult )
            {
                case MessageBoxResult.Yes:
                    // Continue Deleting Score
                    if ( SelectedScore.ScoreNumber != null )
                    {
                        DBCommands.DeleteScore ( SelectedScore.ScoreNumber, SelectedScore.ScoreSubNumber );
                        // Write log info
                        DBCommands.WriteLog ( LibraryUsers.SelectedUserId, DBNames.LogScoreDeleted, $"Partituur: {tbScoreNumber.Text}" );
                        DBCommands.ReAddScore ( SelectedScore.ScoreNumber );

                        // If the selected (Sub) score has number "01" and there is only 1 Score Left and the subscorenumber should be removed from the datagrid
                        if ( SelectedScore.ScoreSubNumber == "01" )
                        {
                            var NumberOfScores = DBCommands.CheckForSubScores(SelectedScore.ScoreNumber);
                            if ( NumberOfScores == 1 )
                            {
                                SelectedScore.ScoreSubNumber = "";
                                SelectedScore.Score = SelectedScore.ScoreNumber;
                            }
                        }
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
        scores = new ScoreViewModel ( );
        DataContext = scores;
    }

    private void GetNotes ( )
    {
        var ContentNotes = string.Empty;

        // Clear the current textbox
        memoNotes.Document.Blocks.Clear ( );

        if ( SelectedScore != null )
        {
            if ( SelectedScore.Notes != null && SelectedScore.Notes != "" )
            {
                ContentNotes = SelectedScore.Notes.ToString ( );

                //convert to byte[]
                byte[] dataArr = Encoding.UTF8.GetBytes(ContentNotes);

                using ( MemoryStream ms = new ( dataArr ) )
                {
                    //load data
                    TextRange flowDocRange = new TextRange(memoNotes.Document.ContentStart, memoNotes.Document.ContentEnd);
                    flowDocRange.Load ( ms, DataFormats.Rtf );
                }
            }
        }
        cbNotes.IsChecked = false;
    }

    #region Get rich text from flow document of a memo field (Lyrics or Notes)

    private string GetRichTextFromFlowDocument ( FlowDocument fDoc )
    {
        string result = string.Empty;

        //convert to string
        if ( fDoc != null )
        {
            TextRange tr = new TextRange(fDoc.ContentStart, fDoc.ContentEnd);

            using ( MemoryStream ms = new MemoryStream ( ) )
            {
                tr.Save ( ms, DataFormats.Rtf );
                result = System.Text.Encoding.UTF8.GetString ( ms.ToArray ( ) );
            }
        }
        return result;
    }

    #endregion Get rich text from flow document of a memo field (Lyrics or Notes)

    private void RenumberClick ( object sender, RoutedEventArgs e )
    {
        if ( SelectedScore != null )
        {
            RenumberScore renumberScore = new RenumberScore(SelectedScore, SelectedScore.ScoreNumber, SelectedScore.ScoreSubNumber);
            renumberScore.Show ( );

            renumberScore.Closed += delegate
            {
                //  The user has closed the dialog.
                scores = new ScoreViewModel ( );
                DataContext = scores;
            };
        }
    }

    private void NewScoreClicked ( object sender, RoutedEventArgs e )
    {
        if ( SelectedScore != null )
        {
            NewScore newScore = new(SelectedScore, SelectedScore.ScoreNumber);
            newScore.Show ( );

            newScore.Closed += delegate
            {
                //  The user has closed the dialog.
                scores = new ScoreViewModel ( );
                DataContext = scores;

                // Select the Newly created Score
                for ( int i = 0 ; i < ScoresDataGrid.Items.Count ; i++ )
                {
                    if ( ( ( ScoreModel ) ( ScoresDataGrid.Items [ i ] ) ).ScoreNumber == NewScoreNo.NewScoreNumber )
                    {
                        ScoresDataGrid.SelectedIndex = i;
                        ScoresDataGrid.ScrollIntoView ( ScoresDataGrid.Items [ ScoresDataGrid.SelectedIndex ] );
                        tbTitle.Text = "";
                        cbNewScore.IsChecked = true;
                        break;
                    }
                }
            };
        }
    }

    private void NewSubScoreClicked ( object sender, RoutedEventArgs e )
    {
        if ( SelectedScore != null )
        {
            var NumberOfSubScores = DBCommands.CheckForSubScores ( SelectedScore.ScoreNumber );
            var SubScore = "";

            if ( NumberOfSubScores == 1 )
            {
                // There are no subscores, Set SubSocre for current Score to "01" and create new Score with SubNumber = "02"
                DBCommands.AddSubScore ( SelectedScore.ScoreNumber, "01" );

                SubScore = "02";
            }
            else
            {
                // There are SubScores get the Highest SubScore Number
                int SubScoreValue = DBCommands.getHighestSubNumber(SelectedScore.ScoreNumber) + 1;

                SubScore = SubScoreValue.ToString ( "00" );
            }

            ObservableCollection<ScoreModel> selectedScore = new();

            selectedScore.Add ( new ScoreModel
            {
                ScoreNumber = SelectedScore.ScoreNumber,
                ScoreSubNumber = SubScore,
                ArchiveId = SelectedScore.ArchiveId,
                AccompanimentId = SelectedScore.AccompanimentId,
                GenreId = SelectedScore.GenreId,
                LanguageId = SelectedScore.LanguageId,
                Publisher1Id = SelectedScore.Publisher1Id,
                Publisher2Id = SelectedScore.Publisher2Id,
                Publisher3Id = SelectedScore.Publisher3Id,
                Publisher4Id = SelectedScore.Publisher4Id,
                RepertoireId = SelectedScore.RepertoireId,
                MusicPiece = SelectedScore.MusicPiece,
                AmountPublisher1 = SelectedScore.AmountPublisher1,
                AmountPublisher2 = SelectedScore.AmountPublisher2,
                AmountPublisher3 = SelectedScore.AmountPublisher3,
                AmountPublisher4 = SelectedScore.AmountPublisher4,
            } );

            // Save the original Selected Score Number
            string _selectedScore = SelectedScore.ScoreNumber;

            DBCommands.AddNewScoreAsSubscore ( selectedScore );

            scores = new ScoreViewModel ( );
            DataContext = scores;

            // Select the Newly created Score
            for ( int i = 0 ; i < ScoresDataGrid.Items.Count ; i++ )
            {
                if ( ( ( ScoreModel ) ( ScoresDataGrid.Items [ i ] ) ).ScoreNumber == _selectedScore && ( ( ScoreModel ) ( ScoresDataGrid.Items [ i ] ) ).ScoreSubNumber == SubScore )
                {
                    ScoresDataGrid.SelectedIndex = i;
                    ScoresDataGrid.ScrollIntoView ( ScoresDataGrid.Items [ ScoresDataGrid.SelectedIndex ] );
                    tbTitle.Text = "";
                    cbNewScore.IsChecked = true;
                    break;
                }
            }

        }
    }

    private void btnClearSearch_Click ( object sender, RoutedEventArgs e )
    {
        tbSearch.Text = "";
        ScoresDataGrid.ItemsSource = scores.Scores;
    }

    private void tbSearch_TextChanged ( object sender, TextChangedEventArgs e )
    {
        var search = sender as TextBox;

        if ( search.Text.Length > 1 )
        {
            if ( !string.IsNullOrEmpty ( search.Text ) )
            {
                var filteredList = scores.Scores.Where(x=> x.SearchField.ToLower().Contains(tbSearch.Text.ToLower()));
                ScoresDataGrid.ItemsSource = filteredList;
            }
            else
            {
                ScoresDataGrid.ItemsSource = scores.Scores;
            }
        }
    }

    private void ButtonDeIncreaseTimeClick ( object sender, RoutedEventArgs e )
    {
        var propertyName = ((Button)sender).Name;
        var _minutes = int.Parse(tbMinutes.Text);
        var _seconds = int.Parse(tbSeconds.Text);

        switch ( propertyName )
        {
            case "btnIncreaseMin":
                _minutes++;
                tbMinutes.Text = _minutes.ToString ( );
                break;
            case "btnIncreaseSec":
                _seconds++;
                if ( _seconds > 59 )
                { _seconds = 0; }
                tbSeconds.Text = _seconds.ToString ( "00" );
                break;
            case "btnDecreaseMin":
                if ( _minutes > 0 )
                {
                    _minutes--;
                    tbMinutes.Text = _minutes.ToString ( );
                }
                else
                {
                    _minutes = 0;
                }
                break;
            case "btnDecreaseSec":
                if ( _seconds > 0 )
                {
                    _seconds--;
                    tbSeconds.Text = _seconds.ToString ( "00" );
                }
                else
                {
                    _seconds = 59;
                    tbSeconds.Text = _seconds.ToString ( "00" );
                }
                break;
        }
    }

    private void tbSecondsChanged ( object sender, TextChangedEventArgs e )
    {
        int value;
        if ( !int.TryParse ( tbSeconds.Text, out value ) || value < 0 || value > 59 )
        {
            MessageBox.Show ( "Gelieve een numerieke waarde tussen 0 en 59 in te voeren." );
            tbSeconds.Focus ( );
            return;
        }
        cbDurationSeconds.IsChecked = true;
    }

    private void TimeSpan_PreviewTextInput ( object sender, System.Windows.Input.TextCompositionEventArgs e )
    {
        var propertyName = ((TextBox)sender).Name;
        var _input ="";
        switch ( propertyName )
        {
            case "tbMinutes":
                _input = tbMinutes.Text;
                break;
            case "tbSeconds":
                _input = tbSeconds.Text;
                break;
        }

        if ( !Regex.IsMatch ( e.Text, "^[0-9]$" ) || _input.Length >= 2 )
        {
            e.Handled = true;
        }
    }

    #region Create Cover Sheet
    private void CreateCoverSheet ( object sender, RoutedEventArgs e )
    {
        var _outputFolder = @LibraryUsers.SelectedUserCoverSheetFolder + "\\";
        var _templatename="Resources\\Template\\";
        int LyricsFontSize = 12, LineCount = 36;

        if ( tbLyrics.LineCount <= 0 )
        { _templatename += "CoverSheet0.docx"; }
        if ( tbLyrics.LineCount > 0 && tbLyrics.LineCount <= 36 )
        { _templatename += "CoverSheet1.docx"; }
        if ( tbLyrics.LineCount > 38 && tbLyrics.LineCount <= 72 )
        { _templatename += "CoverSheet2.docx"; }
        if ( tbLyrics.LineCount > 72 )
        { _templatename += "CoverSheet2.docx"; LyricsFontSize = 9; LineCount = 49; }

        var AccompanimentName = "Piano";
        string ScoreNumber = "", B1 = "¨", B2 = "¨", T1 = "¨", T2 = "¨", Solo = "¨", Accompaniment = "¨";
        var ScoreNumberFontSize = 80;

        // Use ScoreNumber or ScoreNumber-SubScorenumber
        if ( SelectedScore.ScoreSubNumber == "" )
        {
            ScoreNumber = $"{SelectedScore.ScoreNumber}";
        }
        else
        {
            ScoreNumber = $"{SelectedScore.ScoreNumber}-{SelectedScore.ScoreSubNumber}";
            ScoreNumberFontSize = 52;
        }

        // Set the name of the CoverSheet document and image
        var _docname = $"{_outputFolder}{ScoreNumber}.docx";


        // Set the check boxes
        if ( SelectedScore.MP3B1 )
        {
            B1 = "þ";
        }

        if ( SelectedScore.MP3B2 )
        {
            B2 = "þ";
        }

        if ( SelectedScore.MP3T1 )
        {
            T1 = "þ";
        }

        if ( SelectedScore.MP3T2 )
        {
            T2 = "þ";
        }

        if ( SelectedScore.MP3SOL )
        {
            Solo = "þ";
        }

        // Checkbox for Accompaniment should not be set when there is no accompaniment selected (Id:1) or Piano is missing (Id:4) otherwise is will be check marked
        // for the same Ids the AccompaimentName will be set to Piano
        if ( SelectedScore.AccompanimentId != 1 && SelectedScore.AccompanimentId != 4 )
        {
            Accompaniment = "þ";
            AccompanimentName = SelectedScore.AccompanimentName;
        }

        // Load the template document
        WordDocument document = new WordDocument(_templatename, FormatType.Docx);

        // Set the font size of the ScoreNumber
        Syncfusion.DocIO.DLS.TextSelection[] selectedScoreNumber = document.FindAll("<<Nr>>", true, true);

        for ( int i = 0 ; i < selectedScoreNumber.Length ; i++ )
        {
            selectedScoreNumber [ i ].GetAsOneRange ( ).CharacterFormat.FontSize = ScoreNumberFontSize;
        }

        // If the number of lines are bigger then 72, font size should be smaller to be sure all lines will fit
        if ( tbLyrics.LineCount > 72 )
        {
            Syncfusion.DocIO.DLS.TextSelection[] selectedLyrics1 = document.FindAll("<<Lyrics1>>", true, true);
            Syncfusion.DocIO.DLS.TextSelection[] selectedLyrics2 = document.FindAll("<<Lyrics2>>", true, true);

            for ( int i = 0 ; i < selectedLyrics1.Length ; i++ )
            {
                selectedLyrics1 [ i ].GetAsOneRange ( ).CharacterFormat.FontSize = LyricsFontSize;
                selectedLyrics2 [ i ].GetAsOneRange ( ).CharacterFormat.FontSize = LyricsFontSize;
            }
        }

        // Replace the Bookmarks with the actual text
        document.Replace ( "<<Nr>>", ScoreNumber, true, true );
        document.Replace ( "<<Titel>>", SelectedScore.ScoreTitle, true, true );
        document.Replace ( "<<Ondertitel>>", SelectedScore.ScoreSubTitle, true, true );
        document.Replace ( "<<Componist>>", SelectedScore.Composer, true, true );
        document.Replace ( "<<Tekstschrijver>>", SelectedScore.Textwriter, true, true );
        document.Replace ( "<<Arrangement>>", SelectedScore.Arranger, true, true );
        document.Replace ( "<<Genre>>", SelectedScore.GenreName, true, true );
        document.Replace ( "<<Taal>>", SelectedScore.LanguageName, true, true );
        document.Replace ( "<<Begeleiding>>", AccompanimentName, true, true );
        document.Replace ( "<<B1>>", B1, true, true );
        document.Replace ( "<<B2>>", B2, true, true );
        document.Replace ( "<<T1>>", T1, true, true );
        document.Replace ( "<<T2>>", T2, true, true );
        document.Replace ( "<<SOL>>", Solo, true, true );
        document.Replace ( "<<PIA>>", Accompaniment, true, true );

        if ( tbLyrics.LineCount > 0 && tbLyrics.LineCount <= 36 )
        {
            document.Replace ( "<<Lyrics>>", tbLyrics.Text, true, true );
        }

        if ( tbLyrics.LineCount > 36 )
        {
            var _lyrics1 = "";
            var _lyrics2 = "";

            for ( int i = 0 ; i < LineCount ; i++ )
            {
                if ( i == 0 || i == LineCount )
                {
                    if ( tbLyrics.GetLineText ( i ) != "\r\n" )
                    {
                        _lyrics1 += tbLyrics.GetLineText ( i );
                    }
                }
                else
                {
                    _lyrics1 += tbLyrics.GetLineText ( i );
                }
            }

            for ( int i = LineCount ; i < tbLyrics.LineCount ; i++ )
            {
                if ( i == LineCount || i == tbLyrics.LineCount )
                {
                    if ( tbLyrics.GetLineText ( i ) != "\r\n" )
                    {
                        _lyrics2 += tbLyrics.GetLineText ( i );
                    }
                }
                else
                {
                    _lyrics2 += tbLyrics.GetLineText ( i );
                }
            }

            document.Replace ( "<<Lyrics1>>", _lyrics1, true, true );
            document.Replace ( "<<Lyrics2>>", _lyrics2, true, true );
        }

        //Saves the Word document
        document.Save ( _docname );

        DBCommands.WriteLog ( LibraryUsers.SelectedUserId, DBNames.LogCoverSheetCreated, $"Partituur: {tbScoreNumber.Text}" );
    }
    #endregion
}
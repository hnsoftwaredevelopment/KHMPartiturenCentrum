using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KHMPartiturenCentrum.Helpers;
using KHMPartiturenCentrum.Models;
using KHMPartiturenCentrum.ViewModels;

namespace KHMPartiturenCentrum.Views;

/// <summary>
/// Interaction logic for NewScores.xaml
/// </summary>
public partial class NewScores : Page
{
    public NewScoreViewModel? scores;
    public NewScores ()
    {
        InitializeComponent ();
        scores = new NewScoreViewModel ();
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

        #region TAB Score Information
        #region 1st Row (ScoreNumber, Repertoire, Archive, and sing by heart)
        tbScoreNumber.Text = selectedRow.Score;
        //chkByHeart.IsChecked = selectedRow.ByHeart;

        #region Repertoire Combobox
        comRepertoire.Text = selectedRow.RepertoireName;
        foreach ( RepertoireModel repertoire in comRepertoire.Items )
        {
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
            if ( archive.ArchiveName == comArchive.Text.ToString () )
            {
                comArchive.SelectedItem = archive;
            }
        }
        #endregion
        #endregion

        //#region 2th Row (Title and SubTitle)
        //tbTitle.Text = selectedRow.Title;
        //tbSubTitle.Text = selectedRow.SubTitle;
        //#endregion

        //#region 3th Row (Composer, Textwriter and Arranger)
        //tbComposer.Text = selectedRow.Composer;
        //tbTextwriter.Text = selectedRow.Textwriter;
        //tbArranger.Text = selectedRow.Arranger;
        //#endregion

        #region 4th Row (Genre, Accompaniment and Language)
        #region Genre Combobox
        comGenre.Text = selectedRow.GenreName;
        foreach ( GenreModel genre in comGenre.Items )
        {
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
            if ( language.LanguageName == comLanguage.Text.ToString () )
            {
                comLanguage.SelectedItem = language;
            }
        }
        #endregion
        #endregion

        //#region 5th Row (Music Piece)
        //tbMusicPiece.Text = selectedRow.MusicPiece;
        //#endregion

        //#region 6th Row (Date created, Date Modified and Checked)
        //dpDigitized.SelectedDate = selectedRow.DateCreated.ToDateTime ( TimeOnly.Parse ( "00:00 AM" ) );
        //dpDigitized.Text = selectedRow.DateCreatedString;

        //dpModified.SelectedDate = selectedRow.DateModified.ToDateTime ( TimeOnly.Parse ( "00:00 AM" ) );
        //dpModified.Text = selectedRow.DateModifiedString;

        //chkChecked.IsChecked = selectedRow.Checked;
        //#endregion

        //#region 7th Row (Checkboxes for MuseScore, PDF and MP3)
        //#region MuseScore checkboxes
        //chkMSCORP.IsChecked = selectedRow.MuseScoreORP;
        //chkMSCORK.IsChecked = selectedRow.MuseScoreORK;
        //chkMSCTOP.IsChecked = selectedRow.MuseScoreTOP;
        //chkMSCTOK.IsChecked = selectedRow.MuseScoreTOK;
        //#endregion

        //#region PDF checkboxes
        //chkPDFORP.IsChecked = selectedRow.PDFORP;
        //chkPDFORK.IsChecked = selectedRow.PDFORK;
        //chkPDFTOP.IsChecked = selectedRow.PDFTOP;
        //chkPDFTOK.IsChecked = selectedRow.PDFTOK;
        //#endregion

        //#region MP3 checkboxes
        //chkMP3B1.IsChecked = selectedRow.MP3B1;
        //chkMP3B2.IsChecked = selectedRow.MP3B2;
        //chkMP3T1.IsChecked = selectedRow.MP3T1;
        //chkMP3T2.IsChecked = selectedRow.MP3T2;

        //chkMP3SOL.IsChecked = selectedRow.MP3SOL;
        //chkMP3TOT.IsChecked = selectedRow.MP3TOT;
        //chkMP3PIA.IsChecked = selectedRow.MP3PIA;
        //#endregion

        //#region MuseScore Online checkbox
        //chkMSCOnline.IsChecked = selectedRow.MuseScoreOnline;
        //#endregion
        //#endregion
        //#endregion

        //#region TAB Lyrics
        //// Unknow how to setthe memo field
        //#endregion

        //#region TAB Notes
        //// Unknow how to set the memo field
        //#endregion

        //#region TAB: Licenses
        //#region Publisher 1
        //tbAmountPublisher1.Text = selectedRow.AmountPublisher1.ToString ();

        #region Publisher1 Combobox
        comPublisher1.Text = selectedRow.Publisher1Name;
        foreach ( PublisherModel publisher in comPublisher1.Items )
        {
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
            if ( publisher.PublisherName == comPublisher3.Text.ToString () )
            {
                comPublisher3.SelectedItem = publisher;
            }
        }
        #endregion
        #endregion

        #region Publisher 4
        tbAmountSupplier4.Text = selectedRow.NumberScoresPublisher4.ToString ();

        #region Publisherr4 Combobox
        comPublisher4.Text = selectedRow.Publisher4Name;
        foreach ( PublisherModel publisher in comPublisher4.Items )
        {
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
        //#endregion
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
}

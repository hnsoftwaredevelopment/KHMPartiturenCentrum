using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

using static System.Net.Mime.MediaTypeNames;

namespace KHMPartiturenCentrum.Views
{
    /// <summary>
    /// Interaction logic for Scores.xaml
    /// </summary>
    public partial class Scores : Page
    {
        public ScoreViewModel? scores;
        public Scores()
        {
            InitializeComponent();
            scores = new ScoreViewModel ();
            DataContext = scores;
        }

        private void PageLoaded ( object sender, RoutedEventArgs e )
        {
            //DataTable _dt = DBCommands.GetData(DBNames.ScoresView, DBNames.ScoresFieldNameScoreNumber );
            //ScoresDataGrid.DataContext = _dt;

            comAccompaniment.ItemsSource = DBCommands.GetAccompaniments ();
            comArchive.ItemsSource = DBCommands.GetArchives ();
            comGenre.ItemsSource = DBCommands.GetGenres ();
            comLanguage.ItemsSource = DBCommands.GetLanguages ();
            comRepertoire.ItemsSource = DBCommands.GetRepertoires ();
            comSupplier1.ItemsSource = DBCommands.GetPublishers ();
            comSupplier2.ItemsSource = DBCommands.GetPublishers ();
            comSupplier3.ItemsSource = DBCommands.GetPublishers ();
            comSupplier4.ItemsSource = DBCommands.GetPublishers ();
        }

        private void SelectedScoreChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            //DataRowView selectedRow = dg.SelectedItem as DataRowView;

            ScoreModel selectedRow = (ScoreModel)dg.SelectedItem;

            #region 1st Row (ScoreNumber, Repertoire and Archive)
            tbScoreNumber.Text = selectedRow.ScoreNumber;

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
            comArchive.Text = selectedRow.RepertoireName;
            foreach ( ArchiveModel archive in comArchive.Items )
            {
                if ( archive.ArchiveName == comArchive.Text.ToString () )
                {
                    comArchive.SelectedItem = archive;
                }
            }
            #endregion
            #endregion

            #region 2th Row (Title and SubTitle)
            tbTitle.Text = selectedRow.ScoreTitle;
            tbSubTitle.Text = selectedRow.ScoreSubTitle;
            #endregion

            #region 3th Row (Composer, Textwriter and Arranger)
            tbComposer.Text = selectedRow.Composer;
            tbTextwriter.Text = selectedRow.TextWriter;
            tbArranger.Text = selectedRow.Arranger;
            #endregion

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

            #region 5th Row (Date created, Date Modified and Checked)
            dpDigitized.SelectedDate = selectedRow.DateCreated.ToDateTime(TimeOnly.Parse("00:00 AM"));
            dpDigitized.Text = selectedRow.DateCreatedString;

            dpModified.SelectedDate = selectedRow.DateModified.ToDateTime ( TimeOnly.Parse ( "00:00 AM" ) );
            dpModified.Text = selectedRow.DateModifiedString;

            chkChecked.IsChecked = selectedRow.Check;
            #endregion

            #region 6th Row (Checkboxes for MuseScore, PDF and MP3)
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

            #region TAB Lyrics
            // Unknow how to setthe memo field
            #endregion

            #region TAB: Licenses
            #region Supplier 1
            tbAmountSupplier1.Text = selectedRow.NumberScoresSupplier1.ToString();

            #region Supplier1 Combobox
            comSupplier1.Text = selectedRow.Supplier1Name;
            foreach ( PublisherModel publisher in comSupplier1.Items )
            {
                if ( publisher.PublisherName == comSupplier1.Text.ToString () )
                {
                    comSupplier1.SelectedItem = publisher;
                }
            }
            #endregion
            #endregion

            #region Supplier 2
            tbAmountSupplier2.Text = selectedRow.NumberScoresSupplier2.ToString ();

            #region Supplier2 Combobox
            comSupplier2.Text = selectedRow.Supplier2Name;
            foreach ( PublisherModel publisher in comSupplier2.Items )
            {
                if ( publisher.PublisherName == comSupplier2.Text.ToString () )
                {
                    comSupplier2.SelectedItem = publisher;
                }
            }
            #endregion
            #endregion

            #region Supplier 3
            tbAmountSupplier3.Text = selectedRow.NumberScoresSupplier3.ToString ();

            #region Supplier3 Combobox
            comSupplier3.Text = selectedRow.Supplier3Name;
            foreach ( PublisherModel publisher in comSupplier1.Items )
            {
                if ( publisher.PublisherName == comSupplier3.Text.ToString () )
                {
                    comSupplier3.SelectedItem = publisher;
                }
            }
            #endregion
            #endregion

            #region Supplier 4
            tbAmountSupplier4.Text = selectedRow.NumberScoresSupplier4.ToString ();

            #region Supplier4 Combobox
            comSupplier4.Text = selectedRow.Supplier4Name;
            foreach ( PublisherModel publisher in comSupplier4.Items )
            {
                if ( publisher.PublisherName == comSupplier4.Text.ToString () )
                {
                    comSupplier4.SelectedItem = publisher;
                }
            }
            #endregion
            #endregion

            #region Total
            var Total = selectedRow.NumberScoresSupplier1 + selectedRow.NumberScoresSupplier2 + selectedRow.NumberScoresSupplier3 + selectedRow.NumberScoresSupplier4;
            tbAmountSupplierTotal.Text = Total.ToString ();
            #endregion
            #endregion
        }
        }
}

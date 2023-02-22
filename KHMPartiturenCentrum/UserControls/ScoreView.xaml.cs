using KHMPartiturenCentrum.Helpers;
using KHMPartiturenCentrum.Models;
using KHMPartiturenCentrum.ViewModels;

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

namespace KHMPartiturenCentrum.UserControls
{
    /// <summary>
    /// Interaction logic for ScoreView.xaml
    /// </summary>
    public partial class ScoreView : UserControl
    {
        public ScoreViewModel? scores;
        public ScoreModel? SelectedScore;
        public ScoreView()
        {
            InitializeComponent();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            comAccompaniment.ItemsSource = DBCommands.GetAccompaniments();
            comArchive.ItemsSource = DBCommands.GetArchives();
            comGenre.ItemsSource = DBCommands.GetGenres();
            comLanguage.ItemsSource = DBCommands.GetLanguages();
            comRepertoire.ItemsSource = DBCommands.GetRepertoires();
            comPublisher1.ItemsSource = DBCommands.GetPublishers();
            comPublisher2.ItemsSource = DBCommands.GetPublishers();
            comPublisher3.ItemsSource = DBCommands.GetPublishers();
            comPublisher4.ItemsSource = DBCommands.GetPublishers();
        }

        private void SelectedScoreChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;

            ScoreModel selectedRow = (ScoreModel)dg.SelectedItem;
            SelectedScore = selectedRow;

            #region TAB Score Information
            #region 1st Row (ScoreNumber, Repertoire, Archive, and sing by heart)
            tbScoreNumber.Text = selectedRow.Score;
            chkByHeart.IsChecked = selectedRow.ByHeart;

            #region Repertoire Combobox
            comRepertoire.Text = selectedRow.RepertoireName;
            foreach (RepertoireModel repertoire in comRepertoire.Items)
            {
                if (comRepertoire.Text == null) { comRepertoire.Text = ""; }
                if (repertoire.RepertoireName == comRepertoire.Text.ToString())
                {
                    comRepertoire.SelectedItem = repertoire;
                }
            }
            #endregion

            #region Archive Combobox
            comArchive.Text = selectedRow.ArchiveName;
            foreach (ArchiveModel archive in comArchive.Items)
            {
                if (comArchive.Text == null) { comArchive.Text = ""; }
                if (archive.ArchiveName == comArchive.Text.ToString())
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
            tbTextwriter.Text = selectedRow.Textwriter;
            tbArranger.Text = selectedRow.Arranger;
            #endregion

            #region 4th Row (Genre, Accompaniment and Language)
            #region Genre Combobox
            comGenre.Text = selectedRow.GenreName;
            foreach (GenreModel genre in comGenre.Items)
            {
                if (comGenre.Text == null) { comGenre.Text = ""; }
                if (genre.GenreName == comGenre.Text.ToString())
                {
                    comGenre.SelectedItem = genre;
                }
            }
            #endregion

            #region Accompaniment Combobox
            comAccompaniment.Text = selectedRow.AccompanimentName;
            foreach (AccompanimentModel accompaniment in comAccompaniment.Items)
            {
                if (comAccompaniment.Text == null) { comAccompaniment.Text = ""; }
                if (accompaniment.AccompanimentName == comAccompaniment.Text.ToString())
                {
                    comAccompaniment.SelectedItem = accompaniment;
                }
            }
            #endregion

            #region Language Combobox
            comLanguage.Text = selectedRow.LanguageName;
            foreach (LanguageModel language in comLanguage.Items)
            {
                if (comLanguage.Text == null) { comLanguage.Text = ""; }
                if (language.LanguageName == comLanguage.Text.ToString())
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
            dpDigitized.SelectedDate = selectedRow.DateCreated.ToDateTime(TimeOnly.Parse("00:00 AM"));
            dpDigitized.Text = selectedRow.DateCreatedString;

            dpModified.SelectedDate = selectedRow.DateModified.ToDateTime(TimeOnly.Parse("00:00 AM"));
            dpModified.Text = selectedRow.DateModifiedString;

            chkChecked.IsChecked = selectedRow.Check;
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
            // Unknow how to setthe memo field
            #endregion

            #region TAB Notes
            // Unknow how to set the memo field
            #endregion

            #region TAB: Licenses
            #region Publisher 1
            tbAmountPublisher1.Text = selectedRow.NumberScoresPublisher1.ToString();

            #region Publisher1 Combobox
            comPublisher1.Text = selectedRow.Publisher1Name;
            foreach (PublisherModel publisher in comPublisher1.Items)
            {
                if (comPublisher1.Text == null) { comPublisher1.Text = ""; }
                if (publisher.PublisherName == comPublisher1.Text.ToString())
                {
                    comPublisher1.SelectedItem = publisher;
                }
            }
            #endregion
            #endregion

            #region Publisher 2
            tbAmountPublisher2.Text = selectedRow.NumberScoresPublisher2.ToString();

            #region Publisher2 Combobox
            comPublisher2.Text = selectedRow.Publisher2Name;
            foreach (PublisherModel publisher in comPublisher2.Items)
            {
                if (comPublisher2.Text == null) { comPublisher2.Text = ""; }
                if (publisher.PublisherName == comPublisher2.Text.ToString())
                {
                    comPublisher2.SelectedItem = publisher;
                }
            }
            #endregion
            #endregion

            #region Publisher 3
            tbAmountPublisher3.Text = selectedRow.NumberScoresPublisher3.ToString();

            #region Publisher3 Combobox
            comPublisher3.Text = selectedRow.Publisher3Name;
            foreach (PublisherModel publisher in comPublisher3.Items)
            {
                if (comPublisher3.Text == null) { comPublisher3.Text = ""; }
                if (publisher.PublisherName == comPublisher3.Text.ToString())
                {
                    comPublisher3.SelectedItem = publisher;
                }
            }
            #endregion
            #endregion

            #region Publisher 4
            tbAmountPublisher4.Text = selectedRow.NumberScoresPublisher4.ToString();

            #region Publisherr4 Combobox
            comPublisher4.Text = selectedRow.Publisher4Name;
            foreach (PublisherModel publisher in comPublisher4.Items)
            {
                if (comPublisher4.Text == null) { comPublisher4.Text = ""; }
                if (publisher.PublisherName == comPublisher4.Text.ToString())
                {
                    comPublisher4.SelectedItem = publisher;
                }
            }
            #endregion
            #endregion

            #region Total
            var Total = selectedRow.NumberScoresPublisher1 + selectedRow.NumberScoresPublisher2 + selectedRow.NumberScoresPublisher3 + selectedRow.NumberScoresPublisher4;
            tbAmountSupplierTotal.Text = Total.ToString();
            #endregion
            #endregion
        }

        private void BtnNextClick(object sender, RoutedEventArgs e)
        {
            if (ScoresDataGrid.SelectedIndex + 1 < ScoresDataGrid.Items.Count)
            {
                ScoresDataGrid.SelectedIndex += 1;
            }
            else
            {
                ScoresDataGrid.SelectedIndex = 0;
            }
        }
        private void BtnPreviousClick(object sender, RoutedEventArgs e)
        {
            if (ScoresDataGrid.SelectedIndex > 0)
            {
                ScoresDataGrid.SelectedIndex -= 1;
            }
            else
            {
                ScoresDataGrid.SelectedIndex = ScoresDataGrid.Items.Count - 1;
            }
        }

        private void BtnLastClick(object sender, RoutedEventArgs e)
        {
            ScoresDataGrid.SelectedIndex = ScoresDataGrid.Items.Count - 1;
        }

        private void BtnFirstClick(object sender, RoutedEventArgs e)
        {
            ScoresDataGrid.SelectedIndex = 0;
        }

        private void TextBoxChanged(object sender, TextChangedEventArgs e)
        {
            var propertyName = ((TextBox)sender).Name;


            if (SelectedScore != null)
            {
                switch (propertyName)
                {
                    case "tbTitle":
                        if (tbTitle.Text == SelectedScore.ScoreTitle) { cbTitle.IsChecked = false; } else { cbTitle.IsChecked = true; }
                        break;
                    case "tbSubTitle":
                        if (tbSubTitle.Text == SelectedScore.ScoreSubTitle) { cbSubTitle.IsChecked = false; } else { cbSubTitle.IsChecked = true; }
                        break;
                    case "tbComposer":
                        if (tbComposer.Text == SelectedScore.Composer) { cbComposer.IsChecked = false; } else { cbComposer.IsChecked = true; }
                        break;
                    case "tbTextwriter":
                        if (tbTextwriter.Text == SelectedScore.Textwriter) { cbTextwriter.IsChecked = false; } else { cbTextwriter.IsChecked = true; }
                        break;
                    case "tbArranger":
                        if (tbArranger.Text == SelectedScore.Arranger) { cbArranger.IsChecked = false; } else { cbArranger.IsChecked = true; }
                        break;
                    case "tbMusicPiece":
                        if (tbMusicPiece.Text == SelectedScore.MusicPiece) { cbMusicPiece.IsChecked = false; } else { cbMusicPiece.IsChecked = true; }
                        break;
                    case "tbAmountPublisher1":
                        if (tbAmountPublisher1.Text == SelectedScore.NumberScoresPublisher1.ToString()) { cbAmountPublisher1.IsChecked = false; } else { cbAmountPublisher1.IsChecked = true; }
                        break;
                    case "tbAmountPublisher2":
                        if (tbAmountPublisher2.Text == SelectedScore.NumberScoresPublisher2.ToString()) { cbAmountPublisher2.IsChecked = false; } else { cbAmountPublisher2.IsChecked = true; }
                        break;
                    case "tbAmountPublisher3":
                        if (tbAmountPublisher3.Text == SelectedScore.NumberScoresPublisher3.ToString()) { cbAmountPublisher3.IsChecked = false; } else { cbAmountPublisher3.IsChecked = true; }
                        break;
                    case "tbAmountPublisher4":
                        if (tbAmountPublisher4.Text == SelectedScore.NumberScoresPublisher4.ToString()) { cbAmountPublisher4.IsChecked = false; } else { cbAmountPublisher4.IsChecked = true; }
                        break;
                }
            }
            CheckChanged();
        }

        private void ComboBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            var propertyName = ((ComboBox)sender).Name;

            if (SelectedScore != null)
            {
                switch (propertyName)
                {
                    case "comRepertoire":
                        if (((RepertoireModel)comRepertoire.SelectedItem).RepertoireId == SelectedScore.RepertoireId) { cbRepertoire.IsChecked = false; } else { cbRepertoire.IsChecked = true; }
                        break;
                    case "comArchive":
                        if (((ArchiveModel)comArchive.SelectedItem).ArchiveId == SelectedScore.ArchiveId) { cbArchive.IsChecked = false; } else { cbArchive.IsChecked = true; }
                        break;
                    case "comGenre":
                        if (((GenreModel)comGenre.SelectedItem).GenreId == SelectedScore.GenreId) { cbGenre.IsChecked = false; } else { cbGenre.IsChecked = true; }
                        break;
                    case "comAccompaniment":
                        if (((AccompanimentModel)comAccompaniment.SelectedItem).AccompanimentId == SelectedScore.AccompanimentId) { cbAccompaniment.IsChecked = false; } else { cbAccompaniment.IsChecked = true; }
                        break;
                    case "comLanguage":
                        if (((LanguageModel)comLanguage.SelectedItem).LanguageId == SelectedScore.LanguageId) { cbLanguage.IsChecked = false; } else { cbLanguage.IsChecked = true; }
                        break;
                    case "comPublisher1":
                        if (((PublisherModel)comPublisher1.SelectedItem).PublisherId == SelectedScore.Publisher1Id) { cbPublisher1.IsChecked = false; } else { cbPublisher1.IsChecked = true; }
                        break;
                    case "comPublisher2":
                        if (((PublisherModel)comPublisher2.SelectedItem).PublisherId == SelectedScore.Publisher2Id) { cbPublisher2.IsChecked = false; } else { cbPublisher2.IsChecked = true; }
                        break;
                    case "comPublisher3":
                        if (((PublisherModel)comPublisher3.SelectedItem).PublisherId == SelectedScore.Publisher3Id) { cbPublisher3.IsChecked = false; } else { cbPublisher3.IsChecked = true; }
                        break;
                    case "comPublisher4":
                        if (((PublisherModel)comPublisher4.SelectedItem).PublisherId == SelectedScore.Publisher4Id) { cbPublisher4.IsChecked = false; } else { cbPublisher4.IsChecked = true; }
                        break;
                }
            }
            CheckChanged();
        }

        private void CheckChanged()
        {
            if (cbAccompaniment.IsChecked == true ||
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
                cbPublisher4.IsChecked == true)
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

            if (SelectedScore != null)
            {
                //It is hard to check if the content of a richtextbox really differs, for that reason the changed is always set when triggered
                switch (propertyName)
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

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            var propertyName = ((CheckBox)sender).Name;

            if (SelectedScore != null)
            {
                switch (propertyName)
                {
                    case "chkByHeart":
                        if (chkByHeart.IsChecked == SelectedScore.ByHeart) { cbByHeart.IsChecked = false; } else { cbByHeart.IsChecked = true; }
                        break;
                    case "chkChecked":
                        if (chkChecked.IsChecked == SelectedScore.Check) { cbChecked.IsChecked = false; } else { cbChecked.IsChecked = true; }
                        break;
                    case "chkMSCORP":
                        if (chkMSCORP.IsChecked == SelectedScore.MuseScoreORP) { cbMSCORP.IsChecked = false; } else { cbMSCORP.IsChecked = true; }
                        break;
                    case "chkMSCORK":
                        if (chkMSCORK.IsChecked == SelectedScore.MuseScoreORK) { cbMSCORK.IsChecked = false; } else { cbMSCORK.IsChecked = true; }
                        break;
                    case "chkMSCTOP":
                        if (chkMSCTOP.IsChecked == SelectedScore.MuseScoreTOP) { cbMSCTOP.IsChecked = false; } else { cbMSCTOP.IsChecked = true; }
                        break;
                    case "chkMSCTOK":
                        if (chkMSCTOK.IsChecked == SelectedScore.MuseScoreTOK) { cbMSCTOK.IsChecked = false; } else { cbMSCTOK.IsChecked = true; }
                        break;
                    case "chkPDFORP":
                        if (chkPDFORP.IsChecked == SelectedScore.PDFORP) { cbPDFORP.IsChecked = false; } else { cbPDFORP.IsChecked = true; }
                        break;
                    case "chkPDFORK":
                        if (chkPDFORK.IsChecked == SelectedScore.PDFORK) { cbPDFORK.IsChecked = false; } else { cbPDFORK.IsChecked = true; }
                        break;
                    case "chkPDFTOP":
                        if (chkPDFTOP.IsChecked == SelectedScore.PDFTOP) { cbPDFTOP.IsChecked = false; } else { cbPDFTOP.IsChecked = true; }
                        break;
                    case "chkPDFTOK":
                        if (chkPDFTOK.IsChecked == SelectedScore.PDFTOK) { cbPDFTOK.IsChecked = false; } else { cbPDFTOK.IsChecked = true; }
                        break;
                    case "chkMP3B1":
                        if (chkMP3B1.IsChecked == SelectedScore.MP3B1) { cbMP3B1.IsChecked = false; } else { cbMP3B1.IsChecked = true; }
                        break;
                    case "chkMP3B2":
                        if (chkMP3B2.IsChecked == SelectedScore.MP3B2) { cbMP3B2.IsChecked = false; } else { cbMP3B2.IsChecked = true; }
                        break;
                    case "chkMP3T1":
                        if (chkMP3T1.IsChecked == SelectedScore.MP3T1) { cbMP3T1.IsChecked = false; } else { cbMP3T1.IsChecked = true; }
                        break;
                    case "chkMP3T2":
                        if (chkMP3T2.IsChecked == SelectedScore.MP3T2) { cbMP3T2.IsChecked = false; } else { cbMP3T2.IsChecked = true; }
                        break;
                    case "chkMP3SOL":
                        if (chkMP3SOL.IsChecked == SelectedScore.MP3SOL) { cbMP3SOL.IsChecked = false; } else { cbMP3SOL.IsChecked = true; }
                        break;
                    case "chkMP3TOT":
                        if (chkMP3TOT.IsChecked == SelectedScore.MP3TOT) { cbMP3TOT.IsChecked = false; } else { cbMP3TOT.IsChecked = true; }
                        break;
                    case "chkMP3PIA":
                        if (chkMP3PIA.IsChecked == SelectedScore.MP3PIA) { cbMP3PIA.IsChecked = false; } else { cbMP3PIA.IsChecked = true; }
                        break;
                    case "chkMSCOnline":
                        if (chkMSCOnline.IsChecked == SelectedScore.MuseScoreOnline) { cbOnline.IsChecked = false; } else { cbOnline.IsChecked = true; }
                        break;
                }
            }
            CheckChanged();
        }

        private void DatePickerChanged(object sender, SelectionChangedEventArgs e)
        {
            var propertyName = ((DatePicker)sender).Name;

            if (SelectedScore != null)
            {
                switch (propertyName)
                {
                    case "dpDigitized":
                        DateTime _CreatedDateTime = new();

                        // If the change event is triggered a data has been entered, this always differs if no date is in the database
                        if (SelectedScore.DateCreatedString != "")
                        {
                            var _selectedDateTime = SelectedScore.DateCreatedString.ToString() + " 00:00:00 AM";
                            _CreatedDateTime = DateTime.Parse(_selectedDateTime);

                            if (dpDigitized.SelectedDate == _CreatedDateTime) { cbDigitized.IsChecked = false; } else { cbDigitized.IsChecked = true; }
                        }
                        else
                        {
                            if (dpDigitized.SelectedDate.ToString() == "")
                            {
                                // If the change event is triggered a data has been entered, this always differs if no date is in the database
                                cbDigitized.IsChecked = false;
                            }
                            else
                            {
                                cbDigitized.IsChecked = true;
                            }


                        }
                        break;
                    case "dpModified":
                        DateTime _ModifiedDateTime = new();

                        // If the change event is triggered a data has been entered, this always differs if no date is in the database
                        if (SelectedScore.DateModifiedString != "")
                        {
                            var _selectedDateTime = SelectedScore.DateModifiedString.ToString() + " 00:00:00 AM";
                            _ModifiedDateTime = DateTime.Parse(_selectedDateTime);

                            if (dpModified.SelectedDate == _ModifiedDateTime) { cbModified.IsChecked = false; } else { cbModified.IsChecked = true; }
                        }
                        else
                        {
                            // If the change event is triggered a data has been entered, this always differs if no date is in the database
                            cbModified.IsChecked = false;
                        }
                        break;
                }
            }
            CheckChanged();
        }

    }
}

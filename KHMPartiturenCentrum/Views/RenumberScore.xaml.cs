using KHMPartiturenCentrum.Helpers;
using KHMPartiturenCentrum.Models;
using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Utilities;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KHMPartiturenCentrum.Views;

/// <summary>
/// Interaction logic for RenumberScore.xaml
/// </summary>
public partial class RenumberScore : Window
{
    public RenumberScore ( object selectedRow, string selectedScore, string selectedSubScore )
    {
        InitializeComponent ();

        DataContext = selectedRow;
        if ( selectedSubScore != "" && selectedSubScore != null )
        {
            tbSerie.Visibility = Visibility.Visible;
            cbSerie.Visibility = Visibility.Visible;
        }
        else
        {
            tbSerie.Visibility = Visibility.Collapsed;
            cbSerie.Visibility = Visibility.Collapsed;
        }

        var Scores = DBCommands.GetEmptyScores(DBNames.NewScoresView, DBNames.ScoresFieldNameScoreNumber);

        cbxNewScores.ItemsSource = Scores.Select ( x => x.ScoreNumber ).ToList ();

    }
    #region Selected a new number for the score
    private void NewScoreChanged ( object sender, SelectionChangedEventArgs e )
    {
        var _newScore = "";
        btnRenumber.Visibility = Visibility.Visible;
        if ( tbScoreNumber.Text != "" )
        {
            if ( cbSerie.IsChecked != false )
            { _newScore = cbxNewScores.SelectedValue.ToString () + tbScoreNumber.Text.ToString ().Substring ( 3, 3 ); }
            else
            { _newScore = cbxNewScores.SelectedValue.ToString (); }

            btnText.Text = tbScoreNumber.Text + " => " + _newScore;
        }
    }
    #endregion

    #region Checked/Unchecked the box to include the complete serie
    private void IncludeSeriesChanged ( object sender, RoutedEventArgs e )
    {
        var _newScore = "";

        if ( tbScoreNumber.Text != "" && cbxNewScores.SelectedValue != "")
        {
            if ( cbSerie.IsChecked != false )
            { _newScore = cbxNewScores.SelectedValue.ToString () + tbScoreNumber.Text.ToString ().Substring ( 3, 3 ); }
            else
            { _newScore = cbxNewScores.SelectedValue.ToString (); }

            btnText.Text = tbScoreNumber.Text + " => " + _newScore;
        }
    }
    #endregion

    #region Drag Widow
    private void Window_MouseDown ( object sender, MouseButtonEventArgs e )
    {
        if ( e.LeftButton == MouseButtonState.Pressed )
        {
            DragMove ();
        }
    }
    #endregion

    #region Close Window is pressend
    private void Close_Click ( object sender, RoutedEventArgs e )
    {
        this.Close ();
    }
    #endregion

    #region Renumber the selected score or Score-Serie
    private void Renumber_Click ( object sender, RoutedEventArgs e )
    {
        DataTable ScoreInfo = new();

        //Check if the score belongs to a serie, score number will contain a - if it does
        if ( !tbScoreNumber.Text.Contains ( "-" ) )
        {
            // Does not belong to a serie
            ScoreInfo = DBCommands.GetData(DBNames.ScoresTable, DBNames.ScoresFieldNameScoreNumber, DBNames.ScoresFieldNameScoreNumber, tbScoreNumber.Text);
            SaveToNewScore ( ScoreInfo, "" );
            //DeleteScore(Oldnumber)
            //AddScore(OldNumber)
        }
        else
        {
            //Belongs to a serie
            // Check if the entire serie should be copied
            if (cbSerie.IsChecked != false )
            {
                // Complete series should be copied
            }
            else
            {
                // Only Current score should be copied to a new score (New score has nu subNumber
                ScoreInfo = DBCommands.GetData ( DBNames.ScoresTable, DBNames.ScoresFieldNameScoreNumber, DBNames.ScoresFieldNameScoreNumber, tbScoreNumber.Text );
                SaveToNewScore ( ScoreInfo, "" );
                // Check SubVersions
                // If Current Subnumer is 01 Do nothing Extra
                // If Current Subnumer is > 01 but there are only two scores with subnumer 
                //      If remaining Score has subnumber 01 remove the subnumer from that score also else do nothing with the remaining score
                // If there are more Scores in the serie just remove this score

                //DeleteScore(Oldnumber)
                //AddScore(OldNumber)
            }

        }
    }
    #endregion

    #region Check RepertoireId for Range
    private void CheckRepertoireId(string newScoreNumber )
    {
        ObservableCollection<RepertoireModel> RepertoireList = new(DBCommands.GetRepertoires ());
        // TestRange (newScoreNumber, minvalue, maxvalue)
        
    }
    #endregion

    #region Checķ if a value is in a range
    bool TestRange ( int numberToCheck, int bottom, int top )
    {
        return ( numberToCheck >= bottom && numberToCheck <= top );
    }
    #endregion

    #region Svae To NewScoreList
    private void SaveToNewScore(DataTable scoreInfo, string subScoreNumber )
    {
        ObservableCollection<SaveScoreModel> Score = new();

        Score.Add ( new SaveScoreModel
        {
            ScoreId = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 0 ].ToString () ),
            ScoreMainNumber = tbScoreNumber.Text,
            ScoreSubNumber = subScoreNumber,
            Title = scoreInfo.Rows [ 0 ].ItemArray [ 4 ].ToString (),
            SubTitle = scoreInfo.Rows [ 0 ].ItemArray [ 5 ].ToString (),
            Composer = scoreInfo.Rows [ 0 ].ItemArray [ 6 ].ToString (),
            Textwriter = scoreInfo.Rows [ 0 ].ItemArray [ 7 ].ToString (),
            Arranger = scoreInfo.Rows [ 0 ].ItemArray [ 8 ].ToString (),
            ArchiveId = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 9 ].ToString () ),
            RepertoireId = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 11 ].ToString () ),
            LanguageId = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 13 ].ToString () ),
            GenreId = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 15 ].ToString () ),
            Lyrics = scoreInfo.Rows [ 0 ].ItemArray [ 17 ].ToString (),
            Checked = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 18 ].ToString () ),
            DateDigitized = scoreInfo.Rows [ 0 ].ItemArray [ 19 ].ToString (),
            DateModified = scoreInfo.Rows [ 0 ].ItemArray [ 20 ].ToString (),
            AccompanimentId = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 21 ].ToString () ),
            PDFORP = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 23 ].ToString () ),
            PDFORK = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 24 ].ToString () ),
            PDFTOP = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 25 ].ToString () ),
            PDFTOK = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 26 ].ToString () ),
            MuseScoreORP = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 27 ].ToString () ),
            MuseScoreORK = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 28 ].ToString () ),
            MuseScoreTOP = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 29 ].ToString () ),
            MuseScoreTOK = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 30 ].ToString () ),
            MP3TOT = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 31 ].ToString () ),
            MP3T1 = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 32 ].ToString () ),
            MP3T2 = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 33 ].ToString () ),
            MP3B1 = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 34 ].ToString () ),
            MP3B2 = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 35 ].ToString () ),
            MP3SOL = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 36 ].ToString () ),
            MP3PIA = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 37 ].ToString () ),
            MuseScoreOnline = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 38 ].ToString () ),
            ByHeart = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 39 ].ToString () ),
            MusicPiece = scoreInfo.Rows [ 0 ].ItemArray [ 40 ].ToString (),
            Notes = scoreInfo.Rows [ 0 ].ItemArray [ 41 ].ToString (),
            AmountPublisher1 = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 42 ].ToString () ),
            AmountPublisher2 = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 43 ].ToString () ),
            AmountPublisher3 = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 44 ].ToString () ),
            AmountPublisher4 = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 45 ].ToString () ),
            Publisher1Id = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 46 ].ToString () ),
            Publisher2Id = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 48 ].ToString () ),
            Publisher3Id = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 50 ].ToString () ),
            Publisher4Id = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 52 ].ToString () ),
        } );
    }
    #endregion
}

using Google.Protobuf.WellKnownTypes;
using KHMPartiturenCentrum.Helpers;
using KHMPartiturenCentrum.Models;
using KHMPartiturenCentrum.ViewModels;
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

        var Scores = DBCommands.GetEmptyScores(DBNames.AvailableScoresView, DBNames.ScoresFieldNameScoreNumber);

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

        if ( tbScoreNumber.Text.ToString() != "" && cbxNewScores.SelectedValue.ToString () != "")
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

    #region Close Window is pressed
    private void Close_Click ( object sender, RoutedEventArgs e )
    {
        this.Close ();
    }
    #endregion

    #region Renumber the selected score or Score-Serie
    private void Renumber_Click ( object sender, RoutedEventArgs e )
    {
        DataTable ScoreInfo = new();

        // Get Id of targetScore
        int TargetScoreId = DBCommands.GetTargetId(cbxNewScores.SelectedValue.ToString());

        //Check if the score belongs to a serie, score number will contain a - if it does
        if ( !tbScoreNumber.Text.Contains ( "-" ) )
        {
            // Does not belong to a serie
            ScoreInfo = DBCommands.GetData(DBNames.ScoresTable, DBNames.ScoresFieldNameScoreNumber, DBNames.ScoresFieldNameScoreNumber, tbScoreNumber.Text);
           

            SaveToNewScore ( ScoreInfo, TargetScoreId, "", "replace" );
            DBCommands.DeleteScore(tbScoreNumber.Text, "" );
            DBCommands.ReAddScore(tbScoreNumber.Text);
        }
        else
        {
            //Belongs to a serie
            string[] _oldScoreNumber = tbScoreNumber.Text.Replace(" ", "").Split("-");

            long NumberOfScores = DBCommands.CheckForSubScores(_oldScoreNumber[0]);

            // Check if the entire serie should be copied
            if (cbSerie.IsChecked != false )
            {
                // Complete series should be copied
                DataTable ScoreList = DBCommands.GetData(DBNames.ScoresTable, DBNames.ScoresFieldNameScoreSubNumber, DBNames.ScoresFieldNameScoreNumber, _oldScoreNumber[0]);

                if ( cbxNewScores.SelectedValue != null )
                {
                    // Delete the currently selected new score, so all the scores can be added in the new process
                    DBCommands.DeleteScore ( cbxNewScores.SelectedValue.ToString (), "" );

                    DBCommands.RenumberScoreList(ScoreList, cbxNewScores.SelectedValue.ToString() );

                    // Delete the original Series
                    DBCommands.DeleteScore ( _oldScoreNumber [ 0 ], "" );

                    // The Original Score number should be added again, as a single score
                    DBCommands.ReAddScore ( _oldScoreNumber [ 0 ] );
                }
            }
            else
            {
                // Only Current score should be copied to a new score (New score has nu subNumber

                // Renumber Current Score and Delete the selected Record
                ScoreInfo = DBCommands.GetData(DBNames.ScoresTable, DBNames.ScoresFieldNameScoreNumber, DBNames.ScoresFieldNameScoreNumber, _oldScoreNumber [0], DBNames.ScoresFieldNameScoreSubNumber, _oldScoreNumber [1] );
                SaveToNewScore(ScoreInfo, TargetScoreId, "", "replace");
                DBCommands.DeleteScore(_oldScoreNumber[0], _oldScoreNumber[1]);

                if ( NumberOfScores == 2 ) 
                {
                    // Remove the Sub number from the remaining Score
                    DBCommands.RemoveSubScore(_oldScoreNumber[0]);
                }
            }

        }
        this.Close ();
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

    #region Save To NewScoreList
    private void SaveToNewScore(DataTable scoreInfo, int TargetScoreId, string subScoreNumber, string AddReplace )
    {
        ObservableCollection<SaveScoreModel> Score = new();

        Score.Add ( new SaveScoreModel
        {
            ScoreId = TargetScoreId,
            ScoreMainNumber = cbxNewScores.SelectedValue.ToString(),
            ScoreSubNumber = subScoreNumber,
            Title = scoreInfo.Rows [ 0 ].ItemArray [ 5 ].ToString (),
            SubTitle = scoreInfo.Rows [ 0 ].ItemArray [ 6 ].ToString (),
            Composer = scoreInfo.Rows [ 0 ].ItemArray [ 7 ].ToString (),
            Textwriter = scoreInfo.Rows [ 0 ].ItemArray [ 8 ].ToString (),
            Arranger = scoreInfo.Rows [ 0 ].ItemArray [ 9 ].ToString (),
            ArchiveId = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 1 ].ToString () ),
            RepertoireId = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 2 ].ToString () ),
            LanguageId = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 10 ].ToString () ),
            GenreId = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 11 ].ToString () ),
            Lyrics = scoreInfo.Rows [ 0 ].ItemArray [ 12 ].ToString (),
            Checked = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 13 ].ToString () ),
            DateDigitized = GetDate(scoreInfo.Rows [ 0 ].ItemArray [ 14 ].ToString()),
            DateModified = GetDate(scoreInfo.Rows [ 0 ].ItemArray [ 15 ].ToString ()),
            AccompanimentId = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 16 ].ToString () ),
            PDFORP = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 17 ].ToString () ),
            PDFORK = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 18 ].ToString () ),
            PDFTOP = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 19 ].ToString () ),
            PDFTOK = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 20 ].ToString () ),
            MuseScoreORP = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 21 ].ToString () ),
            MuseScoreORK = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 22 ].ToString () ),
            MuseScoreTOP = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 23 ].ToString () ),
            MuseScoreTOK = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 24 ].ToString () ),
            MP3TOT = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 25 ].ToString () ),
            MP3T1 = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 26 ].ToString () ),
            MP3T2 = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 27 ].ToString () ),
            MP3B1 = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 28 ].ToString () ),
            MP3B2 = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 29 ].ToString () ),
            MP3SOL = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 30 ].ToString () ),
            MP3PIA = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 31 ].ToString () ),
            MuseScoreOnline = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 32 ].ToString () ),
            ByHeart = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 33 ].ToString () ),
            MusicPiece = scoreInfo.Rows [ 0 ].ItemArray [ 34 ].ToString (),
            Notes = scoreInfo.Rows [ 0 ].ItemArray [ 35 ].ToString (),
            AmountPublisher1 = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 36 ].ToString () ),
            AmountPublisher2 = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 37 ].ToString () ),
            AmountPublisher3 = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 38 ].ToString () ),
            AmountPublisher4 = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 39 ].ToString () ),
            Publisher1Id = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 40 ].ToString () ),
            Publisher2Id = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 41 ].ToString () ),
            Publisher3Id = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 42 ].ToString () ),
            Publisher4Id = int.Parse ( scoreInfo.Rows [ 0 ].ItemArray [ 43 ].ToString () ),
        } );

        switch (AddReplace.ToLower())
        {
            case "add":
                // Add as new record
                break;
            case "replace":
                // Replace an existing record with the new values
                DBCommands.SaveScore(Score);
                break;
        }
        
        //DBCommands.GetScores(DBNames.ScoresView, DBNames.ScoresFieldNameScoreNumber, null, null);

    }
    #endregion

    #region Get The Date from the DateTime string
    /// <summary>
    /// Transform the date from a string "d-M-yyyy hh:mm:ss" To a date only "YYYY-MM-DD"
    /// </summary>
    /// <param name="Original datestring"></param>
    /// <returns = new datestring></returns>
    public static string GetDate(string _date )
    {
        var dateString = "";
        if ( _date != "" )
        {
            DateOnly _dateOutput;

            string[] dateTime = _date.Split(" ");
            string[] date = dateTime[0].Split("-");

            string year = date[2];
            string month = "0" + date[1];
            string day = "0" + date[0];

            dateString = $"{year}-{month.Substring ( month.Length - 2, 2 )}-{day.Substring ( day.Length - 2, 2 )}";
            _dateOutput = DateOnly.FromDateTime ( DateTime.Parse ( dateString + " 00:00:00 AM" ) );
        }
        return dateString;
    }
    #endregion
}
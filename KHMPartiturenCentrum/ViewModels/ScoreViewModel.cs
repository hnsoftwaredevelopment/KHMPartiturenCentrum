using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using KHMPartiturenCentrum.Helpers;
using KHMPartiturenCentrum.Models;
using KHMPartiturenCentrum.Views;

namespace KHMPartiturenCentrum.ViewModels;

public partial class ScoreViewModel: ObservableObject
{
    [ObservableProperty]
    public int id = 0 ;

    [ObservableProperty]
    public string scoreNumber = "";

    [ObservableProperty]
    public string scoreSubNumber = "";

    [ObservableProperty]
    public string score = "";

    [ObservableProperty]
    public string scoreTitle = "";

    [ObservableProperty]
    public string scoreSubTitle = "" ;

    [ObservableProperty]
    public string composer = "" ;

    [ObservableProperty]
    public string textWriter = "" ;

    [ObservableProperty]
    public string arranger = "" ;

    [ObservableProperty]
    public int repertoireId  = 0;

    [ObservableProperty]
    public string repertoireName = "" ;

    [ObservableProperty]
    public int archiveId  = 0;

    [ObservableProperty]
    public string archiveName = "" ;

    [ObservableProperty]
    public int genreId  = 0;

    [ObservableProperty]
    public string genreName = "" ;

    [ObservableProperty]
    public int accompanimentId = 0 ;

    [ObservableProperty]
    public string accompanimentName = "" ;

    [ObservableProperty]
    public int languageId = 0 ;

    [ObservableProperty]
    public string languageName = "" ;

    [ObservableProperty]
    public DateOnly dateCreated = DateOnly.FromDateTime(DateTime.Now) ;

    [ObservableProperty]
    public string dateCreatedString = "" ;

    [ObservableProperty]
    public DateOnly dateModified =DateOnly.FromDateTime(DateTime.Now) ;

    [ObservableProperty]
    public string dateModifiedString = "" ;

    [ObservableProperty]
    public bool check = false;

    [ObservableProperty]
    public int checkInt = 0 ;

    [ObservableProperty]
    public bool museScoreORP = false ;

    [ObservableProperty]
    public int museScoreORPInt = 0 ;

    [ObservableProperty]
    public bool museScoreORK = false;

    [ObservableProperty]
    public int museScoreORKInt = 0 ;

    [ObservableProperty]
    public bool museScoreTOP = false;

    [ObservableProperty]
    public int museScoreTOPInt = 0 ;

    [ObservableProperty]
    public bool museScoreTOK = false ;

    [ObservableProperty]
    public int museScoreTOKInt = 0 ;

    [ObservableProperty]
    public bool pDFORP = false ;

    [ObservableProperty]
    public int pDFORPInt = 0 ;

    [ObservableProperty]
    public bool pDFORK = false ;

    [ObservableProperty]
    public int pDFORKInt = 0 ;

    [ObservableProperty]
    public bool pDFTOP = false ;

    [ObservableProperty]
    public int pDFTOPInt = 0 ;

    [ObservableProperty]
    public bool pDFTOK = false ;

    [ObservableProperty]
    public int pDFTOKInt = 0 ;

    [ObservableProperty]
    public bool mP3B1 = false ;

    [ObservableProperty]
    public int mP3B1Int = 0 ;

    [ObservableProperty]
    public bool mP3B2 = false ;

    [ObservableProperty]
    public int mP3B2Int = 0 ;

    [ObservableProperty]
    public bool mP3T1 = false ;

    [ObservableProperty]
    public int mP3T1Int = 0 ;

    [ObservableProperty]
    public bool mP3T2 = false ;

    [ObservableProperty]
    public int mP3T2Int = 0 ;

    [ObservableProperty]
    public bool mP3SOL = false ;

    [ObservableProperty]
    public int mP3SOLInt = 0 ;

    [ObservableProperty]
    public bool mP3TOT = false ;

    [ObservableProperty]
    public int mP3TOTInt = 0 ;

    [ObservableProperty]
    public bool mP3PIA = false ;

    [ObservableProperty]
    public int mP3PIAInt = 0 ;

    [ObservableProperty]
    public bool museScoreOnline = false ;

    [ObservableProperty]
    public int museScoreOnlineInt = 0 ;

    [ObservableProperty]
    public bool byHeart = false ;

    [ObservableProperty]
    public int byHeartInt = 0 ;

    [ObservableProperty]
    public string lyrics = "" ;

    [ObservableProperty]
    public string musicPiece = "" ;

    [ObservableProperty]
    public string notes = "" ;

    [ObservableProperty]
    public int numberScoresPublisher1 = 0 ;

    [ObservableProperty]
    public int publisherId = 0 ;

    [ObservableProperty]
    public string publisherName = "" ;

    [ObservableProperty]
    public int publisher1Id = 0 ;

    [ObservableProperty]
    public string publisher1Name = "" ;

    [ObservableProperty]
    public int numberScoresPublisher2 = 0 ;

    [ObservableProperty]
    public int publisher2Id = 0 ;

    [ObservableProperty]
    public string publisher2Name = "" ;

    [ObservableProperty]
    public int numberScoresPublisher3 = 0 ;

    [ObservableProperty]
    public int publisher3Id = 0 ;

    [ObservableProperty]
    public string publisher3Name = "" ;

    [ObservableProperty]
    public int numberScoresPublisher4 = 0 ;

    [ObservableProperty]
    public int publisher4Id = 0 ;

    [ObservableProperty]
    public string publisher4Name = "" ;

    [ObservableProperty]
    public int numberScoresTotal = 0 ;

    [ObservableProperty]
    public object selectedItem ="";

    public ScoreModel? SelectedScoreModel { get; set; }

    public ObservableCollection<AccompanimentModel>? Accompaniments { get; set; }
    public ObservableCollection<GenreModel>? Genres { get; set; }
    public ObservableCollection<ArchiveModel>? Archives { get; set; }
    public ObservableCollection<LanguageModel>? Languages { get; set; }
    public ObservableCollection<PublisherModel>? Publishers { get; set; }
    public ObservableCollection<PublisherModel>? Publishers1 { get; set; }
    public ObservableCollection<RepertoireModel>? Repertoires { get; set; }
    public ObservableCollection<ScoreModel> Scores { get; set; }
    public ObservableCollection<ScoreModel> AvailableScores { get; set; }
    public ObservableCollection<ScoreModel> AvailableChristmasScores { get; set; }

    public ScoreViewModel ()
    {
        //Scores = new ObservableCollection<ScoreModel> ();
        //Accompaniments = DBCommands.GetAccompaniments ();
        //Archives = DBCommands.GetArchives ();
        //Genres = DBCommands.GetGenres ();
        //Languages = DBCommands.GetLanguages ();
        //Publishers = DBCommands.GetPublishers ();
        //Publishers1 = DBCommands.GetPublishers ();
        //Repertoires = DBCommands.GetRepertoires ();
        Scores = DBCommands.GetScores (DBNames.ScoresView, DBNames.ScoresFieldNameScoreNumber);
        AvailableScores = DBCommands.GetAvailableScores (  );
        AvailableChristmasScores = DBCommands.GetAvailableChristmasScores (  );
    }


}

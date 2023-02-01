using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using KHMPartiturenCentrum.Helpers;
using KHMPartiturenCentrum.Models;

namespace KHMPartiturenCentrum.ViewModels;

public partial class ScoreViewModel: ObservableObject
{
    //[ObservableProperty]
    //public int id = 0;
    [ObservableProperty]
    public int id = 0 ;

    [ObservableProperty]
    public string scoreNumber = "";

    [ObservableProperty]
    public string coreTitle = "";

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
    public string lyrics = "" ;

    [ObservableProperty]
    public int numberScoresSupplier1 = 0 ;

    [ObservableProperty]
    public int supplier1Id = 0 ;

    [ObservableProperty]
    public string supplier1Name = "" ;

    [ObservableProperty]
    public int numberScoresSupplier2 = 0 ;

    [ObservableProperty]
    public int supplier2Id = 0 ;

    [ObservableProperty]
    public string supplier2Name = "" ;

    [ObservableProperty]
    public int numberScoresSupplier3 = 0 ;

    [ObservableProperty]
    public int supplier3Id = 0 ;

    [ObservableProperty]
    public string supplier3Name = "" ;

    [ObservableProperty]
    public int numberScoresSupplier4 = 0 ;

    [ObservableProperty]
    public int supplier4Id = 0 ;

    [ObservableProperty]
    public string supplier4Name = "" ;

    [ObservableProperty]
    public int numberScoresTotal = 0 ;

    public ObservableCollection<AccompanimentModel>? Accompaniments { get; set; }
    public ObservableCollection<GenreModel>? Genres { get; set; }
    public ObservableCollection<LanguageModel>? Languages { get; set; }
    public ObservableCollection<PublisherModel>? Publishers { get; set; }
    public ObservableCollection<RepertoireModel>? Repertoires { get; set; }
    public ObservableCollection<ScoreModel>? Scores { get; set; }

    public ScoreViewModel ()
    {
        Accompaniments = DBCommands.GetAccompaniments ();
        Genres = DBCommands.GetGenres ();
        Languages = DBCommands.GetLanguages ();
        Publishers = DBCommands.GetPublishers ();
        Repertoires = DBCommands.GetRepertoires ();
        Scores = DBCommands.GetScores ();
    }


}

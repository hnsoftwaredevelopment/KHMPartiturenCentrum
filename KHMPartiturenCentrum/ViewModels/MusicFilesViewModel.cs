namespace KHM.ViewModels;
public partial class MusicFilesViewModel : ObservableObject
{
    [ObservableProperty]
    public int scoreId = 0;

    [ObservableProperty]
    public string scoreNumber = "";

    [ObservableProperty]
    public string scoreTitle = "";

    [ObservableProperty]
    public int filesIndexId = 0;

    [ObservableProperty]
    public bool pDFORP = false;

    [ObservableProperty]
    public int pDFORPId = 0;

    [ObservableProperty]
    public bool pDFORK = false;

    [ObservableProperty]
    public int pDFORKId = 0;

    [ObservableProperty]
    public bool pDFTOP = false;

    [ObservableProperty]
    public int pDFTOPId = 0;

    [ObservableProperty]
    public bool pDFTOK = false;

    [ObservableProperty]
    public int pDFTOKId = 0;

    [ObservableProperty]
    public bool pDFPIA = false;

    [ObservableProperty]
    public int pDFPIAId = 0;

    [ObservableProperty]
    public bool mSCORP = false;

    [ObservableProperty]
    public int mSCORPId = 0;

    [ObservableProperty]
    public bool mSCORK = false;

    [ObservableProperty]
    public int mSCORKId = 0;

    [ObservableProperty]
    public bool mSCTOP = false;

    [ObservableProperty]
    public int mSCTOPId = 0;

    [ObservableProperty]
    public bool mSCTOK = false;

    [ObservableProperty]
    public int mSCTOKId = 0;

    [ObservableProperty]
    public bool mP3TOT = false;

    [ObservableProperty]
    public int mP3TOTId = 0;

    [ObservableProperty]
    public bool mP3T1 = false;

    [ObservableProperty]
    public int mP3T1Id = 0;

    [ObservableProperty]
    public bool mP3T2 = false;

    [ObservableProperty]
    public int mP3T2Id = 0;

    [ObservableProperty]
    public bool mP3B1 = false;

    [ObservableProperty]
    public int mP3B1Id = 0;

    [ObservableProperty]
    public bool mP3B2 = false;

    [ObservableProperty]
    public int mP3B2Id = 0;

    [ObservableProperty]
    public bool mP3SOL = false;

    [ObservableProperty]
    public int mP3SOLId = 0;

    [ObservableProperty]
    public bool mP3PIA = false;

    [ObservableProperty]
    public int mP3PIAId = 0;

    [ObservableProperty]
    public bool mP3UITV = false;

    [ObservableProperty]
    public int mP3UITVId = 0;

    [ObservableProperty]
    public bool mP3TOTVoice = false;

    [ObservableProperty]
    public int mP3TOTVoiceId = 0;

    [ObservableProperty]
    public bool mP3T1Voice = false;

    [ObservableProperty]
    public int mP3T1VoiceId = 0;

    [ObservableProperty]
    public bool mP3T2Voice = false;

    [ObservableProperty]
    public int mP3T2VoiceId = 0;

    [ObservableProperty]
    public bool mP3B1Voice = false;

    [ObservableProperty]
    public int mP3B1VoiceId = 0;

    [ObservableProperty]
    public bool mP3B2Voice = false;

    [ObservableProperty]
    public int mP3B2VoiceId = 0;

    [ObservableProperty]
    public bool mP3SOLVoice = false;

    [ObservableProperty]
    public int mP3SOLVoiceId = 0;

    [ObservableProperty]
    public string searchField = "";

    [ObservableProperty]
    public object selectedItem = "";

    public ObservableCollection<MusicFilesModel> Scores { get; set; }

    public MusicFilesViewModel ( )
    {
        Scores = DBCommands.GetMusicFileInfo ( DBNames.MusicFilesView, DBNames.MusicFilesFieldNameScoreNumber );
    }
}
namespace KHM.ViewModels
{
    public partial class HistoryViewModel : ObservableObject
    {
        [ObservableProperty]
        public int logId = 0;

        [ObservableProperty]
        public string logDate = "";

        [ObservableProperty]
        public string logTime = "";

        [ObservableProperty]
        public string userName = "";

        [ObservableProperty]
        public string performedAction = "";

        [ObservableProperty]
        public string description = "";

        [ObservableProperty]
        public string modifiedField = "";

        [ObservableProperty]
        public string oldValue = "";

        [ObservableProperty]
        public string newValue = "";

        public ObservableCollection<HistoryModel> HistoryLog { get; set; }

        public HistoryViewModel() 
        {
            HistoryLog = new ObservableCollection<HistoryModel> ();
            HistoryLog = DBCommands.GetHistoryLog ();
        }
    }

}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using KHMPartiturenCentrum.Helpers;
using KHMPartiturenCentrum.Models;
using Microsoft.VisualBasic.ApplicationServices;

namespace KHMPartiturenCentrum.ViewModels
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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using KHM.Helpers;
using KHM.Models;
using Microsoft.VisualBasic.ApplicationServices;

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

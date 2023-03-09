using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

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
    }
}

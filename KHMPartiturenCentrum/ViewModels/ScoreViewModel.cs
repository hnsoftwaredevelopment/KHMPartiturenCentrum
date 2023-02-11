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

public partial class ScoreViewModel: BaseScoreViewModel
{
    public ScoreViewModel ()
    {
        Scores = DBCommands.GetScores (DBNames.ScoresView, DBNames.ScoresFieldNameScoreNumber);
        //AvailableScores = DBCommands.GetAvailableScores (  );
        //AvailableChristmasScores = DBCommands.GetAvailableChristmasScores (  );
    }


}

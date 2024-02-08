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

using KHM.Helpers;
using KHM.Models;
using KHM.Views;

namespace KHM.ViewModels;

public partial class ScoreViewModel: BaseScoreViewModel
{
    public ScoreViewModel ()
    {
        Scores = DBCommands.GetScores (DbNames.ScoresView, "nosort", null, null);
    }
}

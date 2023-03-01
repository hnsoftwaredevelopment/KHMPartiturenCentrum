using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KHMPartiturenCentrum.Helpers;

namespace KHMPartiturenCentrum.ViewModels;

public class NewScoreViewModel : BaseScoreViewModel
{
    public NewScoreViewModel ()
    {
        //Scores = DBCommands.GetEmptyScores ( DBNames.AvailableScoresView, DBNames.AvailableScoresFieldNameNumber );
        AvailableScores = DBCommands.GetAvailableScores ();
    }
}

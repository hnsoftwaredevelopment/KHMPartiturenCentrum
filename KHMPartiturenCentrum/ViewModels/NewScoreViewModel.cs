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
        //Scores = new ObservableCollection<ScoreModel> ();
        //Accompaniments = DBCommands.GetAccompaniments ();
        //Archives = DBCommands.GetArchives ();
        //Genres = DBCommands.GetGenres ();
        //Languages = DBCommands.GetLanguages ();
        //Publishers = DBCommands.GetPublishers ();
        //Publishers1 = DBCommands.GetPublishers ();
        //Repertoires = DBCommands.GetRepertoires ();
        Scores = DBCommands.GetEmptyScores ( DBNames.NewScoresView, DBNames.ScoresFieldNameScoreNumber );
        AvailableScores = DBCommands.GetAvailableScores ();
        AvailableChristmasScores = DBCommands.GetAvailableChristmasScores ();
    }
}

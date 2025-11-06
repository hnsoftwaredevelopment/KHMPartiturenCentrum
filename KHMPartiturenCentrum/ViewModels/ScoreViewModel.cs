namespace KHM.ViewModels;

public partial class ScoreViewModel : BaseScoreViewModel
{
	public ScoreViewModel()
	{
		Scores = DBCommands.GetScores( DBNames.ScoresView, DBNames.ScoresFieldNameScoreNumber, null, null );
	}
}

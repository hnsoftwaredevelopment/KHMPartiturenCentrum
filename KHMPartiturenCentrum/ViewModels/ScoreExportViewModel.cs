using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KHM.ViewModels
{
    public partial class ScoreExportViewModel : BaseScoreViewModel
    {
        public ObservableCollection<ScoreModel> Scores { get; set; } = new();

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged(); // from BaseScoreViewModel
                }
            }
        }

        public async Task LoadScoresAsync()
        {
            IsLoading = true;
            try
            {
                var scores = await DBCommands.GetScoresAsync();

                Scores.Clear();
                foreach (var s in scores)
                    Scores.Add(s);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}

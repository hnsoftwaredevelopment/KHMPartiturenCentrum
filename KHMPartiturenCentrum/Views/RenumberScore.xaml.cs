using KHMPartiturenCentrum.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KHMPartiturenCentrum.Views
{
    /// <summary>
    /// Interaction logic for RenumberScore.xaml
    /// </summary>
    public partial class RenumberScore : Window
    {
        public RenumberScore (object selectedRow, string selectedScore, string selectedSubScore)
        {
            InitializeComponent ();

            DataContext = selectedRow;
            if(selectedSubScore != "" && selectedSubScore != null)
            {
                tbSerie.Visibility = Visibility.Visible;
                cbSerie.Visibility = Visibility.Visible;
            }
            else
            {
                tbSerie.Visibility = Visibility.Collapsed;
                cbSerie.Visibility = Visibility.Collapsed;
            }

            var Scores = DBCommands.GetEmptyScores(DBNames.NewScoresView, DBNames.ScoresFieldNameScoreNumber);

            
            // Youtube video with example: https://www.youtube.com/watch?v=1FjF2r-UKzU
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using KHMPartiturenCentrum.Helpers;
using KHMPartiturenCentrum.Models;
using KHMPartiturenCentrum.ViewModels;

namespace KHMPartiturenCentrum.Views
{
    /// <summary>
    /// Interaction logic for Scores.xaml
    /// </summary>
    public partial class Scores : Page
    {
        public ScoreViewModel? scores;
        public Scores()
        {
            InitializeComponent();
            scores = new ScoreViewModel ();
            DataContext = scores;
        }

        private void PageLoaded ( object sender, RoutedEventArgs e )
        {
            //DataTable _dt = DBCommands.GetData(DBNames.ScoresView, DBNames.ScoresFieldNameScoreNumber );
            //ScoresDataGrid.DataContext = _dt;

            comAccompaniment.ItemsSource = DBCommands.GetAccompaniments ();
            comArchive.ItemsSource = DBCommands.GetArchives ();
            comGenre.ItemsSource = DBCommands.GetGenres ();
            comLanguage.ItemsSource = DBCommands.GetLanguages ();
            comRepertoire.ItemsSource = DBCommands.GetRepertoires ();
            comSupplier1.ItemsSource = DBCommands.GetPublishers ();
            comSupplier2.ItemsSource = DBCommands.GetPublishers ();
            comSupplier3.ItemsSource = DBCommands.GetPublishers ();
            comSupplier4.ItemsSource = DBCommands.GetPublishers ();
        }
    }
}

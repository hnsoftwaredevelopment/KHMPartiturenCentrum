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

using static System.Net.Mime.MediaTypeNames;

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

        private void SelectedScoreChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            //DataRowView selectedRow = dg.SelectedItem as DataRowView;

            ScoreModel selectedRow = (ScoreModel)dg.SelectedItem;

            var nr = selectedRow.ScoreNumber.ToString();
            Console.WriteLine( selectedRow.ScoreNumber);

            if (selectedRow!= null)
            {
                //string nr = selectedRow["ScoreNumber"].ToString();
                Console.WriteLine("Hello " + nr);
            }
            //if (ScoresDataGrid.SelectedItem != null)
            //{
            // Get the selected item
            //object selectedItem = ScoresDataGrid.SelectedItem;


            //ScoreModel selectedScoreNumber = (ScoreModel)selecteditem.ScoreNumber;

            // Cast the selected item to the appropriate type
            // (for example, if your DataGrid is bound to a List<Person>,
            // you would cast to a Person)
            //
            // Example:
            // Person selectedPerson = (Person)selectedItem;

            // Do something with the selected item
            //}
        }
        }
}

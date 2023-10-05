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
using System.Windows.Navigation;
using System.Windows.Shapes;
using KHM.Helpers;
using KHM.Models;
using KHM.ViewModels;

namespace KHM.Views;

/// <summary>
/// Interaction logic for NewScores.xaml
/// </summary>
public partial class AvailableScores : Page
{
    public NewScoreViewModel? scores;
    public AvailableScores ()
    {
        InitializeComponent ();
        scores = new NewScoreViewModel ();
        DataContext = scores;
    }
}
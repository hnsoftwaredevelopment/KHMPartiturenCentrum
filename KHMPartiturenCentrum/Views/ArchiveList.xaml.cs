using System.Windows.Controls;
using KHMPartiturenCentrum.ViewModels;

namespace KHMPartiturenCentrum.Views;
/// <summary>
/// Interaction logic for ArchiveList.xaml
/// </summary>
public partial class ArchiveList : Page
{
    public ScoreViewModel? scores;
    public ArchiveList ( )
    {
        InitializeComponent ( );

        scores = new ScoreViewModel ( );
        DataContext = scores;
    }
}

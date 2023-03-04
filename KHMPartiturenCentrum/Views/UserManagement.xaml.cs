using KHMPartiturenCentrum.Helpers;
using KHMPartiturenCentrum.Models;
using KHMPartiturenCentrum.ViewModels;

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

namespace KHMPartiturenCentrum.Views;
/// <summary>
/// Interaction logic for UserManagement.xaml
/// </summary>
public partial class UserManagement : Page
{
    public UserViewModel? users;
    public UserModel? SelectedUser;

    public UserManagement()
    {
        InitializeComponent();

        users = new UserViewModel();
        DataContext = users;

        cbUserRole.ItemsSource = DBCommands.GetUserRoles();
    }

    private void SelectedUserChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void TextBoxChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void PasswordChanged(object sender, RoutedEventArgs e)
    {

    }

    private void SaveUserProfileClicked(object sender, RoutedEventArgs e)
    {

    }
}

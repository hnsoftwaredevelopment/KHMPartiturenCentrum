using KHMPartiturenCentrum.Helpers;
using KHMPartiturenCentrum.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using KHMPartiturenCentrum.Converters;
using static KHMPartiturenCentrum.App;

namespace KHMPartiturenCentrum.Views;
/// <summary>
/// Interaction logic for LoginPage.xaml
/// </summary>
public partial class LoginPage : Window
{
    public LoginPage()
    {
        InitializeComponent();
    }

    #region Button Close | Restore | Minimize 
    #region Button Close
    private void btnClose_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
    #endregion

    #region Button Restore
    private void btnRestore_Click(object sender, RoutedEventArgs e)
    {
        if (WindowState == WindowState.Normal)
            WindowState = WindowState.Maximized;
        else
            WindowState = WindowState.Normal;
    }
    #endregion

    #region Button Minimize
    private void btnMinimize_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }
    #endregion
    #endregion

    #region Drag Widow
    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            DragMove();
        }
    }
    #endregion

    private void btnLogin_Click(object sender, RoutedEventArgs e)
    {
        tbInvalidLogin.Visibility = Visibility.Collapsed;
        int UserId = DBCommands.CheckUser(tbUserName.Text, Helper.HashPassword(tbPassword.Password, tbUserName.Text));
        if (UserId != 0)
        {
            ScoreUsers.SelectedUserId = UserId;
            ObservableCollection<UserModel> Users = DBCommands.GetUsers ( );

            foreach ( UserModel user in Users )
            {
                if ( user.UserId == UserId )
                {
                    ScoreUsers.SelectedUserName = user.UserName;
                    ScoreUsers.SelectedUserEmail = user.UserEmail;
                    ScoreUsers.SelectedUserRoleId = user.UserRoleId;
                }
            }
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        else
        {
            tbInvalidLogin.Visibility = Visibility.Visible;
        }
    }
}

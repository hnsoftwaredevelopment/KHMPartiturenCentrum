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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        int UserId = DBCommands.CheckUser(tbUserName.Text, tbPassword.Password);
        if (UserId != 0)
        {
            // Use userId the get Fullname and Role from the database
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

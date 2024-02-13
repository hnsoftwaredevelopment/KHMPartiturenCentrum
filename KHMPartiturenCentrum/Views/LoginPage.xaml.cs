using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using KHM.Helpers;
using KHM.Models;
using static KHM.App;

namespace KHM.Views;
/// <summary>
/// Interaction logic for LoginPage.xaml
/// </summary>
public partial class LoginPage : Window
{
    public LoginPage ( )
    {
        InitializeComponent ( );
    }

    #region Button Close | Restore | Minimize 
    #region Button Close
    private void btnClose_Click ( object sender, RoutedEventArgs e )
    {
        Close ( );
    }
    #endregion

    #region Button Restore
    private void btnRestore_Click ( object sender, RoutedEventArgs e )
    {
        if ( WindowState == WindowState.Normal )
            WindowState = WindowState.Maximized;
        else
            WindowState = WindowState.Normal;
    }
    #endregion

    #region Button Minimize
    private void btnMinimize_Click ( object sender, RoutedEventArgs e )
    {
        WindowState = WindowState.Minimized;
    }
    #endregion
    #endregion

    #region Drag Widow
    private void Window_MouseDown ( object sender, MouseButtonEventArgs e )
    {
        if ( e.LeftButton == MouseButtonState.Pressed )
        {
            DragMove ( );
        }
    }
    #endregion

    private void btnLogin_Click ( object sender, RoutedEventArgs e )
    {
        tbInvalidLogin.Visibility = Visibility.Collapsed;
        int UserId = DBCommands.CheckUserPassword(tbUserName.Text, tbPassword.Password);
        if ( UserId != 0 )
        {
            ScoreUsers.SelectedUserId = UserId;
            ObservableCollection<UserModel> Users = DBCommands.GetUsers ( );

            foreach ( UserModel user in Users )
            {
                if ( user.UserId == UserId )
                {
                    ScoreUsers.SelectedUserName = user.UserName;
                    ScoreUsers.SelectedUserFullName = user.UserFullName;
                    ScoreUsers.SelectedUserPassword = user.UserPassword;
                    ScoreUsers.SelectedUserEmail = user.UserEmail;
                    ScoreUsers.SelectedUserRoleId = user.UserRoleId;
                    ScoreUsers.SelectedUserCoverSheetFolder = user.CoverSheetFolder;
                }
            }

            // Write Login to Logfile
            DBCommands.WriteLog ( UserId, DBNames.LogUserLoggedIn, $"{ScoreUsers.SelectedUserFullName} is ingelogt" );

            int ForcePasswordReset = 1;
            if ( ForcePasswordReset != 0 )
            { }


            MainWindow mainWindow = new MainWindow();
            mainWindow.Show ( );
            this.Close ( );
        }
        else
        {
            tbInvalidLogin.Visibility = Visibility.Visible;
        }
    }

    private void PressedEnterOnPassword ( object sender, KeyEventArgs e )
    {
        if ( e.Key == Key.Enter )
        {
            btnLogin.RaiseEvent ( new RoutedEventArgs ( ButtonBase.ClickEvent ) );
        }
    }
}

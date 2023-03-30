global using Syncfusion.DocIO.DLS;
using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using KHMPartiturenCentrum.Helpers;
using static KHMPartiturenCentrum.App;

namespace KHMPartiturenCentrum;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow ( )
    {
        InitializeComponent ( );
        tbUserName.Text = ScoreUsers.SelectedUserFullName;
        tbLogedInUserName.Text = ScoreUsers.SelectedUserName;
        tbLogedInFullName.Text = ScoreUsers.SelectedUserFullName;

        // Set the value to control weather or not an administrator has logged in
        if ( ScoreUsers.SelectedUserRoleId == 4 || ScoreUsers.SelectedUserRoleId == 6 || ScoreUsers.SelectedUserRoleId == 8 || ScoreUsers.SelectedUserRoleId == 15 )
        {
            tbShowAdmin.Text = "Visible";
        }
        else
        {
            tbShowAdmin.Text = "Collapsed";
        }
    }

    #region Button Close | Restore | Minimize 
    #region Button Close
    private void btnClose_Click ( object sender, RoutedEventArgs e )
    {
        DBCommands.WriteLog ( ScoreUsers.SelectedUserId, DBNames.LogUserLoggedOut, $"{tbLogedInFullName.Text} heeft de applicatie afgesloten" );
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

    #region Drag Window
    private void Window_MouseDown ( object sender, MouseButtonEventArgs e )
    {
        if ( e.LeftButton == MouseButtonState.Pressed )
        {
            DragMove ( );
        }
    }
    #endregion

    #region MenuLeft PopupButton
    #region Score Menu
    #region On Click
    private void btnScores_Click ( object sender, RoutedEventArgs e )
    {
        fContainer.Navigate ( new System.Uri ( "Views/Scores.xaml", UriKind.RelativeOrAbsolute ) );
    }
    #endregion

    #region On Mouse Enter
    private void btnScores_MouseEnter ( object sender, MouseEventArgs e )
    {
        if ( Tg_Btn.IsChecked == false )
        {
            Popup.PlacementTarget = btnScores;
            Popup.Placement = PlacementMode.Right;
            Popup.IsOpen = true;
            Header.PopupText.Text = "Partituren overzicht";
        }
    }
    #endregion

    #region On Mouse Leave
    private void btnScores_MouseLeave ( object sender, MouseEventArgs e )
    {
        Popup.Visibility = Visibility.Collapsed;
        Popup.IsOpen = false;
    }
    #endregion
    #endregion

    #region Available scores Menu
    #region On Click
    private void btnAvailableScores_Click ( object sender, RoutedEventArgs e )
    {
        fContainer.Navigate ( new System.Uri ( "Views/AvailableScores.xaml", UriKind.RelativeOrAbsolute ) );
    }
    #endregion

    #region On Mouse Enter
    private void btnAvailableScores_MouseEnter ( object sender, MouseEventArgs e )
    {
        if ( Tg_Btn.IsChecked == false )
        {
            Popup.PlacementTarget = btnFreeNumbers;
            Popup.Placement = PlacementMode.Right;
            Popup.IsOpen = true;
            Header.PopupText.Text = "Beschikbare nummers";
        }
    }
    #endregion

    #region On Mouse Leave
    private void btnAvailableScores_MouseLeave ( object sender, MouseEventArgs e )
    {
        Popup.Visibility = Visibility.Collapsed;
        Popup.IsOpen = false;
    }
    #endregion
    #endregion

    #region Users Profile Menu
    #region On Click
    private void btnUserProfile_Click ( object sender, RoutedEventArgs e )
    {
        fContainer.Navigate ( new System.Uri ( "Views/UserProfile.xaml", UriKind.RelativeOrAbsolute ) );
    }
    #endregion

    #region On Mouse Enter
    private void btnUserProfile_MouseEnter ( object sender, MouseEventArgs e )
    {
        if ( Tg_Btn.IsChecked == false )
        {
            Popup.PlacementTarget = btnUserProfile;
            Popup.Placement = PlacementMode.Right;
            Popup.IsOpen = true;
            Header.PopupText.Text = "Wijzig de gebruikers gegevens";
        }
    }
    #endregion

    #region On Mouse Leave
    private void btnUserProfile_MouseLeave ( object sender, MouseEventArgs e )
    {
        Popup.Visibility = Visibility.Collapsed;
        Popup.IsOpen = false;
    }
    #endregion
    #endregion

    #region Users Management
    #region On Click
    private void btnUsersManagement_Click ( object sender, RoutedEventArgs e )
    {
        fContainer.Navigate ( new System.Uri ( "Views/UserManagement.xaml", UriKind.RelativeOrAbsolute ) );
    }
    #endregion

    #region On Mouse Enter
    private void btnUsersManagement_MouseEnter ( object sender, MouseEventArgs e )
    {
        if ( Tg_Btn.IsChecked == false )
        {
            Popup.PlacementTarget = btnUsersManagement;
            Popup.Placement = PlacementMode.Right;
            Popup.IsOpen = true;
            Header.PopupText.Text = "Gebruikers beheer";
        }
    }
    #endregion

    #region On Mouse Leave
    private void btnUsersManagement_MouseLeave ( object sender, MouseEventArgs e )
    {
        Popup.Visibility = Visibility.Collapsed;
        Popup.IsOpen = false;
    }
    #endregion
    #endregion

    #region Logging
    #region On Click
    private void btnLogging_Click ( object sender, RoutedEventArgs e )
    {
        fContainer.Navigate ( new System.Uri ( "Views/History.xaml", UriKind.RelativeOrAbsolute ) );
    }
    #endregion

    #region On Mouse Enter
    private void btnLogging_MouseEnter ( object sender, MouseEventArgs e )
    {
        if ( Tg_Btn.IsChecked == false )
        {
            Popup.PlacementTarget = btnLogging;
            Popup.Placement = PlacementMode.Right;
            Popup.IsOpen = true;
            Header.PopupText.Text = "Log bestand";
        }
    }
    #endregion

    #region On Mouse Leave
    private void btnLogging_MouseLeave ( object sender, MouseEventArgs e )
    {
        Popup.Visibility = Visibility.Collapsed;
        Popup.IsOpen = false;
    }
    #endregion
    #endregion
    #endregion

    #region Reload MainPage (After UserFullName update)
    // Make reload of the MainWindow possible from the age where the UserName can be changed, so it will be updated in the MainWindow after save
    public static void ReloadMainWindow ( )
    {
        MainWindow newMainWindow = new MainWindow();
        newMainWindow.Show ( );
        Application.Current.MainWindow.Close ( );
        Application.Current.MainWindow = newMainWindow;
    }
    #endregion

    #region Archive List Menu
    #region On Click
    private void btnArchiveList_Click ( object sender, RoutedEventArgs e )
    {
        fContainer.Navigate ( new System.Uri ( "Views/ArchiveList.xaml", UriKind.RelativeOrAbsolute ) );
    }
    #endregion

    #region On Mouse Enter
    private void btnArchiveList_MouseEnter ( object sender, MouseEventArgs e )
    {
        if ( Tg_Btn.IsChecked == false )
        {
            Popup.PlacementTarget = btnArchiveList;
            Popup.Placement = PlacementMode.Right;
            Popup.IsOpen = true;
            Header.PopupText.Text = "Partituren overzicht per archief";
        }
    }
    #endregion

    #region On Mouse Leave
    private void btnArchiveList_MouseLeave ( object sender, MouseEventArgs e )
    {
        Popup.Visibility = Visibility.Collapsed;
        Popup.IsOpen = false;
    }
    #endregion
    #endregion
}


using KHMPartiturenCentrum.Converters;
using KHMPartiturenCentrum.Helpers;
using KHMPartiturenCentrum.Models;
using KHMPartiturenCentrum.ViewModels;

using Microsoft.VisualBasic.ApplicationServices;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using static KHMPartiturenCentrum.App;

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

        if (ScoreUsers.SelectedUserRoleId == 4 || ScoreUsers.SelectedUserRoleId == 6 || ScoreUsers.SelectedUserRoleId == 8 || ScoreUsers.SelectedUserRoleId == 10 || ScoreUsers.SelectedUserRoleId == 11 || ScoreUsers.SelectedUserRoleId == 13 || ScoreUsers.SelectedUserRoleId == 14 || ScoreUsers.SelectedUserRoleId == 15)
        {
            tbAdminMode.Text = "Visible";
        }
        else
        {
            tbAdminMode.Text = "Collapsed";
        }

        users = new UserViewModel();
        DataContext = users;
        comUserRole.ItemsSource = DBCommands.GetUserRoles();
    }

    private void SelectedUserChanged(object sender, SelectionChangedEventArgs e)
    {
        // Clear all the fields
        tbUserName.Text = string.Empty;
        tbFullName.Text = string.Empty;
        tbEMail.Text = string.Empty;
        pbPassword.Password = string.Empty;
        comUserRole.SelectedIndex = 0;

        DataGrid dg = (DataGrid)sender;

        UserModel selectedRow = (UserModel)dg.SelectedItem;

        if ( selectedRow == null )
        {
            object item = dg.Items[0];
            dg.SelectedItem = item;
            selectedRow = (UserModel) dg.SelectedItem;

            // Scroll to the item in the DataGrid
            dg.ScrollIntoView ( dg.Items [ 0 ] );
            UsersDataGrid.SelectedIndex = 0;

            // Scroll to the item in the GridView
            UsersDataGrid.ScrollIntoView(UsersDataGrid.Items[UsersDataGrid.SelectedIndex]);
        }

        SelectedUser = selectedRow;


        tbUserName.Text = selectedRow.UserName;
        tbFullName.Text = selectedRow.UserFullName;
        tbEMail.Text = selectedRow.UserEmail;

        #region UserRoles Combobox
        comUserRole.Text = selectedRow.RoleDescription;

        foreach ( UserRoleModel userrole in comUserRole.Items )
        {
            if ( comUserRole.Text == null ) { comUserRole.Text = ""; }
            if ( userrole.RoleDescription == comUserRole.Text.ToString() )
            {
                comUserRole.SelectedItem = userrole;
                break;
            }
        }
        #endregion

        ResetChanged ();
    }

    private void TextBoxChanged(object sender, TextChangedEventArgs e)
    {
        var propertyName = ((TextBox)sender).Name;


        if ( SelectedUser != null )
        {
            switch ( propertyName )
            {
                case "tbFullName":
                    if ( tbFullName.Text == SelectedUser.UserFullName )
                    { cbFullNameChanged.IsChecked = false; }
                    else
                    { cbFullNameChanged.IsChecked = true; }
                    break;
                case "tbEMail":
                    if ( tbEMail.Text == SelectedUser.UserEmail )
                    { cbEMailChanged.IsChecked = false; }
                    else
                    { cbEMailChanged.IsChecked = true; }
                    break;
            }
        }
        CheckChanged ();
    }

    private void ComboBoxChanged ( object sender, SelectionChangedEventArgs e )
    {
        var propertyName = ((ComboBox)sender).Name;

        if ( SelectedUser != null )
        {
            switch ( propertyName )
            {
                case "comUserRole":
                    if ( comUserRole.SelectedItem != null )
                    {
                        if ( ( (UserRoleModel) comUserRole.SelectedItem ).RoleId == SelectedUser.UserRoleId )
                        { cbUserRoleChanged.IsChecked = false; }
                        else
                        { cbUserRoleChanged.IsChecked = true; }
                    }
                    break;
            }
        }
        CheckChanged ();
    }

    private void PasswordChanged(object sender, RoutedEventArgs e)
    {
        var _newPassword = Helper.HashPepperPassword ( pbPassword.Password, tbUserName.Text );

        if (_newPassword== SelectedUser.UserPassword )
        { cbPasswordChanged.IsChecked = false; }
        else
        { cbPasswordChanged .IsChecked = true; }

        CheckChanged ();

    }

    private void SaveUserProfileClicked(object sender, RoutedEventArgs e)
    {
        ObservableCollection<UserModel> modifiedUser = new ObservableCollection<UserModel>();

        var SaveStatus = tbAdminMode.IsEnabled;
        var UserName = "";

        switch (SaveStatus)
        {
            case true:
                // User to save is a new user also save UserName
                UserName = tbUserName.Text;
                break;
            case false:
                // User to save is an existing User, do nothing with UserName
                break;
        }

        // Fill the modifiedUser collection

        // When the saved user is a newly added user disable the UserName box again
        tbUserName.IsEnabled = false;

        // Reload the DataGrid
        users = new UserViewModel();
        DataContext = users;
    }

    private void PageLoaded ( object sender, RoutedEventArgs e )
    {
        comUserRole.ItemsSource = DBCommands.GetUserRoles ();
        ResetChanged ();
    }

    private void BtnFirstClick ( object sender, RoutedEventArgs e )
    {
        UsersDataGrid.SelectedIndex = 0;

        // Scroll to the item in the GridView
        UsersDataGrid.ScrollIntoView(UsersDataGrid.Items[UsersDataGrid.SelectedIndex]);
    }

    private void BtnLastClick ( object sender, RoutedEventArgs e )
    {
        UsersDataGrid.SelectedIndex = UsersDataGrid.Items.Count - 1;

        // Scroll to the item in the GridView
        UsersDataGrid.ScrollIntoView(UsersDataGrid.Items[UsersDataGrid.SelectedIndex]);
    }

    private void BtnPreviousClick ( object sender, RoutedEventArgs e )
    {
        if (UsersDataGrid.SelectedIndex > 0)
        {
            UsersDataGrid.SelectedIndex -= 1;
        }
        else
        {
            UsersDataGrid.SelectedIndex = UsersDataGrid.Items.Count - 1;
        }

        // Scroll to the item in the GridView
        UsersDataGrid.ScrollIntoView(UsersDataGrid.Items[UsersDataGrid.SelectedIndex]);
    }

    private void BtnNextClick ( object sender, RoutedEventArgs e )
    {
        if (UsersDataGrid.SelectedIndex + 1 < UsersDataGrid.Items.Count)
        {
            UsersDataGrid.SelectedIndex += 1;
        }
        else
        {
            UsersDataGrid.SelectedIndex = 0;
        }

        // Scroll to the item in the GridView
        UsersDataGrid.ScrollIntoView(UsersDataGrid.Items[UsersDataGrid.SelectedIndex]);
    }

    private void NewUserClicked ( object sender, RoutedEventArgs e )
    {
        DBCommands.AddNewUser();

        // Reload the DataGrid
        users = new UserViewModel();
        DataContext = users;

        // Jump to newly added user
        int LatestUserId = DBCommands.GetAddedUserId();
        UsersDataGrid.SelectedIndex = LatestUserId;

        // Scroll to the item in the GridView
        UsersDataGrid.ScrollIntoView(UsersDataGrid.Items[UsersDataGrid.SelectedIndex]);

        // Enable Editmode of the UserName
        tbUserName.IsEnabled= true;
        // After adding a new user get the highest UserId and select it
        // Enable tbUserName to enter a Username.
        // Validation on Username Should be filled and Unique
        // Also validate E-Mail, should also be unique

    }

    private void DeleteUser ( object sender, RoutedEventArgs e )
    {
        if (SelectedUser != null)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show($"Weet je zeker dat je {SelectedUser.UserFullName} wilt verwijderen?", $"Verwijder gebruiker {SelectedUser.UserId}", MessageBoxButton.YesNoCancel);

            switch (messageBoxResult)
            {
                case MessageBoxResult.Yes:
                    // Continue Deleting User
                    if (SelectedUser.UserId != null)
                    {
                        DBCommands.DeleteUser(SelectedUser.UserId.ToString());
                    }
                    break;
                case MessageBoxResult.No:
                    // Do nothing no deletion wanted
                    break;
                case MessageBoxResult.Cancel:
                    // Do Nothing Deletion canceled
                    break;
            }
        }
        users = new UserViewModel();
        DataContext = users;
    }

    private void CheckChanged ()
    {
        if (cbEMailChanged.IsChecked == true ||
            cbFullNameChanged.IsChecked == true ||
            cbPasswordChanged.IsChecked == true ||
            cbUserRoleChanged.IsChecked == true )
        {
            if ( ScoreUsers.SelectedUserRoleId == 4 || ScoreUsers.SelectedUserRoleId == 6 || ScoreUsers.SelectedUserRoleId == 8 || ScoreUsers.SelectedUserRoleId == 10 || ScoreUsers.SelectedUserRoleId == 11 || ScoreUsers.SelectedUserRoleId == 13 || ScoreUsers.SelectedUserRoleId == 14 || ScoreUsers.SelectedUserRoleId == 15 )
            {
                tbEnableEdit.Text = "Visible";
                btnSave.IsEnabled = true;
                btnSave.ToolTip = "Sla de gewijzigde gegevens op";
            }
            else
            {
                tbEnableEdit.Text = "Collapsed";
            }
        }
        else
        {
            tbEnableEdit.Text = "Collapsed";
            btnSave.IsEnabled = false;
            btnSave.ToolTip = "Er zijn geen gegevens aangepast, opslaan niet mogelijk";
        }
    }

    public void ResetChanged ()
    {
        cbEMailChanged.IsChecked = false;
        cbFullNameChanged.IsChecked = false;
        cbPasswordChanged.IsChecked = false;
        cbUserRoleChanged.IsChecked = false;

        tbEnableEdit.Text = "Collapsed";
        btnSave.IsEnabled = false;
        btnSave.ToolTip = "Er zijn geen gegevens aangepast, opslaan niet mogelijk";
    }
}

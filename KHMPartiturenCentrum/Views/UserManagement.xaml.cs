using KHMPartiturenCentrum.Converters;
using KHMPartiturenCentrum.Helpers;
using KHMPartiturenCentrum.Models;
using KHMPartiturenCentrum.ViewModels;

using System;
using System.Collections.Generic;
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

        users = new UserViewModel();
        DataContext = users;

        comUserRole.ItemsSource = DBCommands.GetUserRoles();
    }

    private void SelectedUserChanged(object sender, SelectionChangedEventArgs e)
    {
        DataGrid dg = (DataGrid)sender;

        UserModel selectedRow = (UserModel)dg.SelectedItem;

        if ( selectedRow == null )
        {
            object item = dg.Items[0];
            dg.SelectedItem = item;
            selectedRow = (UserModel) dg.SelectedItem;

            // Scroll to he item in the Datagrid
            dg.ScrollIntoView ( dg.Items [ 0 ] );
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
                        if ( ( (UserModel) comUserRole.SelectedItem ).UserRoleId == SelectedUser.UserRoleId )
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

    }

    private void PageLoaded ( object sender, RoutedEventArgs e )
    {
        comUserRole.ItemsSource = DBCommands.GetUserRoles ();
        ResetChanged ();
    }

    private void BtnFirstClick ( object sender, RoutedEventArgs e )
    {

    }

    private void BtnLastClick ( object sender, RoutedEventArgs e )
    {

    }

    private void BtnPreviousClick ( object sender, RoutedEventArgs e )
    {

    }

    private void BtnNextClick ( object sender, RoutedEventArgs e )
    {

    }

    private void BtnSaveClick ( object sender, RoutedEventArgs e )
    {

    }

    private void NewUserClicked ( object sender, RoutedEventArgs e )
    {

    }

    private void DeleteUser ( object sender, RoutedEventArgs e )
    {

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

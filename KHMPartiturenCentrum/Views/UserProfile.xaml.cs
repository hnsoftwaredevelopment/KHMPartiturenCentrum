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
using KHMPartiturenCentrum.Converters;
using KHMPartiturenCentrum.Helpers;
using KHMPartiturenCentrum.Models;
using Org.BouncyCastle.Utilities;
using static KHMPartiturenCentrum.App;

namespace KHMPartiturenCentrum.Views;

/// <summary>
/// Interaction logic for UserProfile.xaml
/// </summary>
public partial class UserProfile : Page
{
    public UserProfile ()
    {
        InitializeComponent ();

        // Fill the text changed TextBoxes
        tbCheckFullName.Text = ScoreUsers.SelectedUserFullName;
        tbCheckEMail.Text = ScoreUsers.SelectedUserEmail;
        tbCheckPassword.Text = ScoreUsers.SelectedUserPassword;

        // Fill The Form fields
        tbUserName.Text = ScoreUsers.SelectedUserName;
        tbFullName.Text = ScoreUsers.SelectedUserFullName;
        tbEMail.Text = ScoreUsers.SelectedUserEmail;

        ResetChanged ();
    }
    private void TextBoxChanged ( object sender, TextChangedEventArgs e )
    {
        var propertyName = ((TextBox)sender).Name;

            switch ( propertyName )
            {
                case "tbFullName":
                    if ( tbFullName.Text == tbCheckFullName.Text )
                    { cbFullNameChanged.IsChecked = false; }
                    else
                    { cbFullNameChanged.IsChecked = true; }
                    break;
                case "tbEMail":
                    if ( tbEMail.Text == tbCheckEMail.Text )
                    { cbEMailChanged.IsChecked = false; }
                    else
                    { cbEMailChanged.IsChecked = true; }
                    break;
            }
        CheckChanged ();
    }
    private void PasswordChanged ( object sender, RoutedEventArgs e )
    {
        var checkPassword = Helper.HashPepperPassword(pbPassword.Password, tbUserName.Text);
        if ( checkPassword == tbCheckPassword.Text )
        { cbPasswordChanged.IsChecked = false; }
        else
        { cbPasswordChanged.IsChecked = true; }
        CheckChanged ();
    }
    private void CheckChanged ()
    {
        if ( cbFullNameChanged.IsChecked == true ||
            cbEMailChanged.IsChecked == true ||
            cbPasswordChanged.IsChecked == true )
        {
            btnSaveUserProfile.IsEnabled = true;
            btnSaveUserProfile.ToolTip = "Sla de gewijzigde gegevens op";
        }
        else
        {
            btnSaveUserProfile.IsEnabled = false;
            btnSaveUserProfile.ToolTip = "Er zijn geen gegevens aangepast, opslaan niet mogelijk";
        }

    }
    public void ResetChanged ()
    {
        cbFullNameChanged.IsChecked = false;
        cbEMailChanged.IsChecked = false;
        cbPasswordChanged.IsChecked = false;
        btnSaveUserProfile.IsEnabled = false;
        btnSaveUserProfile.ToolTip = "Er zijn geen gegevens aangepast, opslaan niet mogelijk";
    }
    private void SaveUserProfileClicked ( object sender, RoutedEventArgs e )
    {
        ObservableCollection<UserModel> ModifiedUser = new();

        string _FullName = "", _EMail = "", _Password = "";

        if ( (bool) cbFullNameChanged.IsChecked )
        { 
            _FullName = tbFullName.Text; 
            ScoreUsers.SelectedUserFullName = _FullName;
        }

        if ( (bool) cbEMailChanged.IsChecked )
        {
            _EMail = tbEMail.Text;
            ScoreUsers.SelectedUserEmail = _EMail;
        }

        if ( (bool) cbPasswordChanged.IsChecked )
        {
            var checkPassword = Helper.HashPepperPassword(pbPassword.Password, tbUserName.Text);
            _Password = checkPassword;
            ScoreUsers.SelectedUserPassword = _Password;
        }

        ModifiedUser.Add ( new UserModel
        {
            UserName = tbUserName.Text,
            UserFullName = _FullName,
            UserEmail = _EMail,
            UserPassword = _Password
        } );

        DBCommands.UpdateUser ( ModifiedUser );

        ResetChanged ();

        MainWindow.ReloadMainWindow ();
    }
}

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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KHM.Converters;
using KHM.Helpers;
using KHM.Models;
using Org.BouncyCastle.Utilities;
using static KHM.App;

namespace KHM.Views;

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
        tbCheckCoverSheets.Text = ScoreUsers.SelectedUserCoverSheetFolder;
        tbCheckDownloadFolder.Text = ScoreUsers.SelectedUserDownloadFolder;

        // Fill The Form fields
        tbUserName.Text = ScoreUsers.SelectedUserName;
        tbFullName.Text = ScoreUsers.SelectedUserFullName;
        tbEMail.Text = ScoreUsers.SelectedUserEmail;
        tbCoverSheets.Text = ScoreUsers.SelectedUserCoverSheetFolder;
        tbDownloadFolder.Text = ScoreUsers.SelectedUserDownloadFolder;

        ResetChanged ();
    }

    private void TextBoxChanged ( object sender, TextChangedEventArgs e )
    {
        var propertyName = ((System.Windows.Controls.TextBox)sender).Name;

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
            case "tbCoverSheets":
                if ( tbCoverSheets.Text == tbCheckCoverSheets.Text )
                { cbCoverSheetsFolderChanged.IsChecked = false; }
                else
                { cbCoverSheetsFolderChanged.IsChecked = true; }
                break;

            case "tbDownloadFolder":
                if ( tbDownloadFolder.Text == tbCheckDownloadFolder.Text )
                { cbDownloadFolderChanged.IsChecked = false; }
                else
                { cbDownloadFolderChanged.IsChecked = true; }
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
            cbPasswordChanged.IsChecked == true || cbCoverSheetsFolderChanged.IsChecked == true  || cbDownloadFolderChanged.IsChecked == true)
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
        cbCoverSheetsFolderChanged.IsChecked = false;
        btnSaveUserProfile.IsEnabled = false;
        btnSaveUserProfile.ToolTip = "Er zijn geen gegevens aangepast, opslaan niet mogelijk";
    }
    private void SaveUserProfileClicked ( object sender, RoutedEventArgs e )
    {
        ObservableCollection<UserModel> ModifiedUser = new();

        string _FullName = "", _EMail = "", _Password = "", _CoverSheetsFolder = "", _DownloadFolder = "";

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

        if ( (bool) cbCoverSheetsFolderChanged.IsChecked )
        {
            _CoverSheetsFolder = tbCoverSheets.Text;
            ScoreUsers.SelectedUserCoverSheetFolder = _CoverSheetsFolder;
        }

        if ( (bool) cbDownloadFolderChanged.IsChecked )
        {
            _DownloadFolder = tbDownloadFolder.Text;
            ScoreUsers.SelectedUserDownloadFolder = _DownloadFolder;
        }

        ModifiedUser.Add ( new UserModel
        {
            UserId=ScoreUsers.SelectedUserId,
            UserName = ScoreUsers.SelectedUserName,
            UserFullName = _FullName,
            UserEmail = _EMail,
            UserPassword = _Password,
            CoverSheetFolder = _CoverSheetsFolder,
            DownloadFolder = _DownloadFolder
        } );

        DBCommands.UpdateUser ( ModifiedUser );

        WriteHistory ( ModifiedUser );

        ModifyScoreUserData( ModifiedUser );

        ResetChanged ();

        MainWindow.ReloadMainWindow ();
    }

    private void WriteHistory(ObservableCollection<UserModel> modifiedUser )
    {
        DBCommands.WriteLog ( ScoreUsers.SelectedUserId, DBNames.LogUserChanged, ScoreUsers.SelectedUserFullName );

        int historyId = DBCommands.GetAddedHistoryId ();

        if ( modifiedUser [ 0 ].UserFullName != "" )
        {
            DBCommands.WriteDetailLog(historyId, DBNames.LogUserFullName, tbCheckFullName.Text, modifiedUser [ 0 ].UserFullName );
        }

        if ( modifiedUser [ 0 ].UserEmail != "" )
        {
            DBCommands.WriteDetailLog ( historyId, DBNames.LogUserEMail, tbCheckEMail.Text, modifiedUser [ 0 ].UserEmail );
        }

        if ( modifiedUser [ 0 ].UserPassword != "" )
        {
            DBCommands.WriteDetailLog ( historyId, DBNames.LogUserPassword, "", "" );
        }

        if ( modifiedUser [ 0 ].CoverSheetFolder != "" )
        {
            DBCommands.WriteDetailLog ( historyId, DBNames.LogUserCoverSheetFolder, @tbCheckCoverSheets.Text, @modifiedUser [ 0 ].CoverSheetFolder );
        }

        if ( modifiedUser [ 0 ].DownloadFolder != "" )
        {
            DBCommands.WriteDetailLog ( historyId, DBNames.LogUserDownloadFolder, @tbCheckDownloadFolder.Text, @modifiedUser [ 0 ].DownloadFolder );
        }
    }

    private void ModifyScoreUserData ( ObservableCollection<UserModel> modifiedUser )
    {
        if ( modifiedUser [ 0 ].UserFullName != "" )
        {
            ScoreUsers.SelectedUserFullName = modifiedUser [ 0 ].UserFullName;
        }

        if ( modifiedUser [ 0 ].UserEmail != "" )
        {
            ScoreUsers.SelectedUserEmail = modifiedUser [ 0 ].UserEmail;
        }

        if ( modifiedUser [ 0 ].UserPassword != "" )
        {
            ScoreUsers.SelectedUserPassword = modifiedUser [ 0 ].UserPassword;
        }

        if ( modifiedUser [ 0 ].CoverSheetFolder != "" )
        {
            ScoreUsers.SelectedUserCoverSheetFolder = modifiedUser [ 0 ].CoverSheetFolder;
        }

        if ( modifiedUser [ 0 ].DownloadFolder != "" )
        {
            ScoreUsers.SelectedUserDownloadFolder = modifiedUser [ 0 ].DownloadFolder;
        }    }

    private void BrowseToFolder_Click ( object sender, RoutedEventArgs e )
    {
        var senderButton = sender as System.Windows.Controls.Button;

        var dialogDescription = "Selecteer de map om de voorbladen op te slaan";
        var dialogSelectedPath = '"' + ScoreUsers.SelectedUserCoverSheetFolder.Replace(@"\", @"\\") +'"';

        using ( var dialog = new FolderBrowserDialog () )
        {
            dialog.SelectedPath = @dialogSelectedPath;
            dialog.Description = dialogDescription;
            dialog.ShowNewFolderButton = true;
            dialog.UseDescriptionForTitle = true;

            if ( dialog.ShowDialog () == DialogResult.OK )
            {
                if ( senderButton != null )
                {
                    tbCoverSheets.Text = dialog.SelectedPath;
                }
            }
        }
    }

    private void BrowseToDownloadFolder_Click ( object sender, RoutedEventArgs e )
    {
        var senderButton = sender as System.Windows.Controls.Button;

        var dialogDescription = "Selecteer de map om de bestanden op te slaan";
        var dialogSelectedPath = '"' + ScoreUsers.SelectedUserDownloadFolder.Replace(@"\", @"\\") +'"';

        using ( var dialog = new FolderBrowserDialog () )
        {
            dialog.SelectedPath = @dialogSelectedPath;
            dialog.Description = dialogDescription;
            dialog.ShowNewFolderButton = true;
            dialog.UseDescriptionForTitle = true;

            if ( dialog.ShowDialog () == DialogResult.OK )
            {
                if ( senderButton != null )
                {
                    tbDownloadFolder.Text = dialog.SelectedPath;
               }
            }
        }
    }

    private void FolderChanged ( object sender, TextChangedEventArgs e )
    {
        //var senderButton = sender as System.Windows.Controls.Button;
        var senderTextBox = sender as System.Windows.Controls.TextBox;

        if ( senderTextBox != null )
        {
            if ( tbCoverSheets.Text != ScoreUsers.SelectedUserCoverSheetFolder )
            { 
                cbCoverSheetsFolderChanged.IsChecked = true; 
            }
            else
            { 
                cbCoverSheetsFolderChanged.IsChecked = false; 
            }

            if ( tbDownloadFolder.Text != ScoreUsers.SelectedUserDownloadFolder )
            { 
                cbDownloadFolderChanged.IsChecked = true; 
            }
            else
            { 
                cbDownloadFolderChanged.IsChecked = false; 
            }
        }
    }
}

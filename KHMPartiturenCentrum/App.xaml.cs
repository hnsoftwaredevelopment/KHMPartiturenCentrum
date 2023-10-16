global using System.Collections.ObjectModel;
global using System.Windows;
global using KHM.Models;
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;
global using CommunityToolkit.Mvvm.ComponentModel;
global using KHM.Helpers;
global using Microsoft.VisualBasic.ApplicationServices;
global using System.Data;
global using System.IO;
global using System.Windows.Controls;
global using KHM.ViewModels;
global using Syncfusion.Pdf;
global using Syncfusion.Pdf.Graphics;
global using Syncfusion.Pdf.Grid;

namespace KHM;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App ( )
    {
        //Register Syncfusion license
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense ("Ngo9BigBOggjHTQxAR8/V1NHaF5cWWNCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdgWH9fdXVURWJeUkBzW0Y=");
    }
    public class ScoreUsers
    {
        public static int SelectedUserId { get; set; }
        public static string SelectedUserName { get; set; }
        public static string SelectedUserFullName { get; set; }
        public static string SelectedUserEmail { get; set; }
        public static string SelectedUserPassword { get; set; }
        public static int SelectedUserRoleId { get; set; }
        public static string SelectedUserCoverSheetFolder { get; set; }
        public static int SelectedUserRoleName { get; set; }
        public static int SelectedUserRoleDescription { get; set; }
        public static ObservableCollection<UserModel> User { get; set; }
    }

    public class FilePaths
    {
        public static string? DownloadPath { get; set; }
    }
}

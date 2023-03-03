using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using KHMPartiturenCentrum.Helpers;
using KHMPartiturenCentrum.Models;
using KHMPartiturenCentrum.ViewModels;
using Microsoft.VisualBasic.ApplicationServices;

namespace KHMPartiturenCentrum;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public class ScoreUsers
    {
        public static int SelectedUserId { get; set; }
        public static string SelectedUserName { get; set; }
        public static string SelectedUserFullName { get; set; }
        public static string SelectedUserEmail { get; set; }
        public static string SelectedUserPassword { get; set; }
        public static int SelectedUserRoleId { get; set; }
        public static ObservableCollection<UserModel> User { get; set; }
    }
}

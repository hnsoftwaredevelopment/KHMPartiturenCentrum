using CommunityToolkit.Mvvm.ComponentModel;
using KHM.Helpers;
using KHM.Models;
using KHM.Views;

using Microsoft.VisualBasic.ApplicationServices;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHM.ViewModels;
public partial class UserViewModel : ObservableObject
{
    [ObservableProperty]
    public int userId;

    [ObservableProperty]
    public string userName;

    [ObservableProperty]
    public string userEmail;

    [ObservableProperty]
    public string userPassword;

    [ObservableProperty]
    public string userFullName;

    [ObservableProperty]
    public int userRoleId;

    [ObservableProperty]
    public string coverSheetFolder;

    [ObservableProperty]
    public string downloadFolder;

    [ObservableProperty]
    public string roleName;

    [ObservableProperty]
    public string roleDescription;

    [ObservableProperty]
    public object selectedItem ="";

    public ObservableCollection<UserModel> Users { get; set; }

    public UserViewModel() 
    {
        Users = new ObservableCollection<UserModel>();
        Users = DBCommands.GetUsers(  );
    }

}

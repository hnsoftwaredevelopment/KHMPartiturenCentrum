using CommunityToolkit.Mvvm.ComponentModel;
using KHMPartiturenCentrum.Helpers;
using KHMPartiturenCentrum.Models;
using KHMPartiturenCentrum.Views;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHMPartiturenCentrum.ViewModels;
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
    public string roleName;

    [ObservableProperty]
    public string roleDescription;

    public ObservableCollection<UserModel> Users { get; set; }

    public UserViewModel() 
    {
        Users = DBCommands.GetUsers();
    }

}

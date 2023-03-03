using CommunityToolkit.Mvvm.ComponentModel;
using KHMPartiturenCentrum.Helpers;
using KHMPartiturenCentrum.Models;
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

    public ObservableCollection<UserModel> User { get; set; }

    public UserViewModel(int UserId) 
    { 
        //User = DBCommands.GetUsers(UserId );
    }

}
